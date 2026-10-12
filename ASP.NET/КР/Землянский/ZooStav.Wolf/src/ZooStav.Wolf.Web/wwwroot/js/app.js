/* ZooStav — небольшие клиентские улучшения: лайтбокс, «живая» веб-камера,
   динамические поля формы дневника, подтверждения удаления. */
(function () {
    'use strict';

    // ---------- Лайтбокс галереи ----------
    const gallery = document.querySelectorAll('.gallery img');
    if (gallery.length) {
        const box = document.createElement('div');
        box.className = 'lightbox';
        box.innerHTML = '<img alt="Фото" />';
        document.body.appendChild(box);
        const img = box.querySelector('img');
        gallery.forEach(el => el.addEventListener('click', () => {
            img.src = el.src;
            box.classList.add('is-open');
        }));
        box.addEventListener('click', () => box.classList.remove('is-open'));
        document.addEventListener('keydown', e => { if (e.key === 'Escape') box.classList.remove('is-open'); });
    }

    // ---------- «Живая» веб-камера: часы и счётчик наблюдателей ----------
    const clock = document.querySelector('[data-webcam-clock]');
    if (clock) {
        const tick = () => {
            const d = new Date();
            clock.textContent = d.toLocaleString('ru-RU', { hour12: false });
        };
        tick();
        setInterval(tick, 1000);
    }
    const viewers = document.querySelector('[data-webcam-viewers]');
    if (viewers) {
        let n = 12 + Math.floor(Math.random() * 25);
        const upd = () => { viewers.textContent = n; };
        upd();
        setInterval(() => {
            n = Math.max(5, n + Math.floor(Math.random() * 7) - 3);
            upd();
        }, 4000);
    }
    // ---------- HLS-плеер реального потока камеры ----------
    // Инициализация на window.load: к этому моменту подгружен локальный /lib/hls.min.js.
    function initHls() {
        const video = document.querySelector('video[data-hls-src]');
        if (!video) return;
        const src = video.dataset.hlsSrc;
        if (!src) return;

        const badge = document.querySelector('.webcam-badge');
        const setBadge = (html) => { if (badge) badge.innerHTML = html; };
        const snapshotFallback = (reason) => {
            const img = document.createElement('img');
            img.src = '/api/webcam/snapshot';
            img.alt = 'Кадры камеры вольера';
            img.dataset.webcamFrame = 'true';
            img.dataset.liveSrc = '/api/webcam/snapshot';
            img.dataset.liveInterval = '3000';
            video.replaceWith(img);
            setBadge('<span class="tag tag-observation">ФОТО-РЕЖИМ</span> ' + (reason || ''));
            let t = 0;
            setInterval(() => { img.src = '/api/webcam/snapshot?t=' + (++t) + Date.now(); }, 3000);
        };

        if (video.canPlayType('application/vnd.apple.mpegurl')) {
            // Safari / iOS умеют HLS нативно
            video.src = src;
        } else if (window.Hls && window.Hls.isSupported()) {
            const hls = new window.Hls({
                lowLatencyMode: true,
                liveSyncDurationCount: 3,
                maxBufferLength: 8,
                manifestLoadingMaxRetry: 5,
                levelLoadingMaxRetry: 5
            });
            hls.loadSource(src);
            hls.attachMedia(video);
            hls.on(window.Hls.Events.MANIFEST_PARSED, () => { video.play().catch(() => {}); });
            hls.on(window.Hls.Events.ERROR, (_, data) => {
                if (!data.fatal) return;
                if (data.type === window.Hls.ErrorTypes.NETWORK_ERROR) hls.startLoad();
                else if (data.type === window.Hls.ErrorTypes.MEDIA_ERROR) hls.recoverMediaError();
                else snapshotFallback('поток недоступен, показаны кадры');
            });
            window.__zooHls = hls;
        } else {
            snapshotFallback('браузер не поддерживает HLS — показаны кадры');
        }
    }
    window.addEventListener('load', initHls);

    // ---------- Периодический опрос состояния камеры ----------
    const statusEl = document.querySelector('[data-webcam-live-state]');
    if (statusEl) {
        const poll = async () => {
            try {
                const r = await fetch('/api/webcam/status', { cache: 'no-store' });
                const s = await r.json();
                statusEl.textContent = (s.isLive ? 'LIVE: ' : 'демо-режим: ') + s.sourceDescription +
                    (s.lastFrameUtc ? ' · кадр ' + new Date(s.lastFrameUtc).toLocaleTimeString('ru-RU') : '');
            } catch { statusEl.textContent = 'нет связи с сервером'; }
        };
        poll();
        setInterval(poll, 15000);
    }

    // MJPEG-поток: если камера отвалилась, плавно переходим на кадры (/api/webcam/snapshot),
    // чтобы посетитель видел последний доступный снимок, а не «сломанную» картинку.
    const mjpegImg = document.querySelector('img[src="/api/webcam/mjpeg"]');
    if (mjpegImg) {
        let switched = false;
        mjpegImg.addEventListener('error', () => {
            if (switched) return;
            switched = true;
            mjpegImg.dataset.webcamFrame = 'true';
            mjpegImg.dataset.liveSrc = '/api/webcam/snapshot';
            mjpegImg.dataset.liveInterval = '3000';
            const badge = document.querySelector('.webcam-badge');
            if (badge) badge.innerHTML = '<span class="tag tag-observation">ФОТО-РЕЖИМ</span> камера недоступна';
            const refresh = () => { mjpegImg.src = '/api/webcam/snapshot?t=' + Date.now(); };
            refresh();
            setInterval(refresh, 3000);
        });
    }

    // «Живой» кадр: каждые N секунд запрашиваем свежий снимок с сервера
    // (для камер с одиночным JPEG-кадром; для RTSP/HLS используется плеер выше).
    const liveFrame = document.querySelector('[data-webcam-frame][data-live-src]');
    if (liveFrame) {
        const src = liveFrame.dataset.liveSrc;
        const interval = parseInt(liveFrame.dataset.liveInterval || '5000', 10);
        const refresh = () => { liveFrame.src = src + (src.includes('?') ? '&' : '?') + 't=' + Date.now(); };
        setTimeout(refresh, 1200);
        setInterval(refresh, interval);
    }

    // Имитация обновления кадра с сервера (демо-режим без подключённой камеры).
    const frame = document.querySelector('[data-webcam-frame][data-rotate="true"]:not([data-live-src])');
    if (frame) {
        const sources = (frame.dataset.frames || '').split(';').filter(Boolean);
        if (sources.length > 1) {
            let i = 0;
            setInterval(() => { i = (i + 1) % sources.length; frame.src = sources[i]; }, 6000);
        }
    }

    // ---------- Форма записи дневника: показ полей под тип записи ----------
    const typeSelect = document.querySelector('[data-entry-type]');
    if (typeSelect) {
        const groups = document.querySelectorAll('[data-field-group]');
        const apply = () => {
            const t = typeSelect.value;
            groups.forEach(g => {
                const types = (g.dataset.fieldGroup || '').split(',');
                g.style.display = types.includes(t) ? '' : 'none';
            });
        };
        apply();
        typeSelect.addEventListener('change', apply);
    }

    // ---------- Подтверждение удаления ----------
    document.querySelectorAll('form[data-confirm]').forEach(f => {
        f.addEventListener('submit', e => {
            if (!window.confirm(f.dataset.confirm || 'Удалить запись?')) e.preventDefault();
        });
    });

    // ---------- Заполнить «сейчас» в поле даты события ----------
    document.querySelectorAll('[data-now-default]').forEach(el => {
        if (!el.value) {
            const d = new Date();
            d.setMinutes(d.getMinutes() - d.getTimezoneOffset());
            el.value = d.toISOString().slice(0, 16);
        }
    });

    // ---------- Кнопки-подстановки рациона ----------
    document.querySelectorAll('[data-preset]').forEach(btn => {
        btn.addEventListener('click', () => {
            const target = document.querySelector(btn.dataset.presetTarget || '#FoodType');
            if (target) target.value = btn.dataset.preset;
        });
    });
})();

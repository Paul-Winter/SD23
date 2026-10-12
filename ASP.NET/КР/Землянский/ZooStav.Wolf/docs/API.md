# API приложения «ZooStav — северный волк»

Базовый адрес: `https://wolf.zoostav.ru` (а также основной домен `https://zoostav.ru` и локальный запуск).
Формат — JSON (UTF-8). Интерактивная документация — **`/swagger`**.

Аутентификация:
* **публичные методы** — без токена;
* **служебные** — заголовок `Authorization: Bearer <JWT>` (токен выдаёт `POST /api/auth/login`);
  вместо JWT можно использовать cookie работника, полученную на странице `/staff/login`.

Роли: `Admin`, `Veterinarian`, `Keeper`, `Observer`.

---

## 1. Авторизация

### POST /api/auth/login — получить JWT

```bash
curl -X POST https://wolf.zoostav.ru/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"userName":"keeper","password":"Keeper#2026"}'
```

Ответ:

```json
{
  "accessToken": "eyJhbGciOiJIUzI1NiIs…",
  "tokenType": "Bearer",
  "expiresAtUtc": "2026-10-08T23:05:00Z",
  "userId": "53b2d0cb…",
  "userName": "keeper",
  "displayName": "Егоров Дмитрий Сергеевич",
  "role": "Keeper",
  "position": "Старший кипер вольера «Северная тундра»",
  "permissions": ["diary:write", "diary:read:internal"]
}
```

Неверный логин/пароль → `401` `{ "error": "Неверный логин или пароль" }`.
Каждый вход фиксируется в журнале изменений (`action = Login`).

### GET /api/auth/me — профиль и права текущего пользователя

```bash
curl https://wolf.zoostav.ru/api/auth/me -H "Authorization: Bearer $TOKEN"
```

---

## 2. Публичные методы (без токена)

| Метод | Назначение |
|---|---|
| `GET /api/animals` | список животных (стая) с адресами страниц |
| `GET /api/animals/{slug}` | карточка животного: описание, история, медиа, публичный дневник, статус кормления |
| `GET /api/animals/{slug}/media` | фото, видео, веб-камера |
| `GET /api/feeding-status?slug=wolf` | последняя кормёжка: время, специалист, рацион, часов назад |
| `GET /api/diary` | публичная лента дневника (записи `isPublic = true`) |
| `GET /api/diary/{id}` | запись дневника (служебные — только работнику с токеном) |
| `GET /api/diary/{id}/comments` | комментарии работников к записи |
| `GET /api/donations/summary` | сводка сборов: корм, лечение, вольер, свободное назначение |
| `GET /api/donations/recent` | последние донаты |
| `GET /api/meta/enums` | справочники: типы записей, роли, назначения донатов |

Примеры:

```bash
# Последняя кормёжка волка Умки (слаг wolf)
curl "https://wolf.zoostav.ru/api/feeding-status?slug=wolf"

# Карточка животного целиком
curl "https://wolf.zoostav.ru/api/animals/wolf"
```

Ответ `/api/feeding-status`:

```json
{
  "lastFeedingAt": "2026-10-08T11:25:45",
  "lastFeedingSpecialist": "Егоров Дмитрий Сергеевич",
  "lastFoodType": "Оленина, говяжьи субпродукты, витаминный премикс",
  "lastFoodAmountKg": 2.4,
  "hoursSinceLastFeeding": 6.02,
  "isOverdue": false,
  "feedingsLast24h": 2,
  "feedingsLast7Days": 6,
  "nextPlannedFeeding": "09.10 11:00"
}
```

---

## 3. Дневник особи/популяции

### GET /api/diary — лента с фильтрацией

Параметры (все необязательные):

| Параметр | Описание |
|---|---|
| `from`, `to` | период по дате события (`2026-10-01`) |
| `type` | тип записи: `Feeding`, `Vaccination`, `Mating`, `Offspring`, `Illness`, `Treatment`, `Observation`, `Weighing`, `Examination`, `Relocation`, `Comment` |
| `authorId` | идентификатор специалиста, внёсшего запись |
| `animalId` | конкретная особь |
| `search` | поиск по заголовку, описанию, рациону, препарату, диагнозу |
| `includeNonPublic` | `true` — показать служебные записи (**только с токеном работника**) |
| `sort` | `date_desc` (по умолчанию) или `date_asc` |
| `page`, `pageSize` | страницы (по умолчанию 1 и 10) |

```bash
# Все кормёжки за последнюю неделю
curl "https://wolf.zoostav.ru/api/diary?type=Feeding&from=2026-10-01"

# Служебные записи (ветеринар/кипер)
curl "https://wolf.zoostav.ru/api/diary?includeNonPublic=true" -H "Authorization: Bearer $TOKEN"

# Записи конкретного специалиста
curl "https://wolf.zoostav.ru/api/diary?authorId=53b2d0cb…" -H "Authorization: Bearer $TOKEN"
```

Ответ (сокращённо):

```json
{
  "items": [
    {
      "id": 1,
      "animalId": 1,
      "animalName": "Умка",
      "type": "Кормёжка",
      "typeCode": "Feeding",
      "occurredAt": "2026-10-08T11:25:45",
      "title": "Утренняя кормёжка",
      "details": "Рацион съеден полностью…",
      "foodType": "Оленина, говяжьи субпродукты, витаминный премикс",
      "foodAmountKg": 2.4,
      "isPublic": true,
      "authorId": "…",
      "authorName": "Егоров Дмитрий Сергеевич",
      "authorRole": "Keeper",
      "authorPosition": "Старший кипер вольера «Северная тундра»",
      "createdAt": "2026-10-08T11:35:45Z",
      "updatedAt": "2026-10-08T11:55:45Z",
      "commentCount": 2,
      "comments": [ { "id": 1, "text": "…", "authorName": "…", "createdAt": "…" } ]
    }
  ],
  "page": 1, "pageSize": 10, "totalCount": 26, "totalPages": 3
}
```

### POST /api/diary — создать запись (роли Keeper, Veterinarian, Admin)

```bash
curl -X POST https://wolf.zoostav.ru/api/diary \
  -H "Authorization: Bearer $TOKEN" -H "Content-Type: application/json" \
  -d '{
    "animalId": 1,
    "type": "Feeding",
    "occurredAt": "2026-10-08T18:00:00",
    "title": "Вечерняя кормёжка",
    "details": "Съедено полностью, вода заменена",
    "foodType": "Оленина, морская рыба",
    "foodAmountKg": 2.4,
    "isPublic": true
  }'
```

Поля, специфичные для типов:
`foodType`/`foodAmountKg` — кормёжка; `medication`/`dosage` — вакцинация и лечение;
`partnerName`/`offspringCount` — спаривание и потомство; `weightKg`/`temperatureC` — взвешивание и осмотр;
`diagnosis` — болезнь/лечение (служебное поле).

Создание фиксируется в журнале: `action = Created`, с указанием специалиста и времени.

### PUT /api/diary/{id} — изменить запись

Права: автор записи либо `Admin`. Все изменения пишутся в журнал со списком изменённых полей:
`«масса корма: 2,60 → 2,80; описание: "…" → "…"»`.

```bash
curl -X PUT https://wolf.zoostav.ru/api/diary/27 \
  -H "Authorization: Bearer $TOKEN" -H "Content-Type: application/json" \
  -d '{"foodAmountKg": 2.8, "details": "Добавка 0,2 кг по холодной погоде"}'
```

### DELETE /api/diary/{id} — удалить запись (только Admin)

```bash
curl -X DELETE https://wolf.zoostav.ru/api/diary/27 -H "Authorization: Bearer $ADMIN_TOKEN"
```

### GET /api/diary/{id}/history — история изменений записи

Возвращает все события журнала по этой записи: кто создал, кто и когда менял, что именно.

---

## 4. Комментарии работников

### POST /api/diary/{entryId}/comments — добавить комментарий (любая роль работника)

```bash
curl -X POST https://wolf.zoostav.ru/api/diary/1/comments \
  -H "Authorization: Bearer $TOKEN" -H "Content-Type: application/json" \
  -d '{"text":"Проверил рацион — добавок не требуется."}'
```

Автор, должность и время подставляются автоматически, действие попадает в журнал (`CommentAdded`).

### DELETE /api/diary/comments/{id} — удалить комментарий

Доступно автору комментария или администратору.

---

## 5. Служебные методы (кабинет работника)

| Метод | Роль | Назначение |
|---|---|---|
| `GET /api/staff/dashboard?slug=wolf` | любая роль работника | сводка: последняя кормёжка, счётчики, последние записи, комментарии, изменения |
| `GET /api/staff/feeding-status?slug=wolf` | любая роль работника | детальный статус кормления |
| `GET /api/staff/users` | Admin | список работников |

```bash
curl "https://wolf.zoostav.ru/api/staff/dashboard?slug=wolf" -H "Authorization: Bearer $TOKEN"
```

---

## 6. Журнал изменений (аудит)

| Метод | Роль |
|---|---|
| `GET /api/audit` | Admin, Veterinarian |
| `GET /api/audit/summary` | Admin, Veterinarian |

Фильтры `GET /api/audit`: `from`, `to`, `userId`, `action`
(`Login`, `Created`, `Updated`, `Deleted`, `CommentAdded`, `CommentDeleted`, `DonationCreated`, `Logout`),
`entity` (`DiaryEntry`, `DiaryComment`, `Donation`, `AppUser`), `page`, `pageSize`.

```bash
curl "https://wolf.zoostav.ru/api/audit?action=Updated&pageSize=20" -H "Authorization: Bearer $VET_TOKEN"
```

```json
{
  "items": [
    {
      "id": 63,
      "entityName": "DiaryEntry",
      "entityId": "27",
      "action": "Updated",
      "summary": "Изменена запись №27 «Кормёжка (проверка через API)». масса корма: «2,60» → «2,80». Специалист: Егоров Дмитрий Сергеевич (Старший кипер…)",
      "userId": "…",
      "userName": "Егоров Дмитрий Сергеевич",
      "userRole": "Keeper",
      "ipAddress": "10.20.0.44",
      "timestampUtc": "2026-10-08T17:29:19Z"
    }
  ],
  "page": 1, "pageSize": 20, "totalCount": 71, "totalPages": 4
}
```

`GET /api/audit/summary` — сколько действий внёс каждый специалист и разбивка по типам действий.

---

## 7. Донаты

### POST /api/donations — оформить донат (доступно без авторизации)

```bash
# обычный донат
curl -X POST https://wolf.zoostav.ru/api/donations -H "Content-Type: application/json" \
  -d '{"donorName":"Тестовый даритель","amount":2500,"purpose":"Корм","isRecurring":true}'

# free donation: сумма 0, свободное назначение
curl -X POST https://wolf.zoostav.ru/api/donations -H "Content-Type: application/json" \
  -d '{"donorName":"Гость","amount":0,"purpose":"Свободное назначение","comment":"Поддерживаем стаю"}'
```

Назначения: `Корм`, `Лечение`, `Вольер`, `Свободное назначение`.

| Метод | Доступ |
|---|---|
| `GET /api/donations/summary` | публично |
| `GET /api/donations/recent?take=10` | публично |
| `GET /api/donations/staff/list?purpose=Корм&onlyUnprocessed=true` | работники |

Оплата в проекте эмулируется (провайдер `demo`) — точка интеграции реального эквайринга:
`DonationService.AddAsync` и метод `OnPostAsync` страницы `/wolf/{slug}/donate`.

---

## 10. Веб-камера вольера

| Метод | Доступ | Назначение |
|---|---|---|
| `GET /api/webcam/status` | публично | режим (`hls`/`mjpeg`/`snapshot`/`demo`), живой ли поток, источник, ошибки, подсказки по настройке |
| `GET /api/webcam/snapshot` | публично | текущий кадр вольера в JPEG (с камеры или демонстрационный) |
| `GET /api/webcam/mjpeg` | публично | прокси MJPEG-потока камеры (`multipart/x-mixed-replace`), без перекодирования |
| `GET /hls/wolf.m3u8` | публично | HLS-плейлист потока, создаваемый ffmpeg из RTSP-камеры |
| `GET /hls/{segment}.ts` | публично | сегменты HLS (`video/mp2t`) |

```bash
# Состояние камеры
curl https://wolf.zoostav.ru/api/webcam/status

# Кадр (подходит для превью и мобильных приложений)
curl -o frame.jpg https://wolf.zoostav.ru/api/webcam/snapshot

# Проверка потока без правки конфигурации — переменными окружения:
#   Webcam__StreamUrl=https://camera.zoostav.ru/wolf/index.m3u8   (HLS-поток камеры)
#   Webcam__CameraInput=rtsp://user:pass@192.168.1.64:554/...     (RTSP -> HLS средствами ffmpeg)
#   Webcam__MjpegUrl=http://192.168.1.64/video                    (проксирование MJPEG)
#   Webcam__SnapshotUrl=http://192.168.1.64/snapshot.jpg          (кадры JPEG раз в PollSeconds)
#   Webcam__Mode=demo                                             (принудительно демо-режим)
```

Пример ответа `/api/webcam/status`:

```json
{
  "mode": "hls",
  "isLive": true,
  "location": "Вольер «Северная тундра», камера №2",
  "sourceDescription": "поток с камеры rtsp://192.168.1.64:554/... (транскодирование в HLS средствами приложения)",
  "streamUrl": "/hls/wolf.m3u8",
  "transcoding": true,
  "transcodeError": null,
  "lastFrameUtc": "2026-10-08T18:02:11Z",
  "pollSeconds": 3,
  "settings": { "mode": "auto", "hasCameraInput": true, "localPlaylistReady": true }
}
```

Если камера недоступна, методы не «падают»: `/api/webcam/snapshot` возвращает последний кадр
(или демонстрационный), а страница веб-камеры автоматически переключается на режим кадров.

---

## 8. Коды ответов

| Код | Когда |
|---|---|
| 200 / 201 / 204 | успех / создано / удалено |
| 400 | ошибка валидации (`ValidationProblem`) |
| 401 | нет или истёк токен (служебный метод) |
| 403 | недостаточно прав (например, кипер удаляет запись или запрашивает служебные записи без прав) |
| 404 | животное, запись или комментарий не найдены |

## 9. Быстрый сценарий проверки (копировать целиком)

```bash
BASE=https://wolf.zoostav.ru

TOKEN=$(curl -s -X POST $BASE/api/auth/login -H "Content-Type: application/json" \
  -d '{"userName":"keeper","password":"Keeper#2026"}' | python3 -c "import sys,json;print(json.load(sys.stdin)['accessToken'])")

# 1) публичные данные
curl -s "$BASE/api/animals"
curl -s "$BASE/api/feeding-status?slug=wolf"

# 2) создаём запись о кормёжке
curl -s -X POST $BASE/api/diary -H "Authorization: Bearer $TOKEN" -H "Content-Type: application/json" \
  -d '{"animalId":1,"type":"Feeding","occurredAt":"2026-10-08T18:00:00","title":"Вечерняя кормёжка","foodType":"Оленина, рыба","foodAmountKg":2.4}'

# 3) убеждаемся, что «последняя кормёжка» обновилась и кто её внёс
curl -s "$BASE/api/staff/feeding-status?slug=wolf" -H "Authorization: Bearer $TOKEN"

# 4) комментарий и журнал изменений
curl -s -X POST $BASE/api/diary/1/comments -H "Authorization: Bearer $TOKEN" \
  -H "Content-Type: application/json" -d '{"text":"Проверка комментария"}'

VET=$(curl -s -X POST $BASE/api/auth/login -H "Content-Type: application/json" \
  -d '{"userName":"vet","password":"Vet#2026"}' | python3 -c "import sys,json;print(json.load(sys.stdin)['accessToken'])")
curl -s "$BASE/api/audit?pageSize=10" -H "Authorization: Bearer $VET"
```

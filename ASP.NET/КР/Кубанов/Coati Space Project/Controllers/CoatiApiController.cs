using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Coati_Space_Project.Models;
using System.Text;

namespace Coati_Space_Project.Controllers
{
    [Route("api/coati")]
    public class CoatiApiController : Controller
    {
        ApplicationContext db;

        public CoatiApiController(ApplicationContext context)
        {
            db = context;
        }

        // Текстовая сводка об особи носухи на русском языке
        [HttpGet("")]
        [HttpGet("info")]
        public async Task<IActionResult> GetInfo()
        {
            var coati = await db.Coatis.FirstOrDefaultAsync();
            if (coati == null)
            {
                return Content("Данные о носухе в базе данных пока отсутствуют.", "text/plain; charset=utf-8");
            }

            var sb = new StringBuilder();
            sb.AppendLine("==================================================");
            sb.AppendLine("ИНФОРМАЦИОННАЯ СВОДКА ЗООПАРКА: НОСУХА ХАХАТУХА");
            sb.AppendLine("==================================================");
            sb.AppendLine($"Кличка:             {coati.Name}");
            sb.AppendLine($"Биологический вид:  {coati.Species}");
            sb.AppendLine($"Пол:                {coati.Gender}");
            sb.AppendLine($"Дата рождения:      {coati.BirthDate:dd.MM.yyyy}");
            sb.AppendLine($"Текущее состояние:  {coati.HealthStatus}");
            sb.AppendLine($"Место содержания:   {coati.Habitat}");
            sb.AppendLine($"Рацион питания:     {coati.Diet}");
            sb.AppendLine("--------------------------------------------------");
            sb.AppendLine("История и биография особи:");
            sb.AppendLine(coati.Biography);
            sb.AppendLine("==================================================");

            return Content(sb.ToString(), "text/plain; charset=utf-8");
        }

        // Текстовый журнал событий дневника наблюдений на русском языке
        [HttpGet("diary")]
        public async Task<IActionResult> GetDiary()
        {
            var entries = await db.DiaryEntries.OrderByDescending(e => e.EventDate).ToListAsync();
            if (!entries.Any())
            {
                return Content("В дневнике наблюдений пока нет зафиксированных событий.", "text/plain; charset=utf-8");
            }

            var sb = new StringBuilder();
            sb.AppendLine("==================================================");
            sb.AppendLine("ЖУРНАЛ НАБЛЮДЕНИЙ ЗООЛОГОВ И КИПЕРОВ (НОСУХА ХАХАТУХА)");
            sb.AppendLine("==================================================");
            sb.AppendLine($"Всего записей в журнале: {entries.Count}");
            sb.AppendLine();

            int index = 1;
            foreach (var entry in entries)
            {
                sb.AppendLine($"Запись #{index++} от {entry.EventDate:dd.MM.yyyy HH:mm}");
                sb.AppendLine($"Категория события: [{entry.EventType}]");
                sb.AppendLine($"Тема:              {entry.Title}");
                sb.AppendLine($"Сотрудник:         {entry.Author}");
                sb.AppendLine($"Подробности:       {entry.Description}");
                sb.AppendLine("--------------------------------------------------");
            }

            return Content(sb.ToString(), "text/plain; charset=utf-8");
        }

        // Текстовая сводка по донатам и сборам на русском языке
        [HttpGet("donations")]
        public async Task<IActionResult> GetDonations()
        {
            var donations = await db.Donations.OrderByDescending(d => d.CreatedAt).ToListAsync();
            var total = donations.Sum(d => d.Amount);

            var sb = new StringBuilder();
            sb.AppendLine("==================================================");
            sb.AppendLine("ОТЧЁТ О ДОБРОВОЛЬНЫХ ПОЖЕРТВОВАНИЯХ");
            sb.AppendLine("==================================================");
            sb.AppendLine($"Общая сумма сборов: {total:N0} рублей");
            sb.AppendLine($"Количество взносов: {donations.Count}");
            sb.AppendLine();

            foreach (var donation in donations)
            {
                sb.AppendLine($"[{donation.CreatedAt:dd.MM.yyyy HH:mm}] {donation.DonorName} внес {donation.Amount:N0} руб.");
                sb.AppendLine($"Цель: {donation.Target}");
                if (!string.IsNullOrWhiteSpace(donation.Message))
                {
                    sb.AppendLine($"Пожелание: «{donation.Message}»");
                }
                sb.AppendLine("--------------------------------------------------");
            }

            return Content(sb.ToString(), "text/plain; charset=utf-8");
        }

        // Текстовый статус телеметрии и веб-камеры
        [HttpGet("status")]
        public IActionResult GetStatus()
        {
            string status = "==================================================\n" +
                   "СТАТУС ВОЛЬЕРА И ТРАНСЛЯЦИИ НОСУХИ\n" +
                   "==================================================\n" +
                   "Состояние веб-камеры:   ОНЛАЙН (Поток активен)\n" +
                   "Разрешение потока:      1920x1080 (Full HD, 30 FPS)\n" +
                   "Температура в вольере:  +24.5 °C\n" +
                   "Влажность воздуха:      62 %\n" +
                   "Освещение павильона:    Дневное (эмуляция тропиков)\n" +
                   "Активность носухи:      Умеренно-активное перемещение по ярусам\n" +
                   "==================================================";

            return Content(status, "text/plain; charset=utf-8");
        }
    }
}

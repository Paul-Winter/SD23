namespace Coati_Space_Project.Models
{
    public class CoatiViewModel
    {
        public Coati Coati { get; set; } = null!;
        public List<string> GalleryImages { get; set; } = new();
        public List<DiaryEntry> RecentDiaryEntries { get; set; } = new();
        public decimal TotalDonations { get; set; }
        public int DonationsCount { get; set; }
    }
}

namespace WebAppMVC.Models
{
    public class CompanyViewModel
    {
        public IEnumerable<Employees> Employees { get; set; } = new List<Employees>();
        public IEnumerable<Departs> Departs { get; set; } = new List<Departs>();

    }
}

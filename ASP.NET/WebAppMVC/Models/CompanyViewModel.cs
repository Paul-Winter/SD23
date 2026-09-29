namespace WebAppMVC.Models
{
    public class CompanyViewModel
    {
        public IEnumerable<Employee> Employees { get; set; } = new List<Employee>();
        public IEnumerable<Depart> Departs { get; set; } = new List<Depart>();

    }
}

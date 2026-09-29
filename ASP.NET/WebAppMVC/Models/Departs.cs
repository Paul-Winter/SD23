using System.Reflection;

namespace WebAppMVC.Models
{
    public class Departs
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Departs(int id, string name, string adress, string phone, string email)
        {
            Id = id;
            Name = name;
            Adress = adress;
            Email = email;
            Phone = phone;
        }
        public override string ToString()
        {
            return $"Id: {Id};\nName: {Name};\nAdress: {Adress};\n" +
                   $"Email: {Email};\nPhone: {Phone};\n";
        }
    }
}

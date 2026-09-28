namespace WebAppMVC.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Gender { get; set; }
        public string Marriage { get; set; }
        public string ProgrammingLanguage { get; set; }
        public User(int id, string login, string pass, string email, DateOnly birth,
                    string gender, string marriage, string progLang)
        {
            Id = id;
            Login = login;
            Password = pass;
            Email = email;
            BirthDate = birth;
            Gender = gender;
            Marriage = marriage;
            ProgrammingLanguage = progLang;
        }
        public override string ToString()
        {
            return $"Id: {Id};\nLogin: {Login};\nPassword: {Password};\n" +
                   $"Email: {Email};\nBirth Date: {BirthDate};\nGender: {Gender};\n" +
                   $"Marriage: {Marriage};\nProgramming Language: {ProgrammingLanguage};\n";
        }
    }
}

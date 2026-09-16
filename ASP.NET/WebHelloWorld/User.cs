namespace WebHelloWorld
{
    public class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public User(string login, string pass, string email)
        {
            this.Login = login;
            this.Password = pass;
            this.Email = email;
        }
    }
}

namespace WebHelloWorld
{
    public class UserDate : IUserData
    {
        public string GetUserData()
        {
            return DateTime.Now.ToLongDateString();
        }
    }
}

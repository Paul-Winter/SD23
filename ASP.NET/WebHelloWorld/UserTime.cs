namespace WebHelloWorld
{
    public class UserTime : IUserData
    {
        public string GetUserData()
        {
            return DateTime.Now.ToLongTimeString();
        }
    }
}

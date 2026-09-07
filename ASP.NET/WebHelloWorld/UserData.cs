namespace WebHelloWorld
{
    public class UserData
    {
        IUserData userData;
        public UserData(IUserData userData)
        {
            this.userData = userData;
        }
        public string GetUserData() => $"From class: {userData.ToString()}";
    }
}

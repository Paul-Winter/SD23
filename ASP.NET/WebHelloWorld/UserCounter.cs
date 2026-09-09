namespace WebHelloWorld
{
    public class UserCounter : ICounter
    {
        private static Random random = new Random();
        private int value;
        public UserCounter()
        {
            this.value = random.Next(0, 100);
        }
        public int Value => value;
    }
}

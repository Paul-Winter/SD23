namespace WebHelloWorld
{
    public class UserDataMiddleware
    {
        private readonly RequestDelegate rd;
        private readonly IEnumerable<IUserData> userDatas;

        public UserDataMiddleware(RequestDelegate rd, IEnumerable<IUserData> userDatas)
        {
            this.rd = rd;
            this.userDatas = userDatas;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            context.Response.ContentType = "text/html; charset=utf-8";
            string text = "";

            foreach (var ud in userDatas)
            {
                text += $"<h3>{ud.GetUserData()}</h3>";
            }

            await context.Response.WriteAsync($"{text}");
        }
    }
}

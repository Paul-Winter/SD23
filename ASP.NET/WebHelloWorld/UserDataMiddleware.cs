namespace WebHelloWorld
{
    public class UserDataMiddleware
    {
        private readonly RequestDelegate rd;
        public UserDataMiddleware(RequestDelegate rd)
        {
            this.rd = rd;
        }
        public async Task InvokeAsync(HttpContext context, IUserData userData)
        {
            context.Response.ContentType = "text/html; charset=utf-8";
            await context.Response.WriteAsync($"<h3>{userData.GetUserData()}</h3>");
        }
    }
}

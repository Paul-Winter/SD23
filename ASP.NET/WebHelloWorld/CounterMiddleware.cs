namespace WebHelloWorld
{
    public class CounterMiddleware
    {
        private RequestDelegate rd;
        int i = 0;
        public CounterMiddleware(RequestDelegate rd)
        {
            this.rd = rd;
        }
        public async Task InvokeAsync(HttpContext httpContext,
                                      ICounter counter,
                                      CounterService counterService)
        {
            i++;
            httpContext.Response.ContentType = "text/html; charset=utf-8";
            await httpContext.Response.WriteAsync($"Запрос №{i};" +
                                    $"Счётчик: {counter.Value};" +
                                    $"Сервис: {counterService.Counter.Value}");
        }
    }
}

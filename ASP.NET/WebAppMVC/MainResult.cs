using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace WebAppMVC
{
    public class MainResult : IActionResult
    {
        private string result;
        public MainResult(string html)
        {
            result = html;
        }
        public async Task ExecuteResultAsync(ActionContext context)
        {
            string htmlCode = $@"<!DOCTYPE html>
            <html>
                <head>
                    <title>IT TOP COLLEGE</title>
                    <meta charset=utf-8/>
                </head>
                <body>
                    {result}
                </body>
            </html>";

            await context.HttpContext.Response.WriteAsync(htmlCode);
        }        
    }
}

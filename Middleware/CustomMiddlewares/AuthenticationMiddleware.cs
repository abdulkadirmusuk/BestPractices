using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Middleware.CustomMiddlewares
{
    public class AuthenticationMiddleware
    {
        //bir sonraki delege ye geç demek için request delege eklenir

        private readonly RequestDelegate _next;
        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next; //bir sonraki delege ye geç
        }
        public async Task Invoke(HttpContext httpContext)
        {
            string authHeader = httpContext.Request.Headers["Authorization"];
            //basic kadir:12345 şeklinde data gelecek
            //authToken varsa ve basic ile başlıyorsa
            if (authHeader != null && authHeader.StartsWith("basic", System.StringComparison.OrdinalIgnoreCase))
            {
                var token = authHeader.Substring(6).Trim();
                //base64 convertion
                var credentialString = Encoding.UTF8.GetString(Convert.FromBase64String(token));
                var credentials = credentialString.Split(':');
                if (credentials[0] == "kadir" && credentials[1] == "12345")
                {
                    //identity principle create
                    var claims = new[]
                    {
                        new Claim("name", credentials[0]),
                        new Claim(ClaimTypes.Role, "Admin") //admin rolu ataması
                    };
                    var identity = new ClaimsIdentity(claims, "Basic");
                    httpContext.User = new ClaimsPrincipal(identity);
                }

            }
            else httpContext.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
            await _next(httpContext); //ilgili context sonraki middleware e aktarılır.
        }
    }
}

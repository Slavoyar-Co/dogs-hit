using System.Net;
using System.Security.Authentication;

namespace IdentityService
{
    public class ExceptionLoggerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger _logger;
        public ExceptionLoggerMiddleware(RequestDelegate next, ILogger logger)
        {
            this.next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            HttpResponse response = context.Response;

            try
            {
                await next(context);
            }
            catch (Exception error)
            {
                switch (error) //todo add possible exceptions and handle them
                {
                    case AuthenticationException:

                        if (context.User.Identity is { IsAuthenticated: true })
                        {
                        }

                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
            }
            finally
            {
                if (context.Response.StatusCode == 404)
                {
                    _logger.LogInformation("404 Not Found. " + context.Request.Path.Value);
                }
            }
        }
    }
}
namespace GraduationThesis_CarServices.Middleware
{
    public class StorageMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            // Check if the request has a valid token
            if (context.Request.Headers.ContainsKey("Authorization"))
            {
                string token = context.Request.Headers["Authorization"]!;

                string[] tokenString = token.Split(' ');
                // Store the token in context
                context.Items["Token"] = tokenString[1];
            }

            // Call the next middleware in the pipeline
            await next(context);
        }
    }
}
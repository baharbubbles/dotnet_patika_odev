namespace Exercise1.Midlewares;


public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // buraya yazılan kod controllera girmeden önce çalışır
        try
        {
            await _next.Invoke(context);
        }
        catch (System.Exception e)
        {
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("İçerde bir hata yaşandı.");
        }

        // buraya yazılan kod controllerdan çıktıktan sonra çalışır
    }
}
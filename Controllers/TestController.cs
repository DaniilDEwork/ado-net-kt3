using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace Kt3HttpResponse.Controllers;

public class TestController : Controller
{
    private readonly IWebHostEnvironment environment;

    public TestController(IWebHostEnvironment environment)
    {
        this.environment = environment;
    }

    [HttpGet]
    public async Task Text()
    {
        Response.ContentType = "text/plain";
        await Response.WriteAsync("Hello, world!");
    }

    [HttpGet]
    public async Task Html()
    {
        Response.ContentType = "text/html";
        await Response.WriteAsync("<html lang=\"en\"><head><meta charset=\"utf-8\"><title>HTML response</title></head><body><h1>Hello, world!</h1><p>This is an HTML response</p></body></html>");
    }

    [HttpGet]
    public async Task Json()
    {
        var person = new { Name = "Daniil", Age = 20 };
        Response.ContentType = "application/json";
        await Response.WriteAsJsonAsync(person, new JsonSerializerOptions(), "application/json");
    }

    [HttpGet]
    public async Task File()
    {
        string path = Path.Combine(environment.ContentRootPath, "Files", "test.txt");
        Response.ContentType = "text/plain";
        Response.Headers.ContentDisposition = "attachment; filename=\"test.txt\"";
        await Response.SendFileAsync(path);
    }

    [HttpGet]
    public async Task Status()
    {
        Response.StatusCode = 404;
        Response.ContentType = "text/plain";
        await Response.WriteAsync("Resource Not Found");
    }

    [HttpGet]
    public async Task Cookie()
    {
        Response.Cookies.Append("user", "Answer");
        Response.ContentType = "text/plain";
        await Response.WriteAsync("Cookie user=Answer has been set");
    }
}
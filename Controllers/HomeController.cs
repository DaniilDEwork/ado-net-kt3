using Microsoft.AspNetCore.Mvc;

namespace Kt3HttpResponse.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        ViewData["Title"] = "Главная";
        return View();
    }

    public IActionResult Demo(string id)
    {
        string? title = id switch
        {
            "Text" => "Текстовый ответ",
            "Html" => "HTML-ответ",
            "Json" => "JSON-ответ",
            "File" => "Отправка файла",
            "Status" => "Статусный код",
            "Cookie" => "Установка cookie",
            _ => null
        };

        if (title == null)
        {
            return NotFound();
        }

        ViewData["Title"] = title;
        ViewData["Action"] = id;
        return View(model: id);
    }
}
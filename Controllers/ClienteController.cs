using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using trabajo_final_grupo_verde.Models;

namespace trabajo_final_grupo_verde.Controllers;

public class ClienteController : Controller
{
    private readonly ILogger<ClienteController> _logger;

    public ClienteController(ILogger<ClienteController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Create([Bind("Nombres,ApellidoPaterno,ApellidoMaterno,Email,Dni,Celular,Genero")] User cliente)
    {
        cliente.Rol=1;
        if (ModelState.IsValid)
        {
            ViewData["Message"] = "El cliente se ha registrado, Cliente Rol : "+ cliente.Rol;

            return View("Index",cliente);
        }else{
             ViewData["Message"] = "Error al crear al cliente";
        }
        return View("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

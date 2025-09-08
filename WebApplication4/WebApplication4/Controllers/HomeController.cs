using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApplication4.Models;

namespace WebApplication4.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly HttpClient _httpClient;
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7071/") // Ajusta según tu API
        };
    }
   
    public async Task<IActionResult> Index()
    {
        //var query = new ListarDestinatariosQuery
        //{
        //    nombreCodigo = "0000",
        //    idEmpresa = 2
        //};

        //var url = $"/api/prueba/v1/listar?nombreCodigo={query.nombreCodigo}&idEmpresa={query.idEmpresa}";

        //_httpClient.DefaultRequestHeaders.Clear();
        //_httpClient.DefaultRequestHeaders.Add("REFERENCE_ID", "abc123");
        //_httpClient.DefaultRequestHeaders.Add("CONSUMER", "mi_frontend");

        //List<Destinatario> destinatarios = new List<Destinatario>();

        //try
        //{
        //    var response = await _httpClient.GetAsync(url);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var json = await response.Content.ReadAsStringAsync();
        //        destinatarios = JsonConvert.DeserializeObject<List<Destinatario>>(json);
        //    }
        //}
        //catch (Exception ex)
        //{
        //    _logger.LogError("Error llamando al API: " + ex.Message);
        //}

        return View();
    
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

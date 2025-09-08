using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using WebApplication4.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApplication4.Controllers
{
    public class DestinatarioController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DestinatarioController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private HttpClient GetClient()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:7071/api/"); // Cambia por la URL de tu API
            //client.DefaultRequestHeaders.Add("REFERENCE_ID", "123");
            //client.DefaultRequestHeaders.Add("CONSUMER", "frontend");
            return client;
        }

        public async Task<IActionResult> Index(int? id, string nombre = null)
        {
            string idDestinatario = id.ToString();
            ConsultarDestinatarioQuery modelo = new ConsultarDestinatarioQuery();
            modelo.idDestinatario = idDestinatario;
            var client = GetClient();

            if (id.HasValue)
            {
                // Buscar solo ese destinatario por ID
                var content = new StringContent(JsonSerializer.Serialize(modelo), Encoding.UTF8, "application/json");

                var response = await client.PostAsync("/api/prueba/v1/consultar/", content);
             
                if (!response.IsSuccessStatusCode)
                {
                    if (!response.IsSuccessStatusCode)
                        return View(new List<Destinatario>());
                   
                }
                var json = await response.Content.ReadAsStringAsync();

                var destinatario = JsonSerializer.Deserialize<List<Destinatario>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return View(destinatario);
               
            }

            if (string.IsNullOrWhiteSpace(nombre))
            {
                return View(new List<Destinatario>()); // No mostrar nada si no hay filtro
            }

            // Buscar por nombre
            
            var url = $"/api/prueba/v1/listar?nombreCodigo={nombre}";

            var listaResponse = await client.GetAsync(url);
            if (!listaResponse.IsSuccessStatusCode)
                return View(new List<Destinatario>());

            var listaJson = await listaResponse.Content.ReadAsStringAsync();
            var model = JsonSerializer.Deserialize<List<Destinatario>>(listaJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Destinatario model)
        {
          
            var client = GetClient();
            var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/prueba/v1/crear", content);

            if (response.IsSuccessStatusCode)
            {
              
                var idJson = await response.Content.ReadAsStringAsync();

                
                int idCreado = JsonSerializer.Deserialize<int>(idJson);

                
                return RedirectToAction("Index", new { id = idCreado });
            }

            ModelState.AddModelError("", "Error al crear el destinatario.");
            return View(model);
            
        }

        public async Task<IActionResult> Edit(string id)
        {
            string idDestinatario = id;
            ConsultarDestinatarioQuery model = new ConsultarDestinatarioQuery();
            model.idDestinatario = idDestinatario;
            var client = GetClient();
          
            var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/prueba/v1/consultar/", content);
            if (!response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            var json = await response.Content.ReadAsStringAsync();
            var modelo = JsonSerializer.Deserialize<List<Destinatario>>
                (json, 
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

           
            return View(modelo.FirstOrDefault());
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Destinatario model)
        {
            var client = GetClient();
            var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");

            var response = await client.PutAsync("/api/prueba/v1/actualiza", content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index", new { id = model.idDestinatario });

            ModelState.AddModelError("", "Error al actualizar.");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var client = GetClient();

            var response = await client.PutAsync($"/api/prueba/v1/eliminar?idDestinatario={id}", null);


            return RedirectToAction("Index");
        }
    }
}

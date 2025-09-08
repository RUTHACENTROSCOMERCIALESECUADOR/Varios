using Newtonsoft.Json;

namespace WebApplication3.Models
{
    public class ListarDestinatariosQuery
    {
        [JsonProperty("nombre")]
        public string nombreCodigo { get; set; }

        [JsonProperty("idEmpresa")]
        public int idEmpresa { get; set; }


    }
}

using Newtonsoft.Json;

namespace WebApplication3.Models
{
    public class ConsultarDestinatarioQuery
    {

        [JsonProperty("idDestinatario")]
        public string idDestinatario { get; set; }
        // public List<ConsultarDetalleDestinatarioQuery> Destinatarios { get; set; }
        // [JsonProperty("idEmpresa")] public int IdEmpresa { get; set; }


    }
}

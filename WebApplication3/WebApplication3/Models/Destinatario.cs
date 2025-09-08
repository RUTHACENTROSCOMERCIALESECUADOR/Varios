using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class Destinatario
    {
       
        public string idDestinatario { get; set; }


        public string codigoDestinatario { get; set; }

        public string rucDestinatario { get; set; }


        public string nombreDestinatario { get; set; }


        public int secuenciaDestinatario { get; set; }


        public string direccionDestinatario { get; set; }


        public string correoDestinatario { get; set; }


        public string ciudadDestinatario { get; set; }


        public string provinciaDestinatario { get; set; }


        public bool escaneoPorBulto { get; set; }
    }
}

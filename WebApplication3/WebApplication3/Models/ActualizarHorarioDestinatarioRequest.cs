namespace WebApplication3.Models
{
    public class ActualizarHorarioDestinatarioRequest
    {
        public int IdDestinatarioTiempoEntrega { get; set; }

        public string IdDestinatario { get; set; }

        public int Dia { get; set; }


        public string HoraInicio { get; set; }

        public string HoraFin { get; set; }

        public string Usuario { get; set; }

        public string IpMaquina { get; set; }

        public int IdClasificacionPlan { get; set; }

        public string Modo { get; set; }

        public string IdParroquia { get; set; }

        public string NombreParroquia { get; set; }
        public int EsFacturaUnica { get; set; }

        public int EscaneoPorBulto { get; set; }

        public string Latitud { get; set; }

        public string Longitud { get; set; }

        public string Observacion { get; set; }

        public string CorreoDestinatario { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace insitum.Utiles
{

        public static class Constantes
        {
            public const string IdUsuario = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/IdUsuario";
            public static string EmailAdmin { get; set; }
            public static string UsuarioReport { get; set; }
            public static string ContrasenaReporte { get; set; }
            public static string ServerReportUrl { get; set; }
            public static string ReporteGestionPath { get; set; }
            public static string ReporteClientesPath { get; set; }
            public static string DireccionFisicahtmlCorreoTimer { get; set; }
            public static int DiasNotificacion { get; set; }
            public static int TiempoCicloMinutosNotificacionEmail { get; set; }
            public static int HoraInicioCiclo { get; set; }
            public static int MinutoInicioCiclo { get; set; }
    }

}
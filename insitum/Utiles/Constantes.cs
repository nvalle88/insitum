﻿using System;
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
            public static string Contrasena { get; set; }
            public static string ServerReportUrl { get; set; }
            public static string ReporteCompromisosPath { get; set; }
            public static string ReporteVendedoresPath { get; set; }
            public static string ReporteClientesPath { get; set; }
        }

}
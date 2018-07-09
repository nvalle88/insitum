using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace insitum.Utiles
{
    public static class Mensaje
    {
        public static string UsuarioActivo { get; set; }
        public static string UsuarioInactivo { get; set; }
        public static string ExisteCorreo { get;set; }
        public static string UsuarioConfirmado { get; set; }
        public static string UsuarioSinConfirmar { get; set; }

        public static string CreacionCuentaTrabajador { get; set; }
        public static string CreacionCuentaCliente { get; set; }
        public static string RecuperarContrasena { get; set; }
        public static string CuentaActivada { get; set; }
        public static string ActivacionCuentaAdministrador { get; set; }
        public static string DesactivacionCuentaAdministrador { get; set; }
        public static string CuentaEliminada { get; set; }

        public static string ContrasenaTemporal { get; set; }
        public static string UsuarioContrasenaIncorrecto { get; internal set; }
        public static string InformacionActivarCuenta { get; internal set; }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.ComTypes;
using System.Web;

namespace insitum.Utiles.Template
{
    public static class InfoMail
    {
        public static string CreacionCuentaTrabajador()
        {
            //  var direccion = Path.Combine(Environment.CurrentDirectory, "\\Utils\\Template\\InfoMail.html");
            string path = System.Web.HttpContext.Current.Request.MapPath("~\\Utiles\\Template\\CreacionCuentaTrabajador.html");
            string readText = File.ReadAllText(path);
            return readText;
        }

        public static string CuentaActivada()
        {
            //  var direccion = Path.Combine(Environment.CurrentDirectory, "\\Utils\\Template\\InfoMail.html");
            string path = System.Web.HttpContext.Current.Request.MapPath("~\\Utiles\\Template\\CuentaActivada.html");
            string readText = File.ReadAllText(path);
            return readText;
        }


        public static string CreacionCuentaCliente()
        {
            //  var direccion = Path.Combine(Environment.CurrentDirectory, "\\Utils\\Template\\InfoMail.html");
            string path = System.Web.HttpContext.Current.Request.MapPath("~\\Utiles\\Template\\CreacionCuentaCliente.html");
            string readText = File.ReadAllText(path);
            return readText;
        }

        public static string ActivacionCuentaAdministrador()
        {
            //  var direccion = Path.Combine(Environment.CurrentDirectory, "\\Utils\\Template\\InfoMail.html");
            string path = System.Web.HttpContext.Current.Request.MapPath("~\\Utiles\\Template\\ActivacionCuentaAdministrador.html");
            string readText = File.ReadAllText(path);
            return readText;
        }

        public static string CuentaEliminada()
        {
            //  var direccion = Path.Combine(Environment.CurrentDirectory, "\\Utils\\Template\\InfoMail.html");
            string path = System.Web.HttpContext.Current.Request.MapPath("~\\Utiles\\Template\\CuentaEliminada.html");
            string readText = File.ReadAllText(path);
            return readText;
        }

        public static string DesactivacionCuentaAdministrador()
        {
            //  var direccion = Path.Combine(Environment.CurrentDirectory, "\\Utils\\Template\\InfoMail.html");
            string path = System.Web.HttpContext.Current.Request.MapPath("~\\Utiles\\Template\\DesactivacionCuentaAdministrador.html");
            string readText = File.ReadAllText(path);
            return readText;
        }
    }
}
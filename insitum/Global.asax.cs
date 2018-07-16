using insitum.Models;
using insitum.Utiles;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace insitum
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            CorreoUtil.SmtpServer = System.Configuration.ConfigurationManager.AppSettings["SmtpServer"];
            CorreoUtil.Port = System.Configuration.ConfigurationManager.AppSettings["SmtpPort"];
            var Ssl = true;
            if (Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["EnableSsl"]) ==true )
            {
                Ssl = true;
            }
            else
            {
                Ssl = false;
            }

            var SmtpUseDefaultCredentials = true;
            if (Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["SmtpUseDefaultCredentials"]) == true)
            {
                SmtpUseDefaultCredentials = true;
            }
            else
            {
                SmtpUseDefaultCredentials = false;
            }

            CorreoUtil.EnableSsl = Ssl;
            CorreoUtil.SmtpUseDefaultCredentials = SmtpUseDefaultCredentials;
            CorreoUtil.UserName = System.Configuration.ConfigurationManager.AppSettings["Usuario"];
            CorreoUtil.Password = System.Configuration.ConfigurationManager.AppSettings["Contrasena"];


            UsuarioEstado.Activo = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["AdministradorActivo"]);
            UsuarioEstado.Inactivo = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["AdministradorInactivo"]);

            EstadoAcciones.EnProceso = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["AccionProceso"]);
            EstadoAcciones.Finalizada = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["AccionFinalizada"]);


            RolUsuario.Administrador = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["RolAdministrador"]);
            RolUsuario.Trabajador = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["RolUsuario"]);
            RolUsuario.Cliente = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["RolCliente"]);

            Mensaje.UsuarioActivo = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["UsuarioActivo"]);
            Mensaje.UsuarioInactivo = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["UsuarioInactivo"]);
            Mensaje.ExisteCorreo = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["ExisteCorreo"]);
            Mensaje.UsuarioConfirmado = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["UsuarioConfirmado"]);
            Mensaje.UsuarioSinConfirmar = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["UsuarioSinConfirmar"]);
            Mensaje.CreacionCuentaTrabajador = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["CreacionCuentaTrabajador"]);
            Mensaje.ContrasenaTemporal = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["ContrasenaTemporal"]);
            Mensaje.CuentaActivada = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["CuentaActivada"]);
            Mensaje.UsuarioContrasenaIncorrecto = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["UsuarioContrasenaIncorrecto"]);
            Mensaje.InformacionActivarCuenta = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["InformacionActivarCuenta"]);
            Mensaje.ActivacionCuentaAdministrador = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["ActivacionCuentaAdministrador"]);
            Mensaje.DesactivacionCuentaAdministrador = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["DesactivacionCuentaAdministrador"]);
            Mensaje.CuentaEliminada = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["CuentaEliminada"]);
            Mensaje.CreacionCuentaCliente = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["CreacionCuentaCliente"]);
            Mensaje.RecuperarContrasena = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["RecuperarContrasena"]);

            CuotasCodigos.CuotaInferiorCodigo = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CuotaInferiorCodigo"]);
            CuotasCodigos.CuotaSuperiorCodigo = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CuotaSuperiorCodigo"]);

            ApplicationDbContext db = new ApplicationDbContext();
            CreateRoles(db);
           

           
        }


        private void CreateRoles(ApplicationDbContext db)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            if (!roleManager.RoleExists(RolUsuario.Administrador))
            {
                roleManager.Create(new IdentityRole(RolUsuario.Administrador));
            }

            if (!roleManager.RoleExists(RolUsuario.Trabajador))
            {
                roleManager.Create(new IdentityRole(RolUsuario.Trabajador));
            }

            if (!roleManager.RoleExists(RolUsuario.Cliente))
            {
                roleManager.Create(new IdentityRole(RolUsuario.Cliente));
            }
        }
    }
}

using insitum.Controllers;
using insitum.Models;
using insitum.Utiles;
using insitum.Utiles.Template;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Timers;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Diagnostics;
using ScheduledTaskExample.ScheduledTasks;

namespace insitum
{

    public class MvcApplication : System.Web.HttpApplication
    {
        private static double TimerIntervalInMilliseconds = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["TimerIntervalInMilliseconds"]);

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Constantes.TiempoCicloMinutosNotificacionEmail = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["TiempoCicloMinutosNotificacionEmail"]);
            Constantes.HoraInicioCiclo = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["HoraInicioCiclo"]);
            Constantes.MinutoInicioCiclo = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["MinutoInicioCiclo"]);


            
            Constantes.ServerReportUrl = System.Configuration.ConfigurationManager.AppSettings["ServerReportUrl"];
            Constantes.UsuarioReport = System.Configuration.ConfigurationManager.AppSettings["UsuarioReport"];
            Constantes.ContrasenaReporte = System.Configuration.ConfigurationManager.AppSettings["ContrasenaReporte"];

            Constantes.ReporteClientesPath = System.Configuration.ConfigurationManager.AppSettings["ReporteClientesPath"];
            Constantes.ReporteGestionPath = System.Configuration.ConfigurationManager.AppSettings["ReporteGestionPath"];
            Constantes.DireccionFisicahtmlCorreoTimer = System.Configuration.ConfigurationManager.AppSettings["DireccionFisicahtmlCorreoTimer"];
            Constantes.DiasNotificacion =Convert.ToInt32( System.Configuration.ConfigurationManager.AppSettings["DiasNotificacion"]);

           

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
            CorreoUtil.EmailNotificationActivity = System.Configuration.ConfigurationManager.AppSettings["EmailNotificationActivity"];


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


            JobScheduler.Start();


            ApplicationDbContext db = new ApplicationDbContext();
            CreateRoles(db);


            ////Timer timer = new Timer(TimerIntervalInMilliseconds);
            //timer.Enabled = true;
            //timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            //timer.Start();


        }

        static void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            DateTime MyScheduledRunTime = DateTime.Parse(System.Configuration.ConfigurationManager.AppSettings["TimerStartTime"]);
            DateTime CurrentSystemTime = DateTime.Now;
            DateTime LatestRunTime = MyScheduledRunTime.AddMilliseconds(TimerIntervalInMilliseconds);
            if ((CurrentSystemTime.CompareTo(MyScheduledRunTime) >= 0) && (CurrentSystemTime.CompareTo(LatestRunTime) <= 0))
            {

                // RUN YOUR PROCESSES HERE
            }
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

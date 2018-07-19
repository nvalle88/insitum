using insitum.Models;
using insitum.Utiles.Template;
using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace insitum.Utiles
{
    public class EmailJob : IJob
    {
        public void Execute(IJobExecutionContext context)
         {


            ApplicationDbContext db = new ApplicationDbContext();
            var listaacciones = db.Acciones.Where(x => x.Estado == EstadoAcciones.EnProceso).ToList();
            if (listaacciones.Count > 0)
            {
                


                var fechaActual = DateTime.Now;
                foreach (var item in listaacciones)
                {
                    var tiempoDias = item.TipoAccion.TiempoDias;
                    var fechaLimite = item.FechaInicio.AddDays(Convert.ToDouble(tiempoDias));
                    TimeSpan FechaReal = fechaLimite- fechaActual;

                    var diasDiferencia = FechaReal.Days;
                    if (diasDiferencia <= Constantes.DiasNotificacion)
                    {
                        string path = Constantes.DireccionFisicahtmlCorreoTimer;
                        string htmlData = File.ReadAllText(path);

                        htmlData = htmlData.Replace("@Identificacion", item.Proceso.ApplicationUser.Identificacion);
                        htmlData = htmlData.Replace("@NombresApellidos", item.Proceso.ApplicationUser.Nombres + "  " + item.Proceso.ApplicationUser.Apellidos);
                        htmlData = htmlData.Replace("@NIP", item.Proceso.NIP);
                        htmlData = htmlData.Replace("@DetalleProceso", item.Proceso.Detalle);
                        htmlData = htmlData.Replace("@TipoActividad", item.TipoAccion.Nombre);
                        htmlData = htmlData.Replace("@DetalleActividad", item.Detalle);
                        htmlData = htmlData.Replace("@FechaInicio", item.FechaInicio.ToLongDateString());
                        htmlData = htmlData.Replace("@FechaFin", item.FechaInicio.AddDays(Convert.ToDouble(item.TipoAccion.TiempoDias)).ToLongDateString());
                        using (var message = new MailMessage(CorreoUtil.UserName, CorreoUtil.EmailNotificationActivity))
                        {

                            message.Subject = "Actividad próxima a vencerse";
                            message.Body = htmlData;
                            message.IsBodyHtml = true;
                            using (SmtpClient client = new SmtpClient
                            {
                                EnableSsl = CorreoUtil.EnableSsl,
                                Host = CorreoUtil.SmtpServer,
                                Port = Convert.ToInt32(CorreoUtil.Port),
                                Credentials = new NetworkCredential(CorreoUtil.UserName,CorreoUtil.Password)
                            })
                            {
                                client.Send(message);
                            }
                        }

                    }
                }
            }
            db.Dispose();
        }
    }
}
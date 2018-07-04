using insitum.Models;
using insitum.Utiles;
using insitum.Utiles.Template;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace insitum.Controllers
{
    [Authorize(Roles ="Administrador")]
    public class TrabajadorController : Controller
    {
       
        // GET: Administrador
        public async Task<ActionResult> ListarTrabajador()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var idRol=db.Roles.Where(x => x.Name == RolUsuario.Trabajador).FirstOrDefault().Id;
            var lista =await db.Users.Where(x=>x.Roles.FirstOrDefault().RoleId==idRol).ToListAsync();
            db.Dispose();
            return View(lista);
        }


        private void CargarCiudades()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ViewBag.IdCiudad = new SelectList(db.Ciudades.OrderBy(x => x.Nombre), "IdCiudad", "Nombre");
        }
        public async Task<ActionResult> NuevoTrabajador()
        {
            CargarCiudades();
            ApplicationUser user = new ApplicationUser();
            return View(user);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> NuevoTrabajador(ApplicationUser applicationUser)
        {
            try
            {
                ApplicationDbContext db = new ApplicationDbContext();
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

                var user = userManager.FindByName(applicationUser.Email);
                if (user != null)
                {
                    ViewBag.IdCiudad = new SelectList(db.Ciudades.OrderBy(x => x.Nombre), "IdCiudad", "Nombre", applicationUser.IdCiudad);
                    ModelState.AddModelError("Email", Mensaje.ExisteCorreo);
                    return View(applicationUser);
                }

                applicationUser.UserName = applicationUser.Email;
                applicationUser.Estado = UsuarioEstado.Activo;
                applicationUser.EmailConfirmed = false;
                var contrasenaTmp= GenerarCodigo.Generar(CuotasCodigos.CuotaInferiorCodigo, CuotasCodigos.CuotaSuperiorCodigo);
                userManager.Create(applicationUser,Convert.ToString(contrasenaTmp));
                userManager.AddToRole(applicationUser.Id, RolUsuario.Trabajador);

                WebMail.SmtpServer = CorreoUtil.SmtpServer;
                //gmail port to send emails  
                WebMail.SmtpPort = Convert.ToInt32(CorreoUtil.Port);
                WebMail.SmtpUseDefaultCredentials = true;
                //sending emails with secure protocol  
                WebMail.EnableSsl = true;
                //EmailId used to send emails from application  
                WebMail.UserName = CorreoUtil.UserName;
                WebMail.Password = CorreoUtil.Password;

                //Sender email address.  
                WebMail.From = CorreoUtil.UserName;

                string htmlData = InfoMail.CreacionCuentaTrabajador();
                //Send email  
                WebMail.Send(to: applicationUser.Email, subject: Mensaje.CreacionCuentaTrabajador, body: "<b> "+Mensaje.ContrasenaTemporal +Convert.ToString(contrasenaTmp) +"<br/>" + htmlData, isBodyHtml: true);
                db.Dispose();
                
            }
            catch (Exception ex)
            {

                throw;
            }

            return RedirectToAction("ListarTrabajador");
        }

    }
}
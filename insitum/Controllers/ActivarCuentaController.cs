using insitum.Models;
using insitum.Utiles;
using insitum.Utiles.Template;
using insitum.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace insitum.Controllers
{
    public class ActivarCuentaController : Controller
    {
        // GET: ActivarCuenta
        public ActionResult Register()
        {
           ViewBag.Tipo = 1;
           ViewBag.Error = Mensaje.InformacionActivarCuenta;
           return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(ActivarCuentaViewModel activarCuentaViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            ApplicationDbContext db = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var user = userManager.Find(activarCuentaViewModel.Email, activarCuentaViewModel.PasswordTmp);
            if (user!=null)
            {
                user.EmailConfirmed = true;
                await userManager.UpdateAsync(user);
                await userManager.ChangePasswordAsync(user.Id, user.PasswordHash, activarCuentaViewModel.Password);
                

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

                string htmlData = InfoMail.CuentaActivada();
                //Send email  
                WebMail.Send(to: user.Email, subject: Mensaje.CuentaActivada, body:  htmlData, isBodyHtml: true);
                return RedirectToAction("Login", "Account");
            }
            ViewBag.Tipo = 2;
            ViewBag.Error = Mensaje.UsuarioContrasenaIncorrecto;
            return View();
        }
    }
}
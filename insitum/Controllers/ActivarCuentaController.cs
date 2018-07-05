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
                string htmlData = InfoMail.CuentaActivada();
                EnviarCorreo.Enviar(user.Email,Mensaje.CuentaActivada,htmlData);
                return RedirectToAction("Login", "Account");
            }
            ViewBag.Tipo = 2;
            ViewBag.Error = Mensaje.UsuarioContrasenaIncorrecto;
            return View();
        }
    }
}
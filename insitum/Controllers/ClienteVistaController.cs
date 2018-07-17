using insitum.Models;
using insitum.Utiles;
using insitum.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace insitum.Controllers
{
    [Authorize(Roles = "Cliente")]
    public class ClienteVistaController : Controller
    {

        private ApplicationUserManager _userManager;
        private ApplicationSignInManager _signInManager;

        public ClienteVistaController()
        {
        }

        public ClienteVistaController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET: ClienteVista
        public ActionResult DetalleProceso()
        {
            try
            {
                var userWithClaims = (ClaimsPrincipal)User;
                var idUsuario = userWithClaims.Claims.First(c => c.Type == Constantes.IdUsuario).Value;
                var user = UserManager.FindById(idUsuario);
                ViewBag.Identificacion = $"{user.Identificacion}";
                ViewBag.NombreApellido = $"{user.Nombres + "  " + user.Apellidos}";
                ViewBag.PhoneNumber = $"{user.PhoneNumber}";
                ViewBag.Email = $"{user.Email}";

                ViewBag.IdentificacionConyuge = $"{user.IdentificacionConyuge}";
                ViewBag.NombreApellidoConyugue = $"{user.NombresConyuge + "  " + user.ApellidosConyuge}";
                ViewBag.CorreoConyuge = $"{user.CorreoConyuge}";
                ViewBag.TelefonoConyuge = $"{user.TelefonoConyuge}";



                ApplicationDbContext db = new ApplicationDbContext();

                var listaProcesos = db.Procesos.Where(x => x.Id == idUsuario).OrderByDescending(x => x.FechaInicio).ToList();
                ViewBag.TotalProcesos = listaProcesos.Count();
                var procesoViewModel = new ProcesoViewModel { Id = idUsuario, ListaProcesos = listaProcesos };
                db.Dispose();
                return View(procesoViewModel);
            }
            catch (Exception)
            {
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                return RedirectToAction("Login", "Account");
            }
        }


        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult DetalleAcciones(int id)
        {
            try
            {
                ApplicationDbContext db = new ApplicationDbContext();
                var a = User;
                var listaacciones = db.Acciones.Where(x => x.IdProceso == id).OrderByDescending(x => x.FechaInicio).ToList();
                var userWithClaims = (ClaimsPrincipal)User;
                var idUsuario = userWithClaims.Claims.First(c => c.Type == Constantes.IdUsuario).Value;
                var proceso = db.Procesos.Where(x => x.IdProceso == id && x.Id == idUsuario).FirstOrDefault();
                if (proceso == null)
                {
                    return View("Error404");
                }
                var accionesViewModel = new AccionesViewModel { Proceso = proceso, ListaAcciones = listaacciones };
                return View(accionesViewModel);
            }
            catch (Exception)
            {
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                return RedirectToAction("Login", "Account");
            }
        }
    }
}
using insitum.Models;
using insitum.Models.Negocio;
using insitum.Utiles;
using insitum.Utiles.Template;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace insitum.Controllers
{
    [Authorize(Roles ="Trabajador")]
    public class ClienteController : Controller
    {

        private ApplicationUserManager _userManager;

        public ClienteController()
        {
        }

        public ClienteController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
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



        // GET: Cliente
        public async Task<ActionResult> ListarCliente()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var idRol = db.Roles.Where(x => x.Name == RolUsuario.Cliente).FirstOrDefault().Id;
            var lista = await db.Users.Where(x => x.Roles.FirstOrDefault().RoleId == idRol).ToListAsync();
            return View(lista);
        }


        private void CargarCiudades()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ViewBag.IdCiudad = new SelectList(db.Ciudades.OrderBy(x => x.Nombre), "IdCiudad", "Nombre");
        }
        public ActionResult NuevoCliente()
        {
            CargarCiudades();
            ApplicationUser user = new ApplicationUser();
            return View(user);
        }


        public ActionResult EditarCliente( string id)
        {
            CargarCiudades();
            var cliente = UserManager.FindById(id);
            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditarCliente(ApplicationUser applicationUser)
        {
            try
            {
                ApplicationDbContext db = new ApplicationDbContext();

                var user = UserManager.FindByName(applicationUser.Email);
                if (user!=null)
                {
                    if (user.Id != applicationUser.Id)
                    {
                        ViewBag.IdCiudad = new SelectList(db.Ciudades.OrderBy(x => x.Nombre), "IdCiudad", "Nombre", applicationUser.IdCiudad);
                        ModelState.AddModelError("Email", Mensaje.ExisteCorreo);
                        return View(applicationUser);
                    }
                }

                var clienteActualizar = UserManager.FindById(applicationUser.Id);

                clienteActualizar.IdCiudad = applicationUser.IdCiudad;
                clienteActualizar.Identificacion = applicationUser.Identificacion;
                clienteActualizar.IdentificacionConyuge = applicationUser.IdentificacionConyuge;
                clienteActualizar.Nombres = applicationUser.Nombres;
                clienteActualizar.NombresConyuge = applicationUser.NombresConyuge;
                clienteActualizar.PhoneNumber = applicationUser.PhoneNumber;
                clienteActualizar.TelefonoConyuge = applicationUser.TelefonoConyuge;
                clienteActualizar.UserName = applicationUser.Email;
                clienteActualizar.Apellidos = applicationUser.Apellidos;
                clienteActualizar.ApellidosConyuge = applicationUser.ApellidosConyuge;
                clienteActualizar.CorreoConyuge = applicationUser.CorreoConyuge;
                clienteActualizar.Email = applicationUser.Email;
                clienteActualizar.CorreoNotificacion_1 = applicationUser.CorreoNotificacion_1;
                clienteActualizar.CorreoNotificacion_2 = applicationUser.CorreoNotificacion_2;
                clienteActualizar.CorreoNotificacion_3 = applicationUser.CorreoNotificacion_3;
                clienteActualizar.CorreoNotificacion_4 = applicationUser.CorreoNotificacion_4;
                clienteActualizar.Direccion = applicationUser.Direccion;

                await UserManager.UpdateAsync(clienteActualizar);
                
                //var contrasenaTmp = GenerarCodigo.Generar(CuotasCodigos.CuotaInferiorCodigo, CuotasCodigos.CuotaSuperiorCodigo);
               
                return RedirectToAction("ListarCliente");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "");
                throw;
            }
        }




        public async Task<ActionResult> Eliminar(string id)
        {
            try
            {
                ApplicationDbContext db = new ApplicationDbContext();
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
                var user = userManager.FindById(id);
                user.Estado = UsuarioEstado.Inactivo;
                var rol = user.Roles.FirstOrDefault();
                var idUsuario = user.Id;
                var rolName = await roleManager.FindByIdAsync(rol.RoleId);
                await userManager.DeleteAsync(user);
                await userManager.RemoveFromRoleAsync(idUsuario, rolName.Name);

                string htmlData = InfoMail.CuentaEliminada();
                //Send email  
                EnviarCorreo.Enviar(user.Email, Mensaje.CuentaEliminada, htmlData);
                db.Dispose();

            }
            catch (Exception )
            {
                return View("NoEsPosibleEliminar");
            }

            return RedirectToAction("ListarCliente");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NuevoCliente(ApplicationUser applicationUser)
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
                //var contrasenaTmp = GenerarCodigo.Generar(CuotasCodigos.CuotaInferiorCodigo, CuotasCodigos.CuotaSuperiorCodigo);
                userManager.Create(applicationUser, Convert.ToString(applicationUser.Identificacion));
                userManager.AddToRole(applicationUser.Id, RolUsuario.Cliente);

                string htmlData = InfoMail.CreacionCuentaCliente();
                //Send email  
                EnviarCorreo.Enviar(applicationUser.Email, Mensaje.CreacionCuentaCliente, "<b> " + Mensaje.ContrasenaTemporal  + "</b><br/><br/><br/>" + htmlData);

            return RedirectToAction("DetalleProceso", "Proceso",new  {id=applicationUser.Id });
            }
            catch (Exception )
            {
                ModelState.AddModelError("","");
                throw;
            }
        }
    }
}
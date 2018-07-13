using insitum.Models;
using insitum.Models.Negocio;
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
using System.Web.Mvc;

namespace insitum.Controllers
{
    [Authorize(Roles ="Trabajador")]
    public class ClienteController : Controller
    {
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
                var contrasenaTmp = GenerarCodigo.Generar(CuotasCodigos.CuotaInferiorCodigo, CuotasCodigos.CuotaSuperiorCodigo);
                userManager.Create(applicationUser, Convert.ToString(contrasenaTmp));
                userManager.AddToRole(applicationUser.Id, RolUsuario.Cliente);

                string htmlData = InfoMail.CreacionCuentaCliente();
                //Send email  
                EnviarCorreo.Enviar(applicationUser.Email, Mensaje.CreacionCuentaCliente, "<b> " + Mensaje.ContrasenaTemporal + Convert.ToString(contrasenaTmp) + "</b><br/><br/><br/>" + htmlData);

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
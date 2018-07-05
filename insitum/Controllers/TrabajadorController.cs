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


        public async Task<ActionResult> ReenviarCorreo(string id)
        {
            try
            {
                ApplicationDbContext db = new ApplicationDbContext();
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                var user = userManager.FindById(id);
                var contrasenaTmp = GenerarCodigo.Generar(CuotasCodigos.CuotaInferiorCodigo, CuotasCodigos.CuotaSuperiorCodigo);
                await userManager.ChangePasswordAsync(user.Id,user.PasswordHash,Convert.ToString(contrasenaTmp));
                user.EmailConfirmed = false;
                await userManager.UpdateAsync(user);
                
                string htmlData = InfoMail.CreacionCuentaTrabajador();
                //Send email  
                EnviarCorreo.Enviar(user.Email, Mensaje.CreacionCuentaTrabajador, "<b> " + Mensaje.ContrasenaTemporal + Convert.ToString(contrasenaTmp) + "</b><br/><br/><br/>" + htmlData);
                db.Dispose();

            }
            catch (Exception ex)
            {

                throw;
            }

            return RedirectToAction("ListarTrabajador");
        }

        public async Task<ActionResult> Activar(string id)
        {
            try
            {
                ApplicationDbContext db = new ApplicationDbContext();
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                var user = userManager.FindById(id);
                user.Estado = UsuarioEstado.Activo;
                await userManager.UpdateAsync(user);
                string htmlData = InfoMail.ActivacionCuentaAdministrador();
                //Send email  
                EnviarCorreo.Enviar(user.Email, Mensaje.ActivacionCuentaAdministrador, htmlData);
                db.Dispose();

            }
            catch (Exception ex)
            {

                throw;
            }

            return RedirectToAction("ListarTrabajador");
        }

        public async Task<ActionResult> Desactivar(string id)
        {
            try
            {
                ApplicationDbContext db = new ApplicationDbContext();
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                var user = userManager.FindById(id);
                user.Estado = UsuarioEstado.Inactivo;
                await userManager.UpdateAsync(user);
                string htmlData = InfoMail.DesactivacionCuentaAdministrador();
                //Send email  
                EnviarCorreo.Enviar(user.Email, Mensaje.DesactivacionCuentaAdministrador, htmlData);
                db.Dispose();

            }
            catch (Exception ex)
            {

                throw;
            }

            return RedirectToAction("ListarTrabajador");
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
                var rol= user.Roles.FirstOrDefault();
                var rolName= await roleManager.FindByIdAsync(rol.RoleId);
                await userManager.RemoveFromRoleAsync(user.Id,rolName.Name);
                await userManager.DeleteAsync(user);
                string htmlData = InfoMail.CuentaEliminada();
                //Send email  
                EnviarCorreo.Enviar(user.Email, Mensaje.CuentaEliminada, htmlData);
                db.Dispose();

            }
            catch (Exception ex)
            {

                throw;
            }

            return RedirectToAction("ListarTrabajador");
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

                string htmlData = InfoMail.CreacionCuentaTrabajador();
                //Send email  
                EnviarCorreo.Enviar(applicationUser.Email,Mensaje.CreacionCuentaTrabajador, "<b> " + Mensaje.ContrasenaTemporal + Convert.ToString(contrasenaTmp) + "</b><br/><br/><br/>" + htmlData);
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
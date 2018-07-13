using insitum.Models;
using insitum.Models.Negocio;
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
using System.Web.Mvc;

namespace insitum.Controllers
{
    [Authorize(Roles = "Trabajador")]
    public class ProcesoController : Controller
    {
        private ApplicationUserManager _userManager;

        public ProcesoController()
        {
        }

        public ProcesoController(ApplicationUserManager userManager)
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

        public ActionResult DetalleProceso(string id)
        {
            var user= UserManager.FindById(id);
            ViewBag.Identificacion = $"{user.Identificacion}";
            ViewBag.NombreApellido = $"{user.Nombres + "  "+user.Apellidos}";
            ViewBag.PhoneNumber = $"{user.PhoneNumber}";
            ViewBag.Email = $"{user.Email}";

            ViewBag.IdentificacionConyuge = $"{user.IdentificacionConyuge}";
            ViewBag.NombreApellidoConyugue = $"{user.NombresConyuge + "  " + user.ApellidosConyuge}";
            ViewBag.CorreoConyuge = $"{user.CorreoConyuge}";
            ViewBag.TelefonoConyuge = $"{user.TelefonoConyuge}";

            

            ApplicationDbContext db = new ApplicationDbContext();
            var listaProcesos = db.Procesos.Where(x=>x.Id==id).OrderByDescending(x=>x.FechaInicio).ToList();
            ViewBag.TotalProcesos = listaProcesos.Count();
            var procesoViewModel = new ProcesoViewModel {Id=id, ListaProcesos = listaProcesos };
            db.Dispose();
            return View(procesoViewModel);
        }

        public ActionResult EditarProceso(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var proceso = db.Procesos.Where(x => x.IdProceso == id).FirstOrDefault();
            return View(proceso);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditarProceso(Proceso proceso)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var existeNIP = db.Procesos.Where(x => x.IdProceso != proceso.IdProceso && x.NIP==proceso.NIP ).FirstOrDefault();
            if (existeNIP!=null)
            {
                ModelState.AddModelError("NIP", "El NIP ya está asignado otro proceso, por favor intente con otro NIP");
                return View(proceso);
            }

            var procesoActualizar = db.Procesos.Where(x => x.IdProceso == proceso.IdProceso ).FirstOrDefault();

            procesoActualizar.NIP = proceso.NIP;
            procesoActualizar.Detalle = proceso.Detalle;
            procesoActualizar.FechaInicio = proceso.FechaInicio;
            db.Entry(procesoActualizar).State = System.Data.Entity.EntityState.Modified;
            await db.SaveChangesAsync();
            db.Dispose();
            return RedirectToAction("DetalleProceso",new { id=procesoActualizar.Id});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DetalleProceso(ProcesoViewModel procesoView)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            if (!ModelState.IsValid)
            {
               
                var user = UserManager.FindById(procesoView.Id);
                ViewBag.Identificacion = $"{user.Identificacion}";
                ViewBag.NombreApellido = $"{user.Nombres + "  " + user.Apellidos}";
                ViewBag.PhoneNumber = $"{user.PhoneNumber}";
                ViewBag.Email = $"{user.Email}";

                ViewBag.IdentificacionConyuge = $"{user.IdentificacionConyuge}";
                ViewBag.NombreApellidoConyugue = $"{user.NombresConyuge + "  " + user.ApellidosConyuge}";
                ViewBag.CorreoConyuge = $"{user.CorreoConyuge}";
                ViewBag.TelefonoConyuge = $"{user.TelefonoConyuge}";


                var listaProcesos = db.Procesos.Where(x => x.Id == procesoView.Id).OrderByDescending(x => x.FechaInicio).ToList();
                ViewBag.TotalProcesos = listaProcesos.Count();
                var procesoViewModel = new ProcesoViewModel { Id = procesoView.Id, ListaProcesos = listaProcesos, Detalle = procesoView.Detalle, FechaInicio = procesoView.FechaInicio };
                return View(procesoViewModel);
            }


            var existeNIP = db.Procesos.Where(x => x.NIP == procesoView.NIP).FirstOrDefault();


            if (existeNIP!=null)
            {
                ModelState.AddModelError("NIP", "El NIP ya está asignado otro proceso, por favor intente con otro NIP");
                var user = UserManager.FindById(procesoView.Id);
                ViewBag.Identificacion = $"{user.Identificacion}";
                ViewBag.NombreApellido = $"{user.Nombres + "  " + user.Apellidos}";
                ViewBag.PhoneNumber = $"{user.PhoneNumber}";
                ViewBag.Email = $"{user.Email}";

                ViewBag.IdentificacionConyuge = $"{user.IdentificacionConyuge}";
                ViewBag.NombreApellidoConyugue = $"{user.NombresConyuge + "  " + user.ApellidosConyuge}";
                ViewBag.CorreoConyuge = $"{user.CorreoConyuge}";
                ViewBag.TelefonoConyuge = $"{user.TelefonoConyuge}";

               
                var listaProcesos = db.Procesos.Where(x => x.Id == procesoView.Id).OrderByDescending(x => x.FechaInicio).ToList();
                ViewBag.TotalProcesos = listaProcesos.Count();
                var procesoViewModel = new ProcesoViewModel { Id = procesoView.Id, ListaProcesos = listaProcesos,Detalle=procesoView.Detalle,FechaInicio=procesoView.FechaInicio };
                return View(procesoViewModel);
            }
            var proceso = new Proceso { Id = procesoView.Id, Detalle = procesoView.Detalle, FechaInicio = procesoView.FechaInicio, NIP = procesoView.NIP };
            db.Procesos.Add(proceso);

            await db.SaveChangesAsync();

                var usuario = db.Users.Where(x => x.Id == procesoView.Id).FirstOrDefault();
                string htmlData = InfoMail.CreacionProceso();
                //Send email  
                EnviarCorreo.Enviar(usuario.Email,"Se ha creado un proceso",htmlData);
           
            return RedirectToAction("DetalleProceso",new {id= procesoView.Id});
        }


        public ActionResult EditarAccion(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ViewBag.IdTipoAccion = new SelectList(db.TipoAcciones.OrderBy(x => x.Nombre), "IdTipoAccion", "Nombre");
            var accion= db.Acciones.Where(x=>x.IdAccion==id).FirstOrDefault();
            return View(accion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditarAccion(Accion accion)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var accionActualizar = db.Acciones.Where(x => x.IdAccion == accion.IdAccion).FirstOrDefault();
            accionActualizar.IdProceso = accion.Proceso.IdProceso;
            accionActualizar.IdTipoAccion = accion.IdTipoAccion;
            accionActualizar.FechaInicio = accion.FechaInicio;
            accionActualizar.Detalle = accion.Detalle;
            db.Entry(accionActualizar).State=System.Data.Entity.EntityState.Modified;
            await db.SaveChangesAsync();
            db.Dispose();
            return RedirectToAction("DetalleAcciones", new { id = accion.Proceso.IdProceso });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> InsertarAccion(AccionesViewModel accionView)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var accionProceso = new Accion { Detalle = accionView.Detalle, FechaInicio = accionView.FechaInicio, IdProceso = accionView.Proceso.IdProceso, IdTipoAccion = accionView.IdTipoAccion};
           
            db.Acciones.Add(accionProceso);
            await db.SaveChangesAsync();
            var proceso = db.Procesos.Where(x => x.IdProceso == accionView.Proceso.IdProceso).FirstOrDefault();
            var enviar = db.Users.Where(x => x.Id == proceso.Id).FirstOrDefault();
            string htmlData = InfoMail.CreacionAccion();
            //Send email  
            EnviarCorreo.Enviar(enviar.Email, "Se ha creado una acción",htmlData);

            db.Dispose();

            return RedirectToAction("DetalleAcciones",new { id=accionProceso.IdProceso});
        }


        public async Task<ActionResult> EliminarAccion(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var accionEliminar = db.Acciones.Where(x => x.IdAccion == id).FirstOrDefault();
            var idProceso = accionEliminar.IdProceso;
            db.Acciones.Remove(accionEliminar);
            await db.SaveChangesAsync();
            db.Dispose();
            return RedirectToAction("DetalleAcciones", new { id = idProceso });
        }

        public ActionResult BuscarCliente()
        {
            var user = new ApplicationUser();
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BuscarCliente(ApplicationUser applicationUser)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var usuario = db.Users.Where(x => x.Identificacion == applicationUser.Identificacion).FirstOrDefault();
            if (usuario!=null)
            {
                return RedirectToAction("DetalleProceso", new { id = usuario.Id }); ;
            }

            return RedirectToAction("ClienteNoEncontrado");
           
        }

        public ActionResult ClienteNoEncontrado()
        {
            return View();
        }

        public async Task<ActionResult> EliminarProceso(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var procesoEliminar= db.Procesos.Where(x=>x.IdProceso==id).FirstOrDefault();
            var listaProcesos = db.Procesos.Where(x => x.Id == procesoEliminar.Id).OrderByDescending(x => x.FechaInicio).ToList();
            var procesoViewModel = new ProcesoViewModel { Id = procesoEliminar.Id, ListaProcesos = listaProcesos };
            db.Procesos.Remove(procesoEliminar);
            await db.SaveChangesAsync();
            db.Dispose();
           
            return RedirectToAction("DetalleProceso",new {id=procesoViewModel.Id });
        }

        public  ActionResult DetalleAcciones(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var listaacciones = db.Acciones.Where(x => x.IdProceso == id).OrderByDescending(x=>x.FechaInicio).ToList();
            var proceso = db.Procesos.Where(x => x.IdProceso == id).FirstOrDefault();
            ViewBag.IdTipoAccion = new SelectList(db.TipoAcciones.OrderBy(x=>x.Nombre), "IdTipoAccion", "Nombre");
            var accionesViewModel = new AccionesViewModel { Proceso = proceso, ListaAcciones = listaacciones };
            return View(accionesViewModel);
        }
    }
}
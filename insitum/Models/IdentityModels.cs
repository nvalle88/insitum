using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using insitum.Models.Negocio;
using insitum.Utiles;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace insitum.Models
{
    // Para agregar datos de perfil del usuario, agregue más propiedades a su clase ApplicationUser. Visite https://go.microsoft.com/fwlink/?LinkID=317594 para obtener más información.
    public class ApplicationUser : IdentityUser
    {
        [StringLength(13, ErrorMessage = "The field {0} can contain maximun {1} and minimum {2} characters")]
        [Display(Name = "Identificación")]
        public string Identificacion { get; set; }
        
        [StringLength(100, ErrorMessage = "The field {0} can contain maximun {1} and minimum {2} characters")]
        [Display(Name = "Nombres")]
        public string Nombres { get; set; }

        [StringLength(100, ErrorMessage = "The field {0} can contain maximun {1} and minimum {2} characters")]
        [Display(Name = "Apellidos")]
        public string Apellidos { get; set; }

        [StringLength(500, ErrorMessage = "The field {0} can contain maximun {1} and minimum {2} characters")]
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Correo")]
        public string Correo { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Correo notificación")]
        public string CorreoNotificacion_1 { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Correo notificación")]
        public string CorreoNotificacion_2 { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Correo notificación")]
        public string CorreoNotificacion_3 { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Correo notificación")]
        public string CorreoNotificacion_4 { get; set; }

        [StringLength(20, ErrorMessage = "The field {0} can contain maximun {1} and minimum {2} characters")]
        [Display(Name = "Teléfono")]
        public string TelefonoContacto { get; set; }

        //[Required(ErrorMessage = "You must enter a {0}")]
        [StringLength(13, ErrorMessage = "The field {0} can contain maximun {1} and minimum {2} characters")]
        [Display(Name = "Identificación del conyugue")]
        public string IdentificacionConyuge { get; set; }

        //[Required(ErrorMessage = "You must enter a {0}")]
        [StringLength(100, ErrorMessage = "The field {0} can contain maximun {1} and minimum {2} characters")]
        [Display(Name = "Nombres del conyugue")]
        public string NombresConyuge { get; set; }

//        [Required(ErrorMessage = "You must enter a {0}")]
        [StringLength(100, ErrorMessage = "The field {0} can contain maximun {1} and minimum {2} characters")]
        [Display(Name = "Apellidos del conyugue")]
        public string ApellidosConyuge { get; set; }

        [DataType(DataType.EmailAddress)]
        //[Index("EMailIndex", IsUnique = true,Order =2)]
        [Display(Name = "Correo del conyugue")]
        public string CorreoConyuge { get; set; }

  //      [Required(ErrorMessage = "You must enter a {0}")]
        [StringLength(20, ErrorMessage = "The field {0} can contain maximun {1} and minimum {2} characters")]
        [Display(Name = "Teléfono del conyugue")]
        public string TelefonoConyuge { get; set; }

        [Display(Name = "Teléfono del conyugue")]
        public bool Estado { get; set; }

        public virtual List<Proceso> Procesos { get; set; }

        public int IdCiudad { get; set; }
        public virtual Ciudad Ciudades { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Tenga en cuenta que el valor de authenticationType debe coincidir con el definido en CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            ApplicationDbContext db = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var usuario= userManager.FindByEmail(UserName);
            
            userIdentity.AddClaim(new Claim(Constantes.IdUsuario,usuario.Id));
            // Agregar aquí notificaciones personalizadas de usuario
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<Accion> Acciones { get; set; }
        public System.Data.Entity.DbSet<Ciudad> Ciudades { get; set; }
        public System.Data.Entity.DbSet<Proceso> Procesos { get; set; }
        public System.Data.Entity.DbSet<Provincia> Provincias { get; set; }
        public System.Data.Entity.DbSet<TipoAccion> TipoAcciones { get; set; }
    }
}
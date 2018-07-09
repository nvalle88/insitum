using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace insitum.ViewModel
{
    public class RecuperarContrasenaRequest
    {
        [EmailAddress]
        [Display(Name = "Ingrese su correo electrónico")]
        public string Email { get; set; }

        [Display(Name = "Ingrese el código")]
        public string Codigo { get; set; }

        [Display(Name = "Ingrese la nueva contraseña")]
        public string Contrasena { get; set; }

        [Display(Name = "Ingrese la confirmación de la nueva contraseña")]
        public string ConfirmarContrasena { get; set; }
    }
}
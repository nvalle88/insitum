using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace insitum.Models.Negocio
{
    public class TipoAccion
    {
        [Key]
        public int IdTipoAccion { get; set; }

        [Required(ErrorMessage = "You must enter a {0}")]
        [StringLength(100, ErrorMessage = "The field {0} can contain maximun {1} and minimum {2} characters", MinimumLength = 3)]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Display(Name = "Tiempo en días")]
        [Range(1, int.MaxValue, ErrorMessage = "The field {0} can take values between {1} and {2}")]
        public int TiempoDias { get; set; }

        public virtual List<Accion> Acciones { get; set; }
    }
}
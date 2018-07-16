using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace insitum.Models.Negocio
{
    public class Accion
    {
        [Key]
        public int IdAccion { get; set; }

        [Required(ErrorMessage = "You must enter a {0}")]
        [StringLength(8000, ErrorMessage = "The field {0} can contain maximun {1} and minimum {2} characters")]
        [Display(Name = "Detalle")]
        public string Detalle { get; set; }

        [Display(Name = "Fecha de inicio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaInicio { get; set; }

        [Display(Name = "Fecha de inicio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaFin { get; set; }

        [StringLength(8000, ErrorMessage = "The field {0} can contain maximun {1} and minimum {2} characters")]
        [Display(Name = "DetalleFin")]
        public string  DetalleFin { get; set; }

        [Display(Name = "Estado")]
        public int? Estado { get; set; }

        [Display(Name = "Proceso")]
        public int IdProceso { get; set; }
        public virtual Proceso Proceso { get; set; }

        [Display(Name = "Tipo de acción")]
        public int IdTipoAccion { get; set; }
        public virtual TipoAccion TipoAccion { get; set; }


    }
}
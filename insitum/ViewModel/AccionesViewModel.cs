using insitum.Models.Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace insitum.ViewModel
{
    public class AccionesViewModel
    {
        public int IdProceso { get; set; }

        [Required(ErrorMessage = "You must enter a {0}")]
        [StringLength(1000, ErrorMessage = "The field {0} can contain maximun {1} and minimum {2} characters", MinimumLength = 3)]
        [Display(Name = "Detalle")]
        public string Detalle { get; set; }

        [Display(Name = "Fecha de inicio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaInicio { get; set; }

        [Display(Name = "Tipo de acción")]
        public int IdTipoAccion { get; set; }

        public Proceso Proceso { get; set; }

        public List<Accion> ListaAcciones { get; set; }
    }
}
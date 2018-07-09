using insitum.Models.Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace insitum.Models
{
    public class ProcesoViewModel
    {
        public int IdProceso { get; set; }

        [Required(ErrorMessage = "Debe ingresar el {0}")]
        [StringLength(1000, ErrorMessage = "The field {0} can contain maximun {1} and minimum {2} characters", MinimumLength = 3)]
        [Display(Name = "Detalle")]
        public string Detalle { get; set; }

        [Display(Name = "Fecha de inicio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaInicio { get; set; }

        [Required(ErrorMessage = "Debe ingresar {0}")]
        [StringLength(20, ErrorMessage = "The field {0} can contain maximun {1} and minimum {2} characters", MinimumLength = 3)]
        [Display(Name = "NIP")]
        public string NIP { get; set; }

        [Display(Name = "Cliente")]
        [StringLength(128, ErrorMessage = "The field {0} can contain maximun {1} and minimum {2} characters", MinimumLength = 3)]
        public string Id { get; set; }

        [NotMapped]
        public List<Proceso> ListaProcesos { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace insitum.Models.Negocio
{
    public class Provincia
    {
        [Key]
        public int IdProvincia { get; set; }

        [Required(ErrorMessage = "You must enter a {0}")]
        [StringLength(100, ErrorMessage = "The field {0} can contain maximun {1} and minimum {2} characters", MinimumLength = 3)]
        [Display(Name = "Provincia")]
        public string Nombre { get; set; }

        public virtual List<Ciudad> Ciudades { get; set; }
    }
}
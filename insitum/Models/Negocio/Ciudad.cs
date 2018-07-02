using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace insitum.Models.Negocio
{
    public class Ciudad
    {
        [Key]
        public int IdCiudad { get; set; }

        [Required(ErrorMessage = "You must enter a {0}")]
        [StringLength(100, ErrorMessage = "The field {0} can contain maximun {1} and minimum {2} characters", MinimumLength = 3)]
        [Display(Name = "Ciudad")]
        public string Nombre { get; set; }

        [Display(Name = "Provincia")]
        [Range(1, int.MaxValue, ErrorMessage = "The field {0} can take values between {1} and {2}")]
        public int IdProvincia { get; set; }
        public virtual Provincia Provincia { get; set; }

        public virtual List<ApplicationUser> ApplicationUsers { get; set; }
    }
}
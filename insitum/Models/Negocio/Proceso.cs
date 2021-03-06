﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace insitum.Models.Negocio
{
    public class Proceso
    {
        [Key]
        public int IdProceso { get; set; }

        [Required(ErrorMessage = "You must enter a {0}")]
        [StringLength(8000, ErrorMessage = "The field {0} can contain maximun {1} and minimum {2} characters", MinimumLength = 3)]
        [Display(Name = "Detalle")]
        public string Detalle { get; set; }

        [Display(Name = "Fecha de inicio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = false)]
        public DateTime FechaInicio { get; set; }

        [Required(ErrorMessage = "You must enter a {0}")]
        [StringLength(20, ErrorMessage = "The field {0} can contain maximun {1} and minimum {2} characters", MinimumLength = 3)]
        [Display(Name = "NIP")]
        public string NIP { get; set; }

        [Display(Name = "Cliente")]
        [StringLength(128, ErrorMessage = "The field {0} can contain maximun {1} and minimum {2} characters", MinimumLength = 3)]
        public string Id { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual List<Accion> Acciones { get; set; }
    }
}
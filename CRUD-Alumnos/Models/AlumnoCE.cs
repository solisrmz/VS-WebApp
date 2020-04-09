using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRUD_Alumnos.Models
{
    public class AlumnoCE{
        [Required]
        [Display (Name = "Ingresa nombres")]
        public string Nombres { get; set; }

        [Required]
        [Display(Name = "Ingresa apellidos")]
        public string Apellidos { get; set; }

        [Required]
        [Display(Name = "Ingresa edad")]
        public int Edad { get; set; }

        [Required]
        [Display(Name = "Ingresa el sexo")]
        public string Sexo { get; set; }
    }

    [MetadataType(typeof(AlumnoCE))]

    public partial class Alumno
    {
        public String NombreCompleto { get { return Nombres + " " + Apellidos; } }

    }
}
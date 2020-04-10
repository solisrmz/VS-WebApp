using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRUD_Alumnos.Models
{
    public class AlumnoCE{

        //Para poder hacer las validaciones de los campos del formulario
        public int id { get; set; }

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

        [Required]
        [Display(Name = "Ingresa la ciudad")]
        public int CodCiudad { get; set; }

        public string NombreCiudad { get; set; }

        public System.DateTime fechaRegistro { get; set; }

        public String NombreCompleto { get { return Nombres + " " + Apellidos; } }
    }

    //Clase parcial con la cual puedo dar formato a campos de la BD
    [MetadataType(typeof(AlumnoCE))]
    public partial class Alumno
    {
        public String NombreCompleto { get { return Nombres + " " + Apellidos; } }
        public string NombreCiudad { get; set; }
    }
}
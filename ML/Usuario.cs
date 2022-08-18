using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace ML
{
    public class Usuario
    {
        public int IdUsuario { get; set; }

       // [Required(ErrorMessage ="Ingrese el nombre")]
        public string? Nombre { get; set; }

//        [Required(ErrorMessage ="Ingresa el apellido paterno")]
  //      [Display(Name = "Apellido Paterno")]
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }

 //    [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        public ML.Rol? Rol { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Sexo { get; set; }
      //  [Required]
  //      [StringLength(12)]
        public string? Telefono { get; set; }
    //    [Required]
    //    [StringLength(12)]
        public string? Celular { get; set; }
        public DateTime? FechaNacimiento { get; set; }
  //      [Required]
 //       [StringLength(18)]
        public string? CURP { get; set; }
        public string? Imagen { get; set; }
        public ML.Direccion? Direccion { get; set; }

        public bool Status { get; set; }

        public List<object>? Usuarios { get; set; }



    }
}

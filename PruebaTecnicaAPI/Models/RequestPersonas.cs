using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaAPI.Models
{
    public class RequestPersonas
    {
        public int? idPersona { get; set; }
        [Required]
        public string Nombres { get; set; }
        [Required]
        public string Apellidos { get; set; }
        [Required]
        public string Identificacion { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string TipoIdentificacion { get; set; }
        [Required]
        public string Usuario { get; set; }
        [Required]
        public string Pass { get; set; }
    }
}

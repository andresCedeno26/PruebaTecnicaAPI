using System.Text.Json.Serialization;

namespace PruebaTecnicaAPI.Models
{
    public class Login
    {
        public string Usuario { get; set; }
        public string Pass { get; set; }
        [JsonIgnore]
        public string Jwt { get; set; }
    }
}

using System.Data;

namespace PruebaTecnicaAPI.Models
{
    public class LoginResponse
    {
        public DataTable Resultado { get; set; }
        public string ConexionName { get; set; }
    }
}

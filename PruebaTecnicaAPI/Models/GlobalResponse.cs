using System.Data;

namespace PruebaTecnicaAPI.Models
{
    public class GlobalResponse
    {
        public int idError { get; set; }
        public string Mensaje { get; set; }
        public DataTable Resultado { get; set; }
        public string ConexionName { get; set; }
    }
}

using System.Data;
using System.Data.SqlClient;
using PruebaTecnicaAPI.Models;

namespace PruebaTecnicaAPI.Conexion
{
    public class Sql
    {
        private SqlConnection connection = null;
        public Sql(string connectionString)
        {
            connection = new SqlConnection(connectionString);
        }
        public GlobalResponse ExecuteProcedureDynamic(string nameProcedure, Dictionary<string, string> parameters)
        {
            try
            {
                if (string.IsNullOrEmpty(nameProcedure) || parameters == null)
                    throw new ArgumentException("El nombre del procedimiento y los parámetros no pueden ser nulos o vacíos.");

                var global = new GlobalResponse();
                SqlCommand command = new SqlCommand(nameProcedure, connection);
                command.CommandTimeout = 600;
                command.CommandType = CommandType.StoredProcedure;

                foreach (var item in parameters)
                {
                    command.Parameters.AddWithValue(item.Key, item.Value);
                }

                command.Parameters.Add("@idError", SqlDbType.Int, int.MaxValue).Direction = ParameterDirection.Output;
                command.Parameters.Add("@Mensaje", SqlDbType.VarChar, 250).Direction = ParameterDirection.Output;
                command.Connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        var dataTable = new DataTable();
                        dataTable.Load(reader);
                        global.Resultado = dataTable;
                    }
                }

                global.idError = (int)command.Parameters["@idError"].Value;
                global.Mensaje = command.Parameters["@Mensaje"].Value.ToString();

                return global;
            }
            catch (Exception ex)
            {
                connection.Close();
                throw new Exception($"{ex.Message} {ex.InnerException?.Message} {ex.InnerException?.InnerException?.Message}");
            }
            finally
            {
                connection.Close();
            }
        }
    }
}

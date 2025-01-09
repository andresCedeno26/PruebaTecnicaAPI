using System.Net;
using System.Reflection;
using System.Security.Cryptography.Xml;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PruebaTecnicaAPI.Conexion;
using PruebaTecnicaAPI.Models;
using PruebaTecnicaAPI.Util;
using PruebaTecnicaDLLNuGet;

namespace PruebaTecnicaAPI.Controllers
{
    [Route("[controller]/")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<LoginController> _logger;
        private readonly IHttpContextAccessor _contextAccessor;

        public LoginController(IConfiguration configuration, ILogger<LoginController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _logger = logger;
            _contextAccessor = httpContextAccessor;
        }

        [HttpPost("auth")]
        public IActionResult Login(Login login)
        {
            try
            {
                var request = _contextAccessor.HttpContext.Request;
                var conexion = "PT";

                var sql = new Sql(_configuration.GetConnectionString(conexion));

                login.Pass = Class1.EncryptPassword(login.Pass);


                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("@iJson", JsonConvert.SerializeObject(login));

                var response = sql.ExecuteProcedureDynamic("pValidaLogin", parameters);

                if (response.idError > 0)
                {
                    _logger.LogInformation("Error al iniciar sesión.");
                    return base.NotFound(new { message = response.Mensaje, errorCode = HttpStatusCode.BadRequest });
                }

                response.ConexionName = conexion;

                var loginResponse = new
                {
                    jwt = Jwt.GenerateJwtToken(_configuration["TokenKey"], response),
                    conexionName = conexion
                };

                _logger.LogInformation("Sesión iniciada con éxito.", JsonConvert.SerializeObject(loginResponse));
                return Content(JsonConvert.SerializeObject(loginResponse), "application/json");
            }
            catch (Exception ex)
            {
                string message = $"Error en {MethodBase.GetCurrentMethod().Name} {ex.Message} {ex.InnerException?.Message} {ex.InnerException?.InnerException?.Message}";
                _logger.LogError(message);
                return base.BadRequest(new { message = message, errorCode = HttpStatusCode.BadRequest });
            }
        }
    }
}

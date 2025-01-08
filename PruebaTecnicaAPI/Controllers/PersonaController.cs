using Microsoft.AspNetCore.Authorization;
using System.Net;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using PruebaTecnicaAPI.Conexion;
using PruebaTecnicaAPI.Models;
using PruebaTecnicaAPI.Util;

namespace PruebaTecnicaAPI.Controllers
{
    [Route("[controller]/")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<PersonasController> _logger;
        private readonly IHttpContextAccessor _contextAccessor;

        public PersonasController(IConfiguration configuration, ILogger<PersonasController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _logger = logger;
            _contextAccessor = httpContextAccessor;
        }

        [HttpGet("read")]
        [Authorize]
        public IActionResult GetPersonas()
        {
            try
            {
                var request = _contextAccessor.HttpContext.Request;
                Request.Headers.TryGetValue("Authorization", out StringValues headerValue);
                //Valido token
                var token = Jwt.ReadJwtToken(headerValue, _configuration["TokenKey"], new LoginResponse());
                if (token == null)
                {
                    _logger.LogError("Token inválido al consultar Personas");
                    return base.BadRequest(new { message = "Token inválido", errorCode = HttpStatusCode.BadRequest });
                }
                Dictionary<string, string> parameters = new Dictionary<string, string>();

                var sql = new Sql(_configuration.GetConnectionString(token.ConexionName));

                var response = sql.ExecuteProcedureDynamic("pConsultarPersonas", parameters);

                if (response.idError > 0)
                {
                    _logger.LogInformation("Error al consultar personas.");
                    return base.NotFound(new { message = response.Mensaje, errorCode = HttpStatusCode.BadRequest });
                }

                _logger.LogInformation("Consulta de personas realizadas con éxito.");
                return Content(JsonConvert.SerializeObject(response.Resultado), "application/json");

            }
            catch (Exception ex)
            {
                string message = $"Error en {MethodBase.GetCurrentMethod().Name} {ex.Message} {ex.InnerException?.Message} {ex.InnerException?.InnerException?.Message}";
                _logger.LogError(message);
                return base.BadRequest(new { message = message, errorCode = HttpStatusCode.BadRequest });
            }
        }

        [HttpPost("create")]
        [Authorize]
        public IActionResult PostPersona(RequestPersonas requestPersonas)
        {
            try
            {
                var request = _contextAccessor.HttpContext.Request;
                Request.Headers.TryGetValue("Authorization", out StringValues headerValue);
                //Valido token
                var token = Jwt.ReadJwtToken(headerValue, _configuration["TokenKey"], new LoginResponse());
                if (token == null)
                {
                    _logger.LogError("Token inválido al consultar la persona");
                    return base.BadRequest(new { message = "Token inválido", errorCode = HttpStatusCode.BadRequest });
                }
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("@iJson", JsonConvert.SerializeObject(requestPersonas));

                var sql = new Sql(_configuration.GetConnectionString(token.ConexionName));

                var response = sql.ExecuteProcedureDynamic("pCrearPersona", parameters);

                if (response.idError > 0)
                {
                    _logger.LogInformation("Error al crear persona.");
                    return base.NotFound(new { message = response.Mensaje, errorCode = HttpStatusCode.BadRequest });
                }

                _logger.LogInformation("Creación de persona realizadas con éxito.");
                return Content(JsonConvert.SerializeObject(response.Resultado), "application/json");

            }
            catch (Exception ex)
            {
                string message = $"Error en {MethodBase.GetCurrentMethod().Name} {ex.Message} {ex.InnerException?.Message} {ex.InnerException?.InnerException?.Message}";
                _logger.LogError(message);
                return base.BadRequest(new { message = message, errorCode = HttpStatusCode.BadRequest });
            }
        }

        [HttpPost("update")]
        [Authorize]
        public IActionResult PutPersona(RequestPersonas requestPersonas)
        {
            try
            {
                var request = _contextAccessor.HttpContext.Request;
                Request.Headers.TryGetValue("Authorization", out StringValues headerValue);
                //Valido token
                var token = Jwt.ReadJwtToken(headerValue, _configuration["TokenKey"], new LoginResponse());
                if (token == null)
                {
                    _logger.LogError("Token inválido al consultar la persona");
                    return base.BadRequest(new { message = "Token inválido", errorCode = HttpStatusCode.BadRequest });
                }
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("@iJson", JsonConvert.SerializeObject(requestPersonas));

                var sql = new Sql(_configuration.GetConnectionString(token.ConexionName));

                var response = sql.ExecuteProcedureDynamic("pModificarPersona", parameters);

                if (response.idError > 0)
                {
                    _logger.LogInformation("Error al modificar persona.");
                    return base.NotFound(new { message = response.Mensaje, errorCode = HttpStatusCode.BadRequest });
                }

                _logger.LogInformation("Modificación de persona realizadas con éxito.");
                return Content(JsonConvert.SerializeObject(response.Resultado), "application/json");

            }
            catch (Exception ex)
            {
                string message = $"Error en {MethodBase.GetCurrentMethod().Name} {ex.Message} {ex.InnerException?.Message} {ex.InnerException?.InnerException?.Message}";
                _logger.LogError(message);
                return base.BadRequest(new { message = message, errorCode = HttpStatusCode.BadRequest });
            }
        }

        [HttpDelete("delete/{idPersona}")]
        [Authorize]
        public IActionResult DeletePersona(int idPersona)
        {
            try
            {
                var request = _contextAccessor.HttpContext.Request;
                Request.Headers.TryGetValue("Authorization", out StringValues headerValue);
                //Valido token
                var token = Jwt.ReadJwtToken(headerValue, _configuration["TokenKey"], new LoginResponse());
                if (token == null)
                {
                    _logger.LogError("Token inválido al consultar la persona");
                    return base.BadRequest(new { message = "Token inválido", errorCode = HttpStatusCode.BadRequest });
                }
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("@iJson", JsonConvert.SerializeObject(new
                {
                    idPersona = idPersona
                }));

                var sql = new Sql(_configuration.GetConnectionString(token.ConexionName));

                var response = sql.ExecuteProcedureDynamic("pAnulaPersona", parameters);

                if (response.idError > 0)
                {
                    _logger.LogInformation("Error al modificar persona.");
                    return base.NotFound(new { message = response.Mensaje, errorCode = HttpStatusCode.BadRequest });
                }

                _logger.LogInformation("Modificación de persona realizadas con éxito.");
                return Content(JsonConvert.SerializeObject(response.Resultado), "application/json");

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

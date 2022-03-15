using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebApi.Prueba.Backend.Dtos;

namespace WebApi.Prueba.Backend.Controllers.v1
{
    [ApiController]
    [Route("v1/Asteroide")]
    public class AsteroideController : Controller
    {
        private readonly ILogger<AsteroideController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        public AsteroideController(ILogger<AsteroideController> logger,
                                   IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("GetTop3")]
        [ProducesResponseType(typeof(IEnumerable<AsteroideDto>),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> GetTop3Async(string planet)
        {
            try
            {
                if (string.IsNullOrEmpty(planet))
                    return StatusCode(StatusCodes.Status400BadRequest, "Tiene que rellenar un nombre de un planeta");
                
                IEnumerable<AsteroideDto> asteroides = await ObtenerAsteroidesAsync(planet);
                
                if (!asteroides.Any())
                    return NoContent();

                return Ok(asteroides.Take(3));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        #region métodos privados

        private async Task<IEnumerable<AsteroideDto>> ObtenerAsteroidesAsync(string name)
        {

                var fechaActual = DateTime.Today.ToString("yyyy-MM-dd");
                var fechaMas7 = DateTime.Today.AddDays(7).ToString("yyyy-MM-dd");
                var apiKey = "zdUP8ElJv1cehFM0rsZVSQN7uBVxlDnu4diHlLSb";
                IEnumerable<AsteroideDto> asteroides = new HashSet<AsteroideDto>();

                var httpClient = _httpClientFactory.CreateClient("Asteroide");
                var httpResponseMessage = await httpClient.GetAsync($"neo/rest/v1/feed?start_date={fechaActual}&end_date={fechaMas7}&api_key={apiKey}");

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var model = JsonConvert.DeserializeObject<AsteroideNasaDto>(await httpResponseMessage.Content.ReadAsStringAsync());
                   if(model!= null)
                        asteroides = model.NearEarthObjects.SelectMany(m => m.Value)
                                                           .Where(p => p.IsPotentiallyHazardousAsteroid &&
                                                                       p.CloseApproachData.FirstOrDefault().OrbitingBody == name)
                                                           .Select(o => o.MapAsteroideDtoNasaDtoToAsteroideDto())
                                                           .OrderByDescending(t => t.Diametro);
                }

                return asteroides;
        }

        #endregion
    }
}

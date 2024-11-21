using Application.DataTransferObjects.Placemark.Request;
using Application.DataTransferObjects.Placemark.Response;
using Application.Interfaces;
using Infra.Utils;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace DirecionadorVite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlacemarksController : ControllerBase
    {
        private readonly IPlacemarkAppService placemarkAppService;

        public PlacemarksController(IPlacemarkAppService placemarkAppService)
        {
            this.placemarkAppService = placemarkAppService;
        }

        /// <summary>
        /// Recuperar todos os Placemarks no formato JSON
        /// </summary>
        /// <remarks>Recupera todos os Placemarks no formato JSON conforme a filtragem escolhida</remarks>
        /// <returns></returns>
        /// <response code="200">Retorna a lista de Placemarks</response>
        /// <response code="404">Not found</response>
        /// <response code="400">Retorna os erros encontrados</response>
        [ProducesResponseType(typeof(List<PlacemarkResponse>), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        // GET: api/<PlacemarkController>
        [HttpGet(Name = "GetPlacemarkJson")]
        public async Task<IActionResult> GetAllReturnJson([FromQuery] PlacemarkRequestFilter? placemarkRequestFilter)
        {
            try
            {
                var retorno = await placemarkAppService.GetPlacemarksAsync(placemarkRequestFilter);
                if (retorno == null || !retorno.Any())
                {
                    return NotFound(new { Message = "Nenhum Placemark encontrado." });
                }
                return Ok(retorno);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { Message = "Erro nos parâmetros fornecidos.", Details = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Erro interno do servidor.", Details = ex.Message });
            }
        }

        /// <summary>
        /// Recuperar todos os Placemarks em um arquivo KML
        /// </summary>
        /// <remarks>Recupera todos os Placemarks em um arquivo KML conforme a filtragem escolhida</remarks>
        /// <returns></returns>
        /// <response code="200">Retorna o arquivo de Placemarks</response>
        /// <response code="404">Not found</response>
        /// <response code="400">Retorna os erros encontrados</response>
        [ProducesResponseType(typeof(List<PlacemarkResponse>), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        // POST: api/<PlacemarkController/export>
        [HttpPost("export")]

        public async Task<IActionResult> PostReturnAllReturnKML([FromBody] PlacemarkRequestFilter? placemarkRequestFilter)
        {
            try
            {
                var placemarks = await placemarkAppService.GetPlacemarksAsync(placemarkRequestFilter);
                if (placemarks == null || !placemarks.Any())
                {
                    return NotFound(new { Message = "Nenhum Placemark encontrado para gerar o KML." });
                }
                var fileBytes = GenerateKML.GenerateKmlPlacemarks(placemarks);
                var fileName = "direcionadores.kml";

                return File(fileBytes, "application/vnd.google-earth.kml+xml", fileName);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { Message = "Erro nos parâmetros fornecidos.", Details = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Erro interno do servidor ao gerar o KML.", Details = ex.Message });
            }
        }

        /// <summary>
        /// Recuperar todos os identificadores únicos para filtragem
        /// </summary>
        /// <remarks>Recupera todos os identificadores únicos para filtragem</remarks>
        /// <returns></returns>
        /// <response code="200">Retorna os itens para filtragem</response>
        /// <response code="404">Not found</response>
        /// <response code="400">Retorna os erros encontrados</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        // POST: api/<PlacemarkController/export>
        [HttpGet("filters")]

        public async Task<IActionResult> GetFilters()
        {
            try
            {
                var retorno = await placemarkAppService.GetFiltersPlacemarksAsync();
                if (retorno == null)
                {
                    return NotFound(new { Message = "Nenhum filtro encontrado." });
                }
                return Ok(retorno);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { Message = "Erro ao recuperar filtros.", Details = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Erro interno do servidor ao recuperar os filtros.", Details = ex.Message });
            }
        }
    }
}

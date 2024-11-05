using CO2EmissionsAPI.DTOs;
using CO2EmissionsAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CO2EmissionsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmissionsController : ControllerBase
    {
        private readonly IWorldBankEmissionsRepository _repository;
        private readonly ILogger<EmissionsController> _logger;

        public EmissionsController(IWorldBankEmissionsRepository repository, ILogger<EmissionsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("GetEmissions")]
        public async Task<IActionResult> GetEmissions()
        {
            try
            {
                var data = await _repository.GetEmissionsDataAsync();

                if (data == null || !data.Any())
                {
                    _logger.LogWarning("No emissions data found");
                    return NotFound("No emissions data found for");
                }

                var emissionsDatumDtoList = data.Select(ed => new EmissionsDatumDto(ed)).ToList();

                _logger.LogInformation("Successfully retrieved emissions data.");
                return Ok(emissionsDatumDtoList);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Invalid argument");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An unexpected error occurred while retrieving emissions data: {ex.Message}");
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }

        [HttpGet("GetEmissionsByCountry/{countryId}")]
        public async Task<IActionResult> GetEmissionsByCountry(int countryId)
        {
            try
            {
                var data = await _repository.GetEmissionsByCountryAsync(countryId);

                if (data == null || !data.Any())
                {
                    _logger.LogWarning($"No emissions data found for country id: {countryId}");
                    return NotFound($"No emissions data found for country id: {countryId}");
                }

                var emissionsDatumDtoList = data.Select(ed => new EmissionsDatumDto(ed)).ToList();

                _logger.LogInformation($"Successfully retrieved emissions data for country id: {countryId}");
                return Ok(emissionsDatumDtoList);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, $"Invalid argument for country id: {countryId}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An unexpected error occurred while retrieving emissions data for country id {countryId}: {ex.Message}");
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }

        [HttpGet("GetEmissionsByYear/{year}")]
        public async Task<IActionResult> GetEmissionsByYear(int year)
        {
            try
            {
                var data = await _repository.GetEmissionsByYearAsync(year);

                if (data == null || !data.Any())
                {
                    _logger.LogWarning($"No emissions data found for year: {year}");
                    return NotFound("No emissions data found for");
                }

                var emissionsDatumDtoList = data.Select(ed => new EmissionsDatumDto(ed)).ToList();

                _logger.LogInformation("Successfully retrieved emissions data.");
                return Ok(emissionsDatumDtoList);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Invalid argument");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An unexpected error occurred while retrieving emissions data: {ex.Message}");
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }

        [HttpGet("GetEmissionsByYearAndCountry/{year}/{countryId}")]
        public async Task<IActionResult> GetEmissionsByYearAndCountry(int year, int countryId)
        {
            try
            {
                var data = await _repository.GetEmissionsByYearAndCountryAsync(year, countryId);

                if (data == null || !data.Any())
                {
                    _logger.LogWarning($"No emissions data found for year: {year} and country id: {countryId}");
                    return NotFound("No emissions data found for");
                }

                var emissionsDatumDtoList = data.Select(ed => new EmissionsDatumDto(ed)).ToList();

                _logger.LogInformation("Successfully retrieved emissions data.");
                return Ok(emissionsDatumDtoList);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Invalid argument");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An unexpected error occurred while retrieving emissions data: {ex.Message}");
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }

        [HttpGet("GetEmissionsByYearRangeAndCountry/{minYear}/{maxYear}/{countryId}")]
        public async Task<IActionResult> GetEmissionsByYearRangeAndCountry(int minYear, int maxYear, int countryId)
        {
            try
            {
                var data = await _repository.GetEmissionsByYearRangeAndCountryAsync(minYear, maxYear, countryId);

                if (data == null || !data.Any())
                {
                    _logger.LogWarning($"No emissions data found for min year {minYear}, maxYear {maxYear}, and country id: {countryId}");
                    return NotFound("No emissions data found for");
                }

                var emissionsDatumDtoList = data.Select(ed => new EmissionsDatumDto(ed)).ToList();

                _logger.LogInformation("Successfully retrieved emissions data.");
                return Ok(emissionsDatumDtoList);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Invalid argument");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An unexpected error occurred while retrieving emissions data: {ex.Message}");
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }

        [HttpGet("GetCountries")]
        public async Task<IActionResult> GetCountries()
        {
            try
            {
                var countries = await _repository.GetCountries();

                if (countries == null || !countries.Any())
                {
                    _logger.LogWarning("No countries data found");
                    return NotFound("No countries data found");
                }

                _logger.LogInformation("Successfully retrieved countries data");
                return Ok(countries);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Invalid argument for fetching countries");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while retrieving countries data");
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }

        [HttpGet("GetIndicators")]
        public async Task<IActionResult> GetIndicators()
        {
            try
            {
                var indicators = await _repository.GetIndicators();

                if (indicators == null || !indicators.Any())
                {
                    _logger.LogWarning("No indicators data found");
                    return NotFound("No indicators data found");
                }

                _logger.LogInformation("Successfully retrieved indicators data");
                return Ok(indicators);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Invalid argument for fetching indicators");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while retrieving indicators data");
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }
    }
}


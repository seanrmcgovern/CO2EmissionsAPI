using CO2EmissionsAPI.Models;

namespace CO2EmissionsAPI.DTOs
{
    public class EmissionsDatumDto
	{
        public long Id { get; set; }
        public long? CountryId { get; set; }
        public long? IndicatorId { get; set; }
        public long? Year { get; set; }
        public string? Status { get; set; }
        public string? Unit { get; set; }
        public long? Version { get; set; }

        public decimal EmissionsValue { get; set; } 
        public string CountryCode { get; set; }
        public string CountryName { get; set; }

        public EmissionsDatumDto(EmissionsDatum emissionsDatum)
        {
            Id = emissionsDatum.Id;
            CountryId = emissionsDatum.CountryId;
            IndicatorId = emissionsDatum.IndicatorId;
            Year = emissionsDatum.Year;
            Status = emissionsDatum.Status;
            Unit = emissionsDatum.Unit;
            Version = emissionsDatum.Version;

            if (emissionsDatum.EmissionsValue != string.Empty)
            {
                EmissionsValue = Convert.ToDecimal(emissionsDatum.EmissionsValue);
            }
            else
            {
                EmissionsValue = 0;
            }

            CountryCode = emissionsDatum.Country?.IsoCode ?? string.Empty;
            CountryName = emissionsDatum.Country?.Name ?? string.Empty;
        }
    }
}


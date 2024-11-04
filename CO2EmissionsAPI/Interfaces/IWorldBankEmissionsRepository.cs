
using CO2EmissionsAPI.Models;

namespace CO2EmissionsAPI.Interfaces
{
    public interface IWorldBankEmissionsRepository
    {
        Task<IEnumerable<EmissionsDatum>> GetEmissionsDataAsync();
        Task<IEnumerable<EmissionsDatum>> GetEmissionsByCountryAsync(int countryCode);
        Task<IEnumerable<EmissionsDatum>> GetEmissionsByYearAsync(int year);
        Task<IEnumerable<EmissionsDatum>> GetEmissionsByYearAndCountryAsync(int year, int countryId);
        Task<IEnumerable<Country>> GetCountries();
        Task<IEnumerable<EmissionsIndicator>> GetIndicators();
    }
}


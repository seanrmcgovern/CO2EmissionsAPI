using CO2EmissionsAPI.Interfaces;
using CO2EmissionsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CO2EmissionsAPI.Repositories
{
    public class WorldBankEmissionsRepository : IWorldBankEmissionsRepository
    {
        private readonly WorldBankEmissionsContext _context;

        public WorldBankEmissionsRepository(WorldBankEmissionsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EmissionsDatum>> GetEmissionsDataAsync()
        {
            return await _context.EmissionsData.Include(ed => ed.Country).ToListAsync();
        }

        public async Task<IEnumerable<EmissionsDatum>> GetEmissionsByCountryAsync(int countryId)
        {
            return await _context.EmissionsData.Where(ed => ed.CountryId == countryId).Include(ed => ed.Country).ToListAsync();
        }

        public async Task<IEnumerable<EmissionsDatum>> GetEmissionsByYearAsync(int year)
        {
            return await _context.EmissionsData.Where(ed => ed.Year == year).Include(ed => ed.Country).ToListAsync();
        }

        public async Task<IEnumerable<EmissionsDatum>> GetEmissionsByYearAndCountryAsync(int year, int countryId)
        {
            return await _context.EmissionsData.Where(ed => ed.Year == year && ed.CountryId == countryId).Include(ed => ed.Country).ToListAsync();
        }

        public async Task<IEnumerable<EmissionsDatum>> GetEmissionsByYearRangeAndCountryAsync(int minYear, int maxYear, int countryId)
        {
            return await _context.EmissionsData.Where(ed => ed.Year >= minYear && ed.Year <= maxYear && ed.CountryId == countryId).Include(ed => ed.Country).ToListAsync();
        }

        public async Task<IEnumerable<Country>> GetCountries()
        {
            return await _context.Countries.ToListAsync();
        }

        public async Task<IEnumerable<EmissionsIndicator>> GetIndicators()
        {
            return await _context.EmissionsIndicators.ToListAsync();
        }
    }
}


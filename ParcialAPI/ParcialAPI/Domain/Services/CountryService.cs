using Microsoft.EntityFrameworkCore;
using ParcialAPI.DAL;
using ParcialAPI.DAL.Entities;
using ParcialAPI.Domain.Interfaces;

namespace ParcialAPI.Domain.Services
{
    public class CountryService : ICountryService
    {
        private readonly DataBaseContext _context;
        public CountryService(DataBaseContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Country>> GetCountriesAsync()
        {
            try
            {
                return await _context.Countries.ToListAsync();
            }
            catch (DbUpdateException dbUpdateException)
            {

                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
            }
        }

        public async Task<Country> GetCountryByIdAsync(Guid id)
        {
            try
            {
                return await _context.Countries.FirstOrDefaultAsync(c => c.Id == id);
            }
            catch (DbUpdateException dbUpdateException)
            {

                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
            }

        }

        public async Task<Country> CreateCountryAsync(Country country)
        {
            try
            {
                country.Id = Guid.NewGuid();
                country.CreatedDate = DateTime.Now;
                _context.Countries.Add(country);
                await _context.SaveChangesAsync();
                return country;
            }
            catch (DbUpdateException dbUpdateException)
            {

                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
            }
            
        }

        public async Task<Country> UpdateCountryAsync(Country country)
        {
            try
            {
                country.ModifiedDate = DateTime.Now;
                _context.Countries.Update(country);
                await _context.SaveChangesAsync();
                return country;
            }
            catch (DbUpdateException dbUpdateException)
            {

                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
            }
        }

        public async Task<Country> DeleteCountryAsync(Guid id)
        {
            try
            {
                var country = await GetCountryByIdAsync(id);
                if (country == null)
                {
                    throw new Exception("Country not found");
                }
                _context.Countries.Remove(country);
                await _context.SaveChangesAsync();
                return country;
            }
            catch (DbUpdateException dbUpdateException)
            {

                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
            }
        }
    }
}

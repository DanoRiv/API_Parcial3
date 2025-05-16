using ParcialAPI.DAL.Entities;

namespace ParcialAPI.Domain.Interfaces
{
    public interface ICountryService
    {
        Task<IEnumerable<Country>> GetCountriesAsync();
        Task<Country> GetCountryByIdAsync(Guid id);
        Task<Country> CreateCountryAsync(Country country);
        Task<Country> UpdateCountryAsync(Country country);
        Task<Country> DeleteCountryAsync(Guid id);
    }
}

using Microsoft.EntityFrameworkCore;
using ParcialAPI.DAL;
using ParcialAPI.DAL.Entities;
using ParcialAPI.Domain.Interfaces;

namespace ParcialAPI.Domain.Services;

public class StateService : IStateService
{
    private readonly DataBaseContext _context;

    public StateService(DataBaseContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<State>> GetStatesAsync()
    {
        try
        {
            return await _context.States
                .Include(s => s.Country)
                .ToListAsync();
        }
        catch (DbUpdateException dbUpdateException)
        {

            throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
        }
    }

    public async Task<State> GetStateByIdAsync(Guid id)
    {
        try
        {
            return await _context.States
                .Include(s => s.Country)
                .FirstOrDefaultAsync(s => s.Id == id);
        }
        catch (DbUpdateException dbUpdateException)
        {

            throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
        }
    }

    public async Task<State> CreateStateAsync(State state)
    {
        try
        {
            var country = await _context.Countries.FindAsync(state.CountryId);
            if (country == null)
            {
                throw new Exception("Country does not exist.");
            }
            state.Id = Guid.NewGuid();
            state.CreatedDate = DateTime.Now;
            state.Country = country;
            _context.States.Add(state);
            await _context.SaveChangesAsync();
            return state;
        }
        catch (DbUpdateException dbUpdateException)
        {

            throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
        }
    }

    public async Task<State> UpdateStateAsync(State state)
    {
        try
        {
            var country = await _context.Countries.FindAsync(state.CountryId);
            if (country == null)
            {
                throw new Exception("Country not found");
            }
            state.Country = country;
            state.ModifiedDate = DateTime.Now;
            _context.States.Update(state);
            await _context.SaveChangesAsync();
            return state;
        }
        catch (DbUpdateException dbUpdateException)
        {

            throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
        }
    }

    public async Task<State> DeleteStateAsync(Guid id)
    {
        try
        {
            var state = await GetStateByIdAsync(id);
            if (state == null)
            {
                throw new Exception("State not found");
            }
            _context.States.Remove(state);
            await _context.SaveChangesAsync();
            return state;
        }
        catch (DbUpdateException dbUpdateException)
        {

            throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
        }
    }
}

using ParcialAPI.DAL.Entities;

namespace ParcialAPI.Domain.Interfaces;

public interface IStateService
{
    Task<IEnumerable<State>> GetStatesAsync();
    Task<State> GetStateByIdAsync(Guid id);
    Task<State> CreateStateAsync(State state);
    Task<State> UpdateStateAsync(State state);
    Task<State> DeleteStateAsync(Guid id);
}

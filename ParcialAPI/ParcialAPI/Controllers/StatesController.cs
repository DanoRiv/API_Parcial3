using Microsoft.AspNetCore.Mvc;
using ParcialAPI.DAL.Entities;
using ParcialAPI.Domain.Interfaces;

namespace ParcialAPI.Controllers;

[Route("/api/[controller]")]
[ApiController]
public class StatesController : Controller
{
    private readonly IStateService _stateService;

    public StatesController(IStateService stateService)
    {
        _stateService = stateService;
    }

    [HttpGet, ActionName("Get")]
    [Route("GetAll")]
    public async Task<ActionResult<IEnumerable<State>>> GetStatesAsync()
    {
        var states = await _stateService.GetStatesAsync();
        if (states == null || !states.Any())
        {
            return NotFound();
        }
        return Ok(states);
    }
    [HttpGet, ActionName("Get")]
    [Route("GetById/{id}")]
    public async Task<ActionResult<State>> GetStateByIdAsync(Guid id)
    {
        var state = await _stateService.GetStateByIdAsync(id);
        if (state == null)
        {
            return NotFound();
        }
        return Ok(state);
    }
    [HttpPost, ActionName("Create")]
    [Route("Create")]
    public async Task<ActionResult<State>> CreateStateAsync(State state)
    {
        try
        {
            var newState = await _stateService.CreateStateAsync(state);
            if (newState == null) return NotFound();
            return Ok(newState);
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("duplicate"))
            {
                return Conflict(String.Format("{0} already exists", state.Name));
            }
            return Conflict(ex.Message);
        }
    }
    [HttpPut, ActionName("Update")]
    [Route("Update")]
    public async Task<ActionResult<State>> UpdateStateAsync(State state)
    {
        try
        {
            var updatedState = await _stateService.UpdateStateAsync(state);
            if (updatedState == null) return NotFound();
            return Ok(updatedState);
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("duplicate"))
            {
                return Conflict(String.Format("{0} already exists", state.Name));
            }
            return Conflict(ex.Message);
        }
    }
    [HttpDelete, ActionName("Delete")]
    [Route("Delete/{id}")]
    public async Task<ActionResult<State>> DeleteStateAsync(Guid id)
    {
        if(id == Guid.Empty) return BadRequest("Invalid ID provided.");
        var deletedState = await _stateService.DeleteStateAsync(id);
        if (deletedState == null) return NotFound();
        return Ok(deletedState);
    }
}
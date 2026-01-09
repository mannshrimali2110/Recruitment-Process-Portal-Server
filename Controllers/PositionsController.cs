using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using recruitment_process_portal_server.DTOs.Requests;
using recruitment_process_portal_server.DTOs.Responses;
using recruitment_process_portal_server.Services;

namespace recruitment_process_portal_server.Controllers;

/// API endpoints for managing job positions in the recruitment system.
/// Handles position lifecycle, status transitions, and position information retrieval.
[ApiController]
[Route("api/positions")]
[Authorize]
public class PositionsController : ControllerBase
{
    private readonly IPositionService _positionService;
    private readonly ICurrentUserService _currentUser;

    public PositionsController(
        IPositionService positionService,
        ICurrentUserService currentUser)
    {
        _positionService = positionService;
        _currentUser = currentUser;
    }


    [HttpGet("{positionId:int}")]
    [Authorize]
    public async Task<ActionResult<PositionDetailsDto>> GetPosition(int positionId)
    {
        var position = await _positionService.GetPositionAsync(positionId);
        return Ok(position);
    }

    [HttpPut("{positionId:int}/status")]
    [Authorize(Policy = "RecruiterOnly")]
    public async Task<ActionResult<PositionStatusResponseDto>> UpdatePositionStatus(
        int positionId,
        [FromBody] UpdatePositionStatusRequest request)
    {
        var result = await _positionService.UpdatePositionStatusAsync(
            positionId,
            request.NewStatusId,
            _currentUser.UserId,
            request.Reason);

        return Ok(result);
    }


    [HttpGet("statuses")]
    [Authorize]
    public async Task<ActionResult<IReadOnlyList<(int Id, string Name)>>> GetAvailableStatuses()
    {
        var statuses = await _positionService.GetAvailableStatusesAsync();
        return Ok(statuses);
    }

    [HttpGet("{positionId:int}/can-transition/{newStatusId:int}")]
    [Authorize]
    public async Task<ActionResult<bool>> CanTransitionStatus(int positionId, int newStatusId)
    {
        var canTransition = await _positionService.CanTransitionStatusAsync(positionId, newStatusId);
        return Ok(canTransition);
    }
}

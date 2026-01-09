using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using recruitment_process_portal_server.DTOs;
using recruitment_process_portal_server.DTOs.Requests;
using recruitment_process_portal_server.Services;

namespace recruitment_process_portal_server.Controllers;

[ApiController]
[Route("api/applications")]
[Authorize] 
public class ApplicationsController : ControllerBase
{
    private readonly IApplicationService _applicationService;
    private readonly ICurrentUserService _currentUser;

    public ApplicationsController(
        IApplicationService applicationService,
        ICurrentUserService currentUser)
    {
        _applicationService = applicationService;
        _currentUser = currentUser;
    }

    // CREATE APPLICATION

    [HttpPost]
    [Authorize(Policy = "RecruiterOrHR")]
    public async Task<ActionResult<int>> CreateApplication(
        [FromBody] CreateApplicationRequest request)
    {
        var linkId = await _applicationService.CreateApplicationAsync(
            request.CandidateId,
            request.PositionId,
            _currentUser.UserId);

        return CreatedAtAction(
            nameof(GetApplicationDetails),
            new { linkId },
            linkId);
    }

    // STATUS TRANSITIONS

    [HttpPut("{linkId:int}/status")]
    [Authorize] 
    public async Task<IActionResult> UpdateStatus(
        int linkId,
        [FromBody] UpdateApplicationStatusRequest request)
    {
        await _applicationService.UpdateApplicationStatusAsync(
            linkId,
            request.NewStatusId,
            _currentUser.UserId,
            request.Reason);

        return NoContent();
    }

    [HttpPut("{linkId:int}/hold")]
    [Authorize(Policy = "RecruiterOrHR")]
    public async Task<IActionResult> PutOnHold(
        int linkId,
        [FromBody] string reason)
    {
        await _applicationService.PutApplicationOnHoldAsync(
            linkId,
            _currentUser.UserId,
            reason);

        return NoContent();
    }

    [HttpPut("{linkId:int}/reject")]
    [Authorize(Policy = "RecruiterOrHR")]
    public async Task<IActionResult> RejectApplication(
        int linkId,
        [FromBody] UpdateApplicationStatusRequest request)
    {
        await _applicationService.RejectApplicationAsync(
            linkId,
            request.NewStatusId,
            _currentUser.UserId,
            request.Reason);

        return NoContent();
    }

    // SCREENING

    [HttpPost("{linkId:int}/screening")]
    [Authorize(Policy = "RecruiterOnly")]
    public async Task<IActionResult> AddScreeningFeedback(
        int linkId,
        [FromBody] ScreeningFeedbackRequest request)
    {
        await _applicationService.AddScreeningFeedbackAsync(
            linkId,
            _currentUser.UserId,
            request.Comments,
            request.Decision);

        return NoContent();
    }

    [HttpPost("{linkId:int}/screening/complete")]
    [Authorize(Policy = "RecruiterOnly")]
    public async Task<IActionResult> CompleteScreening(
        int linkId,
        [FromQuery] int nextStatusId)
    {
        await _applicationService.CompleteScreeningAsync(
            linkId,
            nextStatusId,
            _currentUser.UserId);

        return NoContent();
    }

    // INTERVIEWS

    [HttpPost("{linkId:int}/interviews")]
    [Authorize(Policy = "RecruiterOnly")]
    public async Task<ActionResult<int>> ScheduleInterview(
        int linkId,
        [FromBody] ScheduleInterviewRequest request)
    {
        var interviewId = await _applicationService.ScheduleInterviewAsync(
            linkId,
            request.InterviewTypeId,
            _currentUser.UserId,
            request.Start,
            request.End,
            request.MeetingLink);

        return Ok(interviewId);
    }

    [HttpPut("interviews/{interviewId:int}/complete")]
    [Authorize(Policy = "InterviewerOnly")]
    public async Task<IActionResult> CompleteInterview(
        int interviewId)
    {
        await _applicationService.CompleteInterviewAsync(
            interviewId,
            _currentUser.UserId);

        return NoContent();
    }

    // OFFERS & HIRING

    [HttpPost("{linkId:int}/offers")]
    [Authorize(Policy = "HROnly")]
    public async Task<ActionResult<int>> CreateOffer(
        int linkId,
        [FromBody] CreateOfferRequest request)
    {
        var offerId = await _applicationService.CreateOfferAsync(
            linkId,
            request.OfferDate,
            request.CTC,
            request.JoiningDate);

        return Ok(offerId);
    }

    [HttpPut("offers/{offerId:int}/status")]
    [Authorize(Policy = "HROnly")]
    public async Task<IActionResult> UpdateOfferStatus(
        int offerId,
        [FromQuery] string newStatus)
    {
        await _applicationService.UpdateOfferStatusAsync(
            offerId,
            newStatus,
            _currentUser.UserId);

        return NoContent();
    }

    [HttpPost("{linkId:int}/hire")]
    [Authorize(Policy = "HROnly")]
    public async Task<IActionResult> ConvertToEmployee(
        int linkId,
        [FromBody] ConvertToEmployeeRequest request)
    {
        await _applicationService.ConvertToEmployeeAsync(
            linkId,
            request.JoiningDate,
            request.WorkEmail,
            _currentUser.UserId);

        return NoContent();
    }

    // READ ENDPOINTS

    [HttpGet("{linkId:int}")]
    public async Task<ActionResult<ApplicationDetailsDto>> GetApplicationDetails(
        int linkId)
    {
        var result = await _applicationService.GetApplicationDetailsAsync(linkId);
        return Ok(result);
    }

    [HttpGet("{linkId:int}/status-history")]
    public async Task<ActionResult<IReadOnlyList<ApplicationStatusLogDto>>> GetStatusHistory(
        int linkId)
    {
        var result = await _applicationService.GetApplicationStatusHistoryAsync(linkId);
        return Ok(result);
    }

    [HttpGet("position/{positionId:int}")]
    public async Task<ActionResult<IReadOnlyList<ApplicationSummaryDto>>> GetApplicationsByPosition(
        int positionId)
    {
        var result = await _applicationService.GetApplicationsByPositionAsync(positionId);
        return Ok(result);
    }

    [HttpGet("{linkId:int}/summary")]
    public async Task<ActionResult<ApplicationSummaryDto>> GetApplicationSummary(
        int linkId)
    {
        var result = await _applicationService.GetApplicationSummaryAsync(linkId);
        return Ok(result);
    }
}

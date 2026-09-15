using Microsoft.AspNetCore.Mvc;
using SupportOps.Application.Interfaces;

namespace SupportOps.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MetadataController : ControllerBase
{
    private readonly IApplicationRepository _applicationRepository;
    private readonly IUserRepository _userRepository;

    public MetadataController(IApplicationRepository applicationRepository, IUserRepository userRepository)
    {
        _applicationRepository = applicationRepository;
        _userRepository = userRepository;
    }

    [HttpGet("applications")]
    public async Task<IActionResult> GetApplications(CancellationToken cancellationToken)
    {
        var apps = await _applicationRepository.GetAllAsync(cancellationToken);
        return Ok(apps.Select(a => new { a.Id, a.Name }));
    }

    [HttpGet("users")]
    public async Task<IActionResult> GetUsers(CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync(cancellationToken);
        return Ok(users.Select(u => new { u.Id, u.Name, u.Email }));
    }
}

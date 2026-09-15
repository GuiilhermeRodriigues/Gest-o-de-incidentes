using FluentValidation;
using SupportOps.Application.DTOs.Requests;

namespace SupportOps.Application.Validators;

public class CreateIncidentRequestValidator : AbstractValidator<CreateIncidentRequest>
{
    public CreateIncidentRequestValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.ApplicationId).NotEmpty();
        RuleFor(x => x.RequesterId).NotEmpty();
        RuleFor(x => x.Category).IsInEnum();
        RuleFor(x => x.Impact).IsInEnum();
        RuleFor(x => x.Urgency).IsInEnum();
    }
}

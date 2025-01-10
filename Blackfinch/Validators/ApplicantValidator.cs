using Blackfinch.DomainModels;

using FluentValidation;

namespace Blackfinch.Validators;

public class ApplicantValidator : AbstractValidator<Applicant>
{
    public ApplicantValidator()
    {
        RuleFor(a => a.CreditScore)
            .GreaterThanOrEqualTo(1)
            .WithMessage("The credit score must be between 1 and 999")
            .LessThanOrEqualTo(999)
            .WithMessage("The credit score must be between 1 and 999");
    }
}

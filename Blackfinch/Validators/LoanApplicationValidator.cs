using Blackfinch.DomainModels;

using FluentValidation;

namespace Blackfinch.Validators;

public class LoanApplicationValidator: AbstractValidator<LoanApplication> 
{
    public LoanApplicationValidator()
    {
        RuleFor(la => la.LoanValue)
            .GreaterThanOrEqualTo(100_000)
            .WithMessage("The loan value must be between 100,000 and 1,500,000")
            .LessThanOrEqualTo(1_500_000)
            .WithMessage("The loan value must be between 100,000 and 1,500,000");

        RuleFor(la => la.LoanToValue)
            .LessThan(90)
            .WithMessage("The loan to value must be less than 90%");

        RuleFor(la => la.Applicant).SetValidator(new ApplicantValidator());
    }
}

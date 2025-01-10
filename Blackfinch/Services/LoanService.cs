namespace Blackfinch.Services;

using Blackfinch.DomainModels;

using FluentValidation;
using FluentValidation.Results;

public class LoanService : ILoanService
{
    private readonly List<LoanApplication> _loanApplications = [];
    private readonly IValidator<LoanApplication> _validator;

    public LoanService(IValidator<LoanApplication> validator)
    {
        _validator = validator;
    }

    public Statistics CreateLoanApplication(LoanApplication loan)
    {
        ValidationResult validationResult = _validator.Validate(loan);

        if (!validationResult.IsValid)
        {
            return new Statistics(validationResult.Errors[0].ErrorMessage, GetApplicationsToDate, GetTotalLoanValue, GetAverageLoanToValue);
        }

        loan.SetApplicationState(loan.IsValidApplication());
        _loanApplications.Add(loan);

        string applicationResult = loan.IsValidApplication() ? "The loan application was successful" : "The loan application was declined";

        return new(applicationResult, GetApplicationsToDate, GetTotalLoanValue, GetAverageLoanToValue);
    }

    private List<Tuple<bool, int>> GetApplicationsToDate => [.. _loanApplications
            .GroupBy(x => x.ApplicationSuccessful)
            .Select(x => new Tuple<bool, int>(x.Key, x.Count()))];

    private decimal GetTotalLoanValue => _loanApplications.Sum(x => x.LoanValue);

    private decimal GetAverageLoanToValue => _loanApplications.Any() ? _loanApplications.Average(x => x.LoanToValue) : 0M;
}

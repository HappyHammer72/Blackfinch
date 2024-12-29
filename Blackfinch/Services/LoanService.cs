namespace Blackfinch.Services;

using FluentValidation.Results;
using Blackfinch.DomainModels;

public class LoanService : ILoanService
{
    private readonly List<LoanApplication> _loanApplications = [];

    public Statistics CreateLoanApplication(LoanApplication loan)
    {
        loan.SetApplicationState(loan.IsValidApplication());
        _loanApplications.Add(loan);

        Statistics statistics = new(loan.IsValidApplication(), GetApplicationsToDate, GetTotalLoanValue, 0);

        return statistics;
    }

    private List<Tuple<bool, int>> GetApplicationsToDate => [.. _loanApplications
            .GroupBy(x => x.ApplicationSuccessful)
            .Select(x => new Tuple<bool, int>(x.Key, x.Count()))];

    private decimal GetTotalLoanValue => _loanApplications.Sum(x => x.LoanValue);

    private decimal GetAverageLoanToValue => _loanApplications.Average(x => x.LoanToValue);
}

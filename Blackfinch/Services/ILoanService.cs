namespace Blackfinch.Services
{
    using Blackfinch.DomainModels;

    public interface ILoanService
    {
        Statistics CreateLoanApplication(LoanApplication loan);
    }
}
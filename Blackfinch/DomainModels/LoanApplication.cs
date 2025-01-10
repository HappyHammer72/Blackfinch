namespace Blackfinch.DomainModels;
public class LoanApplication
{
    public LoanApplication(decimal loanValue, decimal assetValue, Applicant applicant)
    {
        LoanValue = loanValue;
        AssetValue = assetValue;
        Applicant = applicant;
    }

    public decimal LoanValue { get; }

    public decimal AssetValue { get; }

    public Applicant Applicant { get; }

    public bool ApplicationSuccessful { get; private set; }

    public decimal LoanToValue => (LoanValue / AssetValue) * 100;
    
    public bool IsValidApplication()
    {
        if (LoanValue >= 1_000_000)
        {
            return CheckLoanToValueAndCreditScore(60, 950);
        }
        else
        {
            if (CheckLoanToValueAndCreditScore(60,750)
                || CheckLoanToValueAndCreditScore(80, 800)
                || CheckLoanToValueAndCreditScore(90, 900))
            {
                return true;
            }
        }

        return false;
    }

    public void SetApplicationState(bool applicationSuccessful)
    {
        ApplicationSuccessful = applicationSuccessful;
    }

    private bool CheckLoanToValueAndCreditScore(decimal loanToValue, int creditScore) 
        => LoanToValue < loanToValue && Applicant.CreditScore >= creditScore;
}

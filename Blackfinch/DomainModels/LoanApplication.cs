namespace Blackfinch.DomainModels;

using System;

using static System.Net.Mime.MediaTypeNames;

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

    public decimal LoanToValue => LoanValue / AssetValue * 100;
    
    public bool IsValidApplication()
    {
        if (LoanValue < 100_000 || LoanValue > 1_500_000 || LoanToValue > 90)
        {
            return false;
        }

        if (LoanValue >= 1_000_000)
        {
            if (LoanToValue < 60 && Applicant.CreditScore >= 950)
            {
                return true;
            }
        }
        else
        {
            if (LoanToValue < 60 && Applicant.CreditScore >= 750
                || LoanToValue < 80 && Applicant.CreditScore >= 800
                || LoanToValue < 90 && Applicant.CreditScore >= 900)
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
}

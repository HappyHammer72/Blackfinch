namespace Blackfinch.DomainModels;

public class Applicant
{
    public Applicant(decimal creditScore)
    {
        CreditScore = creditScore;
    }

    public decimal CreditScore { get; }
}

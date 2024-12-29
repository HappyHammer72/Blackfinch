
using Blackfinch.DomainModels;
using Blackfinch.Services;

public class Program
{
    private static string? _loanAmountInput;
    private static string? _assetValueInput;
    private static string? _creditScoreInput;
    private static bool _continue = true;

    private static void Main(string[] args)
    {
        CaptureDataInput();
        LoanApplication? loan = ValidateUserInput();

        if (loan is not null)
        {
            ILoanService loanService = new LoanService();
            Statistics statistics = loanService.CreateLoanApplication(loan);
            WriteStatistics(statistics);
        }
    }

    private static void CaptureDataInput()
    {
        while (_continue)
        {
            Console.WriteLine("Enter the amount you would like to borrow");
            _loanAmountInput = Console.ReadLine();
            Console.WriteLine("Enter the value of the asset");
            _assetValueInput = Console.ReadLine();
            Console.WriteLine("Enter your credit score from 1 to 999");
            _creditScoreInput = Console.ReadLine();
            Console.WriteLine("Do you want to continue (Y/N)");
            _continue = Console.ReadLine() == "Y";
        }
    }

    private static LoanApplication? ValidateUserInput()
    {
        if (!decimal.TryParse(_loanAmountInput, out decimal loanAmount))
        {
            Console.WriteLine("Please enter the loan amount in GBP");
            return null;
        }

        if (!decimal.TryParse(_assetValueInput, out decimal assetValue))
        {
            Console.WriteLine("Please enter the asset value in GBP");
            return null;
        }

        if (!decimal.TryParse(_creditScoreInput, out decimal creditScore))
        {
            Console.WriteLine("Please enter a value between 1 and 999");
            return null;
        }

        return new(loanAmount, assetValue, new Applicant(creditScore));
    }

    private static void WriteStatistics(Statistics statistics)
    {
        Console.WriteLine(statistics.ApplicationSuccessful ? "The loan application was successful" : "The loan application failed");
        Console.WriteLine($"Total Applications: {statistics.ApplicationsToDate}");
        Console.WriteLine($"Total Loan Value: {statistics.TotalLoanValue}");
        Console.WriteLine($"Average Loan to Value: {statistics.AverageLoanToValue}");
    }
}
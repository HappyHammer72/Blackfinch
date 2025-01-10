namespace Blackfinch.UnitTests.Tests;

using Blackfinch.DomainModels;
using Blackfinch.Services;
using Blackfinch.Validators;

public class CreateLoanApplicationTests
{    
    [Fact]
    [Trait("Requirement", "If the value of the loan is more than £1.5 million or less than £100,000 then the application must be declined")]
    public void WhenCreditScoreIsZeroShouldReturnFalse()
    {
        // Arrange        
        LoanApplication loan = new(100_000M, 200_000M, new Applicant(0));
        LoanService loanService = new(new LoanApplicationValidator());

        // Act
        Statistics actual = loanService.CreateLoanApplication(loan);

        // Assert
        Assert.Equal("The credit score must be between 1 and 999", actual.ApplicationResult);
    }

    [Fact]
    [Trait("Requirement", "If the value of the loan is more than £1.5 million or less than £100,000 then the application must be declined")]
    public void WhenCreditScoreIsOneThousandShouldReturnFalse()
    {
        // Arrange        
        LoanApplication loan = new(100_000M, 200_000M, new Applicant(1000));
        LoanService loanService = new(new LoanApplicationValidator());

        // Act
        Statistics actual = loanService.CreateLoanApplication(loan);

        // Assert
        Assert.Equal("The credit score must be between 1 and 999", actual.ApplicationResult);
    }

    [Fact]
    [Trait("Requirement", "If the value of the loan is more than £1.5 million or less than £100,000 then the application must be declined")]
    public void WhenLoanValueIsOneHundredThousandShouldReturnTrue()
    {
        // Arrange        
        LoanApplication loan = new(100_000M, 170_000M, new Applicant(750));
        LoanService loanService = new(new LoanApplicationValidator());

        // Act
        Statistics actual = loanService.CreateLoanApplication(loan);

        // Assert
        Assert.Equal("The loan application was successful", actual.ApplicationResult);
    }

    [Fact]
    [Trait("Requirement", "If the value of the loan is more than £1.5 million or less than £100,000 then the application must be declined")]
    public void WhenLoanValueIsOnePointFiveMillionShouldReturnTrue()
    {
        // Arrange        
        LoanApplication loan = new(1_500_000M, 2_600_000M, new Applicant(950));
        LoanService loanService = new(new LoanApplicationValidator());

        // Act
        Statistics actual = loanService.CreateLoanApplication(loan);

        // Assert
        Assert.Equal("The loan application was successful", actual.ApplicationResult);
    }

    [Fact]
    [Trait("Requirement", "If the value of the loan is more than £1.5 million or less than £100,000 then the application must be declined")]
    public void WhenLoanValueIsGreaterThanOnePointFiveMillionShouldReturnFalse()
    {
        // Arrange
        LoanApplication loan = new(1_500_001M, 200_000M, new Applicant(100));
        LoanService loanService = new(new LoanApplicationValidator());

        // Act
        Statistics actual = loanService.CreateLoanApplication(loan);

        // Assert
        Assert.Equal("The loan value must be between 100,000 and 1,500,000", actual.ApplicationResult);
    }

    [Fact]
    [Trait("Requirement", "If the value of the loan is £1 million or more then the LTV must be 60% or less and the credit score of the applicant must be 950 or more")]
    public void WhenLoanValueIsOneMillionAndLoanToValueIsLessThanSixtyPercentAndCreditScoreIsNineFiftyShouldReturnTrue()
    {
        // Arrange
        LoanApplication loan = new(1_000_000, 1_700_000, new Applicant(950));
        LoanService loanService = new(new LoanApplicationValidator());

        // Act
        Statistics actual = loanService.CreateLoanApplication(loan);

        // Assert
        Assert.Equal("The loan application was successful", actual.ApplicationResult);
    }

    [Fact]
    [Trait("Requirement", "If the value of the loan is £1 million or more then the LTV must be 60% or less and the credit score of the applicant must be 950 or more")]
    public void WhenLoanValueIsOneMillionAndLoanToValueIsSixtyPercentAndCreditScoreIsNineFortyNineShouldReturnFalse()
    {
        // Arrange
        LoanApplication loan = new(1_000_000, 1_600_000, new Applicant(949));
        LoanService loanService = new(new LoanApplicationValidator());

        // Act
        Statistics actual = loanService.CreateLoanApplication(loan);

        // Assert
        Assert.Equal("The loan application was declined", actual.ApplicationResult);
    }

    [Fact]
    [Trait("Requirement", "If the value of the loan is £1 million or more then the LTV must be 60% or less and the credit score of the applicant must be 950 or more")]
    public void WhenLoanValueIsOneMillionAndLoanToValueIsLessThanSixtyPercentAndCreditScoreIsNineFortyNineShouldReturnFalse()
    {
        // Arrange
        LoanApplication loan = new(1_000_000, 1_600_000, new Applicant(949));
        LoanService loanService = new(new LoanApplicationValidator());

        // Act
        Statistics actual = loanService.CreateLoanApplication(loan);

        // Assert
        Assert.Equal("The loan application was declined", actual.ApplicationResult);
    }

    [Fact]
    [Trait("Requirement", "If the value of the loan is less than £1 million then the LTV is less than 60%, the credit score of the applicant must be 750 or more")]
    public void WhenLoanValueIsLessThanOneMillionAndLoanToValueIsLessThanSixtyPercentAndCreditScoreIsSevenFiftyShouldReturnTrue()
    {
        // Arrange
        LoanApplication loan = new(500_000, 835_000, new Applicant(750));
        LoanService loanService = new(new LoanApplicationValidator());

        // Act
        Statistics actual = loanService.CreateLoanApplication(loan);

        // Assert
        Assert.Equal("The loan application was successful", actual.ApplicationResult);
    }

    [Fact]
    [Trait("Requirement", "If the value of the loan is less than £1 million then the LTV is less than 60%, the credit score of the applicant must be 750 or more")]
    public void WhenLoanValueIsLessThanOneMillionAndLoanToValueIsSixtyPercentAndCreditScoreIsSevenFiftyShouldReturnFalse()
    {
        // Arrange
        LoanApplication loan = new(500_000, 830_000, new Applicant(750));
        LoanService loanService = new(new LoanApplicationValidator());

        // Act
        Statistics actual = loanService.CreateLoanApplication(loan);

        // Assert
        Assert.Equal("The loan application was declined", actual.ApplicationResult);
    }

    [Fact]
    [Trait("Requirement", "If the value of the loan is less than £1 million then the LTV is less than 60%, the credit score of the applicant must be 750 or more")]
    public void WhenLoanValueIsLessThanOneMillionAndLoanToValueIsSixtyPercentAndCreditScoreIsSevenFortyShouldReturnFalse()
    {
        // Arrange
        LoanApplication loan = new(500_000, 830_000, new Applicant(740));
        LoanService loanService = new(new LoanApplicationValidator());

        // Act
        Statistics actual = loanService.CreateLoanApplication(loan);

        // Assert
        Assert.Equal("The loan application was declined", actual.ApplicationResult);
    }

    [Fact]
    [Trait("Requirement", "If the value of the loan is less than £1 million then the LTV is less than 80%, the credit score of the applicant must be 800 or more")]
    public void WhenLoanValueIsLessThanOneMillionAndLoanToValueIsLessThanEightyPercentAndCreditScoreIsEightHundredShouldReturnTrue()
    {
        // Arrange
        LoanApplication loan = new(500_000, 628_000, new Applicant(800));
        LoanService loanService = new(new LoanApplicationValidator());

        // Act
        Statistics actual = loanService.CreateLoanApplication(loan);

        // Assert
        Assert.Equal("The loan application was successful", actual.ApplicationResult);
    }

    [Fact]
    [Trait("Requirement", "If the value of the loan is less than £1 million then the LTV is less than 80%, the credit score of the applicant must be 800 or more")]
    public void WhenLoanValueIsLessThanOneMillionAndLoanToValueIsEightyPercentAndCreditScoreIsSevenNineNineShouldReturnFalse()
    {
        // Arrange
        LoanApplication loan = new(500_000, 625_000, new Applicant(799));
        LoanService loanService = new(new LoanApplicationValidator());

        // Act
        Statistics actual = loanService.CreateLoanApplication(loan);

        // Assert
        Assert.Equal("The loan application was declined", actual.ApplicationResult);
    }

    [Fact]
    [Trait("Requirement", "If the value of the loan is less than £1 million then the LTV is less than 80%, the credit score of the applicant must be 800 or more")]
    public void WhenLoanValueIsLessThanOneMillionAndLoanToValueIsLessThanEightyPercentAndCreditScoreIsSevenNineNineShouldReturnFalse()
    {
        // Arrange
        LoanApplication loan = new(500_000, 625_000, new Applicant(799));
        LoanService loanService = new(new LoanApplicationValidator());

        // Act
        Statistics actual = loanService.CreateLoanApplication(loan);

        // Assert
        Assert.Equal("The loan application was declined", actual.ApplicationResult);
    }

    [Fact]
    [Trait("Requirement", "If the value of the loan is less than £1 million then the LTV is less than 90%, the credit score of the applicant must be 900 or more")]
    public void WhenLoanValueIsLessThanOneMillionAndLoanToValueIsLessThanNinetyPercentAndCreditScoreIsNineHundredShouldReturnTrue()
    {
        // Arrange
        LoanApplication loan = new(500_000, 556_000, new Applicant(900));
        LoanService loanService = new(new LoanApplicationValidator());

        // Act
        Statistics actual = loanService.CreateLoanApplication(loan);

        // Assert
        Assert.Equal("The loan application was successful", actual.ApplicationResult);
    }

    [Fact]
    [Trait("Requirement", "If the value of the loan is less than £1 million then the LTV is less than 90%, the credit score of the applicant must be 900 or more")]
    public void WhenLoanValueIsLessThanOneMillionAndLoanToValueIsNinetyPercentAndCreditScoreIsEightNineNineShouldReturnFalse()
    {
        // Arrange
        LoanApplication loan = new(500_000, 560_000, new Applicant(899));
        LoanService loanService = new(new LoanApplicationValidator());

        // Act
        Statistics actual = loanService.CreateLoanApplication(loan);

        // Assert
        Assert.Equal("The loan application was declined", actual.ApplicationResult);
    }

    [Fact]
    [Trait("Requirement", "If the value of the loan is less than £1 million then the LTV is less than 90%, the credit score of the applicant must be 900 or more")]
    public void WhenLoanValueIsLessThanOneMillionAndLoanToValueIsLessThanNinetyPercentAndCreditScoreIsEightNineNineShouldReturnFalse()
    {
        // Arrange
        LoanApplication loan = new(500_000, 560_000, new Applicant(899));
        LoanService loanService = new(new LoanApplicationValidator());

        // Act
        Statistics actual = loanService.CreateLoanApplication(loan);

        // Assert
        Assert.Equal("The loan application was declined", actual.ApplicationResult);
    }

    [Fact]
    [Trait("Requirement", "If the LTV is 90% or more, the application must be declined")]
    public void WhenLoanToValueIsGreaterThanNinetyPercentReturnFalse()
    {
        // Arrange
        LoanApplication loan = new(500_000, 525_000, new Applicant(899));
        LoanService loanService = new(new LoanApplicationValidator());

        // Act
        Statistics actual = loanService.CreateLoanApplication(loan);

        // Assert
        Assert.Equal("The loan to value must be less than 90%", actual.ApplicationResult);
    }
}
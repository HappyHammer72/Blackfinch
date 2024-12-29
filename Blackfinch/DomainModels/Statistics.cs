namespace Blackfinch.DomainModels
{
    public record Statistics(bool ApplicationSuccessful, List<Tuple<bool, int>> ApplicationsToDate, decimal TotalLoanValue, decimal AverageLoanToValue);
}

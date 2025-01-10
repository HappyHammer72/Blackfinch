namespace Blackfinch.DomainModels;

public record Statistics(string ApplicationResult, List<Tuple<bool, int>> ApplicationsToDate, decimal TotalLoanValue, decimal AverageLoanToValue);

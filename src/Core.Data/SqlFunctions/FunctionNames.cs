namespace Core.Data.SqlFunctions
{
    public static class FunctionNames
    {
        public const string OpenRoll = "Core.OpenRoll";
        public const string CloseRoll = "Core.CloseRoll";
        public const string GetAvailableBookings = "Core.GetBookings";
        public const string GetAvailableFees = "Core.GetAvailableFees";
        public const string GetAvailableRolls = "Core.GetAvailableRolls";
        public const string CreateBookingSchedule = "Core.CreateBookingSchedule ";
        public const string GetServices = "Core.GetServices";
        public const string GetServiceDefaultInt = "dbo.GetServiceDefault_Int";
        public const string GetPayPeriodsWithUnsubmittedTimesheets = "Labour.GetPayPeriodsWithUnsubmittedTimesheets";
        public const string GetAccountBalancesInBillingCycle = "Reporting.GetAccountBalancesForAllServicesInBillingCycle";
        public const string GetAccountBalance = "Ledger.GetAccountBalance";
        public const string GetEstimatedUsage = "Ledger.GetEstimatedUsage";
        public const string GetWeeklyAvailability = "Labour.GetWeeklyAvailability";
        public const string GetRecurringAvailability = "Labour.GetRecurringAvailability";
        public const string IsLuhnValid = "Secure.IsLuhnValid";
        public const string GetDocumentsForAccountWithoutContent = "Core.GetDocumentsForAccountWithoutContent";

        public const string GetFee = "Core.GetFee";
    }
}

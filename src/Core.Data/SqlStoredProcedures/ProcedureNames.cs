namespace Core.Data.SqlStoredProcedures {
    public static class ProcedureNames
    {
        public const string ChangeUsername = "aspnet_Users_ChangeUsername";
        public const string HelpEmployee = "helpemployee";
        public const string SchoolAttendanceInformation = "School.AttendanceInformation";
        public const string SchoolStaffWorkingToday = "School.StaffWorkingToday";

        public const string CreatePupilFreeDay = "dbo.CreatePupilFreeDay";
        public const string DeactivatePupilFreeDay = "dbo.DeactivePupilFreeDay";

        public const string GetRosterConfigurationData = "dbo.GetRosterConfigurationData";
        public const string CreateVacationPeriodRosters = "Labour.CreateVacationPeriodRosters";

        public const string ConvertFirstListedExcursionAssistantsIntoFirstAssistants =
            "Labour.ConvertFirstListedExcursionAssistantsIntoFirstAssistants";

        public const string ClearPreviousHolidayClubConfirmations = "Labour.ClearPreviousHolidayClubConfirmations";
        public const string AutoFixEmptyShiftTimes = "Labour.AutoFixEmptyShiftTimes";

        public const string SubmitSavedTimesheets = "Labour.SubmitSavedTimesheets";

        public const string EmployeeSkillsGet = "Core.EmployeeSkillsGet";
        public const string EmployeeInductionTestsGet = "Core.EmployeeInductionTestsGet";
        public const string EmployeeExtendedInfoGet = "Core.EmployeeExtendedInfoGet";

        public const string GeneratePaymentsForBalanceDate = "Secure.GeneratePaymentsForBalanceDate";
        public const string GetPaymentsUpdatedStatusPercentage = "Secure.GetPaymentsUpdatedStatusPercentage";

        public const string GetDeliveryChannelsForAudience = "Messaging.GetDeliveryChannelsForAudience";        
        public const string GetServiceReceiptNumber = "Ledger.GetServiceReceiptNumber";
    }
}

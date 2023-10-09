using DataAccessLayer.Entity;

namespace WebAPI.Repository.TimesheetManagement
{
    public interface ITimesheetRepository
    {
        Task<Contractor> GetContractorById(string id);
        Task<IEnumerable<JobCode>> GetJobCodes();
        Task<IEnumerable<MachineCode>> GetMachineCodes();
        Task AddNewTimesheet(Timesheet timesheet);
        Task DeleteTimesheet(int id);
        Task UpdateTimesheetStatus(int id, string status);
        IEnumerable<Timesheet> GetTimesheetsForContractor(string id);
        Task<Timesheet> GetTimesheetById(int id);
        IEnumerable<LaborEntry> GetLaborEntriesForTimesheet(int id);
        IEnumerable<MachineEntry> GetMachineEntriesForTimesheet(int id);
        IEnumerable<Timesheet> GetAllPendingTimesheets();
        IEnumerable<Timesheet> GetAllApprovedTimesheets();
        int GetTotalHrsForTimesheet(int id);
        double GetTotalAmountForTimesheet(int id);
    }
}

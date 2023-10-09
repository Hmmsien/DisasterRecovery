using DataAccessLayer.Data;
using DataAccessLayer.Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace WebAPI.Repository.TimesheetManagement
{
    public class TimesheetRepository : ITimesheetRepository
    {
        private readonly ApplicationDbContext _context;
        public TimesheetRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Contractor> GetContractorById(string id)
        {
            try
            {
                return await _context.Contractors.FindAsync(id);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<JobCode>> GetJobCodes()
        {
            try
            {
                return await _context.JobCodes.ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<MachineCode>> GetMachineCodes()
        {
            try
            {
                return await _context.MachineCodes.ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task AddNewTimesheet(Timesheet timesheet)
        {
            try
            {
                _context.Timesheets.Add(timesheet);

                foreach (var laborEntry in timesheet.LaborEntries)
                {
                    _context.Add(laborEntry);
                }

                foreach (var machineEntry in timesheet.LaborEntries)
                {
                    _context.Add(machineEntry);
                }

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteTimesheet(int id)
        {
            try
            {
                var timesheet = await _context.Timesheets.FindAsync(id);
                _context.Timesheets.Remove(timesheet);
                await _context.SaveChangesAsync();

                var laborEntries = _context.LaborEntries.Where(l => l.TimesheetID == id).Select(l => l).ToList();
                foreach(var laborEntry in laborEntries)
                {
                    _context.LaborEntries.Remove(laborEntry);
                }
                await _context.SaveChangesAsync();

                var machineEntries = _context.MachineEntries.Where(l => l.TimesheetID == id).Select(l => l).ToList();
                foreach (var machineEntry in machineEntries)
                {
                    _context.MachineEntries.Remove(machineEntry);
                }
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task UpdateTimesheetStatus(int id, string status)
        {
            try
            {
                var timesheet = await _context.Timesheets.FindAsync(id);
                timesheet.Status = status;
                if (status == "Approved")
                {
                    timesheet.Approved = true;
                }
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<Timesheet> GetTimesheetsForContractor(string id)
        {
            try
            {
                return _context.Timesheets.Where(t => t.ContractorID == id).Select(t => t).ToList();
            }
            catch
            {
                throw;
            }
        }

        public async Task<Timesheet> GetTimesheetById(int id)
        {
            try
            {
                return await _context.Timesheets.FindAsync(id);
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<LaborEntry> GetLaborEntriesForTimesheet(int id)
        {
            try
            {
                return _context.LaborEntries.Where(l => l.TimesheetID == id).Select(l => l).ToList();
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<MachineEntry> GetMachineEntriesForTimesheet(int id)
        {
            try
            {
                return _context.MachineEntries.Where(m => m.TimesheetID == id).Select(m => m).ToList();
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<Timesheet> GetAllPendingTimesheets()
        {
            try
            {
                return _context.Timesheets.Where(t => t.Status == "Pending" || t.Status == "Viewed").Select(t => t).ToList();
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<Timesheet> GetAllApprovedTimesheets()
        {
            try
            {
                return _context.Timesheets.Where(t => t.Status == "Approved").Select(t => t).ToList();
            }
            catch
            {
                throw;
            }
        }

        public int GetTotalHrsForTimesheet(int id)
        {
            try
            {
                int totalHrsInLaborEntries = 0;
                IEnumerable<LaborEntry> laborEntries = GetLaborEntriesForTimesheet(id);

                foreach (var laborEntry in laborEntries)
                {
                    totalHrsInLaborEntries += laborEntry.HrsWorked;
                }

                return (totalHrsInLaborEntries);
            }
            catch
            {
                throw;
            }
        }
        public double GetTotalAmountForTimesheet(int id)
        {
            try
            {
                double totalAmountInLaborEntries = 0;
                double totalAmountInMachineEntries = 0;
                IEnumerable<LaborEntry> laborEntries = GetLaborEntriesForTimesheet(id);
                IEnumerable<MachineEntry> machineEntries = GetMachineEntriesForTimesheet(id);

                foreach (var laborEntry in laborEntries)
                {
                    totalAmountInLaborEntries += laborEntry.TotalAmount;
                }
                foreach (var machineEntry in machineEntries)
                {
                    totalAmountInMachineEntries += machineEntry.TotalAmount;
                }

                return (totalAmountInLaborEntries + totalAmountInMachineEntries);
            }
            catch
            {
                throw;
            }
        }
    }
}

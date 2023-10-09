using DataAccessLayer.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Repository.TimesheetManagement;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TimesheetController : ControllerBase
    {
        private readonly ITimesheetRepository _timesheetRepository;

        public TimesheetController(ITimesheetRepository timesheetRepository)
        {
            _timesheetRepository = timesheetRepository;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<Contractor> GetContractorById(string id)
        {
            return await _timesheetRepository.GetContractorById(id);
        }


        [HttpGet]
        public async Task<IEnumerable<JobCode>> GetJobCodes()
        {
            return await _timesheetRepository.GetJobCodes();
        }

        [HttpGet]
        public async Task<IEnumerable<MachineCode>> GetMachineCodes()
        {
            return await _timesheetRepository.GetMachineCodes();
        }

        [HttpPost]
        public async Task AddNewTimesheet([FromBody] Timesheet timesheet)
        {
            if (ModelState.IsValid)
            {
                await _timesheetRepository.AddNewTimesheet(timesheet);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task DeleteTimesheet(int id)
        {
            await _timesheetRepository.DeleteTimesheet(id);
        }

        [HttpPost]
        [Route("{id}")]
        public async Task UpdateTimesheetStatus(int id, [FromBody] string status)
        {
            await _timesheetRepository.UpdateTimesheetStatus(id, status);
        }

        [HttpGet]
        [Route("{id}")]
        public IEnumerable<Timesheet> GetTimesheetsForContractor(string id)
        {
            return _timesheetRepository.GetTimesheetsForContractor(id);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<Timesheet> GetTimesheetById(int id)
        {
            return await _timesheetRepository.GetTimesheetById(id);
        }

        [HttpGet]
        [Route("{id}")]
        public IEnumerable<LaborEntry> GetLaborEntriesForTimesheet(int id)
        {
            return _timesheetRepository.GetLaborEntriesForTimesheet(id);
        }

        [HttpGet]
        [Route("{id}")]
        public IEnumerable<MachineEntry> GetMachineEntriesForTimesheet(int id)
        {
            return _timesheetRepository.GetMachineEntriesForTimesheet(id);
        }

        [HttpGet]
        public IEnumerable<Timesheet> GetAllPendingTimesheets()
        {
            return _timesheetRepository.GetAllPendingTimesheets();
        }

        [HttpGet]
        public IEnumerable<Timesheet> GetAllApprovedTimesheets()
        {
            return _timesheetRepository.GetAllApprovedTimesheets();
        }

        [HttpGet]
        [Route("{id}")]
        public int GetTotalHrsForTimesheet(int id)
        {
            return _timesheetRepository.GetTotalHrsForTimesheet(id);
        }

        [HttpGet]
        [Route("{id}")]
        public double GetTotalAmountForTimesheet(int id)
        {
            return _timesheetRepository.GetTotalAmountForTimesheet(id);
        }
    }
}

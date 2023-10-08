using System;
using DataAccessLayer.Entity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Repository.JobCodeManagement;

namespace WebAPI.Controllers
{
    public class JobCodesController : ControllerBase
    {
        private readonly IJobCodeRepository _jobCodeRepository;

        public JobCodesController(IJobCodeRepository repository)
        {
            _jobCodeRepository = repository;
        }

        [HttpGet]
        [Route("api/JobCode/GetAll")]
        public async Task<IEnumerable<JobCode>> Get()
        {
            return await _jobCodeRepository.GetAllJobCodes();
        }

        [HttpGet]
        [Route("api/JobCode/Get/{id}")]
        public async Task<JobCode> GetById(int id)
        {
            return await _jobCodeRepository.GetJobCode(id);
        }


        [HttpPost]
        [Route("api/JobCode/Create")]
        public async Task CreateAsync([FromBody] JobCode jobCode)
        {
            if (ModelState.IsValid)
            {
                await _jobCodeRepository.AddJobCode(jobCode);
            }
        }

        [HttpPut]
        [Route("api/JobCode/Edit/{id}")]
        public async Task Edit(int id, JobCode jobCode)
        {
            if (ModelState.IsValid)
            {
                await _jobCodeRepository.UpdateJobCode(id, jobCode);
            }
        }

        [HttpDelete]
        [Route("api/JobCode/Delete/{id}")]
        public async Task DeleteComfirmedAsync(int id)
        {
            await _jobCodeRepository.DeleteJobCode(id);
        }
    }
}


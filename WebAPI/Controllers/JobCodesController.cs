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
		[Route("api/JobCode/Get")]
		public async Task<IEnumerable<JobCode>> Get()
		{
			return await _jobCodeRepository.GetAllJobCodes();
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
	}
}


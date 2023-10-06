using System;
using DataAccessLayer.Entity;

namespace WebAPI.Repository.JobCodeManagement
{
	public interface IJobCodeRepository
	{
		Task<IEnumerable<JobCode>> GetAllJobCodes();

		Task<JobCode> GetJobCode(int? id);

		Task AddJobCode(JobCode jobCode);

		Task UpdateJobCode(int id, JobCode jobCode);

		Task DeleteJobCode(int id);
	}
}


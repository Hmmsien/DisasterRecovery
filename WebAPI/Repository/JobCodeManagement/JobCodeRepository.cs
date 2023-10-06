using System;
using DataAccessLayer.Entity;
using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Repository.JobCodeManagement
{
	public class JobCodeRepository : IJobCodeRepository
	{
        private readonly ApplicationDbContext _context;

		public JobCodeRepository(ApplicationDbContext context)
		{
            _context = context;
		}

        public async Task AddJobCode(JobCode jobCode)
        {
            _context.JobCodes.Add(jobCode);

            try
            {
                _context.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public Task DeleteJobCode(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<JobCode>> GetAllJobCodes()
        {
            try
            {
                var jobCodes = await _context.JobCodes.ToListAsync();
                return jobCodes;
            }
            catch
            {
                throw;
            }
        }

        public Task<JobCode> GetJobCode(int? id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateJobCode(int id, JobCode jobCode)
        {
            throw new NotImplementedException();
        }
    }
}


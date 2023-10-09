using System;
using DataAccessLayer.Data;
using DataAccessLayer.Entity;
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

        public async Task DeleteJobCode(int id)
        {
            try
            {
                var jobcode = await _context.JobCodes.FindAsync(id);
                _context.JobCodes.Remove(jobcode);
                _context.SaveChanges();
            }
            catch
            {
                throw;
            }
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

        public async Task<JobCode> GetJobCode(int? id)
        {
            try
            {
                var jobCode = await _context.JobCodes.FindAsync(id);
                if (jobCode == null)
                {
                    return null;
                }
                return jobCode;
            }
            catch
            {
                throw;
            }
        }

        public async Task UpdateJobCode(int id, JobCode jobCode)
        {
            try
            {
                var job = await _context.JobCodes.FindAsync(id);
                if (job != null)
                {
                    job.JobCodeName = jobCode.JobCodeName;
                    job.Description = jobCode.Description;
                    job.HourlyRate = jobCode.HourlyRate;
                    job.MaxHoursPerDay = jobCode.MaxHoursPerDay;
                    _context.SaveChanges();
                }
            }
            catch
            {
                throw;
            }
        }
    }
}


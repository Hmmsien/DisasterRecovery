using DataAccessLayer.Data;
using DataAccessLayer.Entity;
using Microsoft.EntityFrameworkCore;
using WebAPI.Repository.MachineManagement;

namespace WebAPI.Repository.MachineCodeManagement
{
    public class MachineCodeRepository : IMachineCodeRepository
    {
        private readonly ApplicationDbContext _context;
        public MachineCodeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MachineCode>> GetAllMachineCode()
        {
            try
            {
                var machineCodes = await _context.MachineCodes.ToListAsync();
                return machineCodes;
            }
            catch
            {
                throw;
            }
        }

        public async Task Add(MachineCode machineCode)
        {
            try
            {
                _context.Add(machineCode);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task Update(MachineCode machineCode)
        {
            try
            {
                _context.Update(machineCode);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                var machineCode = await _context.MachineCodes.FindAsync(id);
                _context.MachineCodes.Remove(machineCode);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}

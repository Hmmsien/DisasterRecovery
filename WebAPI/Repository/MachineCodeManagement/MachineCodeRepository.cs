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

        public async Task<MachineCode> GetMachineCodeById(int? id)
        {
            try
            {
                MachineCode machineCode = await _context.MachineCodes.FindAsync(id);
                if (machineCode == null) return null;
                return machineCode;
            }
            catch
            {
                throw;
            }
        }

        public bool MachineCodeExists(string name)
        {
            try
            {
                var machineCode = _context.MachineCodes.Where(m => m.MachineCodeName == name).Select(m => m);
                if (machineCode.Any()) return true;
                return false;
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

        public async Task Update(int id, MachineCode machineCode)
        {
            try
            {
                var machine = _context.MachineCodes.Find(id);
                if (machine != null)
                {
                    machine.MachineCodeName = machineCode.MachineCodeName;
                    machine.Description = machineCode.Description;
                    machine.UsageRate = machineCode.UsageRate;
                    machine.MaxHoursPerDay = machineCode.MaxHoursPerDay;
                    await _context.SaveChangesAsync();
                }
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

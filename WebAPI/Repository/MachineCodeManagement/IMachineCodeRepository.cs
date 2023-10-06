using DataAccessLayer.Entity;

namespace WebAPI.Repository.MachineManagement
{
    public interface IMachineCodeRepository
    {
        Task<IEnumerable<MachineCode>> GetAllMachineCode();

        // Task<MachineCode> GetMachineCodeById(int? id);

        Task Add(MachineCode machineCode);
        Task Update(MachineCode machineCode);
        Task Delete(int id);
    }
}

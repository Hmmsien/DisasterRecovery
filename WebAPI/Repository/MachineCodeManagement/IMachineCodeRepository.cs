using DataAccessLayer.Entity;

namespace WebAPI.Repository.MachineManagement
{
    public interface IMachineCodeRepository
    {
        Task<IEnumerable<MachineCode>> GetAllMachineCode();
        Task<MachineCode> GetMachineCodeById(int? id);
        bool MachineCodeExists(string name);
        Task Add(MachineCode machineCode);
        Task Update(int id, MachineCode machineCode);
        Task Delete(int id);
    }
}

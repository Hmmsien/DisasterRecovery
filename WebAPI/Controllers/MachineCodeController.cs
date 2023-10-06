using DataAccessLayer.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebAPI.Repository.MachineManagement;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MachineCodeController : ControllerBase
    {
        private readonly IMachineCodeRepository _machineCodeRepository;

        public MachineCodeController(IMachineCodeRepository machineCodeRepository)
        {
            _machineCodeRepository = machineCodeRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<MachineCode>> Index()
        {
            return await _machineCodeRepository.GetAllMachineCode();
        }

        [HttpPost]
        public async Task Create([FromBody] MachineCode machineCode)
        {
            if(ModelState.IsValid)
            {
                await _machineCodeRepository.Add(machineCode);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task Edit(int? id, MachineCode machineCode)
        {
            if(ModelState.IsValid && id == machineCode.MachineCodeID)
            {
                await _machineCodeRepository.Update(machineCode);
            }  
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task Delete(int id)
        {
            await _machineCodeRepository.Delete(id);
        }

    }
}

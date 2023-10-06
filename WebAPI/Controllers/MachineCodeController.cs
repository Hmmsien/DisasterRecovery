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

        [HttpGet]
        [Route("{id}")]
        public async Task<MachineCode> GetById(int id)
        {
            return await _machineCodeRepository.GetMachineCodeById(id);
        }

        [HttpGet]
        [Route("{name}")]
        public bool AlreadyExist(string name)
        {
            return _machineCodeRepository.MachineCodeExists(name);
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
        public async Task Edit(int id, [FromBody] MachineCode machineCode)
        {
            if(ModelState.IsValid)
            {
                await _machineCodeRepository.Update(id, machineCode);
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

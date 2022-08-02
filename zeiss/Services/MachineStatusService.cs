using zeiss.Models;
using zeiss.Repositories;

namespace zeiss.Services
{
    public class MachineStatusService:IMachineStatusService
    {
        private readonly IAsyncRepository<Machine, string> myMachineRepository;
        public MachineStatusService(IAsyncRepository<Machine, string> machineRepository)
        {
            myMachineRepository = machineRepository;
        }

        public async Task<Machine> FindMachineStatus(string machineId)
        {
            var machine = await myMachineRepository.GetFirstOrDefaultAsync(x => x.Machine_Id == machineId);
            return machine;
        }

        public async Task<IList<Machine>> GetAllMachineStatus()
        {
            var machines = await myMachineRepository.All();
            return machines;
        }
    }
}

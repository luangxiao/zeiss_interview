using zeiss.Models;

namespace zeiss.Services
{
    public interface IMachineStatusService
    {
        Task<Machine> FindMachineStatus(string machineId);

        Task<IList<Machine>> GetAllMachineStatus();
    }
}

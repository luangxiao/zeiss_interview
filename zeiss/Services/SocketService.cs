using zeiss.Models;
using zeiss.Repositories;

namespace zeiss.Services
{
    public class SocketService:ISocketService
    {
        private readonly IAsyncRepository<Socket, string> mySocketRepository;
        private readonly IAsyncRepository<Machine, string> myMachineRepository;
        private readonly IWorkUnit myWorkUnit;
        public SocketService(IAsyncRepository<Socket, string> socketRepository,
            IAsyncRepository<Machine, string> machineRepository,
            IWorkUnit workUnit)
        {
            mySocketRepository = socketRepository;
            myMachineRepository = machineRepository;
            myWorkUnit = workUnit;
        }

        public async Task Add(Socket newSocket)
        {
            await mySocketRepository.AddAsync(newSocket);
            await myWorkUnit.SaveAsync();
        }
    }
}

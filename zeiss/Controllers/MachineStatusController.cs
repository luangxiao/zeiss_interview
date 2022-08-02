using Microsoft.AspNetCore.Mvc;
using zeiss.Models;
using zeiss.Services;

namespace zeiss.Controllers
{
    public class MachineStatusController : ControllerBase
    {

        private readonly IMachineStatusService myMachineStatusService;
        public MachineStatusController(IMachineStatusService machineStatusService)
        {
            myMachineStatusService = machineStatusService;
        }

        [HttpGet("/query")]
        public async Task<ActionResult<Machine>> Query( string machineId)
        {
            try
            {
                return Ok(await myMachineStatusService.FindMachineStatus(machineId));
            }
            catch (Exception exception)
            {
                return Problem(exception.ToString());
            }
        }

        [HttpGet("/all")]
        public async Task<ActionResult<Machine>> All()
        {
            try
            {
                return Ok(await myMachineStatusService.GetAllMachineStatus());
            }
            catch (Exception exception)
            {
                return Problem(exception.ToString());
            }
        }
    }
}

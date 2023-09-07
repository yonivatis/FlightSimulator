using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerAirportFinal.BL.AirportBL;
using ServerAirportFinal.Models;


namespace ServerAirportFinal.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AirportController : ControllerBase
    {
        IAirport _airport;

        public AirportController(IAirport airport) => _airport = airport;


        [HttpGet("getstatus")] //text that say if airport is activated or not 
        public string GetStatus() => _airport.GetMsgStatus();  //send it as an <h1> text


        [HttpGet("getprocesses")]  //table of current processes
        public Task<AirportImage> serialize() => _airport.GetCurrentProcesses();
        

        [HttpGet("gethistorydata")]
        public Task<List<FlightModel>> GetHistoryData() => _airport.GetHistoryData(); //table of all done processes


        [HttpGet("start")] //app starts
        public void Initialize() => _airport.StartAsync(); //stations with airplanes
    }
}

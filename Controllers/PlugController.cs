using Microsoft.AspNetCore.Mvc;
//using m4rcus.TuyaCore;
using com.clusterrr.TuyaNet;
using Newtonsoft.Json;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments;
//using TuyaNet;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Dogstray.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PlugController : ControllerBase
    {
     
        [HttpGet]
        public string Get()
        {

            return "service is Running";

        }
        [HttpGet("On")]
        public async Task<string> TurnON()
        {
        byte[] request;
        TuyaDevice dev = new TuyaDevice(ip: "192.168.18.125", localKey: "e17850a5c6ea99a1", deviceId: "8837085484f3ebe7f92c#1", protocolVersion: TuyaProtocolVersion.V33);
        byte[] req1 = dev.EncodeRequest(TuyaCommand.CONTROL, dev.FillJson("{\"dps\":{\"1\":true}}"));
        request = req1;
        byte[] encryptedResponse = await dev.SendAsync(request);
        TuyaLocalResponse response = dev.DecodeResponse(encryptedResponse);
        Console.WriteLine($"Response JSON: {response.JSON}");
        //get current time and store in Db
        return response.JSON;

        }
        [HttpGet("OFF")]
        public async Task<string> TurnOFF()
        {
            byte[] request;
            TuyaDevice dev = new TuyaDevice(ip: "192.168.18.125", localKey: "e17850a5c6ea99a1", deviceId: "8837085484f3ebe7f92c#1", protocolVersion: TuyaProtocolVersion.V33);
            byte[] req2 = dev.EncodeRequest(TuyaCommand.CONTROL, dev.FillJson("{\"dps\":{\"1\":false}}"));
            request = req2;
            byte[] encryptedResponse = await dev.SendAsync(request);
            TuyaLocalResponse response = dev.DecodeResponse(encryptedResponse);
            Console.WriteLine($"Response JSON: {response.JSON}");
            return response.JSON;

        }


    }
}

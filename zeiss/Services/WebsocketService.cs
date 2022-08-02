using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using zeiss.DBContext;
using zeiss.Models;

namespace zeiss.Services
{
    public class WebsocketService:BackgroundService
    {
        
        private static readonly string Connection = "wss://machinestream.herokuapp.com/ws";
        private readonly IConfiguration myConnectionString;

        public WebsocketService(IConfiguration configRoot)
        {
            myConnectionString = configRoot;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
                using (var socket = new ClientWebSocket())
                    try
                    {
                        await socket.ConnectAsync(new Uri(Connection), stoppingToken);

                        await Receive(socket, stoppingToken);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"ERROR - {ex.Message}");
                    }
        }

        private async Task Receive(ClientWebSocket socket, CancellationToken stoppingToken)
        {
            var buffer = new ArraySegment<byte>(new byte[2048]);
            while (!stoppingToken.IsCancellationRequested)
            {
                using var ms = new MemoryStream();
                WebSocketReceiveResult result;
                do
                {
                    result = await socket.ReceiveAsync(buffer, stoppingToken);
                    ms.Write(buffer.Array, buffer.Offset, result.Count);
                } while (!result.EndOfMessage);

                if (result.MessageType == WebSocketMessageType.Close)
                    break;

                ms.Seek(0, SeekOrigin.Begin);
                using var reader = new StreamReader(ms, Encoding.UTF8);
                var returnValue = await reader.ReadToEndAsync();
                Console.WriteLine(returnValue);
                Socket? machineSocket = JsonSerializer.Deserialize<Socket>(returnValue, new JsonSerializerOptions(){PropertyNameCaseInsensitive = true});
                if (machineSocket != null)
                {
                    
                    Console.WriteLine(machineSocket.Event);
                    var contextOptions = new DbContextOptionsBuilder<ZeissContext>()
                        .UseSqlServer(myConnectionString.GetConnectionString("ZeissContext"))
                        .Options;
                    using (var context = new ZeissContext(contextOptions))
                    {
                        context.Sockets.Add(machineSocket);
                        context.SaveChanges();
                    }
                }
            };
        }
    }
}

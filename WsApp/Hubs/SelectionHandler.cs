using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WsApp.Controllers;
using WsApp.Models;

namespace WsApp
{
    public class SelectionHandler : Hub
    {
        public static Context _context;

        public SelectionHandler(Context context)
        {
            _context = context;
        }

        public async Task AddPlayer(string socketId, string selection)
        {
            //Kazkaip iskviesti PlayersController metoda create
            //PlayersController.UseCreate(socketId);
            await Clients.All.SendAsync("AddInformation", socketId, selection);
        }
        public async Task SendSelection(string socketId, string selection)
        {
            await Clients.All.SendAsync("pingSelection", socketId, selection);
        }
        public Task SelectShipType(string selection)
        {
            var socketId = Context.ConnectionId;
            //PlayersController p = new PlayersController(context);
            //bool tmp = false;
            //tmp = p.CreatePlayer(socketId);
            //if (tmp == true)
            //{
            //    await InvokeClientMethodToAllAsync("pingCreatedPlayer", socketId);
            //}
            //else await InvokeClientMethodToAllAsync("pingFullArena", socketId);
            //await InvokeClientMethodToAllAsync("pingSelectedShipType", socketId, selection);
            //if (SearchPlayerBySocketId(socketId) == false)
            //{
            PlayersController p = new PlayersController(_context);
            bool created = p.CreatePlayer(socketId, selection);
            if (tmp == true)
            {
                await InvokeClientMethodToAllAsync("pingCreatedPlayer", socketId);
            }
            //Player tempPlayer = new Player();
            //tempPlayer.Username = socketId;
            //_context.Players.Add(tempPlayer);
            //_context.SaveChanges();
            return Clients.All.SendAsync("pingCreatedPlayer", socketId, selection);
            //}
        }
        public async Task MetodasKurisPadedaLaiva(string socketId, string row, string col)
        {

            await Clients.All.SendAsync("pingSelection", socketId, row, col);
        }
        public async Task SendAttack(string socketId, string row, string col)
        {
            await Clients.All.SendAsync("pingAttack", socketId, row, col);
        }
    }
}

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
        public Context _context;
        private PlayersController playersController;
        private BAController baController;
        private CellsController cellsController;
        private ShipTypeController shipTypeController;
        public SelectionHandler(Context context)
        {
            _context = context;
            playersController = new PlayersController(_context);
            baController = new BAController(_context);
            cellsController = new CellsController(_context);
            shipTypeController = new ShipTypeController(_context);
            
        }

        public async Task AddPlayer(string socketId, string selection)
        {
            await Clients.All.SendAsync("AddInformation", socketId, selection);
        }

        public async Task SelectShipType(string type, int count)

        {
            var socketId = Context.ConnectionId;
            Console.WriteLine("ATEINA TYPE " +type);
            int shipCount = shipTypeController.GetCount(type);
            int sizeCount = shipTypeController.GetSize(type);
            Console.WriteLine("Skaicius " + shipCount + " " + count);
            if (shipCount == count)
            {
                await Clients.All.SendAsync("pingDisableType", socketId, type, sizeCount);
            }
            else
                await Clients.All.SendAsync("pingSelectedShipType", socketId, type, sizeCount);
            //patikrint kelinta sito konkretaus type laiva jau deda, jei paskutinis tai disablint buttona

        }
        public async Task MetodasKurisPadedaLaiva(string socketId, string row, string col)
        {

            await Clients.All.SendAsync("pingSelection", socketId, row, col);
        }
        public async Task SendAttack(string socketId, string row, string col)
        {
            await Clients.All.SendAsync("pingAttack", socketId, row, col);
        }

        //messages :D
        public Task SendMessage(string message)
        {
            int userId = playersController.GetPlayerId(Context.ConnectionId);
            return Clients.All.SendAsync("pingMessage", userId, message);
        }

        //cia reik ne tik player sukurt lentele bet DB sutvarkyt kad ir battle arena ir viskas butu
        public override async Task OnConnectedAsync()
        {
            var socketId = Context.ConnectionId;
            int createdId = playersController.CreatePlayer(socketId);
            Console.WriteLine("PLAYER ID"+createdId);
            int CreatedBAId = baController.CreateBA(createdId);
            Console.WriteLine("BA ID" + CreatedBAId);
            bool editedBAId= playersController.AddPlayerID(createdId, CreatedBAId);
            bool addedCells = cellsController.CreateCells(CreatedBAId);
           // bool edited = baController.AddBoardID(CreatedBAId, boardId);
            //edit BA with boardIDs
            await Clients.All.SendAsync("UserConnected", Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        //but this is not a perfect world :3
        public string GetConnectionId()
        {
            return Context.ConnectionId;
        }
    }
}

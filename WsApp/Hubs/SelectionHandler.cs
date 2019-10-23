using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
        private ShipsController shipsController;
        private DuelsController duelsController;
        private ShipSelectionController shipSelectionController;

        public SelectionHandler(Context context)
        {
            _context = context;
            playersController = new PlayersController(_context);
            baController = new BAController(_context);
            cellsController = new CellsController(_context);
            shipTypeController = new ShipTypeController(_context);
            shipsController = new ShipsController(_context);
            duelsController = new DuelsController(_context);
            shipSelectionController = new ShipSelectionController(_context);

        }

        public async Task AddPlayer(string socketId, string selection)
        {
            await Clients.All.SendAsync("AddInformation", socketId, selection);
        }

        public async Task SelectShipType(string type, string buttonId)
        {
            var socketId = Context.ConnectionId;
            Player player = playersController.GetPlayer(socketId);
            ShipType shipType = shipTypeController.GetShipType(type);
            shipSelectionController.MarkSelection(shipType, player, buttonId);

            await Clients.Caller.SendAsync("pingDisable", buttonId[6]);
        }
        public async Task Atack(string row, string col)
        {
            var socketId = Context.ConnectionId;
            int posX = Int32.Parse(row);
            int posY = Int32.Parse(col);
             
            if (playersController.IsPlayersTurn(socketId) == false)
            {
                await Clients.Caller.SendAsync("invalidTurn", row, col);
            }
            else
            {
                Player atacker = playersController.GetPlayer(socketId);
                int battleArenaId = baController.GetBAId(atacker.PlayerId);
                int attackedCellId = cellsController.AttackCell(posX, posY, battleArenaId); 
                if (attackedCellId ==-2)
                {
                    await Clients.Caller.SendAsync("cellHasBeenAttacked", row, col);
                }
                if (attackedCellId ==-1)
                {
                    //pakeisti turna kitam zaidejui ir parasyt pranesima
                    duelsController.ChangeTurns(socketId);
                    await Clients.Caller.SendAsync("shotMissed");
                    await Clients.Others.SendAsync("changedTurn");

                }
                else
                {
                    shipsController.AttackShip(attackedCellId);
                }
                
            }
        }
        public async Task PlaceShipValidation(string row, string col)
        {
            var socketId = Context.ConnectionId;
            int posX = Int32.Parse(row);
            int posY = Int32.Parse(col);

            if (!shipSelectionController.IsValid(socketId))
            {
                await Clients.Caller.SendAsync("invalidSelection", row, col);
            }
            else
            {
                int battleArenaId = baController.GetBAId(playersController.GetPlayerId(socketId));
                Cell cell = cellsController.ReturnCell(posX, posY, battleArenaId);
                List<Cell> cells = cellsController.ReturnAllCells(battleArenaId);

                bool canPlace = shipSelectionController.ValidatePlacement(socketId, posX, posY, cell, cells);

                if (canPlace)
                {
                    bool placed = shipSelectionController.PlaceShip(socketId);
                    if (placed)
                    {
                        await Clients.Caller.SendAsync("pingShipPlaced", row, col);
                    }
                    else
                    {
                        string id = shipSelectionController.GetButtonId(socketId);
                        await Clients.Caller.SendAsync("pingShipPlaced", row, col);
                        await Clients.Caller.SendAsync("pingRemove", id);
                    }
                }
                else
                {
                    await Clients.Caller.SendAsync("invalidSelection", row, col);
                }
            }
        }

        public async Task RemoveShipType()
        {
            string type = "A1";
            int shipCount = 1000;
            if (shipCount == 0)
            {
                await Clients.Caller.SendAsync("pingRemoveType", type);
            }
        }
        public async Task Ready()
        {
            var socketId = Context.ConnectionId;
            int playerId = playersController.GetPlayerId(socketId);
            int battleArenaId = baController.GetBAId(playerId);

            int dualCount = duelsController.CountDuels();
            Console.WriteLine(dualCount);
            if (dualCount == 0)
            {
                duelsController.StartDuel(socketId, battleArenaId);
                await Clients.Caller.SendAsync("PingWaitingOponent", socketId);
            }
            if (dualCount == 1)
            {
                bool rez = duelsController.JoinDuel(socketId, battleArenaId);
                if (rez == false)
                {
                    await Clients.Caller.SendAsync("PingFullDual", socketId);
                }
                await Clients.All.SendAsync("PingGameIsStarted", socketId);
            }
            
        }
        public async Task MetodasKurisPadedaLaiva(string row, string col, string shipType)
        {
            string socketId = Context.ConnectionId;
            int posX = Int32.Parse(row);
            int posY = Int32.Parse(col);
            int playerId = playersController.GetPlayerId(socketId);
            Console.WriteLine("Player Socket " + socketId);
            int battleArenaId = baController.GetBAId(playerId);
            int cellId = cellsController.ReturnCellId(posX, posY, battleArenaId);
            Console.WriteLine("Koordinates " + posX + " " + posY + "Cell ID " + cellId);
            int shipTypeId = shipTypeController.GetId(shipType);
            bool isAddedShip = shipsController.AddShip(cellId, shipTypeId, shipType);
            Console.WriteLine(isAddedShip);
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
            // Console.WriteLine("PLAYER ID"+createdId);
            int CreatedBAId = baController.CreateBA(createdId);
            // Console.WriteLine("BA ID" + CreatedBAId);
            bool editedBAId = playersController.AddPlayerID(createdId, CreatedBAId);
            bool addedCells = cellsController.CreateCells(CreatedBAId);
            // bool edited = baController.AddBoardID(CreatedBAId, boardId);
            //edit BA with boardIDs
            await Clients.All.SendAsync("UserConnected", Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        //but this is not a perfect world :3
        //public string GetConnectionId()
        //{
        //    return Context.ConnectionId;
        //}
    }
}

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
        private ArmorSelectionController armorSelection;

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
            armorSelection = new ArmorSelectionController(_context);
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

            if (duelsController.isPlayerTurn(socketId) == false)
            {
                await Clients.Caller.SendAsync("invalidTurn", row, col);
            }
            else
            {
                string enemySockeId = duelsController.GetOpponentSocketId(socketId);
                bool didHit = cellsController.AttackCell(posX, posY, socketId);
                if (didHit == false)
                {
                    duelsController.ChangeTurns(socketId);
                    await Clients.Client(enemySockeId).SendAsync("pingAttack", row, col, false, false);
                    await Clients.Caller.SendAsync("pingAttack", row, col, false, true);
                    //await Clients.Others.SendAsync("changedTurn");
                }
                else
                {
                    await Clients.Client(enemySockeId).SendAsync("pingAttack", row, col, true, false);
                    await Clients.Caller.SendAsync("pingAttack", row, col, true, true);
                }
            }
        }

        public bool SelectArmor()
        {
            string id = Context.ConnectionId;
            if (armorSelection.IsArmorSelected(id))
            {
                armorSelection.Deselect(id);
                return false;
            }
            else
            {                
                armorSelection.AddArmorSelection(id);
            }
            return true;
        }

        public async Task PlaceShipValidation(string row, string col)
        {
            var socketId = Context.ConnectionId;
            int posX = Int32.Parse(row);
            int posY = Int32.Parse(col);
            int battleArenaId = baController.GetBAId(playersController.GetPlayerId(socketId));

            //armor placement
            if (armorSelection.IsArmorSelected(socketId))
            {
                bool armored = armorSelection.ArmorUp(posX, posY, battleArenaId, socketId);
                if (armored)
                {
                    await Clients.Caller.SendAsync("pingShipPlaced", row, col, true);
                }
                else
                {
                    await Clients.Caller.SendAsync("invalidSelection", row, col);
                }
            }
            //ship placement | cant place a ship if armor function is selected
            else
            {
                if (!shipSelectionController.IsValid(socketId))
                {
                    await Clients.Caller.SendAsync("invalidSelection", row, col);
                }
                else
                {                    
                    bool canPlace = shipSelectionController.ValidatePlacement(socketId, posX, posY, battleArenaId);

                    if (canPlace)
                    {

                        bool placed = shipSelectionController.PlaceShip(socketId);
                        if (placed)
                        {
                            await Clients.Caller.SendAsync("pingShipPlaced", row, col, false);
                        }
                        else
                        {
                            string id = shipSelectionController.GetButtonId(socketId);
                            await Clients.Caller.SendAsync("pingShipPlaced", row, col, false);
                            await Clients.Caller.SendAsync("pingRemove", id);
                        }
                    }
                    else
                    {
                        await Clients.Caller.SendAsync("invalidSelection", row, col);
                    }
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

        public async Task ReadySingleton()
        {
            var socketId = Context.ConnectionId;
            int playerId = playersController.GetPlayerId(socketId);
            int battleArenaId = baController.GetBAId(playerId);

            Duel duel = duelsController.CreateDuel();
            if (duel.FirstPlayerSocketId == null)
            {
                duel.FirstPlayerSocketId = socketId;
                duel.FirstPlayerBAId = battleArenaId;
                _context.SaveChanges();
                await Clients.Caller.SendAsync("pingMessage", playerId, "Waiting for opponent...");
            }
            else if (duel.FirstPlayerSocketId != null && duel.SecondPlayerSocketId == null)
            {
                duel.SecondPlayerSocketId = socketId;
                duel.SecondPlayerBAId = battleArenaId;
                _context.SaveChanges();
                string firstTurnSocketId = duelsController.SetTurn(duel);
                await Clients.All.SendAsync("pingMessage", null, "The game has started!");
                await Clients.All.SendAsync("pingGameStarted");
            }
            else
            {
                await Clients.Caller.SendAsync("pingMessage", playerId, "No available duels at the moment...");
            }
        }
        //public async Task Ready()
        //{
        //    var socketId = Context.ConnectionId;
        //    int playerId = playersController.GetPlayerId(socketId);
        //    int battleArenaId = baController.GetBAId(playerId);

        //    int dualCount = duelsController.CountDuels();
        //    Console.WriteLine(dualCount);
        //    if (dualCount == 0)
        //    {
        //        duelsController.StartDuel(socketId, battleArenaId);
        //        await Clients.Caller.SendAsync("PingWaitingOponent", socketId);
        //    }
        //    if (dualCount == 1)
        //    {
        //        bool rez = duelsController.JoinDuel(socketId, battleArenaId);
        //        if (rez == false)
        //        {
        //            await Clients.Caller.SendAsync("PingFullDual", socketId);
        //        }
        //        await Clients.All.SendAsync("PingGameIsStarted", socketId);
        //    }
        //}


        //messages
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
            int CreatedBAId = baController.CreateBA(createdId);
            bool editedBAId = playersController.AddPlayerID(createdId, CreatedBAId);
            await Clients.All.SendAsync("UserConnected", Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var socketId = Context.ConnectionId;
            duelsController.removeDuel(socketId);
            return base.OnDisconnectedAsync(exception);
        }

        public void DeleteDuels()
        {
            duelsController.deleteDuels();
        }
    }
}

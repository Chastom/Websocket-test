using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WsApp.Commands;
using WsApp.Controllers;
using WsApp.Models;
using WsApp.Strategies;
using WsApp.Template;
using WsApp.Iterators;

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
        private StrategyController strategyController;
        private PlaceShipSelection placeShipSelection;
        private PlaceArmorSelection placeArmorSelection;

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
            strategyController = new StrategyController(_context);
            placeShipSelection = new PlaceShipSelection(shipSelectionController, _context);
            placeArmorSelection = new PlaceArmorSelection(armorSelection, _context);
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

            await Clients.Caller.SendAsync("pingDisable", buttonId);
        }

        public void SelectStrategy(string strategy)
        {
            string socketId = Context.ConnectionId;
            switch (strategy)
            {
                case "Laser":
                    StrategyHolder.ChangeActiveStrategy(socketId, new LaserAttack());
                    break;
                case "Bomb":
                    StrategyHolder.ChangeActiveStrategy(socketId, new BombAttack());
                    break;
                case "Cross":
                    StrategyHolder.ChangeActiveStrategy(socketId, new CrossAttack());
                    break;
                //default case always sets strategy to Basic Attack
                default:
                    StrategyHolder.ChangeActiveStrategy(socketId, new BasicAttack());
                    break;
            }
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

                //returns active caller's strategy
                Strategy activeStrategy = StrategyHolder.GetPlayerStrategy(socketId);
                //executing the strategy returns all affected cells as AttackOutcome and cell coordinates
                List<CellOutcome> outcomes = strategyController.Attack(posX, posY, socketId);

                //for every outcome we inform both players
                foreach(CellOutcome outcome in outcomes)
                {
                    switch (outcome.attackOutcome)
                    {
                        case AttackOutcome.Hit:
                            await Clients.Client(enemySockeId).SendAsync("pingAttack", outcome.posX, outcome.posY, "Hit", false);
                            await Clients.Caller.SendAsync("pingAttack", outcome.posX, outcome.posY, "Hit", true);
                            break;
                        case AttackOutcome.Armor:
                            duelsController.ChangeTurns(socketId);
                            await Clients.Client(enemySockeId).SendAsync("pingAttack", outcome.posX, outcome.posY, "Armor", false);
                            await Clients.Caller.SendAsync("pingAttack", outcome.posX, outcome.posY, "Armor", true);
                            break;
                        //Missed and Invalid realization not finished...
                        //For now, both trigger default switch
                        default:
                            duelsController.ChangeTurns(socketId);
                            await Clients.Client(enemySockeId).SendAsync("pingAttack", outcome.posX, outcome.posY, "Missed", false);
                            await Clients.Caller.SendAsync("pingAttack", outcome.posX, outcome.posY, "Missed", true);
                            //await Clients.Others.SendAsync("changedTurn");
                            break;
                    }
                }  
            }
        }

        public async Task SelectArmor()
        {
            string socketId = Context.ConnectionId;
            if (armorSelection.IsArmorSelected(socketId))
            {
                armorSelection.Deselect(socketId);
            }
            else
            {
                armorSelection.AddArmorSelection(socketId);
            }
            int count = armorSelection.GetArmorCount(socketId);
            await Clients.Caller.SendAsync("pingArmorCount", count);
        }

        public async Task CommandUndo()
        {
            var socketId = Context.ConnectionId;
            UndoResult result = placeShipSelection.Undo(socketId);
            List<int> buttons = result.removedButtons;
            foreach(var btn in buttons)
            {
                Console.WriteLine("=============================================================== ->>>>>>>> button" + btn);
            }
            int[] btns = buttons.ToArray();
            if(result != null)
            {
                await Clients.Caller.SendAsync("undoShip", result.coordinate.x, result.coordinate.y);
                await Clients.Caller.SendAsync("undoButtons", result.activeButton, btns);
            }
        }

        public async Task PlaceShipValidation(string row, string col)
        {
            var socketId = Context.ConnectionId;
            int posX = Int32.Parse(row);
            int posY = Int32.Parse(col);
            int battleArenaId = baController.GetBAId(playersController.GetPlayerId(socketId));

            SelectionParams param = new SelectionParams(socketId, posX, posY, battleArenaId);            
            CommandOutcome commandOutcome = null;

            //depending on client's selection, executing one of the two commands: place [ armor | ship ] selection
            commandOutcome = armorSelection.IsArmorSelected(socketId) ? placeArmorSelection.Execute(param) : placeShipSelection.Execute(param);         

            switch (commandOutcome.outcome)
            {
                case PlacementOutcome.Armor:
                    await Clients.Caller.SendAsync("pingArmorCount", commandOutcome.count);
                    await Clients.Caller.SendAsync("pingShipPlaced", row, col, true);
                    break;
                case PlacementOutcome.Ship:
                    await Clients.Caller.SendAsync("pingShipPlaced", row, col, false);
                    break;
                case PlacementOutcome.LastShip:
                    await Clients.Caller.SendAsync("pingShipPlaced", row, col, false);
                    await Clients.Caller.SendAsync("pingRemove", commandOutcome.idToRemove);
                    break;
                case PlacementOutcome.Invalid:
                    await Clients.Caller.SendAsync("invalidSelection", row, col);
                    break;
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

        //messages
        public Task SendMessage(string message)
        {
            int userId = playersController.GetPlayerId(Context.ConnectionId);
            return Clients.All.SendAsync("pingMessage", userId, message);
        }

        public override async Task OnConnectedAsync()
        {
            var socketId = Context.ConnectionId;
            int createdId = playersController.CreatePlayer(socketId);
            int CreatedBAId = baController.CreateBA(createdId);
            bool editedBAId = playersController.AddPlayerID(createdId, CreatedBAId);

            //setting Strategy for newly connected player to -> BasicAttack
            StrategyHolder.AddStrategy(socketId, new BasicAttack());
        
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

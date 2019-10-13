﻿using Microsoft.AspNetCore.SignalR;
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

        public SelectionHandler(Context context)
        {
            _context = context;
            playersController = new PlayersController(_context);
        }

        public async Task AddPlayer(string socketId, string selection)
        {
            await Clients.All.SendAsync("AddInformation", socketId, selection);
        }

        public Task SelectShipType(string type)
        {
            var socketId = Context.ConnectionId;
            //patikrint kelinta sito konkretaus type laiva jau deda, jei paskutinis tai disablint buttona
            return Clients.All.SendAsync("pingDisableType", socketId, type);
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
            bool created = playersController.CreatePlayer(socketId);
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

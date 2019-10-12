using System.Threading.Tasks;
using WebSocketManager;
using WsApp.Controllers;
using WsApp.Models;

namespace WsApp
{
    public class SelectionHandler : WebSocketHandler
    {
        public SelectionHandler(WebSocketConnectionManager webSocketConnectionManager) : base(webSocketConnectionManager)
        {
        }
        
        public async Task AddPlayer(string socketId, string selection)
        {
            //Kazkaip iskviesti PlayersController metoda create
            //PlayersController.UseCreate(socketId);
            await InvokeClientMethodToAllAsync("AddInformation", socketId, selection);
        }
        public async Task SendSelection(string socketId, string selection)
        {
            await InvokeClientMethodToAllAsync("pingSelection", socketId, selection);
        }
        public async Task SelectShipType(string socketId, string selection)
        {

          //  PlayersController p = new PlayersController();
          //  bool tmp = false;
          //tmp = p.CreatePlayer(socketId);
          //  if (tmp == true)
          //  {
          //      await InvokeClientMethodToAllAsync("pingCreatedPlayer", socketId);
          //  }
          //  else await InvokeClientMethodToAllAsync("pingFullArena", socketId);
            await InvokeClientMethodToAllAsync("pingSelectedShipType", socketId, selection);
        }
        public async Task MetodasKurisPadedaLaiva(string socketId, string row, string col)
        {
            
            await InvokeClientMethodToAllAsync("pingSelection", socketId, row, col);
        }
        public async Task SendAttack(string socketId, string row, string col)
        {
            await InvokeClientMethodToAllAsync("pingAttack", socketId, row, col);
        }
    }
}

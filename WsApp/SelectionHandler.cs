using System.Threading.Tasks;
using WebSocketManager;

namespace WsApp
{
    public class SelectionHandler : WebSocketHandler
    {
        public SelectionHandler(WebSocketConnectionManager webSocketConnectionManager) : base(webSocketConnectionManager)
        {
        }

        public async Task SendSelection(string socketId, string selection)
        {
            await InvokeClientMethodToAllAsync("pingSelection", socketId, selection);
        }
    }
}

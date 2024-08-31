using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SignalR
{
    public class ConnectionMappingService
    {
        private readonly Dictionary<Guid, string> _connections = new Dictionary<Guid, string>();
        //private readonly Dictionary<Guid, HashSet<string>> _connections = new Dictionary<Guid, HashSet<string>>();


        public void AddConnection(Guid userId, string connectionId)
        {
            _connections[userId] = connectionId;
        }

        public void RemoveConnection(Guid userId)
        {
            _connections.Remove(userId);
        }

        public string GetConnectionId(Guid userId)
        {
            if (_connections.TryGetValue(userId, out var connectionId))
            {
                return connectionId;
            }

            return null;
        }


    }
}

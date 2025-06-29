using System;
using System.Collections.Generic;

namespace AICalendar.MCP
{
    public class MCPServer
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int Port { get; set; }
    }

    public static class MCPServerManager
    {
        private static readonly List<MCPServer> Servers = new();

        public static IEnumerable<MCPServer> GetAll() => Servers;

        public static MCPServer Add(MCPServer server)
        {
            Servers.Add(server);
            return server;
        }
    }
}

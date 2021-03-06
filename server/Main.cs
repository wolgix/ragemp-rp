using System;
using GTANetworkAPI;

namespace Project.Server
{
    public class Main : Script
    {
        [ServerEvent(Event.ResourceStart)]
        public void OnResourceStart()
        {
            using (var database = new Database())
            {
                Console.WriteLine($"Connected to database: {database.Database.CanConnect()}");
            }
        }
    }
}

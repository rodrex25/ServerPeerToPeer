using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerPeerToPeer

{
    public class Program { 
    
        public static Server server = new Server();

        public static void Main(string[] args)
        {
            server.initializeServer();

            while (true)
            {
                server.userConecting();

            }

        }
    }

}

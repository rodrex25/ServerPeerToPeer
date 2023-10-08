using libreriaCompartida;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace ServerPeerToPeer
{
    internal class Node
    {

        private static User user;


        private static TcpClient tcpClient;

        public Node(User user1, TcpClient tcpClient1) {

            user = user1;
            tcpClient = tcpClient1;
            
        }

        public User getUser()
        {
            return user;
            
        }

        public TcpClient getTcpClient()
        {
            return tcpClient;

        }




    }
}

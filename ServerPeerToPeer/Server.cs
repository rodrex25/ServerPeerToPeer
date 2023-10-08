using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//sockets
using libreriaCompartida;
using System.Net;
using System.Net.Sockets;
//serializacion
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.CompilerServices;

namespace ServerPeerToPeer

{
   

    public class Server
    {
        //socket tcp listener
        TcpListener server;
        private int port = 1234;

        //localhost
        static string ipv4Address = "192.168.100.14";
        private IPAddress serverIpAddress = IPAddress.Parse(ipv4Address);
        //
        //Dictionary<string, string> users = new Dictionary<string, string>();

        private static List<Node> nodesConnected = new List<Node>();


        //constructor
        public Server()
        {

        }

        //methods

        public void initializeServer()
        {
            try
            {
                this.server = new TcpListener(serverIpAddress, port);
                server.Start();
                Console.WriteLine("Server> this server is listeting");

            }catch (Exception ex)
            {

                Console.WriteLine("Error to connections: " + ex.Message);

            }
            

        }

        public void userConecting()
        {
            
            //get host flow
            TcpClient client = server.AcceptTcpClient();
            Thread clientThread = new Thread(new ParameterizedThreadStart(handleClientConn));
            clientThread.Start(client);

        }

        private static void  handleClientConn(Object client) {


                TcpClient tcpClient = (TcpClient)client;

                NetworkStream stream = tcpClient.GetStream();
            try {
                //deserializar objeto

                IFormatter formatter = new BinaryFormatter();


                User user = (User)formatter.Deserialize(stream);

                //save user and ip in the list
                nodesConnected.Add(new Node(user, tcpClient));

                //SHOW CONNECTED USERS
                Console.WriteLine("Server> user conected");

                Console.WriteLine("user: " + user.UserName + " ip: " + user.IpAddress);

                foreach (Node node in nodesConnected)
                 {
                    
                     Console.WriteLine("Node User: "+node.getUser().UserName);
                     Console.WriteLine("Node ip: " + node.getUser().IpAddress);
                    Console.WriteLine("--------------------------------------");

                }
                 
                //enviar lista a todos los tcp client conectados
                sendNodesList(nodesConnected, user);

                /*foreach (TcpClient client1 in connectedNodes)
                { 

                    Console.WriteLine(client1.Connected);

                }*/
            }catch(Exception ex) { Console.WriteLine(ex.Message); }








        }

        private static void sendNodesList(List<Node> nodesConnected, User user) {

            try {

                foreach (Node node in nodesConnected)
                {

                    

                    TcpClient client = node.getTcpClient();

                    NetworkStream stream = client.GetStream();

                    IFormatter formatter = new BinaryFormatter();

                        

                    //send serialice object

                        formatter.Serialize(stream, user);

                        Console.WriteLine("lista actualizada");

                    

                    

                    
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error al enviar la lista de usuarios: " + ex.Message);

            }






        }

       


    }
}

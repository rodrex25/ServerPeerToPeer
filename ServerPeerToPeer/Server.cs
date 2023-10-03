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
        Dictionary<string, string> users = new Dictionary<string, string>();

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
            using (TcpClient client = server.AcceptTcpClient())
            {

                Console.WriteLine("Server> client conected");

                using (NetworkStream stream = client.GetStream())
                {
                    //deserializar objeto
                    IFormatter formatter = new BinaryFormatter();


                    User user = (User)formatter.Deserialize(stream);

                    //save user and ip in the dictionary
                    users[user.UserName] = user.IpAddress;

                    //SHOW CONNECTED USERS
                    Console.WriteLine("Server> users conected");

                    foreach (var entry in users) {

                        Console.WriteLine($"Server> Nombre de usuario: {entry.Key}, Dirección IP: {entry.Value}");
                    }

                }



            }


        }

       


    }
}

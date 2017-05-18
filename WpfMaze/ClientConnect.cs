using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace WpfMaze
{
    /// <summary>
    /// Class: ClientConnect
    /// </summary>
    public class ClientConnect
    {
        private Thread senderThread; // Main Thread.
        private Task recieveThread; // Task that receives data from server.
        private static bool isConnect = false; // Indicates connection between client - server.
        private Queue<string> commandQueue;
        private Queue<string> resultQueue;
        /// <summary>
        /// Connects to the server by the specified port.
        /// </summary>
        /// <param name="port">The port.</param>
        public void Connect()
        {
            // Define the end point connection.
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
            // Update all the parameters to null.
            TcpClient client = null;
            NetworkStream stream = null;
            StreamReader reader = null;
            StreamWriter writer = null;
            commandQueue = new Queue<string>();
            resultQueue = new Queue<string>();

            /* Delegate function that returns always void. Handles the receive data
               from the server. If the result from server is "singlePlayer" keeps the connection
               open. If the result from server is "multiPlayer" closes the connection. */
            Action recieveData = new Action(() =>
           {
               while(true)
               {
                   try
                   {
                       // Get data from the server.
                       string result = reader.ReadLine();
                       
                       // Close the connect with the server.
                       if (result.Contains("singlePlayer"))
                       {
                           // Update the boolean status that is connectionless.
                           isConnect = false;
                           client.Close();
                           break;
                       }
                       // Keep the connection.
                       if (result.Contains("multiPlayer"))
                       {
                           continue;
                       }
                       // close the connection.
                       //if (result == "close")
                       //{
                       //    isConnect = false;
                       //    Console.WriteLine("close the game by other client");
                       //    break;
                       //}
                       // Prints the result from the server.
                       if (result != "")
                       {
                           this.resultQueue.Enqueue(result);
                           //Console.WriteLine(result);
                       }
                   }
                   catch (Exception)
                   {
                       isConnect = false;
                       client.Close();
                       break;
                   }
               }
           });
            // The thread that always running. (until the word "exit").
            senderThread = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        //Console.WriteLine("wait for command");
                        //string dataInput = Console.ReadLine();
                        string commandString;
                        if (this.commandQueue.Count > 0)
                        {
                            commandString = this.commandQueue.Dequeue();

                            if (commandString == "exit")
                            {
                                break;
                            }
                            // There is no connection and we start a new connection.
                            if (!isConnect)
                            {
                                // The connect to the srver.
                                client = new TcpClient();
                                client.Connect(ep);
                                stream = client.GetStream();
                                reader = new StreamReader(stream);
                                writer = new StreamWriter(stream);
                                // Update the flag the we connect to the server.
                                isConnect = true;
                                // Create a new Task that handle the receive data from the server.
                                recieveThread = new Task(recieveData);
                                recieveThread.Start();
                            }
                            // Write to the server.
                            writer.WriteLine(commandString);
                            writer.Flush();
                            Thread.Sleep(200);
                        }
                        else {
                            Thread.Sleep(300);
                        }
                    } 
                    // error connection.
                    catch (Exception)
                    {
                        isConnect = false;
                        client.Close();
                    }
                }
            });
            senderThread.Start();
           
        }

        public void AddCommand (string newCommand)
        {
            commandQueue.Enqueue(newCommand);
        }
        public string GetResult ()
        {  
            while (resultQueue.Count == 0)
            {
                Thread.Sleep(300);
            }
            return resultQueue.Dequeue();
           // return null;
        }
    }
}

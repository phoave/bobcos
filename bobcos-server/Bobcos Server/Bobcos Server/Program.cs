using GameServer;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Bobcos_Server
{
    class Program
    {
        static bool isRunning = true;



        static void Main(string[] args)
        {

          
            Console.Title = "Bobcos server";
            itemdata.Initiliaze();
            Shop.Initiliaze();
            CraftingSystem.Initiliaze();
            Server.Start(500, 2020);
            Server.server.EnableStatistics = true;
            FishingSystem.Initialize();
            Thread T = new Thread(new ThreadStart(MainThread));
              T.Start();
            Thread C = new Thread(new ThreadStart(DiscordPart.SetUp));
            C.Start();



        }

        private static void MainThread()
        {
            Console.WriteLine($"Main thread started. Running at {Constants.TICKRATE} ticks per second.");

            while (isRunning)
            {
               try
                {
                    Server.server.PollEvents();

                }
                catch(Exception ex)
                {
                }

                Thread.Sleep(33);


             

            }
        }
        public static void Refresher()
        {
            while(true)
            {
                Thread.Sleep(2000);
                foreach(KeyValuePair<int,Client> cl in Server.Clients)
                {
                    if(cl.Value != null && cl.Value.user != null)
                    {
                        cl.Value.user.ServerHelloSendedToClient = true;
                        ServerSend.SendString(cl.Value.id, "S");

                    }
                }
                Thread.Sleep(1000);
                foreach (KeyValuePair<int, Client> cl in Server.Clients)
                {
                    if (cl.Value != null && cl.Value.user != null)
                    {
                        if(cl.Value.user.ServerHelloReceivedFromClient == false)
                        {
                            cl.Value.Disconnect();
                        }else
                        {
                            cl.Value.user.ServerHelloSendedToClient = false;

                        }
                    }
                }

            }

        }

    }
}


/*     Notes
 * Things I need to do;
 * Add break health system and 2. layer
 * BlockHealth max 5
 * 
 */

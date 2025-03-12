using System.Threading;
using System.Numerics;
using System;
using GameServer;
using System.Collections.Generic;

namespace Bobcos_Server
{
    public class User
    {
        public string username;
        public string realusername;



        public string PlayerSystemLaunguageCountry;
        public string PlayerNetworkLaunguageCountry;
        




        public double SecondsPassedUntilConnected;
        public long TotalPacketsReceivedFromClient;
        public long PacketsSendedToServre;


        public int Health = 5;

        public bool PassedVertifaction = false;
        public int id;
        public string World = null;
        public short CurrentSelecteditem = 2;

        public float xpos, ypos;
        public int worldCraftDataOrder = -1;
        public onGoingTrade Trade;
        public bool ChatCooldown = false;
        public bool isFacingLeft = false;
        public bool isNoclipping = false;
        public bool isInvisible = false;
        public bool CanBreakBlock = true;
        

        public bool ServerHelloSendedToClient = false;
        public bool ServerHelloReceivedFromClient = false;
        internal bool iscatchedfish;
        internal bool isfishing;

        public string hwid;
        public string ip;
        public void SetPos(float x, float y, float maxdistance,bool isjumping)
        {
            Vector2 oldpos = new Vector2(xpos, ypos);
            Vector2 newpos = new Vector2(x, y);




            if (MathF.Floor(xpos) != MathF.Floor(x) || MathF.Floor(ypos) != MathF.Floor(y))
            {
                //block type pos changed check block
               //Check positions between old pos and new pos floor them and add to list

                if (!itemdata.items[Logic.ReadWorldFg(World.ToUpper())[WorldDataConverter.ConvertPosidtoComplex((int)MathF.Floor(x + 0.1f), (int)MathF.Floor(y))]].avoidAntiNoclip)
                {if (!isNoclipping)
                    {


                        ServerSend.SendPosition(id, 0, xpos, ypos, isjumping);
                        return;
                    }
                       
                    
                }

               

                if (World == null)
                {
                    Console.WriteLine("Cant read sign. Because world is null. ");
                    return;
                }

            if(   Logic.ReadWorldFg(World.ToUpper())[WorldDataConverter.ConvertPosidtoComplex((int)MathF.Floor(x), (int)MathF.Floor(y))] == 63)
                {
                    string text = Sign.ReadSign(World.ToUpper(), WorldDataConverter.ConvertPosidtoComplex((int)MathF.Floor(x), (int)MathF.Floor(y)));


                    if(text != "IS.12D")
                    {

                        ServerSend.SendPlayerChatBubble(id, $"Sign: {text}", 0);
                    }



                }


            }

            if (xpos < x)
            {
                isFacingLeft = false;

            }
            else
            {
                isFacingLeft = true;
            }


            if (Vector2.Distance(oldpos,newpos) > 1f)
            {
                ServerSend.SendPosition(id, 0, xpos, ypos,isjumping);
                return;
            }
            if (MathF.Floor(xpos * 4) != MathF.Floor(x * 4) || MathF.Floor(ypos * 4) != MathF.Floor(y * 4))
            {
                //Check item
             
                    DroppingSystem.TakeItem(World, x, y, id);

                

            }
            if (World != null)
            {


                xpos = x; ypos = y;
                Logic.worlds[World].SendPositionToEveryonexpectClient(xpos, ypos, id, isjumping);

            }
            else
            {
                Console.WriteLine("Cant send position data because world is null.");
            }





        }

        public void SetPosWithoutDistanceCheck(float x, float y, float maxdistance, bool isjumping)
        {

            Vector2 oldpos = new Vector2(xpos, ypos);
            Vector2 newpos = new Vector2(x, y);



            if (MathF.Floor(xpos * 4) != MathF.Floor(x * 4) || MathF.Floor(ypos * 4) != MathF.Floor(y * 4))
            {
                //Check item
                DroppingSystem.TakeItem(World, x, y, id);

            }

            if (MathF.Floor(xpos) != MathF.Floor(x) || MathF.Floor(ypos) != MathF.Floor(y))
            {
                //block type pos changed check block

                if (World == null)
                {
                    return;
                }

                if (Logic.ReadWorldFg(World.ToUpper())[WorldDataConverter.ConvertPosidtoComplex((int)x, (int)y)] == 63)
                {
                    string text = Sign.ReadSign(World.ToUpper(), WorldDataConverter.ConvertPosidtoComplex((int)MathF.Floor(x), (int)MathF.Floor(y)));


                    if (text != "IS.12D")
                    {

                        ServerSend.SendPlayerChatBubble(id, $"Sign: {text}", 0);
                    }



                }


            }

            if (xpos < x)
            {
                isFacingLeft = false;

            }
            else
            {
                isFacingLeft = true;
            }


         

            if (World != null)
            {


                xpos = x; ypos = y;
                Logic.worlds[World].SendPositionToEveryonexpectClient(xpos, ypos, id, isjumping);
                 
            }


        }

        public void CheckIfVertifacted()
        {

            if(PassedVertifaction)
            {
                // do nothing
            }
            else
            {
                //Disconnect

            }
        }

        public void Damage(short blockid)
        {
            Health--;

            if(Health == 0)
            {

                if (Server.Clients[id].user.World != "")
                {
                    int[] pos = Logic.GetWhiteDoorPos(Server.Clients[id].user.World.ToUpper());
                    Server.Clients[id].user.SetPosWithoutDistanceCheck(pos[0], pos[1] + 0.4f, 0, false);

                    ServerSend.SendPosition(id, 0, pos[0] + 0.2f, pos[1] + 0.4f, false);
                    Logic.worlds[Server.Clients[id].user.World].SendPositionToEveryonexpectClient(pos[0] , pos[1] + 0.4f, id, false);




                }


                Health = 5;
            }



        }



        
    }
}
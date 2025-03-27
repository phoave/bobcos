using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using System.Threading;
using GameServer;
using System.Numerics;
using System.Data;
using System.Diagnostics;
using Bobcos_Server;


namespace Bobcos_Server
{

    public class DisplayBlockSystem
    {
        public static bool PlaceDisplayBlockItem(int _fromClient, int sira, int itemId)
        {
            try
            {
                string worldName = Server.Clients[_fromClient].user.World.ToUpper();
                int[] blockPos = WorldDataConverter.ConvertComplexidtopos(sira);

                if (!(File.Exists("displayBlockWorldData/" + worldName + ".json")))
                {

                    DisplayBlock[] displayBlocks = new DisplayBlock[4703];
                    List<DisplayBlock> dataread = new List<DisplayBlock>();
                    dataread.AddRange(displayBlocks);

                    displayBlocks[sira].itemid = itemId;
                    displayBlocks[sira].xPos = blockPos[0];
                    displayBlocks[sira].yPos = blockPos[1];

                    Logic.AddItemToInventory(Server.Clients[_fromClient].user.username.ToUpper(), itemId, -1);
                    Logic.GetInventoryAndSend(_fromClient, Server.Clients[_fromClient].user.username.ToUpper());

                    File.WriteAllText("displayBlockWorldData/" + worldName + ".json", JsonSerializer.Serialize(displayBlocks));
                    Logic.worlds[worldName].SendDisplayBlockData();
                    return true;
                }
                else
                {
                    List<DisplayBlock> dbWorldData = JsonSerializer.Deserialize<List<DisplayBlock>>(File.ReadAllText("displayBlockWorldData/" + worldName + ".json"));

                    if (dbWorldData.Count == 0)
                    {
                        DisplayBlock[] displayBlocks = new DisplayBlock[4703];
                        dbWorldData.AddRange(displayBlocks);

                        File.WriteAllText("displayBlockWorldData/" + worldName + ".json", JsonSerializer.Serialize(dbWorldData));
                    }

                    if (dbWorldData[sira] != null)
                    {
                        if (dbWorldData[sira].itemid == 0 && dbWorldData[sira].xPos == 0 && dbWorldData[sira].yPos == 0)
                        {
                            dbWorldData[sira].itemid = itemId;
                            dbWorldData[sira].xPos = blockPos[0];
                            dbWorldData[sira].yPos = blockPos[1];

                            Logic.AddItemToInventory(Server.Clients[_fromClient].user.username.ToUpper(), itemId, -1);
                            Logic.GetInventoryAndSend(_fromClient, Server.Clients[_fromClient].user.username.ToUpper());
                            //ServerSend.SendPriorityInventory(_fromClient);

                            string updateText = JsonSerializer.Serialize(dbWorldData);
                            File.WriteAllText("displayBlockWorldData/" + worldName + ".json", updateText);

                            Logic.worlds[worldName].SendDisplayBlockData();


                            return true;
                        }
                        else
                        {

                            ServerSend.SendChat(_fromClient, "<color=#FF0000FF>This display block is already configured. Break this display and try again.</color>");
                            return false;

                        }
                    }
                    else
                    {
                        dbWorldData[sira] = new DisplayBlock { itemid = itemId, xPos = blockPos[0], yPos = blockPos[1] };

                        string updateText = JsonSerializer.Serialize(dbWorldData);
                        File.WriteAllText("displayBlockWorldData/" + worldName + ".json", updateText);

                        Logic.AddItemToInventory(Server.Clients[_fromClient].user.username.ToUpper(), itemId, -1);
                        Logic.GetInventoryAndSend(_fromClient, Server.Clients[_fromClient].user.username.ToUpper());
                        //ServerSend.SendPriorityInventory(_fromClient);

                        Logic.worlds[worldName].SendDisplayBlockData();

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }

            return false;
        }

        public static bool IsDisplayBlockDisplaying(int _fromClient, int sira)
        {
            string worldName = Server.Clients[_fromClient].user.World.ToUpper();

            if (!File.Exists("displayBlockWorldData/" + worldName + ".json"))
            {
                return false;
            }
            else
            {
                List<DisplayBlock> dbWorldData = JsonSerializer.Deserialize<List<DisplayBlock>>(File.ReadAllText("displayBlockWorldData/" + worldName + ".json"));

                if (dbWorldData[sira] == null || dbWorldData[sira].itemid == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }

    public class DisplayBlockWorldData
    {
        public List<DisplayBlock> displayBlocks { get; set; }
    }

    public class DisplayBlock
    {
        public int itemid { get; set; }
        public int xPos { get; set; }
        public int yPos { get; set; }
    }
}
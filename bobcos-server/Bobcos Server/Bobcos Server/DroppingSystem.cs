using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using System.Threading;
using Bobcos_Server;
using GameServer;

class DroppingSystem
    {


    public static bool DropItem(string Worldname, int itemid, int itemcount, float xpos, float ypos, int clientid)
    {
        
        /*if (itemdata.items[Logic.ReadWorldFg(Worldname.ToUpper())[WorldDataConverter.ConvertPosidtoComplex((int)MathF.Floor(xpos + 0.1f), (int)MathF.Floor(ypos))]].itemid != 0)
        {
            return false;
        }*/

        if (!File.Exists($"dropItemWorldData/{Worldname.ToUpper()}.json"))
        {
           
            DropWorldData newdata = new DropWorldData();
            newdata.DropData = new List<DroppedItem>();
            newdata.DropData.Add(new DroppedItem() { itemcount = itemcount, itemid = itemid, Xpos = xpos, Ypos = ypos });


            File.WriteAllText($"dropItemWorldData/{Worldname.ToUpper()}.json", JsonSerializer.Serialize(newdata));

            Logic.worlds[Worldname.ToUpper()].SendDrop();
            return true;
        }
        else
        {
            
            DropWorldData datareaded = JsonSerializer.Deserialize<DropWorldData>(File.ReadAllText($"dropItemWorldData/{Worldname.ToUpper()}.json"));

            datareaded.DropData.Add(new DroppedItem() { itemcount = itemcount, itemid = itemid, Xpos = xpos, Ypos = ypos });

  
            File.WriteAllText($"dropItemWorldData/{Worldname.ToUpper()}.json", JsonSerializer.Serialize(datareaded));

           
            Logic.worlds[Worldname.ToUpper()].SendDrop();
            return true;
        }
    }


    public static void TakeItem(string Worldname,float posx,float posy,int clientid)
        {
            if (!File.Exists($"dropItemWorldData/{Worldname.ToUpper()}.json"))
            {
                DropWorldData newdata = new DropWorldData();
                newdata.DropData = new List<DroppedItem>();

            }else
            {
                DropWorldData datareaded = JsonSerializer.Deserialize<DropWorldData>(File.ReadAllText($"dropItemWorldData/{Worldname.ToUpper()}.json"));
                int b = 0;
                foreach (DroppedItem i in datareaded.DropData)
                {
                    


                    if(MathF.Floor(i.Xpos * 1.2f) == MathF.Floor(posx * 1.2f) && MathF.Floor(i.Ypos * 2f) == MathF.Floor(posy * 2f))
                    {
                     



       

                    if(!itemdata.items[Logic.ReadWorldFg(Worldname.ToUpper())[WorldDataConverter.ConvertPosidtoComplex((int)MathF.Floor(i.Xpos ), (int)MathF.Floor(i.Ypos))]].avoidAntiNoclip)
                    {
                        Console.WriteLine("Dropped item is inside block, you cant take it");
                        return;
                    }




                    //LOG
                 //   DiscordPart.SendItemTake(Server.Clients[clientid].user.username, Worldname, itemdata.items[i.itemid].itemname, i.itemcount);





              




                    if(Logic.CanTakeItem(Server.Clients[clientid].user.username.ToUpper(), i.itemid, i.itemcount) == false)
                    {
                        ServerSend.SendChat(clientid, "Cant take item: Item Amount Full");
                        return;
                    }else
                    {
                        Logic.AddItemToInventory(Server.Clients[clientid].user.username.ToUpper(), i.itemid, i.itemcount);
                    }
                    Logic.GetInventoryAndSend(clientid, Server.Clients[clientid].user.username.ToUpper());


                        datareaded.DropData.Remove(i);
                     
                    File.WriteAllText($"dropItemWorldData/{Worldname.ToUpper()}.json", JsonSerializer.Serialize(datareaded));
                    Logic.worlds[Worldname.ToUpper()].SendDrop();


                   Logss.WorldLog   ($"Player {Server.Clients[clientid].user.username} Taked item: {itemdata.items[i.itemid].itemname} {i.itemcount}x ", Worldname);
                    Logss.UserLog($"Player {Server.Clients[clientid].user.username} in world {Server.Clients[clientid].user.World} Taked: {itemdata.items[i.itemid].itemname} {i.itemcount}x", Server.Clients[clientid].user.username);
                    return;
                    }
                    b++;
                }

            }
        }
           
     

    }

    class DropWorldData
    {
        public List<DroppedItem> DropData { get; set; }
        public string WorldName;
    }


    class DroppedItem
    {
        public int itemid { get; set; }
        public int itemcount { get; set; }
        public float Xpos { get; set; }
        public float Ypos { get; set; }

    }


using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;

namespace Bobcos_Server
{
    class Sign
    {
        public static void SetSignText(string worldname,string text,int blockid)
        {
            

            if (File.Exists("worldsign/" + worldname + ".json"))
            {

                SignDataWorld data234 = JsonSerializer.Deserialize<SignDataWorld>(File.ReadAllText($"worldsign/{worldname.ToUpper()}.json"));
                data234.Data[blockid] = text;



                File.WriteAllText("worldsign/" + worldname.ToUpper() + ".json",JsonSerializer.Serialize(data234));

            }
            else
            {

                SignDataWorld data123 = new SignDataWorld();

                data123.Data = new string[4703];
                data123.Data[blockid] = text;

               


                File.WriteAllText("worldsign/" + worldname.ToUpper() + ".json", JsonSerializer.Serialize(data123));

               


            }
        }

        public static string ReadSign(string worldname,int blockid)
        {
            if (File.Exists($"worldsign/{worldname.ToUpper()}.json"))
            {

                try
                {


                    SignDataWorld data = JsonSerializer.Deserialize<SignDataWorld>(File.ReadAllText($"worldsign/{worldname.ToUpper()}.json"));

                   

                   if(data.Data[blockid] == null)
                    {
                        return "This sign is empty. use /sign <text> command on sign to write something to sign.";
                    }



                    return data.Data[blockid];

                }
                catch
                {

                }
            }
            return "IS.12D";


        }



    }

   
    class SignDataWorld
    {
        public string[] Data { get; set; } 

    }
}

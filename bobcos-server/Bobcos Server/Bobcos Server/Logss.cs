using System;
using System.Collections.Generic;
using System.Text;
using System.IO;



namespace Bobcos_Server
{
    
    class Logss
    {



       
        public static void GameLog(string log)
        {

            {
                StreamReader reader = new StreamReader("Logs/GameLogs.txt");

                string readded = reader.ReadToEnd();
                reader.Close();


                StreamWriter writer = new StreamWriter("Logs/GameLogs.txt");

                writer.WriteLine(readded);
                writer.WriteLine($"[{DateTime.Now}] {log}");

                writer.Close();
            }

           

        }

        public static void UserLog(string log,string username)
        {

            {



                StreamReader reader = new StreamReader($"Logs/UserLogs/{username.ToUpper()}.txt");

                string readded = reader.ReadToEnd();
                reader.Close();
                StreamWriter writer = new StreamWriter($"Logs/UserLogs/{username.ToUpper()}.txt");
                writer.WriteLine(readded);


                writer.WriteLine($"[{DateTime.Now}] {log}");

                writer.Close();
            }
        }

        public static void WorldLog(string log, string worldname)
        {
          

            {


                StreamReader reader = new StreamReader($"Logs/WorldLogs/{worldname.ToUpper()}.txt");

                string readded = reader.ReadToEnd();
                reader.Close();
                StreamWriter writer = new StreamWriter($"Logs/WorldLogs/{worldname.ToUpper()}.txt");
                writer.WriteLine(readded);

                writer.WriteLine($"[{DateTime.Now}] {log}");

                writer.Close();
            }
        }


    }
        
}
        

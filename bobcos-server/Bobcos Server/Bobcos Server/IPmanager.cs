using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Bobcos_Server
{
    class IPmanager
    {
        public static Dictionary<string, IpCell> IPcells = new Dictionary<string, IpCell>();
        public static List<string> BlackListedIps = new List<string>();
       
        public static int AddIp(string ip)
        {


            if(BlackListedIps.Contains(ip))
            {

                return 0;
            }

                 

            //check if theres already ip
            if (IPcells.ContainsKey(ip))
            {
                //found ip, check for Connection count if its above 3 Return 0 (Dont accept client)






                if(IPcells[ip].TotalConnections >= 3)
                {
                    return 0;
                }else
                {
                    IPcells[ip].TotalConnections++;
                    return 1;
                }
            }else
            {
                //ip doesnt exist, connect.
                IPcells.Add(ip, new IpCell() { IP = ip, TotalConnections = 1 }) ;
                return 1;

            }

        }
       
        public static void RemoveIp(string ip)
        {

            if (IPcells.ContainsKey(ip))
            {
                //found ip, check for Connection count if its above 3 Return 0 (Dont accept client)

              
                    IPcells[ip].TotalConnections--;
                if(IPcells[ip].TotalConnections <= 0)
                {
                    IPcells.Remove(ip);
                }
                  
            }
           
        }

        public static void BlackList(string ip)
        {
            BlackListedIps.Add(ip);

        }


    }

    

    class IpCell
    {
        public string IP;
        public int TotalConnections;
        
    }

   
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using UnityEngine;
using System.Threading;
using System.Net;

public   class ClientHandle
    {
   
    public static void Welcome(Packet _packet)
        {
            string message = _packet.ReadString();
        if (message == "UI_SHOW_CRAFT")
        {
            CraftScript.instance.Show();
            Menu3SCRIPT.instance.Hide();
        }
       
        if (message == "S")
        {
            ClientSend.SendString("S");
        }
        if (message == "night")
        {
            GameManager.instance.SetNight();
        }
        if (message == "day")
        {
            GameManager.instance.SetDay();

        }
        if (message == "AD")
        {
            AdMobScript.instance.ShowRewardAd();

        }

        if (message == "AD2")
        {
            AdMobScript.instance.ShowInter();

        }
        if (message == "N1")
        {
            PlayerManager.instance.Players[0].GetComponent<Player>().NoclipMode = true;
        }
        if (message == "N0")
        {
            PlayerManager.instance.Players[0].GetComponent<Player>().NoclipMode = false;
        }
        if (message == "FBU")
        {
            FisherBob.instance.SetActive();

        }
    }



         public static void LoadingSingal(Packet _packet)
    {
        byte selection = _packet.ReadByte();
        string info = _packet.ReadString();
        if (info == "#1")
        {
            UIManager.instance.MainMenu.SetActive(true);
        }

        UIManager.instance.AddLoadingInfo(info);


      

        if(selection == 0)

        {

            //hide 
            UIManager.instance.HideLoading();


        }
        else if(selection == 1)
        {
            //show
            UIManager.instance.ShowLoading();

        }


    }

    public static void resultofenter(Packet _Packet)
    {


        int selection = _Packet.ReadInt();

        string info = _Packet.ReadString();
        WarninMessage.instance.StartCoroutine(WarninMessage.instance.Display(info));
        

        UIManager.instance.infotext.text = info;
        if(selection == 0)
        {
            UIManager.instance.menu2.SetActive(false);
            UIManager.instance.MainMenu.SetActive(true);
            UIManager.instance.GameMenu.SetActive(false);
        }
        if(selection == 1)
        {
            try
            {


                PlayerPrefs.SetString("username", UIManager.instance.usernamel.text);

                PlayerPrefs.SetString("password", UIManager.instance.pass1.text);
            }
            catch
            {

            }
            AudioManager.instance.StopMusic();

            AdMobScript.instance.EnteredToMainMenu();
            UIManager.instance.menu2.SetActive(true);
            UIManager.instance.MainMenu.SetActive(false);
            UIManager.instance.GameMenu.SetActive(false);
            ItemDropManager.instance.ClearDroppedItems();


            //remove players
            foreach (KeyValuePair<int,GameObject> i in PlayerManager.instance.Players)
            {
                try
                {
                    PlayerManager.instance.RemovePlayer(i.Value.GetComponent<Player>().id);
                }
                catch { }
            }

        }

        if (selection == 2)
        {

            UIManager.instance.menu2.SetActive(false);
            UIManager.instance.MainMenu.SetActive(false);
            UIManager.instance.GameMenu.SetActive(true);
            AdMobScript.instance.EnteredToWORLD();

        }
    }
    public static void MenuFromServer(Packet _Packet)
    {
        bool isended = false;


        while(isended == false)
        {
        string i =    _Packet.ReadString();
            if(i == "END")
            {
                isended = true;
                UIManager.instance.ShowMenu();
               
            }else
            {

                string[] split = i.Split('_');

                if (split[0] == "text")
                {
                    UIManager.instance.AddText(split[1]);
                }
                if(split[0] == "button")
                {
                    UIManager.instance.AddButton(split[1],split[2]);

                }
            }
        }
        
    }

    public static void Chat(Packet _Packet)
    {
        string message = _Packet.ReadString();
        try
        {
            NewChatSystem.newChatSystem.AddChat(message);

        }
        catch
        {

        }
        try
        {
            NewChatSystem.nd2ChatSystem.AddChat(message);

        }
        catch
        {

        }

    }

    public static void SpawnPlayer(Packet _Packet)
    {
    

        string username = _Packet.ReadString();
        bool despawnorspawn = _Packet.ReadBool();
        int id = _Packet.ReadInt();
        Debug.Log("Spawned player");
        if (despawnorspawn == true)
        {
            PlayerManager.instance.AddPlayer(username, id);

        }else
        {
            PlayerManager.instance.RemovePlayer(id);
        }

    }

   
    public static void PositionFromServer(Packet _Packet)
    {




        int id = _Packet.ReadInt();

        

        float x = _Packet.ReadFloat();
        float y = _Packet.ReadFloat();
        bool isjumping = _Packet.ReadBool();


        if (id == 0)
        {
            PlayerManager.instance.Players[id].transform.position = new Vector3(x, y, -5f);

        }else
        {
            try
            {


                PlayerManager.instance.Players[id].GetComponent<Player>().lerppos = new Vector3(x, y, -5f);
            }
            catch
            {
                Debug.Log("Client is trying to insert pos to null player.");
                return;
            }
            PlayerManager.instance.Players[id].GetComponent<Player>().isjumped = isjumping;
          
         
        }







    }

    public static void WarningFromServer(Packet _packet)
    {
        string text = _packet.ReadString();
        UIManager.instance.SetWarningOn(text);

    }

    public static void BubbleReceived(Packet _packet)
    {
        int playerid = _packet.ReadInt();
        byte animId = _packet.ReadByte();
        if (animId == 0)
        {
            PlayerManager.instance.Players[playerid].GetComponent<Player>().Chatting.SetActive(false);
            PlayerManager.instance.Players[playerid].GetComponent<Player>().Trading.SetActive(false);

        }
        if (animId == 1)
        {
            PlayerManager.instance.Players[playerid].GetComponent<Player>().Chatting.SetActive(true);
                        PlayerManager.instance.Players[playerid].GetComponent<Player>().Trading.SetActive(false);

        }
        if (animId == 2)
        {
            PlayerManager.instance.Players[playerid].GetComponent<Player>().Chatting.SetActive(false);
            PlayerManager.instance.Players[playerid].GetComponent<Player>().Trading.SetActive(true);

        }



    }

    public static void WorldDataFGReceived(Packet _Packet)
    {
        bool keepreading = true;
        short[] worldata = new short[0];
        Debug.Log("World data received successfully.");



        string bgorfg = _Packet.ReadString().ToLower();
        while(keepreading)
        {
            short readed = _Packet.ReadShort();

            if(readed != 30431)
            {
                worldata = GameManager.instance.DiziBirlestir(worldata, new short[] { readed });
            }
            else
            {
                keepreading = false;
            }
        }
       
            if(bgorfg == "bg")
            {
            
                GameManager.instance.LoadBGBlockFromArray(worldata);

            

        }
        else
            {
            try
            {
                GameManager.instance.LoadBlockFromArray(worldata);

            }
            catch
            {

            }

            }


        
        
        worldata = new short[0];
    }
   
    public static void Inventory(Packet _Packet)
    {
        bool isended = false;
            InventoryManager.instance.RemoveItems();

       
        while (isended == false)
        {
            short id = _Packet.ReadShort();
            short count = _Packet.ReadShort();
            if(id == 13928)
            {
                isended = true;
            }
            else
            {
                InventoryManager.instance.AddItem(id, count);
            }
        }
    }

    public static void OnBlockData(Packet _Packet)
    {
        string isbgorfg = _Packet.ReadString().ToLower();
        short itemsirasi = _Packet.ReadShort();
        short itemid = _Packet.ReadShort();


        if(itemid == 0)
        {
            AudioManager.instance.Break();

        }else
        {
            AudioManager.instance.Place();


        }

        if (isbgorfg == "fg")
        {
            GameManager.instance.Blocks[itemsirasi].GetComponent<TileScript>().SetId(itemid,1);

        }else
        {
            GameManager.instance.BGBlocks[itemsirasi].GetComponent<TileScript>().SetId(itemid,0);

        }
    }

    public static void BreakBlockAnim(Packet _Packet)
    {
        short BlockPos = _Packet.ReadShort();
        byte breakingblocknumber = _Packet.ReadByte();


        GameManager.instance.AddBreaking(BlockPos, breakingblocknumber);
        AudioManager.instance.Hit();

    }

    public static void DropDatas(Packet _Packet)
    {
        ItemDropManager.instance.ClearDroppedItems();

        while (true)
        {
            short itemid = _Packet.ReadShort();

            if (itemid == 30431)
            {
                return;
            }
            short itemcount = _Packet.ReadShort();
            float Xpos = _Packet.ReadFloat();
            float Ypos = _Packet.ReadFloat();

            ItemDropManager.instance.AddDroppedItem(itemcount, itemid, Xpos, Ypos);
        }
        

    }

    public static void Cash(Packet _Packet)
    {
        int cash = _Packet.ReadInt();

        UIManager.instance.GemSlot1.text = cash.ToString();
        UIManager.instance.GemSlot2.text = cash.ToString();

    }

    public static void ItemInfoReceived(Packet _Packet)
    {
        string itemname = _Packet.ReadString();

        string iteminfo = _Packet.ReadString();
        bool istrashable = _Packet.ReadBool();
        bool iswearable = _Packet.ReadBool();
        bool isdroppable = _Packet.ReadBool();

        ItemInfoScript.instance.Show();

       if(istrashable)
        {
            ItemInfoScript.instance.TrashButton.SetActive(true);
        }else
        {
            ItemInfoScript.instance.TrashButton.SetActive(false);
        }
        if (isdroppable)
        {
            ItemInfoScript.instance.DropButton.SetActive(true);
        }
        else
        {
            ItemInfoScript.instance.DropButton.SetActive(false);
        }
        if (iswearable)
        {
            ItemInfoScript.instance.WearButton.SetActive(true);
        }
        else
        {
            ItemInfoScript.instance.WearButton.SetActive(false);
        }

        ItemInfoScript.instance.InfoText.text = iteminfo;
        ItemInfoScript.instance.ItemName.text = itemname;





    }

    public static void ClientInfo(Packet _Packet)
    {
        Client.instance.myId = _Packet.ReadInt();

        Debug.Log(Client.instance.myId);
        Debug.Log("Client info received");

    }

    public static void AnimReceived(Packet _Packet)
    {
        byte animId = _Packet.ReadByte();
        int PlrId = _Packet.ReadInt();

        if (animId == 0)
        {
            //punch
            PlayerManager.instance.Players[PlrId].GetComponentInChildren<PlayerAnimator2>().StartCoroutine(PlayerManager.instance.Players[PlrId].GetComponentInChildren<PlayerAnimator2>().Punch());
            

        }


    }
    public static void TradeUpdate(Packet _Packet)
    {
        byte sel = _Packet.ReadByte();
        if(sel == 1)
        {
            //Active UI
            TradeUI.instance.UI.SetActive(true);

        }
        if(sel == 2)
        {
            //Disable UI
            TradeUI.instance.UI.SetActive(false);
            TradeUI.instance.Clear();
            UICLICK.IsBusy = false;
        }
        if (sel == 3)
        {
            // Update player 2 Items
            short item1 = _Packet.ReadShort();
            short item1count = _Packet.ReadShort();

            short item2 = _Packet.ReadShort();
            short item2count = _Packet.ReadShort();

            short item3 = _Packet.ReadShort();
            short item3count = _Packet.ReadShort();

            short item4 = _Packet.ReadShort();
            short item4count = _Packet.ReadShort();

            TradeUI.instance.setOfferofplr2(item1, item2, item3, item4, item1count, item2count, item3count, item4count);
            // update

        }
    }



    public static void ShopDataReceived(Packet _Packet)
    {

        ShopItemManager.instance.RemoveItems();
        bool isended = false;
        
        while(!isended)
        {

            string itemname = _Packet.ReadString();

            if(itemname == "<END>")
            {
                isended = true;
                UIManager.instance.HideLoading();

                return;
            }
            int price = _Packet.ReadInt();
            int shopid = _Packet.ReadInt();
            int itemid = _Packet.ReadInt();
            string description = _Packet.ReadString();
            ShopItemManager.instance.AddShopItem((short)itemid, (short)shopid, price, itemname, description);



        }

    }

    public static void PlayerChatBubble(Packet _Packet)
    {
        string text = _Packet.ReadString();
        int playerid = _Packet.ReadInt();

        PlayerManager.instance.Players[playerid].GetComponent<ChatBubleTest>().Chat(text);
        PlayerManager.instance.Players[playerid].GetComponentInChildren<PlayerAnimator2>().StartCoroutine(PlayerManager.instance.Players[playerid].GetComponentInChildren<PlayerAnimator2>().Talk());

    }

    public static void PlayerApperance(Packet _Packet)
    {
        Color32 COlor = new Color32(_Packet.ReadByte(), _Packet.ReadByte(), _Packet.ReadByte(), _Packet.ReadByte());

        short shirt = _Packet.ReadShort();
        short pant = _Packet.ReadShort();
        short shoes = _Packet.ReadShort();
        short backid = _Packet.ReadShort();
        short headid = _Packet.ReadShort();
        short hatid = _Packet.ReadShort();

        short plrid = _Packet.ReadShort();
        short handitem = _Packet.ReadShort();
        byte badge = _Packet.ReadByte();

        Debug.Log("Apperance received");
        PlayerManager.instance.Players[plrid].GetComponentInChildren<PlayerAnimator2>().UpdatePlayerApperance(COlor, shirt, pant, shoes, backid, headid,hatid,handitem,badge);

    }
}


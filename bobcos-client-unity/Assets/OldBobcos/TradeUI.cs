using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradeUI : MonoBehaviour
{

    public static TradeUI instance;
    public GameObject UI;

    public int currentselectedslot = 0;
    public Image i1_i, i2_i, i3_i, i4_i; // Local Images
    public Text _i12, _i22, _i32, _i42; 

    public GameObject Count;
    public InputField count1;

    public Image i1_i2, i2_i2, i3_i2, i4_i2; // player 2 images
    public Text i12, i22, i32, i42; // player 2 item counts

    public short item1id, item2id, item3id, item4id;
    public short item1count, item2count, item3count, item4count;



    public void Start()
    {
        instance = this;
    }

    public void CountCancel()
    {
        Count.SetActive(false);
    }
    public void CountComfirm()
    {
        if (InventoryManager.instance.selectedItem == item1id || InventoryManager.instance.selectedItem == item2id || InventoryManager.instance.selectedItem == item3id || InventoryManager.instance.selectedItem == item4id)
        {
            return;
        }

        if (short.Parse(count1.text) > InventoryManager.instance.selectedItemCount)
        {
            return;
        }
        Count.SetActive(false);

        if(InventoryManager.instance.selectedItem == 2 || InventoryManager.instance.selectedItem == 29)
        {
            return;
        }


        if (currentselectedslot == 1)
        {
            if(short.Parse(count1.text) == 0)
            {
                item1id = 0;
                item1count = 0;
                setImages(0, i1_i);
                ClientSend.TradeInput(item1id, item2id, item3id, item4id, item1count, item2count, item3count, item4count);
                return;
            }

         
            item1id = (short)InventoryManager.instance.selectedItem;
            setImages(InventoryManager.instance.selectedItem, i1_i);
            _i12.text = count1.text;
            item1count = short.Parse(count1.text);
            ClientSend.TradeInput(item1id, item2id, item3id, item4id, item1count, item2count, item3count, item4count);

        }
        if (currentselectedslot == 2)
        {
            if (short.Parse(count1.text) == 0)
            {
                item2id = 0;
                item2count = 0;
                setImages(0, i2_i);
                ClientSend.TradeInput(item1id, item2id, item3id, item4id, item1count, item2count, item3count, item4count);

                return;
            }
            item2id = (short)InventoryManager.instance.selectedItem;

            setImages(InventoryManager.instance.selectedItem, i2_i);
            _i22.text = count1.text;
            item2count = short.Parse(count1.text);
            ClientSend.TradeInput(item1id, item2id, item3id, item4id, item1count, item2count, item3count, item4count);

        }
        if (currentselectedslot == 3)
        {
            if (short.Parse(count1.text) == 0)
            {
                item3id = 0;
                item3count = 0;
                setImages(0, i3_i);
                ClientSend.TradeInput(item1id, item2id, item3id, item4id, item1count, item2count, item3count, item4count);

                return;
            }
            item3id = (short)InventoryManager.instance.selectedItem;

            setImages(InventoryManager.instance.selectedItem, i3_i);
            _i32.text = count1.text;
            item3count = short.Parse(count1.text);
            ClientSend.TradeInput(item1id, item2id, item3id, item4id, item1count, item2count, item3count, item4count);

        }
        if (currentselectedslot == 4)
        {
            if (short.Parse(count1.text) == 0)
            {
                item4id = 0;
                item4count = 0;
                setImages(0, i4_i);
                ClientSend.TradeInput(item1id, item2id, item3id, item4id, item1count, item2count, item3count, item4count);

                return;
            }
            item4id = (short)InventoryManager.instance.selectedItem;

            setImages(InventoryManager.instance.selectedItem, i4_i);
            _i42.text = count1.text;
            item4count = short.Parse(count1.text);
            ClientSend.TradeInput(item1id, item2id, item3id, item4id, item1count, item2count, item3count, item4count);

        }

    }
    public void Item1Click()
    {
        Count.SetActive(true);
        currentselectedslot = 1;
    }
    public void Item2Click()
    {
        Count.SetActive(true); currentselectedslot = 2;

    }
    public void Item3Click()
    {
        Count.SetActive(true); currentselectedslot = 3;


    }
    public void Item4Click()
    {
        Count.SetActive(true); currentselectedslot = 4;


    }

    public void Clear()
    {
        item1id = -1;
        item1count = -1;


        item2id = -1;
        item2count = -1;

        item3id = -1;
        item3count = -1;

        item4id = -1;
        item4count = -1;

        setImages(-1, i1_i);
        setImages(-1, i2_i);
        setImages(-1, i3_i);
        setImages(-1, i4_i);
        setImages(-1, i1_i2);
        setImages(-1, i2_i2);
        setImages(-1, i3_i2);
        setImages(-1, i4_i2);

    }
    public void AcceptTrade()
    {
        ClientSend.ConfirmTrade();
    }
    public void CancelTrade()
    {
        ClientSend.CancelTrade();


    }


    public void setOfferofplr2(short item1,short item2,short item3,short item4,short item1count,short item2count,short item3count,short item4count)
    {
        i12.text = item1count.ToString();
        i22.text = item2count.ToString();
        i32.text = item3count.ToString();
        i42.text = item4count.ToString();

        setImages(item1, i1_i2);
        setImages(item2, i2_i2);

        setImages(item3, i3_i2);
        setImages(item4, i4_i2);

    }

    void setImages(int itemid,Image imageToGetChanged)
    {
        foreach (Block i in ItemManager.instance.BlockItems)
        {
            if (i.id == itemid)
            {
                imageToGetChanged.sprite = i.Icon;
            }
        }
        foreach (Shirt i in ItemManager.instance.ShirtItems)
        {
            if (i.id == itemid)
            {
                imageToGetChanged.sprite = i.Icon;
            }
        }
        foreach (Pant i in ItemManager.instance.PantItems)
        {
            if (i.id == itemid)
            {
                imageToGetChanged.sprite = i.Icon;
            }
        }
        foreach (Shoe i in ItemManager.instance.ShoeItems)
        {
            if (i.id == itemid)
            {
                imageToGetChanged.sprite = i.Icon;
            }
        }
        foreach (Hair i in ItemManager.instance.HairItems)
        {
            if (i.id == itemid)
            {
                imageToGetChanged.sprite = i.Icon;
            }
        }
        foreach (Hat i in ItemManager.instance.HatItems)
        {
            if (i.id == itemid)
            {
                imageToGetChanged.sprite = i.Icon;
            }
        }

        foreach (backitem i in ItemManager.instance.BackItems)
        {
            if (i.id == itemid)
            {
                imageToGetChanged.sprite = i.Icon;
            }
        }
        foreach (HandItem i in ItemManager.instance.HandItems)
        {
            if (i.id == itemid)
            {
                imageToGetChanged.sprite = i.Icon;
            }
        }

    }

}

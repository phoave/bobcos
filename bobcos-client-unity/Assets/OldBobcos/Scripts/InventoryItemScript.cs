using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemScript : MonoBehaviour
{
    public int sıraid;
    public int itemid;
    public int itemcount;
    public Image image;
    public Text counttext;
    public GameObject Select;
   

    public void SetId(int id)
    {

        itemid = id;



        foreach (Block i in ItemManager.instance.BlockItems)
        {
            if (i.id == itemid)
            {



               image.sprite = i.Icon;
            }
        }
        foreach (Shirt i in ItemManager.instance.ShirtItems)
        {
            if (i.id == itemid)
            {



                image.sprite = i.Icon;
            }
        }
        foreach (Pant i in ItemManager.instance.PantItems)
        {
            if (i.id == itemid)
            {



                image.sprite = i.Icon;
            }
        }
        foreach (Shoe i in ItemManager.instance.ShoeItems)
        {
            if (i.id == itemid)
            {



                image.sprite = i.Icon;
            }
        }

        foreach (Hat i in ItemManager.instance.HatItems)
        {
            if (i.id == itemid)
            {



                image.sprite = i.Icon;
            }
        }

        foreach (Hair i in ItemManager.instance.HairItems)
        {
            if (i.id == itemid)
            {



                image.sprite = i.Icon;
            }
        }
        foreach (HandItem i in ItemManager.instance.HandItems)
        {
            if (i.id == itemid)
            {
                image.sprite = i.Icon;
            }
        }
        foreach (backitem i in ItemManager.instance.BackItems)
        {
            if (i.id == itemid)
            {
                image.sprite = i.Icon;
            }
        }
    }

    public void SetCount(int count)
    {
        itemcount = count;

        counttext.text = itemcount.ToString();
    }

    public void OnClick()
    {

        // send click of sira lol
        ClientSend.SendInventory((short)sıraid);

        InventoryManager.instance.Unselect();
        InventoryManager.instance.selectedItem = itemid;
        InventoryManager.instance.selectedItemCount = itemcount;

        Select.SetActive(true);
    }
}

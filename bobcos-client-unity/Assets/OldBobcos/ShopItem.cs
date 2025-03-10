using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public Image itemImage;
    public Text itemName;
    public Text itemPrice;
    public Text description;
    public Toggle CheckBox;
    public int itemShopId;
    public int itemId;






    public void BuyItem()
    {
        if(CheckBox.isOn)
        {
            // Send ItemShopID to server
            ClientSend.BuyItem(itemShopId);
            UIManager.instance.CloseShop();
            CheckBox.isOn = false;
        }
    }

    public void Setup()
    {

        foreach (Block i in ItemManager.instance.BlockItems)
        {
            if (i.id == itemId)
            {



                itemImage.sprite = i.Icon;
            }
        }

        foreach (backitem i in ItemManager.instance.BackItems)
        {
            if (i.id == itemId)
            {



                itemImage.sprite = i.Icon;
            }
        }
        foreach (Shirt i in ItemManager.instance.ShirtItems)
        {
            if (i.id == itemId)
            {



                itemImage.sprite = i.Icon;
            }
        }
        foreach (Pant i in ItemManager.instance.PantItems)
        {
            if (i.id == itemId)
            {



                itemImage.sprite = i.Icon;
            }
        }
        foreach (Shoe i in ItemManager.instance.ShoeItems)
        {
            if (i.id == itemId)
            {



                itemImage.sprite = i.Icon;
            }
        }
        foreach (Hat i in ItemManager.instance.HatItems)
        {
            if (i.id == itemId)
            {



                itemImage.sprite = i.Icon;
            }
        }
        foreach (Hair i in ItemManager.instance.HairItems)
        {
            if (i.id == itemId)
            {



                itemImage.sprite = i.Icon;
            }
        }


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CraftScript : MonoBehaviour
{
    public Image item1,item2;

    public GameObject NotReady;
    public static CraftScript instance;
    public short _item1, _item2;

    private void Start()
    {
        instance = this;
    }


    public void Item1Click()
    {
        int si = InventoryManager.instance.selectedItem;
        _item1 = (short)si;
        foreach (Block i in ItemManager.instance.BlockItems)
        {
            if (i.id == si)
            {



                item1.sprite = i.Icon;
            }
        }
        foreach (Shirt i in ItemManager.instance.ShirtItems)
        {
            if (i.id == si)
            {



                item1.sprite = i.Icon;
            }
        }
        foreach (Pant i in ItemManager.instance.PantItems)
        {
            if (i.id == si)
            {



                item1.sprite = i.Icon;
            }
        }
        foreach (Shoe i in ItemManager.instance.ShoeItems)
        {
            if (i.id == si)
            {



                item1.sprite = i.Icon;
            }
        }

        foreach (Hat i in ItemManager.instance.HatItems)
        {
            if (i.id == si)
            {



                item1.sprite = i.Icon;
            }
        }

        foreach (Hair i in ItemManager.instance.HairItems)
        {
            if (i.id == si)
            {



                item1.sprite = i.Icon;
            }
        }
        foreach (backitem i in ItemManager.instance.BackItems)
        {
            if (i.id == si)
            {
                item1.sprite = i.Icon;
            }
        }
        foreach (HandItem i in ItemManager.instance.HandItems)
        {
            if (i.id == si)
            {
                item1.sprite = i.Icon;
            }
        }

    }
    public void Item2Click()
    {
        int si = InventoryManager.instance.selectedItem;
        _item2 = (short)si;

        foreach (Block i in ItemManager.instance.BlockItems)
        {
            if (i.id == si)
            {



                item2.sprite = i.Icon;
            }
        }
        foreach (Shirt i in ItemManager.instance.ShirtItems)
        {
            if (i.id == si)
            {



                item2.sprite = i.Icon;
            }
        }
        foreach (Pant i in ItemManager.instance.PantItems)
        {
            if (i.id == si)
            {



                item2.sprite = i.Icon;
            }
        }
        foreach (Shoe i in ItemManager.instance.ShoeItems)
        {
            if (i.id == si)
            {



                item2.sprite = i.Icon;
            }
        }

        foreach (Hat i in ItemManager.instance.HatItems)
        {
            if (i.id == si)
            {



                item2.sprite = i.Icon;
            }
        }

        foreach (Hair i in ItemManager.instance.HairItems)
        {
            if (i.id == si)
            {



                item2.sprite = i.Icon;
            }
        }
        foreach (backitem i in ItemManager.instance.BackItems)
        {
            if (i.id == si)
            {
                item2.sprite = i.Icon;
            }
        }
        foreach (HandItem i in ItemManager.instance.HandItems)
        {
            if (i.id == si)
            {
                item2.sprite = i.Icon;
            }
        }


    }


    public void Cancel()
    {
        NotReady.SetActive(false);
    }

    public void Craft()
    {

        ClientSend.Craft(_item1, _item2);
        NotReady.SetActive(false);

    }

    public void Show()
    {

        NotReady.SetActive(true);

    }

}

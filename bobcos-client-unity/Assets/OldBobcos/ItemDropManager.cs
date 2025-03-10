using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropManager : MonoBehaviour
{
    int is2 = 0;
    public GameObject ExampleObject;
    public List<DroppedItem> items = new List<DroppedItem>();
    public static ItemDropManager instance;
    public void Start()
    {
        instance = this;
    }



    public void AddDroppedItem(int itemcount,int itemid,float posx,float posy)
    {
        DroppedItem item = new DroppedItem() { itemid = itemid, itemcount = itemcount };





        GameObject object_ = Instantiate(ExampleObject);
        object_.transform.position = new Vector3(posx, posy,-5f);
        item.Gobject = object_;




        SetImage(itemid, object_.GetComponent<DropItem>().Image);
        object_.GetComponentInChildren<TextMesh>().text = itemcount.ToString();

        items.Add(item);

    }


    public void SetImage(int si, SpriteRenderer item2)
    {

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

    public void ClearDroppedItems()
    {
        foreach(DroppedItem i in items)
        {
            
            Destroy(i.Gobject);

        }
        items.Clear();

    }
}


public class DroppedItem
{
    public int itemid;
    public int itemcount;
    public GameObject Gobject;

}

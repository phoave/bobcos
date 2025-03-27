using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayBlockItemManager : MonoBehaviour
{
    public GameObject gObject;
    public static DisplayBlockItemManager instance;
    public List<DisplayBlockItem> items = new List<DisplayBlockItem>();
    public void Start() {
        instance = this;
    }
    
    public void displayDisplayItem(int itemId, int posx, int posy) {
        DisplayBlockItem item = new DisplayBlockItem() { itemId = itemId };
        GameObject object_ = Instantiate(gObject);
        object_.transform.position = new Vector3((int)Mathf.Floor(posx), (int)Mathf.Floor(posy),0);
        item.gObject = object_;
        
        if (item.itemId > 0) {
            setImages(itemId, object_.GetComponent<DisplayItem>()._sprite);
        }

        items.Add(item);
    }

    public void clearDisplayItems()
    {
        foreach(DisplayBlockItem i in items)
        {
            Debug.Log("Clearing " + i.itemId);
            Destroy(i.gObject);
        }
        items.Clear();
    }


    public void setImages(int imageToSet, SpriteRenderer sprite) {
        foreach(Block i in ItemManager.instance.BlockItems) {
            if(i.id == imageToSet) { sprite.sprite = i.Icon; }
        }
        foreach(Shirt i in ItemManager.instance.ShirtItems) {
            if(i.id == imageToSet) { sprite.sprite = i.Icon; }
        }
        foreach(Pant i in ItemManager.instance.PantItems) {
            if(i.id == imageToSet) { sprite.sprite = i.Icon; }
        }
        foreach(Shoe i in ItemManager.instance.ShoeItems) {
            if(i.id == imageToSet) { sprite.sprite = i.Icon; }
        }
        foreach(Hat i in ItemManager.instance.HatItems) {
            if(i.id == imageToSet) { sprite.sprite = i.Icon; }
        }
        foreach(backitem i in ItemManager.instance.BackItems) {
            if(i.id == imageToSet) { sprite.sprite = i.Icon; }
        }
        foreach(HandItem i in ItemManager.instance.HandItems) {
            if(i.id == imageToSet) { sprite.sprite = i.Icon; }
        }

    }
}

    public class DisplayBlockItem {
        public int itemId;
        public GameObject gObject;
    }
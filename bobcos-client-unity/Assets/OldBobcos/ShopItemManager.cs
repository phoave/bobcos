using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemManager : MonoBehaviour
{
    public GameObject Item;
    public static ShopItemManager instance;


    public void Start()
    {
        instance = this;
    }


    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            AddShopItem(3, 1, 150, "DIRT", "Hello this is description.");
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            RemoveItems();
        }
    }

    public void RemoveItems()
    {
        foreach (Transform child in gameObject.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public void AddShopItem(short itemId,short ItemShopId,int price,string ItemName,string itemDescription)
    {
        GameObject item = Instantiate(Item);

        item.GetComponent<ShopItem>().itemId = itemId;
        item.GetComponent<ShopItem>().itemShopId = ItemShopId;
        item.GetComponent<ShopItem>().itemPrice.text = price.ToString();
        item.GetComponent<ShopItem>().itemName.text = ItemName.ToString();
        item.GetComponent<ShopItem>().description.text = itemDescription.ToString();


        item.GetComponent<ShopItem>().Setup();
       item.transform.SetParent(gameObject.transform);
        item.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
    }
}

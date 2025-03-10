using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    public int sira = 0;
    public int selectedItem = 0;
    public static InventoryManager instance;
    public GameObject exampleitem;
    public GameObject Inventory;
    public int selectedItemCount;

    public void Start()
    {
        instance = this;
    }
    public void Update()
    {
         
    }

    public static void set()
    {
        
    }

    public void AddItem(int itemid,int itemcount)
    {
        GameObject item = Instantiate(exampleitem);
        if(itemid == selectedItem)
        {

            item.GetComponent<InventoryItemScript>().Select.SetActive(true);
        }
        item.GetComponent<InventoryItemScript>().SetId(itemid);
        item.GetComponent<InventoryItemScript>().SetCount(itemcount);
        item.GetComponent<InventoryItemScript>().sıraid = sira;

        sira++;

        item.transform.SetParent(Inventory.transform);
        item.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

    }
    public void RemoveItems()
    {
        instance = this;

        sira = 0;
        foreach (Transform child in Inventory.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
    public void Unselect()
    {
        foreach (Transform child in Inventory.transform)
        {
            child.gameObject.GetComponent<InventoryItemScript>().Select.SetActive(false);
        }
    }
}

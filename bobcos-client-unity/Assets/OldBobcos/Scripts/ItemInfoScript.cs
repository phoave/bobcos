using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoScript : MonoBehaviour
{


    public static ItemInfoScript instance;

    public GameObject UI;
    public GameObject TrashButton;
    public GameObject WearButton;
    public GameObject DropButton;
    public Text ItemName;
    public Text InfoText;

    public GameObject ItemTrash;

    public InputField TrashCount;

    public GameObject ItemDrop;

    public InputField DropCount;

    void Start()
    {
        instance = this;
    }


    public void DisplayItemTrash()
    {

        ItemTrash.SetActive(true);
    }
    public void DisplayItemDrop()
    {

        ItemDrop.SetActive(true);
    }

    public void ItemTrashConfirm()
    {
        Hide();
        ItemTrash.SetActive(false);
        //Send confirm packet and count
        ClientSend.SendTrashRequest(short.Parse(TrashCount.text));
    }

    public void ItemDropConfirm()
    {
        Hide();
        ItemDrop.SetActive(false);
        //Send confirm packet and count
        ClientSend.DropItemRequest(short.Parse(DropCount.text));
    }
    public void WearItem()
    {
        ClientSend.SendWearItem();
    }
    public void ItemTrashCancel()
    {
        ItemTrash.SetActive(false);

    }
    public void ItemDropCancel()
    {
        ItemDrop.SetActive(false);

    }
    public void Hide()
    {
        UI.SetActive(false);

    }

    public void Show()
    {
        UI.SetActive(true);
    }


}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject MainMenu;
    public Text infotext;

    public GameObject ChatBox;
    public InputField messageinputfield;
    public Text message;

    public InputField usernamel,pass1,username2,pass2,email;
    public InputField Worldname;
    public GameObject menu;
    public GameObject menu2;
    public GameObject GameMenu;
    public Text MenuText;
    public GameObject Button;

    public Text GemSlot1;
    public Text GemSlot2;

    public GameObject Shop;
    public GameObject recoverpassword;

    public GameObject NewWarningUI;
    public Text NewWarningUIText;


    public GameObject RealMainMenu, LoginMenu;
    public void DisplayShop()
    {
        ClientSend.SendString("UI_SHOP");
        ShowLoading();
        Shop.SetActive(true);
    }
    public void CloseShop()
    {
        Shop.SetActive(false);


    }
    public void EnableLoginMenu()
    {
        LoginMenu.SetActive(true);
        RealMainMenu.SetActive(false);
    }
    public void EnableRealMenu()
    {
        LoginMenu.SetActive(false);
        RealMainMenu.SetActive(true);
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }
       
    }
    private void Start()
    {
        usernamel.text = PlayerPrefs.GetString("username");
        pass1.text = PlayerPrefs.GetString("password");

    }
    public void Login()
    {
        ShowLoading();
        ClientSend.SendLoginOrWorld(usernamel.text,pass1.text);
    }
    public void Register()
    {
        ShowLoading();

        ClientSend.SendLoginOrWorld(username2.text, pass2.text, email.text);
    }

    public void TryToEnterWorld()
    {
        ShowLoading();

        ClientSend.SendLoginOrWorld(Worldname.text);

    }
    #region LoadingSystem
    public GameObject Loading;
    public Text TipText;
    public void ShowLoading()
    {
        Loading.SetActive(true);
    }
    public void HideLoading()
    {
        Loading.SetActive(false);
    }
    public void AddLoadingInfo(string text)
    {
        TipText.text = text;
    }





    #endregion
    
    #region MenuSystem

      public void AddText(string text)
    {
      Text i = Instantiate(MenuText);
        i.text = text;
        i.transform.SetParent(menu.transform);
        i.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
    }

    public void AddButton(string Buttonname,string buttonid)
    {
        GameObject i = Instantiate(Button);
        i.GetComponent<ButtonComponent>().id = buttonid;
        i.GetComponentInChildren<Text>().text = Buttonname;
        i.transform.SetParent(menu.transform);
        i.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

    }


    public void ShowMenu()
    {
        menu.SetActive(true);
    }
    public void HideMenu()
    {
        menu.SetActive(false);
        foreach (Transform child in menu.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
    #endregion


    public void AddMessageToChat(string text)
    {
        message.text = message.text + Environment.NewLine + text;
        try
        {
            MessageAdded.instance.MessageAdded1();
        }
        catch { }
    }


    public void SendMessage()
    {
        ClientSend.SendChat(messageinputfield.text);
    }

    public void SendStringToServer(string str)
    {
        ClientSend.SendString(str);
    }
    public void CraftMenuRequest()
    {
        ClientSend.SendString("UI_SHOW_CRAFT_REQ");
    }

    public void SetRecoverOn()
    {
        recoverpassword.SetActive(true);

    }

    public void SetWarningOn(string text)
    {
        NewWarningUI.SetActive(true);
        NewWarningUIText.text = text;

        StartCoroutine(wait());
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(10);
        NewWarningUI.SetActive(false);

    }

   
}

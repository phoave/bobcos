using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecoverMyPassword : MonoBehaviour
{
    public GameObject UI;
    public InputField mail, username;
    public void CloseUI()
    {
        UI.SetActive(false);
    }

    public void SendInformation()
    {
        ClientSend.SendPassChange(mail.text, username.text);
        CloseUI();
    }
}

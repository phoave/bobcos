using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PasswordChanger : MonoBehaviour
{
    public GameObject UI;
    public InputField OldPass, NewPass;
    public void CloseUI()
    {
        UI.SetActive(false);
    }

    public void SendInformation()
    {
        ClientSend.SendPassChange(OldPass.text, NewPass.text);
        CloseUI();
    }
}

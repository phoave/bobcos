using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonComponent : MonoBehaviour
{
    public string id;

    public void OnClick()
    {
        UIManager.instance.menu.SetActive(false);
        UIManager.instance.HideMenu();
    }
}

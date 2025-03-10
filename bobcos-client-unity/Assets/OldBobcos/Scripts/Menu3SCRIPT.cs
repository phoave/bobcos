using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu3SCRIPT : MonoBehaviour
{
    public GameObject Menu;
    public static Menu3SCRIPT instance;

    public void Start()
    {
        instance = this;
    }

    public void Show()
    {
        Menu.SetActive(true);
    }
    public void Hide()
    {
        Menu.SetActive(false);
    }
}

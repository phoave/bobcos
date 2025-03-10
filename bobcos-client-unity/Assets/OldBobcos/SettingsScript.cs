using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsScript : MonoBehaviour
{
    public GameObject Menu;
    public GameObject PasswordChangeMenu;



    public void OpenMenu()

    {


        Menu.SetActive(true);

    }

    public void CloseMenu()
    {
        Menu.SetActive(false);

    }
    public void OpenPasswordChangeMenu()
    {
        PasswordChangeMenu.SetActive(true);

    }

    public void  Disconnect()
    {
        Client.instance.Disconnect();
        CloseMenu();
        SceneManager.LoadScene(0);
    }


}

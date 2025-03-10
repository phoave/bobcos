using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public GameObject OriginalPlayer;
    public Dictionary<int,GameObject> Players = new Dictionary<int, GameObject>();
    public void Start()
    {
        instance = this;
    }

    public void AddPlayer(string username,int id)
    {
        GameObject plr = Instantiate(OriginalPlayer);
        Players[id] = plr;

        plr.GetComponent<Player>().Setup(username, id);

    }

    public void RemovePlayer( int id)
    {
        try
        {


            if (id == 0)
            {
                GameManager.instance.Camera.SetActive(true);
                AdvancedCamera.instance.Player = null;
            }

            Destroy(Players[id]);
        }
        catch
        {

        }
    }
}

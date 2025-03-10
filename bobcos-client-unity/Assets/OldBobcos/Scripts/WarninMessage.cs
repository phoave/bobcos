using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarninMessage : MonoBehaviour
{
    public static WarninMessage instance;
    public Text displaytext;
    public GameObject Message;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

  

  public  IEnumerator Display(string msg)
    {
        displaytext.text = msg;
        Message.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        Message.SetActive(false);

    }
}

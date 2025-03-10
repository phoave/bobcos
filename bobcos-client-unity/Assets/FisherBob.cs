using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FisherBob : MonoBehaviour
{
    public static FisherBob instance;
    public GameObject object1;
    public InputField field1, field2, field3;
    public void Start()
    {
        instance = this;
    }


    public void SetActive()
    {
        object1.SetActive(true);
       
    }


    public void SetInactive()
    {
        object1.SetActive(false);
    }



    public void SendAmountDataToServer1()
    {
        ClientSend.SendAmount(93, short.Parse(field1.text));
        SetInactive();
    }
    public void SendAmountDataToServer2()
    {
        ClientSend.SendAmount(94, short.Parse(field2.text));
        SetInactive();

    }
    public void SendAmountDataToServer3()
    {
        ClientSend.SendAmount(95, short.Parse(field3.text));
        SetInactive();

    }


}

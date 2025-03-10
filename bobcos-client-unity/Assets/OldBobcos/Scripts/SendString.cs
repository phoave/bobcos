using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendString : MonoBehaviour
{
    public string stringtosend;
    public GameObject ObjectToSETFALSE;
    public void Send()
    {
        ClientSend.SendString(stringtosend);
        if(ObjectToSETFALSE != null)
        {
            ObjectToSETFALSE.SetActive(false);
        }
    }
}

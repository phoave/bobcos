using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UICLICK : MonoBehaviour, IPointerEnterHandler
{
    public static bool IsBusy = false;
    public static int ACTIVE;
    public static UICLICK instance;
    public bool ismain =false;

    public void Start()
    {
        instance = this;
    }

   


    public void OnPointerEnter(PointerEventData eventData)
    {if(!ismain)
        {
            IsBusy = true;
            Debug.Log("busy");
        }
        else { IsBusy = false;
            Debug.Log("not busy");

        }
    }

   

}

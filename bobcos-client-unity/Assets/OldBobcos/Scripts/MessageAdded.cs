using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageAdded : MonoBehaviour
{
    public RectTransform rt;
   public static MessageAdded instance;
    public float yoffset = 32;

    private void Start()
    {
        instance = this;
    }

    public void MessageAdded1()
    {
        rt.transform.position = new Vector3(rt.transform.position.x, rt.transform.position.y + yoffset);
    }
}

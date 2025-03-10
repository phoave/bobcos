using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatBubleTest : MonoBehaviour
{
    public TextMesh _Chat;
    public GameObject Box;


    public void Update()
    {
       
    }

    public void Chat(string text)
    {


        _Chat.gameObject.SetActive(true);
        Box.SetActive(true);

        _Chat.text = text;
        Box.transform.localScale = new Vector3(0.04f, Box.transform.localScale.y,-1f);
        Box.transform.localScale = new Vector3(Box.transform.localScale.x + (_Chat.text.Length * 0.02f),Box.transform.localScale.y,-1);
        StartCoroutine(WaitFor4Seconds());
    }

    IEnumerator WaitFor4Seconds()
    {
        yield return new WaitForSeconds(4);
        _Chat.gameObject.SetActive(false);
        Box.SetActive(false);
    }

}

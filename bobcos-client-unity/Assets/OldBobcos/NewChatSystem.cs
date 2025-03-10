using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NewChatSystem : MonoBehaviour
{
    public GameObject ChatBox;
    public GameObject TextObjectExample;
    public List<ChatText> _list = new List<ChatText>();
    public int maxChatMessage = 125;
    public static NewChatSystem newChatSystem;
    public static NewChatSystem nd2ChatSystem;

    private void Start()
    {
        if(newChatSystem == null)
        {
            newChatSystem = this;

        }else
        {
            nd2ChatSystem = this;
        }

    }

    public void AddChat(string text)
    {

        if(ChatBox == null)
        {
            AddChat(text);
            return;
        }

        if (ChatBox.active)
        {


            if (_list.Count >= maxChatMessage)
            {
                Destroy(_list[0].textObject.gameObject);
                _list.Remove(_list[0]);
            }

            ChatText t = new ChatText();
            t.text = text;

            GameObject chattext = Instantiate(TextObjectExample, ChatBox.transform);

            t.textObject = chattext.GetComponent<Text>();
            t.textObject.text = text;
            _list.Add(t);
        }
    }

}

public class ChatText
{
    public string text;
    public Text textObject;


}

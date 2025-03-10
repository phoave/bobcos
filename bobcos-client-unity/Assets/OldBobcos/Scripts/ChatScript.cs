using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore;
using UnityEngine.UI;


public class ChatScript : MonoBehaviour
{

    public static ChatScript instance;

    public GameObject _Object;
  public InputField Text;
    public bool ischatting = false;
    public GameObject DisableGameObject;


    private void Start()
    {
        instance = this;
    }
    private void Update()
    {
      

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if(!Text.isFocused)
            {
                Text.ActivateInputField();
            }


            if (_Object.active == false)
            {
                _Object.SetActive(true);

                //disable moving, block break, put during chat


                ClientSend.SendString("CH1");

                DisableGameObject.SetActive(false);
                ischatting = true;

            } else
            if (_Object.active == true)
            {
                

                _Object.SetActive(false);

                ischatting = false;
                DisableGameObject.SetActive(true);
                ClientSend.SendString("CH0");

                if (Text.text != "")
                {
                    //send chat message

                    ClientSend.SendChat(Text.text);

                    Text.text = "";
                }
            }
        }



    }


    public void OpenChat()
    {
        if (!Text.isFocused)
        {
            Text.ActivateInputField();
        }


        if (_Object.active == false)
        {
            _Object.SetActive(true);

            //disable moving, block break, put during chat
            DisableGameObject.SetActive(false);
            ischatting = true;

        }
        else
        if (_Object.active == true)
        {


            _Object.SetActive(false);

            ischatting = false;
            DisableGameObject.SetActive(true);

            if (Text.text != "")
            {
                //send chat message
                ClientSend.SendChat(Text.text);

                Text.text = "";
            }
        }
    }

    public void HideChatText()
    {
        _Object.SetActive(false);

    }
}

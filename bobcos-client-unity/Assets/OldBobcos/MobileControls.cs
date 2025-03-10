using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class MobileControls : MonoBehaviour
{
   public Player player;
    public float axisspeed = 0;
    public float VerticalNoclipOnlyAxis = 0;
    public static MobileControls instance;
    public GameObject NoclipDownButton;
    public void Start()
    {
        instance = this;
    }
    public void Update()
    {

        if(player != null)
        {

            if (!player.NoclipMode)
            {
                NoclipDownButton.SetActive(false);


                player.rb.velocity = new Vector2((player.axis * player.Speed), player.rb.velocity.y);
                if (player.iswalking)
                {
                    player.Background.transform.position = new Vector3(player.Background.transform.position.x + -((player.axis * player.Speed) / 175), player.Background.transform.position.y);

                }
            }
            else
            {
                NoclipDownButton.SetActive(true);

                player.transform.position = new Vector3(player.transform.position.x + player.axis / 5, player.transform.position.y + VerticalNoclipOnlyAxis / 5,-5f);


                if (player.iswalking)
                {
                    player.Background.transform.position = new Vector3(player.Background.transform.position.x + -((player.axis * player.Speed) / 175), player.Background.transform.position.y);

                }







            }
        }
        






    }
    public void MouseLeft()
    {
      

        try
        {
            player.axis = -1f;

        }
        catch
        {

        }

    }
    public void mouseRight()
    {
        try
        {
            player.axis = 1f;

        }
        catch
        {

        }
    }

    public void mouse0()
    {
        try
        {
            player.axis = 0f;

        }
        catch
        {

        }

    }


    public void mouse0_1()
    {
        try
        {
            VerticalNoclipOnlyAxis = 0f;

        }
        catch
        {

        }

    }

    public void Down()
    {
        try
        {
            VerticalNoclipOnlyAxis = -1f;

        }
        catch
        {

        }
    }

    public void Jump()
    {
        if (instance == null)
        {
            instance = this;

        }
        VerticalNoclipOnlyAxis = 1f;

        if (player.TotalJumpAvailable > 0)
        {
            player.TotalJumpAvailable--;
            player.isjumped = true;

            player.rb.AddForce(new Vector3(player.rb.velocity.x, player.JumpPower));
            AudioManager.instance.Jump();
            PlayerApperance apprence = player.gameObject.GetComponentInChildren<PlayerApperance>();



         

                 player.GetComponentInChildren<PlayerAnimator>().anim.SetBool("Ypos", true);
            AudioManager.instance.Jump();
        }

    }


    public void Jump0()
    {
        PlayerApperance apprence = player.gameObject.GetComponentInChildren<PlayerApperance>();



      

        if (player.rb.velocity.y > 3)
        {
            player.rb.velocity = new Vector3(player.rb.velocity.x, 3f);

        }
    }

}

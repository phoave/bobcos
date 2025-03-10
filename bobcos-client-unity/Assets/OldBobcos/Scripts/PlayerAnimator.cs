using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator anim;
    public GameObject Player;
    // Update is called once per frame
    public  float minpos;
     Vector3 OldPos;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (OldPos.y > Player.transform.position.y)
        {
            //play falling anim
            anim.SetBool("Ypos", false);

            anim.Play("PlayerFall"); PlayerApperance apprence = GetComponent<PlayerApperance>();




        }
        if (OldPos.x != Player.transform.position.x)
        {
            // Player is walking


            if(OldPos.x > Player.transform.position.x)
            {
               transform.localScale = new Vector3(-3f, 2.49f, 2.5f);
            }else
            {
               transform.localScale = new Vector3(3f, 2.49f, 2.5f);

            }

            


            anim.SetBool("IsWalking", true);
        }else
        {
            anim.SetBool("IsWalking", false);

        }


        
        
        OldPos = Player.transform.position;

    }


}

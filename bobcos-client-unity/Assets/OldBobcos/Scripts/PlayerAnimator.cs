using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator anim;
    public GameObject Player;
    private Vector3 OldPos;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (OldPos.y > Player.transform.position.y)
        {
            // Play falling animation
            anim.SetBool("Ypos", false);
            anim.Play("PlayerFall");
        }

        if (OldPos.x != Player.transform.position.x)
        {
            // Player is walking
            if (OldPos.x > Player.transform.position.x)
            {
                transform.localScale = new Vector3(-3f, 2.49f, 2.5f);
            }
            else
            {
                transform.localScale = new Vector3(3f, 2.49f, 2.5f);
            }

            anim.SetBool("IsWalking", true);
        }
        else
        {
            anim.SetBool("IsWalking", false);
        }
        if(UICLICK.IsBusy)
        {
            anim.SetBool("IsPunching", true);
        }
        else
        {
            anim.SetBool("IsPunching", false);
        }
        OldPos = Player.transform.position;
    }

    // New method to trigger punch animation

}

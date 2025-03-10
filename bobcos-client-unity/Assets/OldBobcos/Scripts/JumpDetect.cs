using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpDetect : MonoBehaviour
{
    public GameObject Parent;



    public void OnCollisionEnter2D(Collision2D collision)
    {
        Player plrscr = Parent.GetComponent<Player>();

        plrscr.totalcollider++;
        plrscr.isjumped = false;
        plrscr.TotalJumpAvailable = plrscr.MaxJumpAvailable;
       

      



    }
   

}

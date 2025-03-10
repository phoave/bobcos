using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerApperance : MonoBehaviour
{
    [Header("Dont change this objects")]
    public SpriteRenderer Shirt,Pant1,Pant2,Arm1,Arm2,Shoe1,shoe2,Head,back,handitem;
    public SpriteRenderer Face;
    [Header("Player parts")]
    public SpriteRenderer Torso, Leg1, leg2, Arm1_, Arm2_, foot1, foot2, Hair_,Hat_;
    public SpriteRenderer badge;
    public Sprite[] badges;

    public int backitemrightnow;
   
    public void UpdatePlayerApperance(Color32 SkinColor, int ShirtId, int PantId, int ShoeId, int BackId, int HairId,int HatId,int _handitem,byte badgeid)
    {
        badge.sprite = badges[badgeid];
        backitemrightnow = BackId;
        Torso.color = SkinColor;
      //  Leg1.color = SkinColor;
      //  leg2.color = SkinColor;
        Arm1_.color = SkinColor;
        Arm2_.color = SkinColor;
        foot1.color = SkinColor;
        foot2.color = SkinColor;
        Head.color = SkinColor;


       
    }
    int animOrder = 0;
    Sprite[] anims;
    public bool Animate;

    IEnumerator RepeatAnim()
    {
        while (Animate)
        {


            yield return new WaitForSeconds(0.15f);

            if (animOrder == anims.Length)
            {
                animOrder = 0;
            }
            try
            {


                handitem.sprite = anims[animOrder];
            }
            catch
            {

            }
            animOrder++;
        }
    }
}

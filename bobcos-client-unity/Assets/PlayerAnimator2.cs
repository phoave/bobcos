using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator2 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Player;
    public GameObject AnimObj;
    public SpriteRenderer MainSprite;
    int currentanimid;
    int oldc;
    public Sprite Empty;
    Vector3 OldPos;

    public int shirtId, PantId, ShoeId, HairId, HatId, BackId, HandId;
    public SpriteRenderer Shirt, Pant, Shoe, Hair, Hat, Back, Hand;

    public Sprite[] animations;
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {

        
        

        
        if(OldPos.x > Player.transform.position.x)
        {
            AnimObj.transform.localScale = new Vector3(-1, 1, 1);

        }else
        if (OldPos.x < Player.transform.position.x)
        {
            AnimObj.transform.localScale = new Vector3(1, 1, 1);

        }
        else
        {
        }

        if (Player.GetComponent<Rigidbody2D>().velocity.y > 0.05f)
        {
            currentanimid = 5; UpdateCloths();

            MainSprite.sprite = animations[currentanimid];

        }
        if (Player.GetComponent<Rigidbody2D>().velocity.y < -0.2f)
        {
            
          playFallingAnim();
        }

        if (Player.GetComponent<Rigidbody2D>().velocity.x != 0 && !(Player.GetComponent<Rigidbody2D>().velocity.y > 0.05f))
        {
            if (!iswalking) { StartCoroutine(playWalkingAnim()); }

        }
        else
        {
            if (ispunching == false )
            {
                if (iswalking == false)
                {


                    currentanimid = 4; UpdateCloths();

                MainSprite.sprite = animations[currentanimid];
                    
                }
            }
            iswalking = false;
            if (Player.GetComponent<Rigidbody2D>().velocity.y > 0.05f)
            {
                currentanimid = 5; UpdateCloths();

                MainSprite.sprite = animations[currentanimid];
            }
            if (Player.GetComponent<Rigidbody2D>().velocity.y < -0.07f)
            {
                currentanimid = 7;
                UpdateCloths();
                MainSprite.sprite = animations[currentanimid];
            }
        }


       


        OldPos = Player.transform.position;
    }
    public void UpdateCloths()
    {
        foreach (HandItem i in ItemManager.instance.HandItems)
        {

            if (HandId < 1)
            {
                Hand.sprite = Empty;
            }

            if (HandId != 0)
                if (i.id == HandId)
                {
                    if (i.anims.Length > 0)
                        Hand.sprite = i.anims[currentanimid];
                }

        }
        foreach (Shirt i in ItemManager.instance.ShirtItems)
        {
            if (shirtId < 1)
            {
                Shirt.sprite = Empty;
            }
            if (shirtId != 0)

                if (i.id == shirtId)
            {
                    if (i.anims.Length > 0)
                        Shirt.sprite = i.anims[currentanimid];
            }
        }
        foreach (Pant i in ItemManager.instance.PantItems)
        {
            if (PantId < 1)
            {
                Pant.sprite = Empty;
            }
            if (PantId != 0)

                if (i.id == PantId)
            {
                if (i.anims.Length > 0)
                    Pant.sprite = i.anims[currentanimid];
            }
        }
        foreach (Shoe i in ItemManager.instance.ShoeItems)
        {
            if (ShoeId < 1)
            {
                Shoe.sprite = Empty;
            }
            if (ShoeId != 0)

                if (i.id == ShoeId)
            {
                    if (i.anims.Length > 0)
                        Shoe.sprite = i.anims[currentanimid];
            }
        }
        foreach (backitem i in ItemManager.instance.BackItems)
        {
            Player.GetComponent<Player>().MaxJumpAvailable = i.JumpCount;
            Player.GetComponent<Player>().JumpPower = i.jumppower;


            if (BackId < 1)
            {
                Back.sprite = Empty;
            }
            if (BackId != 0)

                if (i.id == BackId)
            {
                    if (i.anims.Length > 0)
                        Back.sprite = i.anims[currentanimid];
            }
        }

        foreach (Hat i in ItemManager.instance.HatItems)
        {
            if (HatId < 1)
            {
                Hat.sprite = Empty;
            }
            if (HatId != 0)

                if (i.id == HatId)
            {
                    if (i.anims.Length > 0)
                        Hat.sprite = i.anims[currentanimid];
            }
        }

        foreach (Hair i in ItemManager.instance.HairItems)
        {
            if (HairId < 1)
            {
                Hair.sprite = Empty;
            }
            if (HairId != 0)

                if (i.id == HairId)
            {
                    if (i.anims.Length > 0)
                        Hair.sprite = i.anims[currentanimid];
            }
        }
       
    }
   public bool iswalking = false;
   public IEnumerator playWalkingAnim()
    {
        iswalking = true;
        while(iswalking)
        {
            currentanimid = 0; UpdateCloths();

            MainSprite.sprite = animations[currentanimid];
            if(!iswalking)
            {
                break;
            }
            yield return new WaitForSeconds(6f * Time.deltaTime);
            currentanimid = 1; UpdateCloths();

            MainSprite.sprite = animations[currentanimid];
            if (!iswalking)
            {
                break;
            }
            yield return new WaitForSeconds(6f * Time.deltaTime);
            currentanimid = 2; UpdateCloths();

            MainSprite.sprite = animations[currentanimid];
            if (!iswalking)
            {
                break;
            }
            yield return new WaitForSeconds(6f * Time.deltaTime);
            currentanimid = 3; UpdateCloths();

            MainSprite.sprite = animations[currentanimid];
            if (!iswalking)
            {
                break;
            }
            yield return new WaitForSeconds(6f * Time.deltaTime);
        }
    }
    void playFallingAnim()
    {
       
            currentanimid = 6; UpdateCloths();

        MainSprite.sprite = animations[currentanimid];
            
            
           
        }

    bool ispunching = false;
    public IEnumerator Punch()
    {
        ispunching = true;
        currentanimid = 8; UpdateCloths();

        MainSprite.sprite = animations[currentanimid];

        yield return new WaitForSeconds(2f * Time.deltaTime);
        currentanimid = 9; UpdateCloths();

        MainSprite.sprite = animations[currentanimid];
        yield return new WaitForSeconds(2f * Time.deltaTime);
        currentanimid = 10; UpdateCloths();

        MainSprite.sprite = animations[currentanimid];
        yield return new WaitForSeconds(2f * Time.deltaTime);
        currentanimid = 11; UpdateCloths();

        MainSprite.sprite = animations[currentanimid];

        if (!istalking)
        {


            yield return new WaitForSeconds(2f * Time.deltaTime);
            currentanimid = 4; UpdateCloths();
        }
        MainSprite.sprite = animations[currentanimid];
        ispunching = false;

    }
    bool istalking = false;
    public IEnumerator Talk()
    {
        istalking = true;
        currentanimid = 13;
        MainSprite.sprite = animations[currentanimid];

        yield return new WaitForSeconds(10 * Time.deltaTime);
        currentanimid = 13;
        MainSprite.sprite = animations[currentanimid];
        yield return new WaitForSeconds(10 * Time.deltaTime);
        currentanimid = 13;
        MainSprite.sprite = animations[currentanimid];
        yield return new WaitForSeconds(10 * Time.deltaTime);
        currentanimid = 13;
        MainSprite.sprite = animations[currentanimid];
        yield return new WaitForSeconds(10 * Time.deltaTime);
        currentanimid = 13;
        MainSprite.sprite = animations[currentanimid];
        yield return new WaitForSeconds(10 * Time.deltaTime);
        currentanimid = 13;
        MainSprite.sprite = animations[currentanimid]; yield return new WaitForSeconds(9f * Time.deltaTime);
        currentanimid = 13;
        MainSprite.sprite = animations[currentanimid];
        yield return new WaitForSeconds(10 * Time.deltaTime);
        currentanimid = 13;
        MainSprite.sprite = animations[currentanimid]; yield return new WaitForSeconds(9f * Time.deltaTime);
        currentanimid = 13;
        MainSprite.sprite = animations[currentanimid];
        yield return new WaitForSeconds(10 * Time.deltaTime);
        currentanimid = 13;
        MainSprite.sprite = animations[currentanimid];

        yield return new WaitForSeconds(10 * Time.deltaTime);
        currentanimid = 4;
        MainSprite.sprite = animations[currentanimid];
        istalking = false;

    }

    public void UpdatePlayerApperance(Color32 cOlor, short shirt, short pant, short shoes, short backid, short headid, short hatid, short handitem, byte badge)
    {
        shirtId = shirt;
        PantId = pant;
        ShoeId = shoes;
        BackId = backid;
        HairId = headid;
        HatId = hatid;
        HandId = handitem;
        UpdateCloths();

    }
}





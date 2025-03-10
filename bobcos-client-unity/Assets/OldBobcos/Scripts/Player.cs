using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int id;
    public string username;
    public bool ismyplayer = false;
    private bool send = false;
    public float axis = 0;
    public float Speed = 1;
    public Rigidbody2D rb;
    public Vector3 oldposition;
    public GameObject VisibleBody;
    public GameObject Background;
    public bool isjumped = false;
    public bool iswalking = false;
    public bool NoclipMode = false;
    public float lerpdelay = 5;
    public bool ignore1 = false;

    public int TotalJumpAvailable = 1;
    public int JumpPower = 250;
    public int MaxJumpAvailable = 1;

    public CapsuleCollider2D JumpDetector;

    public Vector3 lerppos;

    public Sprite Normalface, HurtFace;


   public int totalcollider = 0;

    public GameObject Trading, Chatting;
    public void Setup(string _username,int _id)
    {






        lerppos = transform.position;
        username = _username;
        id = _id;
        GetComponentInChildren<TextMesh>().text = username;

        if (id == 0)
        {
            AdvancedCamera.instance.Player = this.gameObject;


            rb = GetComponent<Rigidbody2D>();

            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            GetComponent<CapsuleCollider2D>().enabled = true;
            JumpDetector.enabled = true;
            JumpDetector.isTrigger = false;


            GameManager.instance.Camera.SetActive(false);
            //local
           
            ismyplayer = true; try
            {
                MobileControls.instance.player = this;

            }
            catch (Exception ex)
            {


            }
        }
        else
        {
            JumpDetector.isTrigger = true;

            Background.SetActive(false);
        }

    }

   
    public void Hurt()
    {
        GetComponentInChildren<PlayerApperance>().Face.sprite = HurtFace;
        StartCoroutine(SetBackToNormalFace());
    }
    IEnumerator SetBackToNormalFace()
    {
        yield return new WaitForSeconds(1.7f);
        GetComponentInChildren<PlayerApperance>().Face.sprite = Normalface;

    }

    public void Update()
    {
       

        

        if(!ismyplayer)
        {
            transform.position = Vector3.Lerp(transform.position, lerppos, 6 * Time.deltaTime);
        }



        if (totalcollider > 2)
        {
            totalcollider = 2;
        }

        if(totalcollider < 0)
        {
            totalcollider = 0;
        }
        if (oldposition != transform.position)
        {
            send = true;
            iswalking = true;

            
            if (ismyplayer)
            {


                ClientSend.SendPosition(transform.position.x, transform.position.y,isjumped);
            }
        }
        else
        {
            send = false;
            iswalking = false;

        }
        
        oldposition = transform.position;
        if (NoclipMode && ismyplayer)
        {

            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            if (Input.GetKey(KeyCode.A))
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x - 0.1f, gameObject.transform.position.y, gameObject.transform.position.z);
            }
            if (Input.GetKey(KeyCode.D))
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x + 0.1f, gameObject.transform.position.y, gameObject.transform.position.z);
            }

            if (Input.GetKey(KeyCode.W))
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.07f, gameObject.transform.position.z);
            }
            if (Input.GetKey(KeyCode.S))
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.07f, gameObject.transform.position.z);
            }
            return;
        }
        else if (!NoclipMode && ismyplayer)
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        try
        {
            if (ismyplayer == true && !ChatScript.instance.ischatting)
            {

                if (Input.GetKeyDown(KeyCode.A))
                {
                    axis = -1f ;
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    axis = 1f;
                }
                if (Input.GetKeyUp(KeyCode.D))
                {
                    axis = 0f ;
                }
                if (Input.GetKeyUp(KeyCode.A))
                {
                    axis = 0f;
                }

                rb.velocity = new Vector2((axis * Speed ) , rb.velocity.y);
                if (iswalking)
                {
                    Background.transform.position = new Vector3(Background.transform.position.x + -((axis * Speed) / 175), Background.transform.position.y);

                }
                if (Input.GetKeyDown(KeyCode.W))
                {
                    if (TotalJumpAvailable > 0)
                    {
                        isjumped = true;
                        TotalJumpAvailable--;



                       
                        rb.AddForce(new Vector3(rb.velocity.x, JumpPower));
                        AudioManager.instance.Jump();

                    }
                }

                if (Input.GetKeyUp(KeyCode.W))
                {
                    



                     

                        if(rb.velocity.y > 3)
                    {
                        rb.velocity = new Vector3(rb.velocity.x, 3f);

                    }


                    
                }



            }

        }
        catch
        {

        }



    }
   



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public string Version = "1.0";
    public GameObject Camera;
    public string Launguage;

    public bool ServerResponseReceived = false;


    public GameObject[] Blocks;
    public GameObject[] BGBlocks;
    public GameObject[] BreakingTileBlocks;

    public Sprite[] BreakingTileImages;

    public string key = "";

    public int[,] BlockPos = new int[100,100];
    public int[,] BlockPosFG = new int[100, 100];
    public int[,] BlockSira = new int[100, 100];

    public Slider Grafic;
    

    public GameObject ShadowObject,OrangeLightObject;

    public Sprite Day, Night;
    public Light2D Sun;
    public void Start()
    {
        instance = this;
        Launguage = Application.systemLanguage.ToString();
       
    }
    public void Onconnected()
    {
        StartCoroutine(Refresher2());

    }

    

    public void SetDay()
    {
        AdvancedCamera.instance.Background.GetComponent<SpriteRenderer>().sprite = Day;
        Sun.intensity = 1.10f;
          
    }

    public  void SetNight()
    {
        AdvancedCamera.instance.Background.GetComponent<SpriteRenderer>().sprite = Night;
        Sun.intensity = 0.19f;

    }

    public void GraphicsUpdate()
    {
        if(Grafic.value == 0f)
        {
            //disable shadows
            Sun.shadowIntensity = 0f;
            UIManager.instance.SetWarningOn("Graphics mode : LOW");
        }else
        {
            Sun.shadowIntensity = 0.29f;
            UIManager.instance.SetWarningOn("Graphics mode : HIGH");

        }
    }




    
    public void AddBreaking(int order, int breakcode)
    {
        try
        {


            BreakingTileBlocks[order].GetComponent<SpriteRenderer>().sprite = BreakingTileImages[breakcode];
            // wait 5 seconds to cancel breaking client side
            StartCoroutine(ResetBreaking(order));
        }
        catch
        {


        }
        }

    IEnumerator ResetBreaking(int order)
    {
        yield return new WaitForSeconds(5);
        BreakingTileBlocks[order].GetComponent<SpriteRenderer>().sprite = BreakingTileImages[0];

    }

    IEnumerator Refresher2()
    {
        while (true)
        {
            yield return new WaitForSeconds(6);
            ClientSend.Refresh("R");
          

        }

    }



    public void LoadBlockFromArray(short[] data)
    {




        int pos = 0;
        foreach (GameObject i in Blocks)
        {
            try
            {

                BlockSira[(int)i.transform.position.x, (int)i.transform.position.y] = pos;

                BlockPos[(int)i.transform.position.x, (int)i.transform.position.y] = data[pos];
                Destroy(Blocks[pos].GetComponent<TileScript>().ShadowObj);
            }
            catch { Debug.Log("error"); }


            pos++;
        }
        pos = 0;
        foreach (GameObject i in Blocks)
        {
            i.GetComponent<TileScript>().SetSiraid(pos);

            pos++;

        }
        pos = 0;

        foreach (GameObject i in Blocks)
        {
           
            i.GetComponent<TileScript>().SetId(data[pos],1);
            BlockPos[(int)i.transform.position.x, (int)i.transform.position.y] = data[pos];

            BlockSira[(int)i.transform.position.x, (int)i.transform.position.y] = pos;


            pos++;
        }
    }
    public void LoadBGBlockFromArray(short[] data)
    {
        int pos = 0;
        foreach (GameObject i in Blocks)
        {
            try
            {


                BlockPosFG[(int)i.transform.position.x, (int)i.transform.position.y] = data[pos];
                Destroy(BGBlocks[pos].GetComponent<TileScript>().ShadowObj);

            }
            catch { Debug.Log("error"); }


            pos++;
        }
         pos = 0;
        foreach (GameObject i in BGBlocks)
        {
            i.GetComponent<TileScript>().SetSiraid(pos);

            i.GetComponent<TileScript>().SetId(data[pos],0);
            pos++;
        }
    }

    public  short[] DiziBirlestir(short[] dizi1, short[] dizi2)
    {
        short[] sonuc = new short[dizi1.Length + dizi2.Length];

        for (int i = 0; i < dizi1.Length; i++)
        {
            sonuc[i] = dizi1[i];
        }

        for (int i = 0; i < dizi2.Length; i++)
        {
            sonuc[dizi1.Length + i] = dizi2[i];
        }
        return sonuc;
    }

    public void GetKey()
    {
        int i = 0;
        foreach(GameObject g in Blocks)
        {
            key = key + g.GetComponent<TileScript>().siraid + ":" + (int)g.transform.position.x + ":" + (int)g.transform.position.y + ",";

                }
    }
}

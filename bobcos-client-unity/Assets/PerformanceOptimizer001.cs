using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PerformanceOptimizer001 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<ShadowCaster2D>().enabled = false;
        StartCoroutine(Refresher1in4());

    }

    // Update is called once per frame
   

   
    IEnumerator Refresher1in4()
    {
        yield return new WaitForSeconds(0.25f);
        if (PlayerManager.instance.Players[0] != null)
        {
           
                float distancebeetwenblockandplayer = Vector3.Distance(PlayerManager.instance.Players[0].transform.position, transform.position);


                if (distancebeetwenblockandplayer > 16)
                {
                    GetComponent<ShadowCaster2D>().enabled = false;
                }
                else
                {
                    GetComponent<ShadowCaster2D>().enabled = true;

                }

            
        }
        StartCoroutine(Refresher1in4());
    }
}

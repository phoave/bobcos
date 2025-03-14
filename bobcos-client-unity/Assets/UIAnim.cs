using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnim : MonoBehaviour
{
    public Vector3 oldscale;
    RectTransform rect;
    void Start()
    {
        rect = gameObject.GetComponent<RectTransform>();
       oldscale = rect.localScale;
        StartCoroutine(waitFor());

    }

    public void OnEnable()
    {
        StartCoroutine(waitFor());   
    }
  
    IEnumerator waitFor()
    {
        rect.localScale = new Vector3(0.001f, 0.001f, 0.001f);

        for (int i = 0; i< 400; i++)
        {
            yield return new WaitForSeconds(0.02f * Time.deltaTime);
            rect.localScale = new Vector3(rect.localScale.x + 0.03f, rect.localScale.y + 0.03f, 1f);

       if(rect.localScale.x > oldscale.x)
            {
                rect.localScale = new Vector3(oldscale.x, rect.localScale.y, 1);
            }
            if (rect.localScale.y > oldscale.y)
            {
                rect.localScale = new Vector3(rect.localScale.x,oldscale.y, 1);
            }
        }
    }
}

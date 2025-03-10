using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBeetwenLandR : MonoBehaviour
{
    public GameObject L, R;
    // Start is called before the first frame update
   
    

    public void ToggleClicked(bool isclicked)
    {

        if(isclicked)
        {
            R.SetActive(true);
            L.SetActive(false);

        }
        else

        {
            R.SetActive(false);
            L.SetActive(true);
        }
    }
}

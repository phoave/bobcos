using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;
    public  Block[]  BlockItems;
    public Shirt[] ShirtItems;
    public Pant[] PantItems;
    public Shoe[] ShoeItems;
    public Hair[] HairItems;
    public Hat[] HatItems;
    public HandItem[] HandItems;
    public backitem[] BackItems;
    public Sprite grassdirt;

    public void Start()
    {
        instance = this;
    }


}



[System.Serializable]
public class Block
{
    public int id;
    public bool iscollider;
    public Sprite Icon;
    public Sprite BlockTexture;
    public bool isdamageBlock = false;
    public bool IsWater = false;
    public bool OrangeLight = false;
    public bool SpotLight = false;
    public Sprite[] Animations;

}

[System.Serializable]
public class Shirt
{
    public int id;
    public Sprite Icon;
    public Sprite[] anims;
    public Vector3 offset;
}


[System.Serializable]
public class Pant
{
    public int id;
    public Sprite Icon;
    public Sprite[] anims;

    public Vector3 offset;

}
[System.Serializable]

public class Shoe
{
    public int id;
    public Sprite Icon;
    public Sprite[] anims;

    public Vector3 offset;

}
[System.Serializable]

public class Hair
{
    public int id;
    public Sprite Icon;
    public Sprite[] anims;
    public Vector3 offset;

}

[System.Serializable]

public class Hat
{
    public int id;
    public Sprite Icon;
    public Sprite[] anims;
    public Vector3 offset;

}
[System.Serializable]

public class HandItem
{
    public int id;
    public Sprite Icon;
    public Sprite[] anims;
    public Vector3 offset;


    public Sprite[] sprites;
}

[System.Serializable]

public class backitem
{
    public int id;
    public Sprite Icon;
    public Sprite[] anims;


    public int jumppower = 300;
    public int JumpCount = 1;
    public Vector3 Offset0;
    public Vector3 Offset1;

}



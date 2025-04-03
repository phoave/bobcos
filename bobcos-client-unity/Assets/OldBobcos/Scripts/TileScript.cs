using System.Collections;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    public int id;
    public bool Collide;
    public int siraid;
    public bool isdamage;
    public bool iswater;
    public bool isPlatform; 
    public GameObject ShadowObj;
    public GameObject LightObj;
    bool Animate;
    Sprite[] anims;

    public void SetId(int _id, int fg1bg0)
    {
        GameManager.instance.BlockPos[(int)transform.position.x, (int)transform.position.y] = _id;
        id = _id;

        foreach (Block i in ItemManager.instance.BlockItems)
        {
            if (i.id == id)
            {
                isdamage = i.isdamageBlock;
                iswater = i.IsWater;
                isPlatform = i.isPlatform; 
                
             
                if (iswater)
                {
                    GetComponent<SpriteRenderer>().sortingOrder = 300;
                }
                else
                {
                    GetComponent<SpriteRenderer>().sortingOrder = 0;
                }

                GetComponent<SpriteRenderer>().sprite = i.BlockTexture;

                if (id == 3)
                {
                    try
                    {
                        if (GameManager.instance.BlockPos[(int)transform.position.x, (int)transform.position.y + 1] != 3)
                        {
                            GetComponent<SpriteRenderer>().sprite = ItemManager.instance.grassdirt;
                        }
                    }
                    catch
                    {
                        Debug.Log("Error");
                    }
                }

                try
                {
                    if (GameManager.instance.BlockPos[(int)transform.position.x, (int)transform.position.y - 1] == 3)
                    {
                        GameManager.instance.Blocks[GameManager.instance.BlockSira[(int)transform.position.x, (int)transform.position.y - 1]].GetComponent<TileScript>().SetId(3, fg1bg0);
                    }
                }
                catch
                {
                    Debug.Log("Error");
                }

                if (i.OrangeLight)
                {
                    LightObj = Instantiate(GameManager.instance.OrangeLightObject);
                    LightObj.transform.position = transform.position;
                }
                else
                {
                    if (LightObj != null)
                    {
                        Destroy(LightObj);
                    }
                }

                if (i.iscollider && !isPlatform) 
                {
                    Collide = true;
                    GetComponent<Collider2D>().isTrigger = false;
                }
                else if (isPlatform)
                {
                    Collide = false;
                    GetComponent<Collider2D>().isTrigger = false; // Ensure it is not a trigger
                    PlatformEffector2D effector = GetComponent<PlatformEffector2D>();
                    if (effector == null)
                    {
                        effector = gameObject.AddComponent<PlatformEffector2D>();
                    }
                    effector.useOneWay = true; // Allow one-way collision
                    effector.rotationalOffset = 0f; // Default: from top only
                }
                else
                {
                    Collide = false;
                    GetComponent<Collider2D>().isTrigger = true; 
                    if (ShadowObj != null)
                    {
                        Destroy(ShadowObj);
                    }
                }

                
                if (i.Animations != null && i.Animations.Length > 0)
                {
                    Animate = true;
                    anims = i.Animations;
                    StartCoroutine(RepeatAnim());
                }
                else
                {
                    Animate = false;
                    anims = null;
                }
            }
        }
    }

    int animOrder = 0;
    IEnumerator RepeatAnim()
    {
        while (Animate)
        {
            yield return new WaitForSeconds(0.15f);
            if (anims != null)
            {
                if (animOrder == anims.Length)
                {
                    animOrder = 0;
                }
                try
                {
                    GetComponent<SpriteRenderer>().sprite = anims[animOrder];
                }
                catch
                {
                }
                animOrder++;
            }
        }
    }

    public void SetSiraid(int _Sira)
    {
        siraid = _Sira;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isdamage)
        {
            ClientSend.SendDamageData((short)id);
            PlayerManager.instance.Players[0].GetComponent<Player>().isjumped = true;
            PlayerManager.instance.Players[0].GetComponent<Player>().TotalJumpAvailable--;
            PlayerApperance apprence = PlayerManager.instance.Players[0].GetComponentInChildren<PlayerApperance>();

            float random1 = UnityEngine.Random.Range(-400, 400);
            PlayerManager.instance.Players[0].GetComponent<Player>().rb.AddForce(new Vector3(PlayerManager.instance.Players[0].GetComponent<Player>().rb.linearVelocity.x + random1, 150));
            PlayerManager.instance.Players[0].GetComponent<Player>().Hurt();
        }
    }
}

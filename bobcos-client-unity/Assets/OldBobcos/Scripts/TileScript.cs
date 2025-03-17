using System.Collections;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    public int id;
    public bool Collide;
    public int siraid;
    public bool isdamage;
    public bool iswater;
    public bool isPlatform; // New platform property
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
                isPlatform = i.isPlatform; // Check if tile is a platform
                
                // Handle sprite rendering based on tile type
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

                if (i.iscollider && !isPlatform) // Only set solid collision for non-platform tiles
                {
                    Collide = true;
                    GetComponent<Collider2D>().isTrigger = false;
                }
                else
                {
                    Collide = false;
                    GetComponent<Collider2D>().isTrigger = true; // Set trigger for platforms so player can jump through
                    if (ShadowObj != null)
                    {
                        Destroy(ShadowObj);
                    }
                }

                // Check if block has animations
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

    // Coroutine to repeat animation
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
            PlayerManager.instance.Players[0].GetComponent<Player>().rb.AddForce(new Vector3(PlayerManager.instance.Players[0].GetComponent<Player>().rb.velocity.x + random1, 150));
            PlayerManager.instance.Players[0].GetComponent<Player>().Hurt();
        }
    }
}

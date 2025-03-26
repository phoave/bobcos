using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerApperance : MonoBehaviour
{
    [Header("Dont change these objects")]
    public SpriteRenderer Shirt, Pant1, Pant2, Arm1, Arm2, Shoe1, Shoe2, Head, Back, HandItem;
    public SpriteRenderer Face;

    [Header("Player parts")]
    public SpriteRenderer Torso, Leg1, Leg2, Arm1_, Arm2_, Foot1, Foot2, Hair_, Hat_;
    public SpriteRenderer Badge;

    public Sprite[] Badges;
    public int BackItemRightNow;

    private Vector3 oldPos;

    private void Start()
    {
        oldPos = transform.position; // Initialize old position
    }

    void Update()
    {
        // **Detect if player is jumping (y increasing)**
        if (transform.position.y > oldPos.y)  // Jumping
        {
            UpdateBackItem(false);  // Set to jumping sprite
        }
        else if (transform.position.y < oldPos.y)  // Falling
        {
            UpdateBackItem(true); // Set to normal sprite
        }

        oldPos = transform.position; // Update old position
    }

    // **Update Back Item Based on Jump State**
    public void UpdateBackItem(bool isJumping)
    {
        foreach (backitem i in ItemManager.instance.BackItems)
        {
            if (i.id == BackItemRightNow)
            {
                // **Change sprite based on jump state**
                if (i.anims.Length > 1) // Ensure there's a jump animation
                {
                    Back.sprite = isJumping ? i.anims[1] : i.anims[0];
                }
                else
                {
                    Back.sprite = i.anims[0]; // Fallback to normal if only one sprite exists
                }
                return;
            }
        }
    }

    // **Set image method for updating item sprites**
    public void SetImage(int si, SpriteRenderer item2)
    {
        foreach (Shirt i in ItemManager.instance.ShirtItems)
        {
            if (i.id == si)
            {
                item2.sprite = i.Icon;
                return;
            }
        }

        foreach (Pant i in ItemManager.instance.PantItems)
        {
            if (i.id == si)
            {
                item2.sprite = i.Icon;
                return;
            }
        }

        foreach (Shoe i in ItemManager.instance.ShoeItems)
        {
            if (i.id == si)
            {
                item2.sprite = i.Icon;
                return;
            }
        }

        foreach (Hat i in ItemManager.instance.HatItems)
        {
            if (i.id == si)
            {
                item2.sprite = i.Icon; // Hats use 'Icon'
                return;
            }
        }

        foreach (Hair i in ItemManager.instance.HairItems)
        {
            if (i.id == si)
            {
                item2.sprite = i.Icon;
                return;
            }
        }

        foreach (backitem i in ItemManager.instance.BackItems)
        {
            if (i.id == si)
            {
                item2.sprite = i.anims[0]; 
                return;
            }
        }

        foreach (HandItem i in ItemManager.instance.HandItems)
        {
            if (i.id == si)
            {
                item2.sprite = i.Icon;
                return;
            }
        }

        Debug.LogWarning($"Item with id {si} not found.");
    }

    // **Update the player's appearance**
    public void UpdatePlayerAppearance(Color32 SkinColor, int ShirtId, short PantId, int ShoeId, int BackId, int HairId, int HatId, int HandItemId, byte BadgeId)
    {
        // **Set Badge Sprite**
        if (BadgeId >= 0 && BadgeId < Badges.Length)
        {
            Badge.sprite = Badges[BadgeId];
        }
        else
        {
            Debug.LogWarning($"Invalid BadgeId: {BadgeId}. Index out of bounds.");
        }

        // **Set Skin Color**
        Torso.color = SkinColor;
        Leg1.color = SkinColor;
        Leg2.color = SkinColor;
        Arm1_.color = SkinColor;
        Arm2_.color = SkinColor;
        Foot1.color = SkinColor;
        Foot2.color = SkinColor;
        Head.color = SkinColor;

        // **Set Clothing Items**
        SetImage(ShirtId, Shirt);
        SetImage(PantId, Pant1);
        SetImage(PantId, Pant2);
        Pant2.transform.localScale = new Vector3(-Mathf.Abs(Pant2.transform.localScale.x), Pant2.transform.localScale.y, Pant2.transform.localScale.z);
        SetImage(ShoeId, Shoe1);
        SetImage(ShoeId, Shoe2);
        SetImage(HairId, Hair_);
        SetImage(HatId, Hat_);
        SetImage(HandItemId, HandItem);

        // **Set Back Item**
        BackItemRightNow = BackId;
        UpdateBackItem(false); // Default to normal state
    }

    // **Animation Handling for Hand Items**
    int animOrder = 0;
    public Sprite[] Animations;
    public bool Animate;

    IEnumerator RepeatAnim()
    {
        while (Animate)
        {
            yield return new WaitForSeconds(0.15f);

            if (animOrder == Animations.Length)
            {
                animOrder = 0;
            }

            try
            {
                HandItem.sprite = Animations[animOrder];
            }
            catch
            {
                Debug.LogWarning("Error setting hand item sprite during animation.");
            }

            animOrder++;
        }
    }
}

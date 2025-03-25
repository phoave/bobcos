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

    // Set image method to update sprites for specific items by id
    public void SetImage(int si, SpriteRenderer item2)
    {
        // Check and update sprite for each item type
        foreach (Block i in ItemManager.instance.BlockItems)
        {
            if (i.id == si)
            {
                item2.sprite = i.Icon;
                return;
            }
        }

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
                item2.sprite = i.Icon;
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
                item2.sprite = i.Icon;
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

        // If no item was found with the specified id, log a warning
        Debug.LogWarning($"Item with id {si} not found.");
    }

    // Method to update the player's appearance based on received data
    public void UpdatePlayerAppearance(Color32 SkinColor, int ShirtId, short PantId, int ShoeId, int BackId, int HairId, int HatId, int HandItemId, byte BadgeId)
    {
        // Update badge sprite
        if (BadgeId >= 0 && BadgeId < Badges.Length)
        {
            Badge.sprite = Badges[BadgeId];
        }
        else
        {
            Debug.LogWarning($"Invalid BadgeId: {BadgeId}. Index out of bounds.");
        }

        // Update skin color for various body parts
        Torso.color = SkinColor;
        Leg1.color = SkinColor;
        Leg2.color = SkinColor;
        Arm1_.color = SkinColor;
        Arm2_.color = SkinColor;
        Foot1.color = SkinColor;
        Foot2.color = SkinColor;
        Head.color = SkinColor;

        // Update item sprites based on their IDs using SetImage
        SetImage(ShirtId, Shirt);
        SetImage(PantId, Pant1);
        SetImage(PantId, Pant2); // Assuming both pants are the same for simplicity
        Pant2.transform.localScale = new Vector3(-Mathf.Abs(Pant2.transform.localScale.x), Pant2.transform.localScale.y, Pant2.transform.localScale.z);
        SetImage(ShoeId, Shoe1);
        SetImage(ShoeId, Shoe2); // Assuming both shoes are the same for simplicity
        SetImage(BackId, Back);
        SetImage(HairId, Hair_); // Assuming hair corresponds to the head sprite
        SetImage(HatId, Hat_);
        SetImage(HandItemId, HandItem);
    }

    // Animation handling
    int animOrder = 0;
    public Sprite[] Animations;
    public bool Animate;

    // Coroutine to repeat the animation
    IEnumerator RepeatAnim()
    {
        while (Animate)
        {
            yield return new WaitForSeconds(0.15f);

            // Reset to the first frame if the animation reaches the end
            if (animOrder == Animations.Length)
            {
                animOrder = 0;
            }

            try
            {
                // Set the current frame as the hand item sprite
                HandItem.sprite = Animations[animOrder];
            }
            catch
            {
                // Catch any error that might happen when accessing the animation array
                Debug.LogWarning("Error setting hand item sprite during animation.");
            }

            // Increment the animation order to get the next frame
            animOrder++;
        }
    }
}

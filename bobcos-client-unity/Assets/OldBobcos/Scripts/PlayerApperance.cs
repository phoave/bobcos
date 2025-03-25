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

        // Update shirt sprite
        if (ShirtId >= 0 && ShirtId < ItemManager.instance.ShirtItems.Length)
        {
            Shirt.sprite = ItemManager.instance.ShirtItems[ShirtId].Icon;
        }
        else
        {
            Debug.LogWarning($"Invalid ShirtId: {ShirtId}. Index out of bounds.");
        }

        // Update pants sprite
        if (PantId >= 0 && PantId < ItemManager.instance.PantItems.Length)
        {
            Pant1.sprite = ItemManager.instance.PantItems[PantId].Icon;
            Pant2.sprite = ItemManager.instance.PantItems[PantId].Icon;
        }
        else
        {
            Debug.LogWarning($"Invalid PantId: {PantId}. Index out of bounds.");
        }

        // Update shoes sprite
        if (ShoeId >= 0 && ShoeId < ItemManager.instance.ShoeItems.Length)
        {
            Shoe1.sprite = ItemManager.instance.ShoeItems[ShoeId].Icon;
            Shoe2.sprite = ItemManager.instance.ShoeItems[ShoeId].Icon;
        }
        else
        {
            Debug.LogWarning($"Invalid ShoeId: {ShoeId}. Index out of bounds.");
        }

        // Update back item sprite
        if (BackId >= 0 && BackId < ItemManager.instance.BackItems.Length)
        {
            Back.sprite = ItemManager.instance.BackItems[BackId].Icon;
        }
        else
        {
            Debug.LogWarning($"Invalid BackId: {BackId}. Index out of bounds.");
        }

        // Update hair sprite
        if (HairId >= 0 && HairId < ItemManager.instance.HairItems.Length)
        {
            Head.sprite = ItemManager.instance.HairItems[HairId].Icon;
        }
        else
        {
            Debug.LogWarning($"Invalid HairId: {HairId}. Index out of bounds.");
        }

        // Update hand item sprite
        if (HandItemId >= 0 && HandItemId < ItemManager.instance.HandItems.Length)
        {
            HandItem.sprite = ItemManager.instance.HandItems[HandItemId].Icon;
        }
        else
        {
            Debug.LogWarning($"Invalid HandItemId: {HandItemId}. Index out of bounds.");
        }
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

using System;
using System.Collections.Generic;
using System.Text;

namespace Bobcos_Server
{
    class itemdata
    {
        static public item[] items = new item[111];

        static public int whitedoorid = 5;
        static public int worldlockid = 28;
        static public int fisherbobid = 107;

        public static void Initiliaze()
        {
            items[0] = new item { itemname = "Air", avoidAntiNoclip = true, itemtype = "AIR", maxgem = 5, mingem = 2, istradable = false };
            items[1] = new item { itemname = "NULL", itemtype = "NULL", maxgem = 4, mingem = 2, istradable = false };

            items[2] = new item { itemname = "Fist",avoidAntiNoclip = true, itemid = 2, itemtype = "FIST", description = "To break blocks, select this.", istrashable = false, istradable = false };
            items[3] = new item { itemname = "Dirt Block",avoidAntiNoclip = true, itemid = 3, itemtype = "BLOCK", description = "Ah yes dirt, the first thing you see when you create a world.", blockhealth = 3, maxgem = 2, mingem = 0, RandomPick = new RandomPicker() { randoms = new int[] { 0, 0, 0, 0, 1, 1, 2, 2, 2, 2, 3, 3 }, itemToGive = 3 } };
            items[4] = new item { itemname = "Cave background",avoidAntiNoclip = true, itemid = 4, itemtype = "BGBLOCK", description = "No info.", maxgem = 2, mingem = 0, RandomPick = new RandomPicker() { randoms = new int[] { 0, 0, 0, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3 }, itemToGive = 4 } };
            items[5] = new item { itemname = "White door", avoidAntiNoclip = true, itemid = 5, itemtype = "BLOCK", description = "No description.", mingem = 0, maxgem = 100, blockhealth = 4, istradable = false };
            items[6] = new item { itemname = "Red Shirt",avoidAntiNoclip = true, itemid = 6, itemtype = "SHIRT", description = "No description.", mingem = 0, maxgem = 100, blockhealth = 1033000, iswearable = true };
            items[7] = new item { itemname = "Red Pants",avoidAntiNoclip = true, itemid = 7, itemtype = "PANT", description = "No description.", mingem = 0, maxgem = 100, blockhealth = 1000, iswearable = true };
            items[8] = new item { itemname = "Red Shoes",avoidAntiNoclip = true, itemid = 8, itemtype = "SHOE", description = "No description.", mingem = 0, maxgem = 100, blockhealth = 1000, iswearable = true };
            items[9] = new item { itemname = "Red Hat",avoidAntiNoclip = true, itemid = 9, itemtype = "HAT", description = "No description.", mingem = 0, maxgem = 100, blockhealth = 1000, iswearable = true };
            items[10] = new item { itemname = "Grey Top Hat",avoidAntiNoclip = true, itemid = 10, itemtype = "HAT", description = "Looks cool.", mingem = 0, maxgem = 100, blockhealth = 1000, iswearable = true };
            items[11] = new item { itemname = "Bedrock Block",avoidAntiNoclip = true, itemid = 11, itemtype = "BLOCK", description = "Wait how did you got this?", blockhealth = 1033000, maxgem = 500, istradable = false, mingem = 2, RandomPick = new RandomPicker() { randoms = new int[] { 0, 0 }, itemToGive = 11 } };
            items[12] = new item { itemname = "Wood Block",avoidAntiNoclip = true, itemid = 12, itemtype = "BLOCK", description = "Nice wood.", blockhealth = 3, maxgem = 3, mingem = 0, RandomPick = new RandomPicker() { randoms = new int[] { 0, 0, 0, 1, }, itemToGive = 12 } };
            items[13] = new item { itemname = "Wood Background",avoidAntiNoclip = true, itemid = 13, itemtype = "BGBLOCK", description = "Nice wood.", blockhealth = 2, maxgem = 4, mingem = 0, RandomPick = new RandomPicker() { randoms = new int[] { 0, 0, 0, 0, 0, 0, 1, 1, 2 }, itemToGive = 13 } };

            // colored blocks
            items[14] = new item { itemname = "Red Block",avoidAntiNoclip = true, itemid = 14, itemtype = "BLOCK", description = "Nice.", blockhealth = 4, maxgem = 5, mingem = 0, RandomPick = new RandomPicker() { randoms = new int[] { 0, 0, 0, 0, 0, 0, 1, 1, 2, 2, 2, 3 }, itemToGive = 14 } };
            items[15] = new item { itemname = "Blue Block",avoidAntiNoclip = true, itemid = 15, itemtype = "BLOCK", description = "Nice.", blockhealth = 4, maxgem = 5, mingem = 0, RandomPick = new RandomPicker() { randoms = new int[] { 0, 0, 0, 0, 0, 0, 1, 1, 2, 2, 2, 3 }, itemToGive = 15 } };
            items[16] = new item { itemname = "Green Block",avoidAntiNoclip = true, itemid = 16, itemtype = "BLOCK", description = "Nice.", blockhealth = 4, maxgem = 5, mingem = 0, RandomPick = new RandomPicker() { randoms = new int[] { 0, 0, 0, 0, 0, 0, 1, 1, 2, 2, 2, 3 }, itemToGive = 16 } };
            items[17] = new item { itemname = "Cyan Block",avoidAntiNoclip = true, itemid = 17, itemtype = "BLOCK", description = "Nice.", blockhealth = 4, maxgem = 5, mingem = 0, RandomPick = new RandomPicker() { randoms = new int[] { 0, 0, 0, 0, 0, 0, 1, 1, 2, 2, 2, 3 }, itemToGive = 17 } };

            items[18] = new item { itemname = "Orange Block",avoidAntiNoclip = true, itemid = 18, itemtype = "BLOCK", description = "Nice.", blockhealth = 4, maxgem = 5, mingem = 0, RandomPick = new RandomPicker() { randoms = new int[] { 0, 0, 0, 0, 0, 0, 1, 1, 2, 2, 2, 3 }, itemToGive = 18 } };
            items[19] = new item { itemname = "Pink Block",avoidAntiNoclip = true, itemid = 19, itemtype = "BLOCK", description = "Nice.", blockhealth = 4, maxgem = 5, mingem = 0, RandomPick = new RandomPicker() { randoms = new int[] { 0, 0, 0, 0, 0, 0, 1, 1, 2, 2, 2, 3 }, itemToGive = 19 } };
            items[20] = new item { itemname = "Yellow Block",avoidAntiNoclip = true, itemid = 20, itemtype = "BLOCK", description = "Nice.", blockhealth = 4, maxgem = 5, mingem = 0, RandomPick = new RandomPicker() { randoms = new int[] { 0, 0, 0, 0, 0, 0, 1, 1, 2, 2, 2, 3 }, itemToGive = 20 } };
            items[21] = new item { itemname = "Purple Block",avoidAntiNoclip = true, itemid = 21, itemtype = "BLOCK", description = "Nice.", blockhealth = 4, maxgem = 5, mingem = 0, RandomPick = new RandomPicker() { randoms = new int[] { 0, 0, 0, 0, 0, 0, 1, 1, 2, 2, 2, 3 }, itemToGive = 21 } };

            items[22] = new item { itemname = "Black Block",avoidAntiNoclip = true, itemid = 22, itemtype = "BLOCK", description = "Nice.", blockhealth = 4, maxgem = 5, mingem = 0, RandomPick = new RandomPicker() { randoms = new int[] { 0, 0, 0, 0, 0, 0, 1, 1, 2, 2, 2, 3 }, itemToGive = 22 } };
            items[23] = new item { itemname = "Yellow Block",avoidAntiNoclip = true, itemid = 23, itemtype = "BLOCK", description = "Nice.", blockhealth = 4, maxgem = 5, mingem = 0, RandomPick = new RandomPicker() { randoms = new int[] { 0, 0, 0, 0, 0, 0, 1, 1, 2, 2, 2, 3 }, itemToGive = 23 } };
            items[24] = new item { itemname = "Grey Block",avoidAntiNoclip = true, itemid = 24, itemtype = "BLOCK", description = "Nice.", blockhealth = 4, maxgem = 5, mingem = 0, RandomPick = new RandomPicker() { randoms = new int[] { 0, 0, 0, 0, 0, 0, 1, 1, 2, 2, 2, 3 }, itemToGive = 24 } };

            items[25] = new item { itemname = "Red Neon Block",avoidAntiNoclip = true, itemid = 25, itemtype = "BLOCK", description = "Nice.", blockhealth = 4, maxgem = 15, mingem = 0, RandomPick = new RandomPicker() { randoms = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 2, 2, 2, 3 }, itemToGive = 25 } };
            items[26] = new item { itemname = "Green Neon Block",avoidAntiNoclip = true, itemid = 26, itemtype = "BLOCK", description = "Nice.", blockhealth = 4, maxgem = 7, mingem = 0, RandomPick = new RandomPicker() { randoms = new int[] { 0, 0, 0, 1, 1, 2, 2, 2, 3 }, itemToGive = 26 } };
            items[27] = new item { itemname = "Blue Neon Block",avoidAntiNoclip = true, itemid = 27, itemtype = "BLOCK", description = "Nice.", blockhealth = 4, maxgem = 9, mingem = 0, RandomPick = new RandomPicker() { randoms = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 2, 2, 2, 3 }, itemToGive = 27 } };
            items[28] = new item { itemname = "World Lock",avoidAntiNoclip = true, itemid = 28, itemtype = "LOCK", description = "You can lock world with this thing. No one can break or place blocks", blockhealth = 5, maxgem = 1, mingem = 0, RandomPick = new RandomPicker() { randoms = new int[] { 1, 1, 1, 1 } } };
            items[29] = new item { itemname = "Wrench", istradable = false, itemid = 29, itemtype = "WRENCH", description = "Ok, looks good.", istrashable = false };
            items[30] = new item { itemname = "Crafting Machine",avoidAntiNoclip = true, itemid = 30, itemtype = "BLOCK", description = "With this machine, You can craft items", blockhealth = 5, maxgem = 1, mingem = 0, RandomPick = new RandomPicker() { isconstant = true, constantint = 1, itemToGive = 30 } };
            items[31] = new item { itemname = "Stone pickaxe",avoidAntiNoclip = true, itemid = 31, itemtype = "HAND", iswearable = true, description = "Nice.", blockhealth = 4, maxgem = 7, mingem = 0, RandomPick = new RandomPicker() { randoms = new int[] { 0, 0, 0, 0, 0, 0, 1, 1, 2, 2, 2, 3 }, itemToGive = 31 } };
            items[32] = new item { itemname = "Blue aqua wings",avoidAntiNoclip = true, itemid = 32, itemtype = "BACK", iswearable = true, description = "Nice.", blockhealth = 4, maxgem = 7, mingem = 0, RandomPick = new RandomPicker() { randoms = new int[] { 0, 0, 0, 0, 0, 0, 1, 1, 2, 2, 2, 3 }, itemToGive = 32 } };
            items[33] = new item { itemname = "Brick",avoidAntiNoclip = true, itemid = 33, itemtype = "BLOCK", description = "Nice.", blockhealth = 5, maxgem = 3, mingem = 0, RandomPick = new RandomPicker() { randoms = new int[] { 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3 }, itemToGive = 33 } };

            items[34] = new item { itemname = "Brown Male Hair",avoidAntiNoclip = true, itemid = 34, itemtype = "HAIR", iswearable = true, description = "Nice.", blockhealth = 4, maxgem = 7, mingem = 0, RandomPick = new RandomPicker() { randoms = new int[] { 0, 0, 0, 0, 0, 0, 1, 1, 2, 2, 2, 3 }, itemToGive = 34 } };
            items[35] = new item { itemname = "Angel Wings",avoidAntiNoclip = true, itemid = 35, itemtype = "BACK", iswearable = true, description = "Nice.", blockhealth = 4, maxgem = 7, mingem = 0, RandomPick = new RandomPicker() { randoms = new int[] { 0, 0, 0, 0, 0, 0, 1, 1, 2, 2, 2, 3 }, itemToGive = 35 } };
            items[36] = new item { itemname = "Black Messy Hair",avoidAntiNoclip = true, itemid = 36, itemtype = "HAIR", iswearable = true, description = "Nice.", blockhealth = 4, maxgem = 7, mingem = 0, RandomPick = new RandomPicker() { randoms = new int[] { 0, 0, 0, 0, 0, 0, 1, 1, 2, 2, 2, 3 }, itemToGive = 36 } };
            items[37] = new item { itemname = "Stone Block",avoidAntiNoclip = true, itemid = 37, itemtype = "BLOCK", description = "Ah yes Stone.", blockhealth = 5, maxgem = 2, mingem = 0, RandomPick = new RandomPicker() { randoms = new int[] { 0, 0, 0, 0, 0, 0, 1, 1 }, itemToGive = 37 } };

            items[38] = new item { itemname = "Green Shirt",avoidAntiNoclip = true, itemid = 38, itemtype = "SHIRT", description = "No description.", mingem = 0, maxgem = 100, blockhealth = 1000, iswearable = true };
            items[39] = new item { itemname = "Blue Shirt",avoidAntiNoclip = true, itemid = 39, itemtype = "SHIRT", description = "No description.", mingem = 0, maxgem = 100, blockhealth = 1000, iswearable = true };
            items[40] = new item { itemname = "Yellow Shirt",avoidAntiNoclip = true, itemid = 40, itemtype = "SHIRT", description = "No description.", mingem = 0, maxgem = 100, blockhealth = 1000, iswearable = true };

            items[41] = new item { itemname = "Green Pants",avoidAntiNoclip = true, itemid = 41, itemtype = "PANT", description = "No description.", mingem = 0, maxgem = 100, blockhealth = 1000, iswearable = true };
            items[42] = new item { itemname = "Blue Pants",avoidAntiNoclip = true, itemid = 42, itemtype = "PANT", description = "No description.", mingem = 0, maxgem = 100, blockhealth = 1000, iswearable = true };
            items[43] = new item { itemname = "Yellow Pants",avoidAntiNoclip = true, itemid = 43, itemtype = "PANT", description = "No description.", mingem = 0, maxgem = 100, blockhealth = 1000, iswearable = true };

            items[44] = new item { itemname = "Green Shoes",avoidAntiNoclip = true, itemid = 44, itemtype = "SHOE", description = "No description.", mingem = 0, maxgem = 100, blockhealth = 1000, iswearable = true };
            items[45] = new item { itemname = "Blue Shoes",avoidAntiNoclip = true, itemid = 45, itemtype = "SHOE", description = "No description.", mingem = 0, maxgem = 100, blockhealth = 1000, iswearable = true };
            items[46] = new item { itemname = "Yellow Shoes",avoidAntiNoclip = true, itemid = 46, itemtype = "SHOE", description = "No description.", mingem = 0, maxgem = 100, blockhealth = 1000, iswearable = true };
            // new items 1 

            items[47] = new item { itemname = "Old Television", avoidAntiNoclip = true, itemid = 47, itemtype = "BLOCK", description = "Old television.", blockhealth = 4, maxgem = 11, mingem = 0, RandomPick = new RandomPicker() { randoms = new int[] { 0, 0, 0, 0, 0, 0, 1, 1 }, itemToGive = 47 } };
            items[48] = new item { itemname = "Flower Pot", avoidAntiNoclip = true, itemid = 48, itemtype = "BLOCK", description = "Flower.", blockhealth = 4, maxgem = 7, mingem = 0, RandomPick = new RandomPicker() { randoms = new int[] { 0, 0, 0, 0, 0, 0, 1, 1, 2 }, itemToGive = 48 } };
            items[49] = new item { itemname = "Wooden Chair", avoidAntiNoclip = true, itemid = 49, itemtype = "BLOCK", description = "I need to sit. Oh look theres chair!", blockhealth = 5, maxgem = 5, mingem = 0, RandomPick = new RandomPicker() { randoms = new int[] { 0, 0, 0, 0, 0, 0, 1, 1 }, itemToGive = 49 } };
            items[50] = new item { itemname = "Wooden Table", avoidAntiNoclip = true, itemid = 50, itemtype = "BLOCK", description = "Wooden table. Nice. Very Nice.", blockhealth = 5, maxgem = 6, mingem = 0, RandomPick = new RandomPicker() { randoms = new int[] { 0, 0, 0, 0, 0, 0, 1, 1, 2, }, itemToGive = 50 } };
            items[51] = new item { itemname = "White Window",avoidAntiNoclip = true, itemid = 51, itemtype = "BGBLOCK", description = "Ok. cool.", blockhealth = 4, maxgem = 7, mingem = 0, RandomPick = new RandomPicker() { randoms = new int[] { 0, 0, 0, 0, 0, 0, 1, 1, 2 }, itemToGive = 51 } };

            items[52] = new item { itemname = "Red Background",avoidAntiNoclip = true, itemid = 52, itemtype = "BGBLOCK", description = "Background.", blockhealth = 4, maxgem = 7, mingem = 0, RandomPick = new RandomPicker() { randoms = new int[] { 0, 0, 0, 0, 0, 0, 1, 1, }, itemToGive = 52 } };
            items[53] = new item { itemname = "Green Background",avoidAntiNoclip = true, itemid = 53, itemtype = "BGBLOCK", description = "Background.", blockhealth = 4, maxgem = 7, mingem = 0, RandomPick = new RandomPicker() { randoms = new int[] { 0, 0, 0, 0, 0, 0, 1 }, itemToGive = 53 } };
            items[54] = new item { itemname = "Blue Background",avoidAntiNoclip = true, itemid = 54, itemtype = "BGBLOCK", description = "Background.", blockhealth = 4, maxgem = 7, mingem = 0, RandomPick = new RandomPicker() { randoms = new int[] { 0, 0, 0, 0, 0, 0, 1, 1, 2, }, itemToGive = 54 } };
            items[55] = new item { itemname = "Pink Background",avoidAntiNoclip = true, itemid = 55, itemtype = "BGBLOCK", description = "Background.", blockhealth = 4, maxgem = 7, mingem = 0, RandomPick = new RandomPicker() { randoms = new int[] { 0, 0, 0, 0, 0, 0, 1, 1, 2, }, itemToGive = 55 } };
            items[56] = new item { itemname = "Purple Background",avoidAntiNoclip = true, itemid = 56, itemtype = "BGBLOCK", description = "Background.", blockhealth = 4, maxgem = 7, mingem = 0, RandomPick = new RandomPicker() { randoms = new int[] { 0, 0, 0, 0, 0, 0, 1, 1, }, itemToGive = 56 } };
            items[57] = new item { itemname = "Cyan Background",avoidAntiNoclip = true, itemid = 57, itemtype = "BGBLOCK", description = "Background.", blockhealth = 4, maxgem = 7, mingem = 0, RandomPick = new RandomPicker() { randoms = new int[] { 0, 0, 0, 0, 0, 0, 1, 1, 2, 2 }, itemToGive = 57 } };
            items[58] = new item { itemname = "Black Background",avoidAntiNoclip = true, itemid = 58, itemtype = "BGBLOCK", description = "Background.", blockhealth = 4, maxgem = 7, mingem = 0, RandomPick = new RandomPicker() { randoms = new int[] { 0, 0, 0, 0, 0, 0, 1, 1, 2, 2 }, itemToGive = 58 } };
            items[59] = new item { itemname = "Grey Background",avoidAntiNoclip = true, itemid = 59, itemtype = "BGBLOCK", description = "Background.", blockhealth = 4, maxgem = 7, mingem = 0, RandomPick = new RandomPicker() { randoms = new int[] { 0, 0, 0, 0, 0, 0, 1, 1, 2, 2, }, itemToGive = 59 } };
            items[60] = new item { itemname = "White Background",avoidAntiNoclip = true, itemid = 60, itemtype = "BGBLOCK", description = "Background.", blockhealth = 4, maxgem = 7, mingem = 0, RandomPick = new RandomPicker() { randoms = new int[] { 0, 0, 0, 0, 0, 0, 1, 1, }, itemToGive = 60 } };

            items[61] = new item { itemname = "Closed Wooden Entrance Door", avoidAntiNoclip = false, itemid = 61, itemtype = "BLOCK", description = "Closed door. No one can enter from this door right now.", blockhealth = 4, maxgem = 4, mingem = 0, RandomPick = new RandomPicker() { randoms = new int[] { 0, 0, 0, 0, 0, 0, 1, 1, 2, 2, 2, 3 }, itemToGive = 61 } };
            items[62] = new item { itemname = "Open Wooden Entrance Door", avoidAntiNoclip = true, itemid = 62, itemtype = "BLOCK", description = "Open door. Everyone Can enter from this door.", blockhealth = 4, maxgem = 4, mingem = 0, RandomPick = new RandomPicker() { randoms = new int[] { 0, 0, 0, 0, 0, 0, 1, 1, 2, 2, 2, 3 }, itemToGive = 62 } };
            items[63] = new item { itemname = "Wooden Sign", avoidAntiNoclip = true, itemid = 63, itemtype = "BLOCK", description = "Wooden sign.", blockhealth = 4, maxgem = 4, mingem = 0, RandomPick = new RandomPicker() { randoms = new int[] { 0, 0, 0, 0, 0, 0, 1, 1 }, itemToGive = 63 } };

            // new items 2

            items[64] = new item { itemname = "Brown Female hair",avoidAntiNoclip = true, itemid = 64, itemtype = "HAIR", iswearable = true, description = "Nice.", blockhealth = 4, maxgem = 7, mingem = 0, RandomPick = new RandomPicker() { randoms = new int[] { 0, 0, 0, 0, 0, 0, 1, 1, 2, 2, 2, 3 }, itemToGive = 64 } };
            items[65] = new item { itemname = "Glass Block",avoidAntiNoclip = true, itemid = 65, itemtype = "BLOCK", description = "Nice.", blockhealth = 4, maxgem = 6, mingem = 0, RandomPick = new RandomPicker() { randoms = new int[] { 0, 0, 0, 0, 0, 0, 1, 1, 2, 2, 3, 3, 3, 3, 3, 4, 4, 4 }, itemToGive = 65 } };
            items[66] = new item { itemname = "Sand Block",avoidAntiNoclip = true, itemid = 66, itemtype = "BLOCK", description = "Nice.", blockhealth = 4, maxgem = 2, mingem = 0, RandomPick = new RandomPicker() { randoms = new int[] { 0, 0, 0, 0, 0, 0, 1, 1, 2, 2, 2, 3, 4, 4, 4 }, itemToGive = 66 } };



            //items 3
            items[67] = new item { itemname = "Golden Angel Wings",avoidAntiNoclip = true, itemid = 67, itemtype = "BACK", iswearable = true, description = "This item is BETA item.", blockhealth = 4, maxgem = 7, mingem = 0, RandomPick = new RandomPicker() { randoms = new int[] { 0, 0, 0, 0, 0, 0, 1, 1, 2, 2, 2, 3 }, itemToGive = 67 } };
            items[68] = new item { itemname = "Golden Top Hat", istradable = false, itemid = 68, itemtype = "HAT", description = "This item is BETA item.", mingem = 0, maxgem = 100, blockhealth = 1000, iswearable = true };

            items[69] = new item { itemname = "Red Rose", avoidAntiNoclip = true, itemid = 69, itemtype = "BLOCK", description = "Nice.", blockhealth = 1, maxgem = 2, mingem = 0, RandomPick = new RandomPicker() { randoms = new int[] { 0, 0, 0, 0, 0, 0, 1, 1, 2, 2 }, itemToGive = 69 } };
            items[70] = new item { itemname = "Blue Flower", avoidAntiNoclip = true, itemid = 70, itemtype = "BLOCK", description = "Nice.", blockhealth = 1, maxgem = 2, mingem = 0, RandomPick = new RandomPicker() { randoms = new int[] { 0, 0, 0, 0, 0, 0, 1, 1, 2, 2 }, itemToGive = 70 } };
            items[71] = new item { itemname = "Yellow Flower", avoidAntiNoclip = true, itemid = 71, itemtype = "BLOCK", description = "Nice.", blockhealth = 1, maxgem = 3, mingem = 0, RandomPick = new RandomPicker() { randoms = new int[] { 0, 0, 0, 0, 0, 0, 1, 1, }, itemToGive = 71 } };
            items[72] = new item { itemname = "Grass", avoidAntiNoclip = true, itemid = 72, itemtype = "BLOCK", description = "Nice.", blockhealth = 1, maxgem = 3, mingem = 2, RandomPick = new RandomPicker() { randoms = new int[] { 0, 0, 0, 0, 0, 0, 1, 1, 2, 2, 2 }, itemToGive = 72 } };

            items[73] = new item { itemname = "Lava Block",avoidAntiNoclip = true, itemid = 73, itemtype = "BLOCK", description = "Nice.", blockhealth = 4, maxgem = 2, mingem = 2, RandomPick = new RandomPicker() { randoms = new int[] { 0, 0, 0, 0, 0, 0, 1, 1, 2, 2, 2 }, itemToGive = 73 } };


            items[74] = new item { itemname = "Ketaru",avoidAntiNoclip = true, itemid = 74, itemtype = "HAND", iswearable = true, description = "Pewain's Katana.", blockhealth = 4, maxgem = 2, mingem = 2, RandomPick = new RandomPicker() { randoms = new int[] { 0, 0, 0, 0, 0, 0, 1, 1, 2, 2, 2 }, itemToGive = 74 } };

            items[75] = new item { itemname = "Red Hat",avoidAntiNoclip = true, itemid = 75, itemtype = "HAT", description = "Looks cool.", mingem = 0, maxgem = 100, blockhealth = 1000, iswearable = true };
            items[76] = new item { itemname = "Green Hat",avoidAntiNoclip = true, itemid = 76, itemtype = "HAT", description = "Looks cool.", mingem = 0, maxgem = 100, blockhealth = 1000, iswearable = true };
            items[77] = new item { itemname = "Blue Hat",avoidAntiNoclip = true, itemid = 77, itemtype = "HAT", description = "Looks cool.", mingem = 0, maxgem = 100, blockhealth = 1000, iswearable = true };
            items[78] = new item { itemname = "Yellow Hat",avoidAntiNoclip = true, itemid = 78, itemtype = "HAT", description = "Looks cool.", mingem = 0, maxgem = 100, blockhealth = 1000, iswearable = true };
            items[79] = new item { itemname = "Blonde Male Hair",avoidAntiNoclip = true, itemid = 79, itemtype = "HAIR", iswearable = true, description = "Nice.", blockhealth = 4, maxgem = 7, mingem = 0, RandomPick = new RandomPicker() { randoms = new int[] { 0, 0, 0, 0, 0, 0, 1, 1, 2, 2, 2, 3 }, itemToGive = 79 } };
            items[80] = new item { itemname = "Blonde Female Hair",avoidAntiNoclip = true, itemid = 80, itemtype = "HAIR", iswearable = true, description = "Nice.", blockhealth = 4, maxgem = 7, mingem = 0, RandomPick = new RandomPicker() { randoms = new int[] { 0, 0, 0, 0, 0, 0, 1, 1, 2, 2, 2, 3 }, itemToGive = 80 } };
            items[81] = new item { itemname = "Water Block", avoidAntiNoclip = true, itemid = 81, itemtype = "BLOCK", description = "Nice.", blockhealth = 4, maxgem = 2, mingem = 1, RandomPick = new RandomPicker() { randoms = new int[] { 0, 0, 0, 0, 0, 0, 1, 1, 2, 2, 2 }, itemToGive = 81 } };
            items[82] = new item { itemname = "Straw Hat",avoidAntiNoclip = true, itemid = 82, itemtype = "HAT", description = "Looks cool.", mingem = 0, maxgem = 100, blockhealth = 1000, iswearable = true };
            items[83] = new item { itemname = "Wooden Fishing Rod",avoidAntiNoclip = true, itemid = 83, itemtype = "HAND", iswearable = true, description = "Nice.", blockhealth = 4, maxgem = 7, mingem = 0, RandomPick = new RandomPicker() { randoms = new int[] { 0, 0, 0, 0, 0, 0, 1, 1, 2, 2, 2, 3 }, itemToGive = 83 } };

            // new items


            items[84] = new item { itemname = "White Cape",avoidAntiNoclip = true, itemid = 84, itemtype = "BACK", iswearable = true, description = "LOOK IM A HERO!" };
            items[85] = new item { itemname = "Betta Wings",avoidAntiNoclip = true, itemid = 85, itemtype = "BACK", iswearable = true, description = "Obtainable from fishing, Looks cool." };
            items[86] = new item { itemname = "Green flower Sweat",avoidAntiNoclip = true, itemid = 86, itemtype = "SHIRT", iswearable = true, description = "Can be obtainable from summer event" };
            items[87] = new item { itemname = "Blue Jacket",avoidAntiNoclip = true, itemid = 87, itemtype = "SHIRT", iswearable = true, description = "What a nice day,Let's wear a jacket and go for a walk! Can be obtainable from summer event" };
            items[88] = new item { itemname = "Green Laser Spear",avoidAntiNoclip = true, itemid = 88, itemtype = "HAND", iswearable = true, description = "Looks very epic, It has epic effect too!" };
            items[89] = new item { itemname = "Fisher coat",avoidAntiNoclip = true, itemid = 89, itemtype = "SHIRT", iswearable = true, description = "Fisher coat, Slows fish speed %25" };
            items[90] = new item { itemname = "Fisher Pants",avoidAntiNoclip = true, itemid = 90, itemtype = "PANT", iswearable = true, description = "Fisher Pants, Reduces fish catching time by %25" };
            items[91] = new item { itemname = "Fisher Shoes",avoidAntiNoclip = true, itemid = 91, itemtype = "SHOE", iswearable = true, description = "Fisher Shoes, Increases bait speed by %25" };
            items[92] = new item { itemname = "Palm Tree",avoidAntiNoclip = true, itemid = 92, itemtype = "BLOCK", description = "Nice.", blockhealth = 7, maxgem = 2, mingem = 3, RandomPick = new RandomPicker() { randoms = new int[] { 0, 0, 0, 0, 0, 0, 1, 1, 2, 2, 2 }, itemToGive = 92 } };

            items[93] = new item { fishGemAmount = 200, itemname = "Betta Fish",avoidAntiNoclip = true, itemid = 93, itemtype = "FISH", description = "Betta fish, gives 200 Gems. Give it to Fisher John To get your gems.", blockhealth = 7, maxgem = 2, mingem = 3 };
            items[94] = new item { fishGemAmount = 125, itemname = "Salmon Fish",avoidAntiNoclip = true, itemid = 94, itemtype = "FISH", description = "Salmon fish, gives 125 Gems. Give it to Fisher John To get your gems.", blockhealth = 7, maxgem = 2, mingem = 3 };
            items[95] = new item { fishGemAmount = 50, itemname = "CatFish",avoidAntiNoclip = true, itemid = 95, itemtype = "FISH", description = "Catfish, gives 50 Gems. Give it to Fisher John To get your gems.", blockhealth = 7, maxgem = 2, mingem = 3 };

            items[96] = new item { itemname = "Worm Lure",avoidAntiNoclip = true, itemid = 96, itemtype = "LURE", description = "", blockhealth = 7, maxgem = 2, mingem = 3 };
            items[97] = new item { itemname = "Pink Blue Lure",avoidAntiNoclip = true, itemid = 97, itemtype = "LURE", description = "", blockhealth = 7, maxgem = 2, mingem = 3 };
            items[98] = new item { itemname = "Green Red Lure",avoidAntiNoclip = true, itemid = 98, itemtype = "LURE", description = "", blockhealth = 7, maxgem = 2, mingem = 3 };
            items[99] = new item { itemname = "Yellow Background",avoidAntiNoclip = true, itemid = 99, itemtype = "BGBLOCK", description = "Background.", blockhealth = 4, maxgem = 7, mingem = 0, RandomPick = new RandomPicker() { randoms = new int[] { 0, 0, 0, 0, 0, 0, 1, 1, }, itemToGive = 99 } };
            items[100] = new item { itemname = "White Block",avoidAntiNoclip = true, itemid = 100, itemtype = "BLOCK", description = "Nice.", blockhealth = 4, maxgem = 5, mingem = 0, RandomPick = new RandomPicker() { randoms = new int[] { 0, 0, 0, 0, 0, 0, 1, 1, 2, 2, 2, 3 }, itemToGive = 100 } };
            items[101] = new item { itemname = "Golden Loot",avoidAntiNoclip = true, itemid = 101, itemtype = "GOLDENLOOT", description = "Select this and tap anywhere to get your 5000 Gem.", blockhealth = 4, maxgem = 5, mingem = 0 };
            items[102] = new item { itemname = "Orange Background",avoidAntiNoclip = true, itemid = 102, itemtype = "BGBLOCK", description = "Background.", blockhealth = 4, maxgem = 7, mingem = 0, RandomPick = new RandomPicker() { randoms = new int[] { 0, 0, 0, 0, 0, 0, 1, 1, }, itemToGive = 102 } };
            items[103] = new item { itemname = "Torch", avoidAntiNoclip = true, itemid = 103, itemtype = "BLOCK", description = "Nice.", blockhealth = 4, maxgem = 2, mingem = 1, RandomPick = new RandomPicker() { randoms = new int[] { 0, 0, 0, 0, 0, 0, 1, 1, 2, 2, 2 }, itemToGive = 103 } };
            items[104] = new item { itemname = "Lamp", avoidAntiNoclip = true, itemid = 104, itemtype = "BLOCK", description = "Nice.", blockhealth = 4, maxgem = 2, mingem = 1, RandomPick = new RandomPicker() { randoms = new int[] { 0, 0, 0, 0, 0, 0, 1, 1, 2, 2, 2 }, itemToGive = 104 } };
            items[105] = new item { itemname = "Green Neon Sword",avoidAntiNoclip = true, itemid = 105, itemtype = "HAND", iswearable = true, description = "Looks very epic, It has epic effect too!" };
            items[106] = new item { itemname = "Diamond Axe",avoidAntiNoclip = true, itemid = 106, itemtype = "HAND", iswearable = true, description = "Looks very epic, It has epic effect too!" };
            items[107] = new item { itemname = "NPC 0 : Fisher Bob",avoidAntiNoclip = true, itemid = 107, itemtype = "BLOCK", description = "Wait, Thats Illegal.", blockhealth = 4, maxgem = 7, mingem = 0, RandomPick = new RandomPicker() { randoms = new int[] { 1, 1 }, itemToGive = 107 } };
            items[108] = new item { itemname = "Galaxy Sword",avoidAntiNoclip = true, itemid = 108, itemtype = "HAND", iswearable = true, description = "Looks very epic, It has epic effect too!" };
            items[109] = new item { itemname = "Galaxy Wings",avoidAntiNoclip = true, itemid = 109, itemtype = "BACK", description = "Galaxy Wings.", iswearable = true };
            items[110] = new item { itemname = "Display Block",avoidAntiNoclip = true, itemid = 110, itemtype = "DISPLAYBLOCK", description = "Display Block.", blockhealth = 4, maxgem = 0, mingem = 0, RandomPick = new RandomPicker() { randoms = new int[] { 1 }, itemToGive = 110 } };
        }

    }

    class item
    {
        public string itemname;
        public int itemid;
        public string itemtype;
        public string description;
        public int mingem, maxgem;
        public int blockhealth = 1;
        public string extrainfo;
        public bool istrashable = true;
        public bool iswearable = false;
        public bool istradable = true;
        public RandomPicker RandomPick;
        public int xpCount = 10;
        public bool avoidAntiNoclip = false;
        public int fishGemAmount = 0;
    }

    class RandomPicker
    {
        public int itemToGive;
        public int[] randoms;
        public bool isconstant = false;
        public int constantint = 0;
        public int Pick()
        {

            if (isconstant)
            {
                return constantint;
            }

            Random rand = new Random();
            return randoms[rand.Next(0, randoms.Length)];
        }
    }
}

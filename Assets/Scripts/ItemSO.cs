using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "ItemSO", menuName = "ScriptableItems", order = 1)]
public class ItemSO : ScriptableObject
{
    public string itemName;

    public int itemWeigth;

    public int itemValue;

    public string itemDescription;

    public enum itemCategory
    {
        Resource,
        HandHeld,
        Misc

    }

}

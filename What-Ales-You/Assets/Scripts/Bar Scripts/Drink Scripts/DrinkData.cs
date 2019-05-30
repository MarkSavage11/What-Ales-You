using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "DrinkData", menuName = "ScriptableObjects/Drink", order = 1)]
public class DrinkData : ScriptableObject
{
    public string drinkName;
    public Ingredients[] ingredients;
    public MixType mixType;
    public Glasses glass;
    public Sprite UISprite;
    public Material finishedDrink;
}

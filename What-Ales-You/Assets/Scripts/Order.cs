using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Order
{
    public string patron;
    public Drink orderedDrink;

    public Order(string patron, string drinkName)
    {
        this.patron = patron;
        try
        {
            this.orderedDrink = Recipes.recipes[drinkName];
        }catch(System.Exception e)
        {
            Debug.Log("Ordered drink name was invalid");
        }
    }
}

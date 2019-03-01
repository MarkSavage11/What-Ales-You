using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishedDrink : MonoBehaviour
{
    //Best estimate of the drink made
    public Drink closestDrink;
    //Percentage of accuracy to the closest drink
    public float accuracy;

    public void Init(Drink closestDrink, float accuracy)
    {
        this.closestDrink = closestDrink;
        this.accuracy = accuracy;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Drink 
{
    //Here if we want to make ingredients into enum, unused for right now
    public enum Ingredient{
        //Liquors:
        Vodka, Tequila, Rum,
        //Sodas:
        Coke, GingerBeer,
        //Juices:
        LemonJuice, LimeJuice, CranberryJuice, OrangeJuice,
        //Other:
        EggWhite, SimpleSyrup
    }

    public string drinkName;
    public List<string> ingredients;
    public string mixType;
    public string glass;

    public Drink(string drinkName, List<string> ingredients, string mixType, string glass)
    {
        this.drinkName = drinkName;
        this.ingredients = ingredients;
        this.mixType = mixType;
        this.glass = glass;
    }

}



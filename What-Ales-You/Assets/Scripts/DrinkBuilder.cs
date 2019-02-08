using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkBuilder : Interactable {
    public string glassName;
    public List<string> ingredients;
    public List<List<string>> recipes;
    public Animator playerAnim;

    public void Awake() {
        //recipes.Add(new List<string>() { "Empty Glass", "NULL", "NULL", "NULL", "NULL", "NULL", "stirred", "Copper Mug" });
        //recipes.Add(new List<string>() { "Unstable Concotion", "NULL", "NULL", "NULL", "NULL", "NULL", "stirred", "Copper Mug" });
        recipes.Add(new List<string>() { "Moscow Mule", "Ginger Beer", "Lime Juice", "Vodka", "NULL", "NULL", "stirred", "Copper Mug" });
        recipes.Add(new List<string>() { "Dark and Stormy", "Ginger Beer", "Lime Juice", "Rum", "NULL", "NULL", "stirred", "Highball" });
        recipes.Add(new List<string>() { "Tennessee Mule", "Ginger Beer", "Lime Juice", "Whiskey", "NULL", "NULL", "stirred", "Copper Mug" });
        recipes.Add(new List<string>() { "Jack & Coke", "Whiskey", "Coke", "NULL", "NULL", "NULL", "stirred", "Collins Glass" });
        recipes.Add(new List<string>() { "Rum & Coke", "Rum", "Coke", "NULL", "NULL", "NULL", "stirred", "Collins Glass" });
        recipes.Add(new List<string>() { "Whiskey Sour", "Whiskey", "Lime Juice", "Egg Whites", "Syrup", "NULL", "shaken", "Old-Fashioned Glass" });
        recipes.Add(new List<string>() { "Vodka Cranberry", "Cranberry Juice", "Lime Juice", "Orange Juice", "Vodka", "NULL", "stirred", "Highball" });
        recipes.Add(new List<string>() { "Screwdriver", "Orange Juice", "Vodka", "NULL", "NULL", "NULL", "stirred", "Highball" });
    }

    public void Add(Holdable other) {
        if (!other.ingredient) {
            this.glassName = other.name;
        } else {
            playerAnim.SetTrigger("Pour");
            ingredients.Add(other.name);
        }
    }

    public void Build(bool isShaken) {
        
        int closestIndex = 0;
        double closestMatch = 0.0;
        double matchPercent = 0.0;
        if (ingredients.Count < 5) {
            while (ingredients.Count < 5) {
                ingredients.Add("NULL");
            }
        }
        //Debug.Log("Ingredients count:" + ingredients.Count);
        //Debug.Log("Recipes count:" + recipes.Count);
        for (int i = 0; i < 8; i++) { //8 = recipes.Count
            Debug.Log("trigger1");
            for (int j = 1; j < 5; j++) { //5 = ingredients.Count
                Debug.Log("trigger2");
                if (j < 6 && recipes[0].Contains(ingredients[j]) || ingredients[j] == "shaken" && isShaken || ingredients[j] == "stirred" && !isShaken || this.glassName == ingredients[j]) {
                    matchPercent += (1 / 8);
                }
            }
            if (matchPercent > closestMatch) {
                closestIndex = i;
            }
        }

        Debug.Log("Glass: " + glassName);
        Debug.Log("Closest drink: " + recipes[closestIndex][0] + ", at " + (closestIndex * 100) + "%.");
    }
}

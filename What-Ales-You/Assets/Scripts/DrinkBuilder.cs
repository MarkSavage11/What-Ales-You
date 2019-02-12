using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DrinkBuilder : Interactable {
    public string glassName;
    public List<string> ingredients;
    public List<List<string>> recipes;
    public Animator playerAnim;

    public void Start() {//Sets up the recipe array. Mark expressed a desire to move this code somewhere else.
        glassName = "";
        ingredients = new List<string>();
        recipes = new List<List<string>> {
            new List<string>() { "Moscow Mule", "Ginger Beer", "Lime Juice", "Vodka", "NULL", "NULL", "stirred", "Tankard" },
            new List<string>() { "Dark and Stormy", "Ginger Beer", "Lime Juice", "Rum", "NULL", "NULL", "stirred", "Highball Glass" },
            new List<string>() { "Tennessee Mule", "Ginger Beer", "Lime Juice", "Whiskey", "NULL", "NULL", "stirred", "Tankard" },
            new List<string>() { "Jack & Coke", "Whiskey", "Coke", "NULL", "NULL", "NULL", "stirred", "Collins Glass" },
            new List<string>() { "Rum & Coke", "Rum", "Coke", "NULL", "NULL", "NULL", "stirred", "Collins Glass" },
            new List<string>() { "Whiskey Sour", "Whiskey", "Lime Juice", "Egg Whites", "Simple Syrup", "NULL", "shaken", "Old-Fashioned Glass" },
            new List<string>() { "Vodka Cranberry", "Cranberry Juice", "Lime Juice", "Orange Juice", "Vodka", "NULL", "stirred", "Highball Glass" },
            new List<string>() { "Screwdriver", "Orange Juice", "Vodka", "NULL", "NULL", "NULL", "stirred", "Highball Glass" }
        };
    }

    public void Add(Holdable other) {
        if (!other.ingredient) {
            glassName = other.name;
        } else {
            playerAnim.SetTrigger("Pour");
            ingredients.Add(other.name);
        }
    }

    public void Build(bool isShaken) {
        if (glassName != "" && ingredients.Count != 0) {
            double matchPercent = 0.0;
            double closestMatch = 0.0;
            int closestIndex = 0;
            int numNull = 0;

            ingredients = ingredients.Distinct().ToList();

            for (int i = 0; i < recipes.Count; i++) {
                numNull = 0;
                matchPercent = 0.0;
                for (int j = 1; j < 6; j++) {
                    if (recipes[i][j] == "NULL") {
                        numNull++;
                    }
                }
                if (recipes[i][6] == "shaken" && isShaken || recipes[i][6] == "stirred" && !isShaken) { matchPercent += (1.0 / (7.0 - numNull)); }
                if (glassName == recipes[i][7]) { matchPercent += (1.0 / (7.0 - numNull)); }
                for (int j = 0; j < ingredients.Count; j++) {
                    if (recipes[i].Contains(ingredients[j])) {
                        matchPercent += (1.0 / (7.0 - numNull));
                    } else {
                        matchPercent -= (1.0 / (7.0 - numNull));
                    }
                }

                Debug.Log(recipes[i][0] + ":" + matchPercent);
                if (matchPercent > closestMatch) {
                    closestMatch = matchPercent;
                    closestIndex = i;
                }
            }
            
            Debug.Log("Glass: " + glassName);
            Debug.Log("Closest drink: " + recipes[closestIndex][0] + ", at " + Math.Round((closestMatch * 100)) + "%.");
        } else {
            if (isShaken) {
                Debug.Log("You successfully shake the air.");
            } else {
                Debug.Log("You successfully stir the air.");
            }
        }
    }
}

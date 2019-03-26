using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DrinkBuilder : Interactable {
    public Transform glassLocation;
    public Holdable glass;
    public List<string> ingredients;
    public Animator playerAnim;

    public GameObject particlePrefab;

    public void Start() {
        ingredients = new List<string>();
    }

    public void Add(Holdable other) {
        if (!other.ingredient) {
            glass = other;
        } else {
            playerAnim.SetTrigger("Pour");
            ingredients.Add(other.iName);
        }
    }

    public void Build(bool isShaken) {
        if (glass != null && ingredients.Count != 0) {
            float matchPercent = 0.0f;
            float closestMatch = 0.0f;
            string closestDrink = "";

            ingredients = ingredients.Distinct().ToList();

            foreach (Drink drink in Recipes.recipes.Values) {
                matchPercent = 0.0f;
                float drinkElemCount = drink.ingredients.Count + 2;
                if (drink.mixType == "Shaken" && isShaken || drink.mixType == "Stirred" && !isShaken) { matchPercent += (1.0f / drinkElemCount); }
                if (glass.iName == drink.glass) { matchPercent += (1.0f / drinkElemCount); }
                for (int j = 0; j < ingredients.Count; j++) {
                    if (drink.ingredients.Contains(ingredients[j])) {
                        matchPercent += (1.0f / drinkElemCount);
                    } else {
                        matchPercent -= (1.0f / drinkElemCount);
                    }
                }
                if (matchPercent > closestMatch) {
                    closestMatch = matchPercent;
                    closestDrink = drink.drinkName;
                }
            }
            glass.gameObject.AddComponent(typeof(FinishedDrink));
            glass.GetComponent<FinishedDrink>().Init(Recipes.recipes[closestDrink], (float)Math.Round((closestMatch * 100)));
            glass.GetComponent<Holdable>().iName = closestDrink;
            this.glass = null;
            this.ingredients.Clear();
            GameObject particle = Instantiate(particlePrefab, glassLocation);
            Destroy(particle);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//This represents a glass that you can build drinks into 
[RequireComponent(typeof(Holdable))]
public class DrinkContainer : MonoBehaviour
{
    public List<string> ingredients;
    private string glassName; //The name of this glass


    public void Start()
    {
        ingredients = new List<string>();
        glassName = this.GetComponent<Holdable>().iName; 
    }

    public void Add(Holdable other)
    {
        if (other.ingredient) { 
            ingredients.Add(other.iName);
        }
    }

    public void Build(bool isShaken)
    {
        if (ingredients.Count != 0)
        {
            float matchPercent = 0.0f;
            float closestMatch = 0.0f;
            string closestDrink = "";

            ingredients = ingredients.Distinct().ToList();

            foreach (Drink drink in Recipes.recipes.Values)
            {
                matchPercent = 0.0f;
                float drinkElemCount = drink.ingredients.Count + 2;
                if (drink.mixType == "Shaken" && isShaken || drink.mixType == "Stirred" && !isShaken) { matchPercent += (1.0f / drinkElemCount); }
                if (glassName == drink.glass) { matchPercent += (1.0f / drinkElemCount); }
                for (int j = 0; j < ingredients.Count; j++)
                {
                    if (drink.ingredients.Contains(ingredients[j]))
                    {
                        matchPercent += (1.0f / drinkElemCount);
                    }
                    else
                    {
                        matchPercent -= (1.0f / drinkElemCount);
                    }
                }
                if (matchPercent > closestMatch)
                {
                    closestMatch = matchPercent;
                    closestDrink = drink.drinkName;
                }
            }

            this.gameObject.AddComponent(typeof(FinishedDrink));
            if (closestMatch > .3f)
            {
                this.GetComponent<FinishedDrink>().Init(Recipes.recipes[closestDrink], (float)Mathf.Round((closestMatch * 100)));
                this.GetComponent<Holdable>().iName = closestDrink;
            }else
            {
                this.GetComponent<FinishedDrink>().Init(new Drink("Mystery Beverage", null, null, this.glassName), (float)Mathf.Round((closestMatch * 100)));
                this.GetComponent<Holdable>().iName = "Mystery Beverage";
            }
            Destroy(GetComponent<DrinkContainer>());

            //GameObject particle = Instantiate(particlePrefab, this.transform);

        }
    }

}

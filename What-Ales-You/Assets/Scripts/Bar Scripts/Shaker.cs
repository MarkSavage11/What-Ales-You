using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : Interactable
{
    public List<string> ingredients;
    public bool shaken; // If the contents of the shaker has been shaken yet.

    public void Add(Holdable other)
    {
        if (other.ingredient)
        {
            ingredients.Add(other.iName);
        }
    }

    public void Shake()
    {
        //Debug.Log("Shaking");
        shaken = true;
        this.gameObject.AddComponent(typeof(Holdable));
    }

    public IEnumerator PourIntoGlass(DrinkContainer dc)
    {
        if (!shaken){ Debug.LogError("Shouldn't be able to pour the shaker contents before shaking"); }

        dc.ingredients = this.ingredients;
        dc.Build(true);

        yield return FindObjectOfType<PickupManager>().PourShaker(dc.transform);

        this.GetComponent<Holdable>().GoHome();
        GameObject.FindObjectOfType<PickupManager>().SetIsHolding(false);

        Destroy(this.GetComponent<Holdable>());
        
    }





}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkBuilder : Interactable
{
    public string glassName;
    public List<string> ingredients;
    public Animator playerAnim;

    public void Add(Holdable other)
    {
        if (!other.ingredient)
        {
            this.glassName = other.name;
        }else
        {
            playerAnim.SetTrigger("Pour");
            ingredients.Add(other.name);
        }
    }


    public void Build(bool isShaken)
    {
        Debug.Log("Glass: " + glassName);

    }


}

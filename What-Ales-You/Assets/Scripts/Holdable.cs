using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Holdable : Interactable
{
    public Transform homePos;
    public new string name;
    public bool ingredient;
    

    public void MoveToHand(Transform hand)
    {
        this.transform.SetPositionAndRotation(hand.position, hand.rotation);
        this.transform.SetParent(hand);
    }

    public void GoHome()
    {
        this.transform.parent = null;
        this.transform.SetPositionAndRotation(homePos.position, homePos.rotation);

    }

    public void Interact(Interactable other)
    {
        //Try Drinkbuilder 
        DrinkBuilder mat;
        mat = other.transform.GetComponent<DrinkBuilder>();
        mat.Add(this);

        if (!this.ingredient)
        {
            this.transform.parent = null;
            this.transform.SetPositionAndRotation(mat.transform.position, mat.transform.rotation);
        }else
        {
            GoHome();
        }

    }

}

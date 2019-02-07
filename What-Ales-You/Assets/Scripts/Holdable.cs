using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Holdable : Interactable
{
    private Vector3 homePos;
    private Quaternion homeRotation;
    public bool ingredient;

    private Transform hand;

    public void Awake()
    {
        this.homePos = this.transform.position;
        this.homeRotation = this.transform.rotation;
    }

    public void MoveToHand(Transform hand)
    {
        this.transform.SetPositionAndRotation(hand.position, hand.rotation);
        this.transform.SetParent(hand);
        this.hand = hand;
    }

    public void GoHome()
    {
        this.transform.parent = null;
        this.transform.SetPositionAndRotation(homePos, homeRotation);
        this.hand = null;

    }

    public void Interact(Interactable other)
    {
        //Try Drinkbuilder 
        DrinkBuilder mat;
        mat = other.transform.GetComponent<DrinkBuilder>();
        if (mat != null)
        {
            mat.Add(this);

            if (!this.ingredient)
            {
                this.transform.parent = null;
                this.transform.SetPositionAndRotation(mat.transform.position, mat.transform.rotation);
            }
            else
            {
                GoHome();
            }
        }

    }

}

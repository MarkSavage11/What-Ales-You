using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Holdable : Interactable
{
    private Vector3 homePos;
    private Quaternion homeRotation;
    public bool ingredient;
    public float bottleHeight;

    //private Transform hand;

    public void Awake()
    {
        this.homePos = this.transform.position;
        this.homeRotation = this.transform.rotation;
    }



    public void MoveToHand(Transform hand)
    {
        //Checks to see if it is placed in a Node
        PlaceNode node = this.GetComponentInParent<PlaceNode>();
        if (node != null)
        {
            node.full = false;
        }

        this.transform.SetPositionAndRotation(hand.position, hand.rotation);
        this.transform.SetParent(hand);
        //this.hand = hand;
    }

    public void GoHome()
    {
        this.transform.parent = null;
        this.transform.SetPositionAndRotation(homePos, homeRotation);
        //this.hand = null;

    }

    public void Interact(Interactable other)
    {

        //Try DrinkContainer
        DrinkContainer container;
        container = other.transform.GetComponent<DrinkContainer>();

        if (container != null)
        {
            Shaker thisShaker = this.GetComponent<Shaker>();

            if (this.tag == "Bar Spoon" && container.ingredients.Count != 0) //Checks to see I am a barspoon 
            {
                StartCoroutine(FindObjectOfType<PickupManager>().Stir(container.transform));
                container.Build(false);
                return;
            }else if (thisShaker != null) //Checks to see if I am a shaker
            {
                
                StartCoroutine(thisShaker.PourIntoGlass(container));
                return;
            }else
            {
                if (this.ingredient)
                {
                    StartCoroutine(FindObjectOfType<PickupManager>().PourIngredient(container.transform, bottleHeight));
                }
                    
                container.Add(this);
                return;
                 
            }
           
        }

        //Try Shaker
        Shaker shaker;
        shaker = other.transform.GetComponent<Shaker>();

        if (shaker != null)
        {
            StartCoroutine(FindObjectOfType<PickupManager>().PourIngredient(shaker.transform, bottleHeight));
            shaker.Add(this);
            return;

        }

        //Try Drink Dump
        if(other.tag == "Dump")
        {
            DrinkContainer myDC = this.GetComponent<DrinkContainer>();
            Shaker myShaker = this.GetComponent<Shaker>();
            FinishedDrink myFD = this.GetComponent<FinishedDrink>();
            
            if(myDC != null)
            {
                if (myFD != null)
                    Destroy(myFD);
                myDC.ingredients.Clear();
                StartCoroutine(GameObject.FindObjectOfType<PickupManager>().PourIngredient(other.transform, 0f));
            }
            else if(myShaker != null)
            {
                //StartCoroutine(GameObject.FindObjectOfType<PickupManager>().PourIngredient(other.transform, 0f));
                myShaker.ingredients.Clear();
                myShaker.shaken = false;
                myShaker.GetComponent<Holdable>().GoHome();
                Destroy(myShaker.GetComponent<Holdable>());
            }
        }

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupManager : MonoBehaviour
{
    [Header("Pickup info")]
    [SerializeField]
    private Camera cam;
    private bool isHolding = false;
    private Holdable held;
    public float range;
    public Transform holdLoc;
    public OrderHandler orderHandler;

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetMouseButtonDown(0))
        {
            if (!isHolding)
            {
                Use();
            }else
            {
                this.UseHeld();
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (isHolding) {
                Place();
            }
            
        }
    }

    void Use()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, range)){
            //Testing for Holdable
            //Debug.Log("Hit " + hit.transform.name);
            held = hit.transform.GetComponent<Holdable>();
            //Debug.Log("Holding " + held.transform.name);
            if (held != null)
            {
                isHolding = true;
                held.MoveToHand(holdLoc);
            }

            //Testing for Drink Finisher
            DrinkFinisher finisher;
            finisher = hit.transform.GetComponent<DrinkFinisher>();

            if(finisher != null)
            {
                finisher.Finish();
            }
        }
    }

    void Place()
    {
        if (held != null)
        {
            RaycastHit hit;
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
            {
                PlaceNode node;
                node = hit.transform.GetComponent<PlaceNode>();
                if (node != null)
                {
                    if (!node.full)
                    {
                        this.isHolding = false;
                        this.held.transform.SetPositionAndRotation(node.transform.position, node.transform.rotation);
                        node.full = true;
                        held.transform.parent = node.transform;
                        this.held = null;
                        
                    }
                }
            }
        }
    }

    //Shouldn't be called if isHolding = false;
    void UseHeld()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            FinishedDrink fd = this.held.GetComponent<FinishedDrink>();
            if (fd != null && orderHandler.currentOrder != null) 
            {
                Patron patron = hit.transform.GetComponent<Patron>();
                if (patron != null)
                {
                    patron.AcceptDrink(this.held.GetComponent<FinishedDrink>());
                    this.isHolding = false;
                }
            }


            //Interactable
            Interactable other;
            other = hit.transform.GetComponent<Interactable>();
            if (other != null)
            {
                //This has to happen in order to place the glass on the drink mat
                if (!held.ingredient && other.GetComponent<DrinkBuilder>() != null) { 
                    this.isHolding = false;
                }
                this.held.Interact(other);
            }   
        }
    }
}

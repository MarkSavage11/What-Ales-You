﻿using System.Collections;
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

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetMouseButtonDown(0))
        {
            if (!isHolding)
            {
                //Debug.Log("Isn't Holding Anything");
                Interact();
            }else
            {
                this.Use();
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (isHolding) {
                Place();
            }
            
        }
    }

    void Interact()
    {
        RaycastHit hit;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range)){
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
        if(held != null)
        {
            this.isHolding = false;
            held.GoHome();
            this.held = null;
        }
    }

    //Shouldn't be called if isHolding = false;
    void Use()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            Interactable other;
            other = hit.transform.GetComponent<Interactable>();
            if (other != null)
            {
                ///BAD DON"T DO THIS
                this.isHolding = false;
                this.held.Interact(other);
            }
        }
    }
}

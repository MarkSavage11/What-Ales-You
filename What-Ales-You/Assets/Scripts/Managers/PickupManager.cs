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
    [SerializeField] private Transform holdLoc;

    public OrderHandler orderHandler;

    private Animator anim;
    private Transform body;

    private bool isFocused = false;

    public void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        body = GetComponentInParent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetMouseButtonDown(0))
        {
            if (!isHolding)
            {
                Use();
                return;
            }else
            {
                this.UseHeld();
                return;
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (isHolding) {
                Place();
                return;
            }
            
        }
    }

    void Use()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, range)){
            //Testing for Holdable

            held = hit.transform.GetComponent<Holdable>();

            if (held != null)
            {
                isHolding = true;
                held.MoveToHand(holdLoc);
                return;
            }

            //Testing for Shaker Top
            ShakerTop shakerTop;
            shakerTop = hit.transform.GetComponent<ShakerTop>();

            if(shakerTop != null)
            {
                shakerTop.Finish();
                return;
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
            //Debug.Log("Has FD: " + fd != null);
            //Debug.Log("Current order isnt' null: " + orderHandler.currentOrder != null);
            if (fd != null && orderHandler.currentOrder != null) 
            {
                
                Debug.Log(orderHandler.currentOrder);
                Patron patron = hit.transform.GetComponent<Patron>();
                if (patron != null)
                {
                    
                    patron.AcceptDrink(this.held.GetComponent<FinishedDrink>());
                    this.isHolding = false;
                    return;
                }
                
            }


            //Interactable
            Interactable other;
            other = hit.transform.GetComponent<Interactable>();
            if (other != null)
            {
                this.held.Interact(other);
                
            }
        }
      
    }


    public void SetIsHolding(bool b)
    {
        this.isHolding = b;
    }


    private Vector3 pourOffset = new Vector3(.05f, .2f, 0f);

    ///ANIMATION CODE
    public IEnumerator PourIngredient(Transform target, float offset)
    {
        FindObjectOfType<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().isFocused = true;
        Vector3 startHandPos = holdLoc.position;
        Quaternion startHandRotation = holdLoc.rotation;

        holdLoc.SetPositionAndRotation(target.position + pourOffset + new Vector3(offset, 0,0), Quaternion.identity);
       
        anim.SetTrigger("Pour");
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);

        holdLoc.SetPositionAndRotation(startHandPos, startHandRotation);
        FindObjectOfType<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().isFocused = false;
    }

    public IEnumerator PourShaker(Transform target)
    {
        FindObjectOfType<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().isFocused = true;
        Vector3 startHandPos = holdLoc.transform.position;
        holdLoc.SetPositionAndRotation(target.position + pourOffset +new Vector3(.1f, 0, 0), Quaternion.identity);
        anim.SetTrigger("Pour");

        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);

        holdLoc.SetPositionAndRotation(startHandPos, Quaternion.identity);
        FindObjectOfType<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().isFocused = false;
        ParticleSystem ps = Instantiate(ParticleManager.instance.poofPrefab, target.position + new Vector3(0f, .1f, 0f), Quaternion.identity) as ParticleSystem;
        Destroy(ps.gameObject, ps.main.duration);
    }

    public IEnumerator Stir(Transform target)
    {
        FindObjectOfType<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().isFocused = true;
        Vector3 startHandPos = holdLoc.transform.position;
        holdLoc.SetPositionAndRotation(target.position + new Vector3(0f, .15f, 0f), Quaternion.identity);
        holdLoc.rotation = Quaternion.identity;
        Quaternion startSpoonRotation = this.held.transform.rotation;
        held.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        anim.SetTrigger("Stir");
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        held.transform.rotation = startSpoonRotation;
        holdLoc.SetPositionAndRotation(startHandPos, Quaternion.identity);
        FindObjectOfType<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().isFocused = false;
        ParticleSystem ps = Instantiate(ParticleManager.instance.poofPrefab, target.position + new Vector3(0f, .1f, 0f), Quaternion.identity) as ParticleSystem;
        Destroy(ps.gameObject, ps.main.duration);

    }

    public IEnumerator Shake(Shaker shaker, ShakerTop shakerTop)
    {
        shakerTop.transform.parent = shaker.transform;
        shakerTop.transform.position = shaker.transform.position + new Vector3(0f, .1f, 0f);
        shaker.Shake();
        isHolding = true;
        shaker.GetComponent<Holdable>().MoveToHand(holdLoc);
        this.held = shaker.GetComponent<Holdable>();

        shaker.transform.Translate(new Vector3(.15f, .05f, .15f));
        shaker.transform.localRotation = Quaternion.Euler(new Vector3(0f, 42f, -50f));

        anim.SetTrigger("Shake");
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);

        shaker.transform.SetPositionAndRotation(holdLoc.position, holdLoc.rotation);
        shakerTop.GoHome();
    }

    public void DebugHandMovement()
    {
        Debug.Log("Moving hand");
        //this.holdLoc.parent = null;
        Debug.Log("StartingLoc: " + this.holdLoc.position);
        this.holdLoc.position = new Vector3(0, 1, 2);
        //this.holdLoc.GetComponent<Hand>().Interact(new Vector3(0, 2, 3));
        Debug.Log("MovedLoc: " + this.holdLoc.position);
        
    }
}

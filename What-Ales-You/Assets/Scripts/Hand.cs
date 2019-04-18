using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    private bool interacting = false;
    private Vector3 target;

    // Update is called once per frame
    void Update()
    {
        if (interacting)
        {
            this.transform.position = target;
        }
    }

    public void Interact(Vector3 target)
    {
        interacting = true;
        this.target = target;
    }

    public void StopInteract()
    {
        interacting = false;
    }
}

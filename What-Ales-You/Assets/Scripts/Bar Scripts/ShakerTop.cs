using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakerTop : Interactable
{

    [SerializeField]
    private Shaker shaker;
    private Vector3 startPosition;

    public void Start()
    {
        startPosition = this.transform.position;
    }

    public void Finish()
    {
        if (shaker.ingredients.Count > 0)
        {
            StartCoroutine(FindObjectOfType<PickupManager>().Shake(shaker, this));
        }
    }

    public void GoHome()
    {
        this.transform.parent = null;
        this.transform.position = startPosition;
        this.transform.rotation = Quaternion.identity;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakerTop : Interactable
{

    [SerializeField]
    private Shaker shaker;

    public void Finish()
    {
        StartCoroutine(FindObjectOfType<PickupManager>().Shake(shaker, this.transform));
    }
}

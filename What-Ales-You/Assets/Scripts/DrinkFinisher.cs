using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkFinisher : Interactable
{
    public DrinkBuilder mat;
    public bool isShaker;

    public void Finish()
    {
        mat.Build(isShaker);
    }
}

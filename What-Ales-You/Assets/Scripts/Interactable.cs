using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{

    public Text hoverText;
    public Image reticle;
    public Sprite defaultReticle;
    public Sprite handReticle;
    public new string name;

    public void OnMouseEnter()
    {
        reticle.sprite = handReticle;
        hoverText.text = "| " + this.GetComponent<Interactable>().name;
    }

    public void OnMouseExit()
    {
        reticle.sprite = defaultReticle;
        hoverText.text = "";
    }
}

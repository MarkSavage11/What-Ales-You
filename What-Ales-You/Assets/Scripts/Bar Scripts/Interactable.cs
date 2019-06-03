using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{

    public Text hoverText;
    public Image reticle;
    public Sprite interactReticle;
    public string iName;

    public void Start()
    {
        this.hoverText = GameObject.Find("HoverText").GetComponent<Text>();
        this.reticle = GameObject.Find("Reticle").GetComponent<Image>();
        if (iName == null) iName = this.name;
    }

    public void OnMouseEnter()
    {

        //If this object has a specified interact reticle, it uses that, otherwise uses the default.
        if(interactReticle != null)
        {
            reticle.sprite = interactReticle;
        }else
        {
            reticle.sprite = reticle.GetComponent<Reticle>().grabReticle;
        }
        hoverText.text = "| " + iName;
    }

    public void OnMouseExit()
    {
        reticle.sprite = reticle.GetComponent<Reticle>().defaultReticle;
        hoverText.text = "";
    }
}

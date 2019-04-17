using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueContainerAlignment : MonoBehaviour
{
    //Player object, in order to track rotation.
    public GameObject player;

    [Tooltip("Angle player must be turned for textbox to move to a the left or right location")]
    public float activationAngle;

    //UI Animator 
    public Animator anim;


    public void Update()
    {
        float playerRotation = player.transform.rotation.eulerAngles.y;

        if(playerRotation > activationAngle && playerRotation < 360 - activationAngle)
        {
            if(playerRotation > 180)
            {
                anim.SetTrigger("ShiftRight");
                Debug.Log("ShiftRight");
            }else
            {
                anim.SetTrigger("ShiftLeft");
            }

        }else
        {
            anim.SetTrigger("ReturnCenter");
        }
    }

}

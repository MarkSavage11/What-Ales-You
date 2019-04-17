using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class KeyButton : MonoBehaviour
{
    public KeyCode key;
    public Button buttonMe;

    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            buttonMe.onClick.Invoke();
        }
    }
}

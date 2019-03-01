using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderHandler : MonoBehaviour
{
    //If we want multiple active orders, make this a List
    public Order currentOrder = null;
    public GameObject orderWindow;
    public Text orderText;

    public void TakeOrder(string patron, string drinkName)
    {
        currentOrder = new Order(patron, drinkName);
        orderWindow.SetActive(true);
        orderText.text = drinkName + "for " + patron;

    }

    public void FinishOrder(FinishedDrink drink)
    {
        Debug.Log("Order for " + currentOrder.patron + " completed");
        currentOrder = null;
        orderWindow.SetActive(false);
    }


}

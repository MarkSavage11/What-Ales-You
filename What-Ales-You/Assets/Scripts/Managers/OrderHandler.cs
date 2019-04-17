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
    [Header("Score Variables")]
    public float goalTime = 45f;
    public float totalScore = 0f;
    public Text scoreText;

    private float startTime;

    public void TakeOrder(string patron, string drinkName)
    {
        currentOrder = new Order(patron, drinkName);
        orderWindow.SetActive(true);
        orderText.text = drinkName + " for " + patron;
        startTime = Time.time;

    }

    public void FinishOrder(FinishedDrink drink)
    {
        Debug.Log("Order for " + currentOrder.patron + " completed");
        currentOrder = null;
        orderWindow.SetActive(false);
        Score(drink);
    }

    public void Score(FinishedDrink drink)
    {
        float timeTaken = Time.time - startTime;
        Debug.Log("Order took : " + timeTaken);
        float score = Mathf.Clamp(Mathf.Floor(drink.accuracy / 100 * (goalTime - timeTaken)), 5, 50);
        Debug.Log("Score: " + score);
        totalScore += score;
        scoreText.text = "Score: " + totalScore.ToString();
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn;

public class Patron : MonoBehaviour
{

    public string characterName;
    public string textNode;

    public OrderHandler orderHandler;


    public Transform startMarker;
    public Transform endMarker;

    public Transform hand;

    // Movement speed in units/sec.
    public float speed = 1.0F;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;

    private bool hasOrdered = false;

    void Start()
    {
        // Keep a note of the time the movement started.
        startTime = Time.time;

        // Calculate the journey length.
        journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
    }

    [Yarn.Unity.YarnCommand("order")]
    public void OrderDrink(string[] drinkName)
    {
        string orderName = "";
        foreach (string word in drinkName)
        {
            orderName += word + " ";
        }
        orderHandler.TakeOrder(characterName, orderName);

    }

    public void Update()
    {

            // Distance moved = time * speed.
            float distCovered = (Time.time - startTime) * speed;

            // Fraction of journey completed = current distance divided by total distance.
            float fracJourney = distCovered / journeyLength;

            // Set our position as a fraction of the distance between the markers.
            transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fracJourney);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasOrdered)
        {
            FindObjectOfType<Yarn.Unity.DialogueRunner>().StartDialogue(this.textNode);
        }
    }

    public void AcceptDrink(FinishedDrink drink)
    {
        drink.transform.SetPositionAndRotation(this.hand.transform.position, this.hand.rotation);
        drink.transform.SetParent(this.hand);
        orderHandler.FinishOrder(drink);
        this.hasOrdered = true;

        FindObjectOfType<Yarn.Unity.DialogueRunner>().StartDialogue("OrderComplete"+this.characterName);
        Transform newEndMarker = this.startMarker;
        this.transform.Rotate(0, 180, 0);
        this.startTime = Time.time;
        this.startMarker = this.transform;
        this.endMarker = newEndMarker;
    }


}

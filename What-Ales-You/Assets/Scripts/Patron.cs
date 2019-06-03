using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Yarn;

public class Patron : MonoBehaviour
{

    public string characterName;
    public string textNode;

    public OrderHandler orderHandler;


    public Transform barMarker;
    public Transform endMarker;

    public Transform hand;

    public Animator anim;
    private NavMeshAgent thisAgent;
    private bool hasOrdered = false;
    private bool hasThankedYou = false;
    private bool hasFinished = false;
    private bool reachedBar = false;

    public void Start()
    {
        thisAgent = GetComponent<NavMeshAgent>();
        GoToBar();
    }

    public void Update()
    {
        if(!hasThankedYou && hasOrdered && !FindObjectOfType<Yarn.Unity.DialogueRunner>().isDialogueRunning)
        {
            hasThankedYou = true;
            StartCoroutine(LeaveBar()); 
        }

        //Animation
        float speedPercent = thisAgent.velocity.magnitude / thisAgent.speed;
        anim.SetFloat("SpeedPercent", speedPercent);
        //Debug.Log(speedPercent);
    }

    public void GoToBar()
    {
        thisAgent.SetDestination(barMarker.position);
    }

    [Yarn.Unity.YarnCommand("order")]
    public void OrderDrink(string[] drinkName)
    {
        string orderName = "";
        for (int i = 0; i < drinkName.Length; i++)
        {
            orderName += drinkName[i];
            if (i != drinkName.Length - 1) orderName += " ";
        }
        orderHandler.TakeOrder(characterName, orderName);

    }


    private void OnTriggerEnter(Collider other)
    {
        if (!reachedBar)
        {
            reachedBar = true;
            //this.transform.rotation = Quaternion.Euler(0, 180, 0);
            if (!hasOrdered)
            {
                FindObjectOfType<Yarn.Unity.DialogueRunner>().StartDialogue(this.textNode);
            }
        }
    }

    public void AcceptDrink(FinishedDrink drink)
    {
        drink.transform.SetPositionAndRotation(this.hand.transform.position, this.hand.rotation);
        drink.transform.SetParent(this.hand);
        orderHandler.FinishOrder(drink);
        this.hasOrdered = true;


    }

    public IEnumerator LeaveBar()
    {
        //anim.SetTrigger("Drink");

        try { 
            FindObjectOfType<Yarn.Unity.DialogueRunner>().StartDialogue("OrderComplete" + this.characterName);
        }catch(System.Exception e)
        {
            Debug.Log("No Order Complete node for " + this.characterName);
        }
        while (FindObjectOfType<Yarn.Unity.DialogueRunner>().isDialogueRunning)
        {
            yield return null;
        }
        thisAgent.SetDestination(endMarker.position);
        yield return new WaitForSeconds(5);
        hasFinished = true;

    }

    public bool HasFinished()
    {
        return hasFinished;
    }


}

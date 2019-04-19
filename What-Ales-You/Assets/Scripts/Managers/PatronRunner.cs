using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatronRunner : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> patronList;

    public Transform spawn;

    public float startDelay;
    public float averageDelay;
    public float delayVarience;
    public float endDelay;
    public GameObject endScreen;

    private Queue<GameObject> patrons;
    private GameObject currentPatron;
    private bool gettingNext = false;

    public void Awake()
    {
        patrons = new Queue<GameObject>(patronList);
    }

    public IEnumerator Start()
    {
        yield return new WaitForSeconds(startDelay);
        currentPatron = patrons.Dequeue();
        currentPatron.transform.SetPositionAndRotation(spawn.position, Quaternion.identity);
        currentPatron.SetActive(true);

    }

    public void Update()
    {
        if (currentPatron != null)
        {
            currentPatron.GetComponent<Patron>().HasFinished();
            if (currentPatron.GetComponent<Patron>().HasFinished() && !gettingNext)
            {
                StartCoroutine(NextPatron());
                
            }
        }

    }

    public IEnumerator EndDay()
    {
        yield return StartCoroutine(Wait(endDelay));
        StopAllCoroutines();
        Debug.Log("End of Day");
        endScreen.SetActive(true);
    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
    }

    IEnumerator NextPatron()
    {
        gettingNext = true;
        yield return new WaitForSeconds(5);
              
        Debug.Log(patrons.Count);
        if (patrons.Count > 0)
        {
            
            currentPatron.SetActive(false);
            yield return new WaitForSeconds(Random.Range(averageDelay - delayVarience, averageDelay + delayVarience));
            currentPatron = patrons.Dequeue();
            currentPatron.transform.SetPositionAndRotation(spawn.position, Quaternion.identity);
            currentPatron.SetActive(true);
        }
        else
        {
            currentPatron = null;
            yield return EndDay();
        }
        gettingNext = false;
    }


}

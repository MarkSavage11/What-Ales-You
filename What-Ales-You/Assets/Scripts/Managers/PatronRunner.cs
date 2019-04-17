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

    private Queue<GameObject> patrons;
    private GameObject currentPatron;

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
            bool hasFinished = currentPatron.GetComponent<Patron>().HasFinished();
            if (hasFinished)
            {
                currentPatron.SetActive(false);
                StartCoroutine(Wait(Random.Range(averageDelay - delayVarience, averageDelay + delayVarience)));
                Debug.Log(patrons.Count);
                if (patrons.Count > 0)
                {
                    currentPatron = patrons.Dequeue();
                    currentPatron.transform.SetPositionAndRotation(spawn.position, Quaternion.identity);
                    currentPatron.SetActive(true);
                }
                else
                {
                    currentPatron = null;
                    EndDay();
                }
            }
        }

    }

    public IEnumerator EndDay()
    {
        yield return StartCoroutine(Wait(endDelay));
        Debug.Log("End of Day");
    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Panic : GoapAction
{
    bool completed = false;
    float startTime = 0;
    public float workDuration = 2; // seconds
    public GameObject tears;
    public Panic()
    {
        addPrecondition("isPanicking", true);
        addEffect("doJob", true);
        name = "Panic";
    }

    public override void reset()
    {
        completed = false;
        startTime = 0;
    }

    public override bool isDone()
    {
        return completed;
    }

    public override bool requiresInRange()
    {
        return false;
    }

    public override bool checkProceduralPrecondition(GameObject agent)
    {
        return true;
    }

    public override bool perform(GameObject agent)
    {
        if (startTime == 0)
        {
            this.GetComponent<NavMeshAgent>().SetDestination(new Vector3(Random.Range(-10,10), 0, Random.Range(-10,10)));
            this.GetComponent<NavMeshAgent>().speed = 1.5f;
            tears.SetActive(true);
            Debug.Log("Starting: " + name);
            startTime = Time.time;
        }

        if (Time.time - startTime > workDuration)
        {
            tears.SetActive(false);
            this.GetComponent<NavMeshAgent>().speed = 1f;
            Debug.Log("Finished: " + name);
            completed = true;
        }
        return true;
    }
}

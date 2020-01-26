using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseAlien : GoapAction
{
    bool completed = false;
    float startTime = 0;
    public float workDuration = 2; // seconds

    public ChaseAlien()
    {
        addPrecondition("isChasing", true);
        addEffect("doJob", true);
        name = "ChaseAlien";
    }

    void Update()
    {   
        target = GetComponent<Soldier>().alienTarget;
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
            this.GetComponent<NavMeshAgent>().speed = 1.5f;
            Debug.Log("Starting: " + name);
            startTime = Time.time;
        }

        if (Time.time - startTime > workDuration)
        {
            this.GetComponent<NavMeshAgent>().speed = 0.5f;
            Debug.Log("Finished: " + name);
            completed = true;
        } else {
            this.GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
        }
        return true;
    }
}

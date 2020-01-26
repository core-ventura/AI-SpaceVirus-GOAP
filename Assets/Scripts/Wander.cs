using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : GoapAction
{
    bool completed = false;
    float startTime = 0;
    public float workDuration = 2; // seconds

    SpawnManager spawnManager;

    void Start()
    {
        spawnManager = FindObjectOfType<SpawnManager>();
        target = spawnManager.alienWanderTargets[Random.Range(0, spawnManager.alienWanderTargets.Length)];
    }

    public Wander()
    {
        addEffect("doJob", true);
        name = "Wander";
    }

    public override void reset()
    {
        completed = false;
        startTime = 0;
        target = spawnManager.alienWanderTargets[Random.Range(0, spawnManager.alienWanderTargets.Length)];
    }

    public override bool isDone()
    {
        return completed;
    }

    public override bool requiresInRange()
    {
        return true;
    }

    public override bool checkProceduralPrecondition(GameObject agent)
    {
        return true;
    }

    public override bool perform(GameObject agent)
    {
        if (startTime == 0)
        {
            Debug.Log("Starting: " + name);
            startTime = Time.time;
        }

        if (Time.time - startTime > workDuration)
        {
            Debug.Log("Finished: " + name);
            completed = true;
        }
        return true;
    }
}

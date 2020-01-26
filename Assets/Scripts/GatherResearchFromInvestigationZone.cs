using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherResearchFromInvestigationZone : GoapAction
{
    bool completed = false;
    float startTime = 0;
    public float workDuration = 2; // seconds
    public Inventory investigationZoneInventory;
    public int infectionChance; 
    
    public GatherResearchFromInvestigationZone()
    {
        addPrecondition("investigationZoneHasResearch", true);
        addEffect("hasResearch", true);
        name = "GatherResearchFromInvestigationZone";
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
            investigationZoneInventory.research--;

            // Infected
            int infectionProc = Random.Range(0, 101);
            if(infectionProc <= infectionChance)
            {
                // Spawn
                Debug.Log("Infestation!");
                SpawnManager spawnManager = FindObjectOfType<SpawnManager>();
                GameObject newAlien = Instantiate(spawnManager.alien, this.transform.position, this.transform.rotation);
                newAlien.GetComponent<Wander>().target = spawnManager.alienWanderTargets[Random.Range(0, spawnManager.alienWanderTargets.Length)];
                ParticleSystem bloodEffect = Instantiate(spawnManager.alienConversionParticles, this.transform.position, this.transform.rotation);
                bloodEffect.Play();
                //Destroy(astronautTarget);
                this.gameObject.SetActive(false);
            }

            completed = true;
        }
        return true;
    }

}

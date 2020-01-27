using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipSoldierGear : GoapAction
{
    bool completed = false;
    float startTime = 0;
    public float workDuration = 2; // seconds
    public GameObject encourage;
    public Inventory armoryInventory;
    public EquipSoldierGear()
    {
        addPrecondition("isEncouraged", true);
        addEffect("doJob", true);
        name = "EquipSoldierGear";
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
            encourage.SetActive(false);
            armoryInventory.weapons--;

			SpawnManager spawnManager = FindObjectOfType<SpawnManager>();
			GameObject newSoldier = Instantiate(spawnManager.soldier, this.transform.position, this.transform.rotation);
			newSoldier.GetComponent<Wander>().target = spawnManager.alienWanderTargets[Random.Range(0, spawnManager.alienWanderTargets.Length)];
			//Destroy(this.gameObject);
			this.gameObject.SetActive(false);
            Debug.Log("Finished: " + name);
            completed = true;
        }
        return true;
    }
}

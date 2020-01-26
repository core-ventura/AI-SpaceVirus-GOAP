using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class Crewman : MonoBehaviour, IGoap
{
	NavMeshAgent agent;
	Vector3 previousDestination;
    public Inventory crashSiteInventory;
	public Inventory communicationsStationInventory;
	public Inventory storageFacilityInventory;
	public Inventory infirmaryInventory;
	public Inventory armoryInventory;
	public Inventory investigationZoneInventory;

	// 0 -> Normal
	// 1 -> Panic mode
	// 2 -> Encouraged mode

	int alertState = 0; 

	void Start()
	{
		agent = this.GetComponent<NavMeshAgent>();
	}

	public HashSet<KeyValuePair<string,object>> GetWorldState () 
	{
		HashSet<KeyValuePair<string,object>> worldData = new HashSet<KeyValuePair<string,object>> ();

        worldData.Add(new KeyValuePair<string, object>("isPanicking", (this.alertState == 1)));
        worldData.Add(new KeyValuePair<string, object>("isEncouraged", (this.alertState == 2)));

        worldData.Add(new KeyValuePair<string, object>("crashSiteHasSpaceshipParts", (crashSiteInventory.spaceshipParts > 0)));
	    worldData.Add(new KeyValuePair<string, object>("communicationsStationHasBlueprints", (communicationsStationInventory.blueprints > 0)));
        worldData.Add(new KeyValuePair<string, object>("infirmaryHasMedipacks", (infirmaryInventory.medipacks > 0)));
        worldData.Add(new KeyValuePair<string, object>("armoryHasWeapons", (crashSiteInventory.weapons > 0)));
        worldData.Add(new KeyValuePair<string, object>("investigationZoneHasResearch", (investigationZoneInventory.research > 0)));

        return worldData;
	}


	public HashSet<KeyValuePair<string,object>> CreateGoalState ()
	{
		HashSet<KeyValuePair<string,object>> goal = new HashSet<KeyValuePair<string,object>> ();
        goal.Add(new KeyValuePair<string, object>("doJob", true));
		return goal;
	}


	public bool MoveAgent(GoapAction nextAction) {
		
		// If we don't need to move anywhere
		if(previousDestination == nextAction.target.transform.position)
		{
			nextAction.setInRange(true);
			return true;
		}
		
		agent.SetDestination(nextAction.target.transform.position);
		
		if (agent.hasPath && agent.remainingDistance < 0.25) {
			nextAction.setInRange(true);
			previousDestination = nextAction.target.transform.position;
			return true;
		} else
			return false;
	}

	void Update()
	{
		if(agent.hasPath)
		{
			Vector3 toTarget = agent.steeringTarget - this.transform.position;
         	float turnAngle = Vector3.Angle(this.transform.forward,toTarget);
         	agent.acceleration = turnAngle * agent.speed;
		}
	}

	public void PlanFailed (HashSet<KeyValuePair<string, object>> failedGoal)
	{

	}

	public void PlanFound (HashSet<KeyValuePair<string, object>> goal, Queue<GoapAction> actions)
	{

	}

	public void ActionsFinished ()
	{

	}

	public void PlanAborted (GoapAction aborter)
	{

	}

	void OnTriggerEnter(Collider collider)
	{
		if(collider.tag == "Alien")
		{
			GetComponentInChildren<SphereCollider>().enabled = false;
			if(armoryInventory.weapons > 0)
			{
				alertState = Random.Range(1,3);
			} else {
				alertState = 1;
			}

			if(alertState == 1){
				Debug.Log("Alien detected! Crewman is panicking!");  
			}
			if(alertState == 2){
				Debug.Log("Alien detected! Crewman is encouraged!");  
				this.transform.Find("Encourage").gameObject.SetActive(true);
			}
			GoapAgent ga = this.GetComponent<GoapAgent>();
			ga.AbortAndReturnToIdleState();
		}
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Alien : MonoBehaviour, IGoap
{
    NavMeshAgent agent;
	Vector3 previousDestination;
	[HideInInspector]
	public GameObject astronautTarget;

	// 0 -> Normal mode
	// 1 -> Chase mode
	int alertState = 0; 

	void Start()
	{
		agent = this.GetComponent<NavMeshAgent>();
	}

	public HashSet<KeyValuePair<string,object>> GetWorldState () 
	{
		HashSet<KeyValuePair<string,object>> worldData = new HashSet<KeyValuePair<string,object>> ();

        worldData.Add(new KeyValuePair<string, object>("isChasing", (this.alertState == 1)));
        return worldData;
	}


	public HashSet<KeyValuePair<string,object>> CreateGoalState ()
	{
		HashSet<KeyValuePair<string,object>> goal = new HashSet<KeyValuePair<string,object>> ();
        goal.Add(new KeyValuePair<string, object>("doJob", true));
		return goal;
	}


	public bool MoveAgent(GoapAction nextAction) {
		
		//if we don't need to move anywhere
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

		if(alertState == 1 && Vector3.Distance(this.transform.position, astronautTarget.transform.position) < 0.35f)
		{
			alertState = 0;
			SpawnManager spawnManager = FindObjectOfType<SpawnManager>();
			GameObject newAlien = Instantiate(spawnManager.alien, astronautTarget.transform.position, astronautTarget.transform.rotation);
			newAlien.GetComponent<Wander>().target = spawnManager.alienWanderTargets[Random.Range(0, spawnManager.alienWanderTargets.Length)];
            ParticleSystem bloodEffect = Instantiate(spawnManager.alienConversionParticles, this.transform.position, this.transform.rotation);
            bloodEffect.Play();
			//Destroy(astronautTarget);
			astronautTarget.SetActive(false);
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
		if(collider.tag == "Astronaut")
		{
			astronautTarget = collider.gameObject.transform.parent.gameObject;
			alertState = 1;
			Debug.Log("Astronaut detected! FOOD!");  
			GoapAgent ga = this.GetComponent<GoapAgent>();
			ga.AbortAndReturnToIdleState();
		}
	}
}
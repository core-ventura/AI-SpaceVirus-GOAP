using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Soldier : MonoBehaviour, IGoap
{
    NavMeshAgent agent;
	Vector3 previousDestination;
	[HideInInspector]
	public GameObject alienTarget;

	// 0 -> Normal
	// 1 -> Chase
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

		if(alertState == 1 && Vector3.Distance(this.transform.position, alienTarget.transform.position) < 0.5f)
		{
			alertState = 0;
			SpawnManager spawnManager = FindObjectOfType<SpawnManager>();
            ParticleSystem bloodEffect = Instantiate(spawnManager.alienExplosionParticles, this.transform.position, this.transform.rotation);
            bloodEffect.Play();
			//Destroy(alienTarget);
			alienTarget.SetActive(false);
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
			alienTarget = collider.gameObject.transform.parent.gameObject;
			alertState = 1;
			Debug.Log("Alien detected! FIRE!");  
			GoapAgent ga = this.GetComponent<GoapAgent>();
			ga.AbortAndReturnToIdleState();
		}
	}
}

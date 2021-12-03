using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    [SerializeField] private Sight sight;
	[SerializeField] private Automaton automaton;
	
	static State RandMove = new State("RandMove");
    static State EnemySpotted = new State("EnemySpotted");

    //RandMove.SetConnection("Enemy spotted", EnemySpotted);		invalid token '(' in class, struct or interface member declaration
    //EnemySpotted.SetConnection("Enemy lost", RandMove);
        
	State CurrentState = RandMove;// = automaton.AgentAutomaton;

    void Awake()
    {
        sight.OnEnterVision += EnterVision;
    }

    void EnterVision(object sender, VisionEventArgs args)
    {
        Debug.Log($"{gameObject.name} sighted: {args.collider.gameObject.name}");
    }
    
    void FixedUpdate()
    {
        
        
        Reason();
        MakeMove();
    }

    void Reason()
    {
        foreach (Collider collider in sight.inSight)
        {
			if (!collider.gameObject.name.Contains("Enemy"))
			{
				//CurrentState.GetNextState("Enemy lost");
			}
            else if (collider.gameObject.name.Contains("Enemy"))
			{
				//CurrentState.GetNextState("Enemy spotted");
			}
        }
    }

    void MakeMove()
    {
        if (CurrentState.name == "RandMove")
		{
			this.transform.Translate(new Vector3(.02f, 0f, 0f)); //.02f = Time.fixedTimeDelta 
		}
		else if (CurrentState.name == "EnemySpotted")
		{
			foreach (Collider collider in sight.inSight)
			{
				if (collider.gameObject.name.Contains("Enemy"))
				{
					float step = 1.0f * Time.deltaTime;
					this.transform.position = Vector3.MoveTowards(this.transform.position, collider.transform.position, step);
				}
			}
		}
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Automaton : MonoBehaviour
{
    public State AgentAutomaton()
    {
        State RandMove = new State("RandMove");
        State EnemySpotted = new State("EnemySpotted");

        RandMove.SetConnection("Enemy spotted", EnemySpotted);
        EnemySpotted.SetConnection("Enemy lost", RandMove);
        
        return RandMove;
    }
}
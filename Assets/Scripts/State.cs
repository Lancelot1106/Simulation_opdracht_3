using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    public string name;
    public Dictionary<string, State> connections = new Dictionary<string,State>();
    
    public State(string name)
    {
        this.name = name;
    }

    public void SetConnection(string action, State state)
    {
        connections.Add(action, state);
    }

    public State GetNextState(string action)
    {
        return connections[action];
    }
}

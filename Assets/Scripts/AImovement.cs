using UnityEngine;
using UnityEngine.AI;

public class AImovement : MonoBehaviour
{
    private NavMeshAgent agent;
    public Vector3 Destination;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = Destination;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

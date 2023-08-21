using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent2FSM : MonoBehaviour
{
    public GameObject player;
    public GameObject target;
    public GameObject agent1;
    public float speed = 5.0f;
    public float agent1DistanceThreshold = 3.0f;

    private enum State
    {
        ChasePlayer,
        ChaseTarget,
        Flee
    }

    private State currentState = State.ChasePlayer;

    private void Update()
    {
        float playerDistance = Vector3.Distance(transform.position, player.transform.position);
        float targetDistance = Vector3.Distance(transform.position, target.transform.position);
        float agent1Distance = Vector3.Distance(transform.position, agent1.transform.position);

        switch (currentState)
        {
            case State.ChasePlayer:
                if (agent1Distance < agent1DistanceThreshold)
                {
                    currentState = State.Flee;
                }
                else if (targetDistance < playerDistance && agent1Distance >= agent1DistanceThreshold)
                {
                    currentState = State.ChaseTarget;
                }
                break;

            case State.ChaseTarget:
                if (agent1Distance < agent1DistanceThreshold)
                {
                    currentState = State.Flee;
                }
                else if (playerDistance < targetDistance && agent1Distance >= agent1DistanceThreshold)
                {
                    currentState = State.ChasePlayer;
                }
                break;

            case State.Flee:
                if (agent1Distance >= agent1DistanceThreshold)
                {
                    if (playerDistance < targetDistance)
                    {
                        currentState = State.ChasePlayer;
                    }
                    else
                    {
                        currentState = State.ChaseTarget;
                    }
                }
                break;
        }

        // Execute the appropriate behavior based on the current state
        switch (currentState)
        {
            case State.ChasePlayer:
                Vector3 chasePlayerDirection = player.transform.position - transform.position;
                transform.Translate(chasePlayerDirection.normalized * speed * Time.deltaTime);
                break;

            case State.ChaseTarget:
                Vector3 chaseTargetDirection = target.transform.position - transform.position;
                transform.Translate(chaseTargetDirection.normalized * speed * Time.deltaTime);
                break;

            case State.Flee:
                Vector3 fleeDirection = transform.position - agent1.transform.position;
                transform.Translate(fleeDirection.normalized * speed * Time.deltaTime);
                break;
        }
    }
}

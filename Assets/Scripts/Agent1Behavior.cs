using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent1Behavior : MonoBehaviour
{
    public GameObject player;
    public GameObject target;
    public float speed = 5.0f;
    public float playerDistanceThreshold = 5.0f;
    public float targetDistanceThreshold = 3.0f;

    private void Update()
    {
        float playerDistance = Vector3.Distance(transform.position, player.transform.position);
        float targetDistance = Vector3.Distance(transform.position, target.transform.position);

        if (targetDistance < targetDistanceThreshold)
        {
            if (playerDistance < playerDistanceThreshold)
            {
                // Flee from player
                Vector3 fleeDirection = transform.position - player.transform.position;
                transform.Translate(fleeDirection.normalized * speed * Time.deltaTime);
            }
            else
            {
                // Chase player
                Vector3 chaseDirection = player.transform.position - transform.position;
                transform.Translate(chaseDirection.normalized * speed * Time.deltaTime);
            }
        }
        else
        {
            // Chase player
            Vector3 chaseDirection = player.transform.position - transform.position;
            transform.Translate(chaseDirection.normalized * speed * Time.deltaTime);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCWander : MonoBehaviour
{

    //CURRENT BUGS:
    //gets stuck on walls, I think because it's picking a point inside and obstacle that it then never reaches(therefore never activating a new spot)
    public enum STATES  {
        STOPPED,
        WALKING
    }
    public NavMeshAgent agent;
    public STATES current_state = STATES.STOPPED;
    public float minStopTime = 0f;
    public float maxStopTime = 5f;
    public float range = 5;


    Vector3 targetPos;

    private void Start()
    {
        Walk();

    }

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(targetPos);
        if(current_state == STATES.WALKING)
        {
            float dist = agent.remainingDistance;
            if (agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance == 0) {
                //Arrived.
                Pause();
            }
        }
    }

    void Walk()
    {
        current_state = STATES.WALKING;

        //----find acceptable position on map...
        bool foundDestination = false;
        targetPos = this.transform.position + Random.insideUnitSphere * range;
        NavMeshHit hit;
      
            foundDestination = NavMesh.SamplePosition(targetPos, out hit, 1.0f, NavMesh.AllAreas);
       if(foundDestination)
        Debug.Log("FOUND POSITION!");
       else
            Debug.Log("tried to find pos and failed...");

        //----------------------------------


        agent.SetDestination(targetPos);
        
    }

    void Pause()
    {
        current_state = STATES.STOPPED;
        float randomTime = Random.Range(minStopTime, maxStopTime);
        Invoke("Walk", randomTime);
    }

   
}

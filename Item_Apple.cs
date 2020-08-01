using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Apple : MonoBehaviour
{

    //when a player enters a trigger for the apple, set 'canpickup' to true in PlayerPickup, set to false when leave trigger
    //BUT ONLY FOR THAT PLAYER

    Transform mySpawnTransform; //used by the scene manager to erase it from occupied spawn positions whem picked up

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSpawnTransform(Transform transform)
    {
        mySpawnTransform = transform;

    }

    public Transform GetSpawnTransform()
    {
        return mySpawnTransform;
    }

  
}

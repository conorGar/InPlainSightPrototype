using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using Mirror;

public class SceneManager : NetworkBehaviour
{
    public static SceneManager Instance;

    public TextMeshProUGUI pickupPrompt;
    public float spawnDelay;
    public int maxItems = 3;
    public GameObject objectToSpawn; //To be replaced with an ObjectPool
    public GameObject NpcPrefab;
    public int numOfNPCs = 30;

    public List<Item_Apple> currentItems = new List<Item_Apple>(); //each new item spawned/picked up is kept track of
    public List<Transform> itemPossibleSpawns = new List<Transform>();

    List<Transform> currentlyOccupiedItemSpawns = new List<Transform>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InvokeRepeating("SpawnItem", spawnDelay, spawnDelay);
    }
    public override void OnStartServer()
    {
        base.OnStartServer();
        SpawnNPCs();
    }

    [Command]
    void CmdSpawnItem()
    {
        if (currentlyOccupiedItemSpawns.Count < maxItems)
        {
            int listIndex = Random.Range(0, itemPossibleSpawns.Count);

            if (!currentlyOccupiedItemSpawns.Contains(itemPossibleSpawns[listIndex]))
            {
                //spawn
                GameObject item = Instantiate(objectToSpawn, itemPossibleSpawns[listIndex].position, Quaternion.identity);
                item.GetComponent<Item_Apple>().SetSpawnTransform(itemPossibleSpawns[listIndex]);
                currentItems.Add(item.GetComponent<Item_Apple>());
                NetworkServer.Spawn(item);

                currentlyOccupiedItemSpawns.Add(itemPossibleSpawns[listIndex]);
             
            }
            else
            {
                while (currentlyOccupiedItemSpawns.Contains(itemPossibleSpawns[listIndex]))
                {
                    //keep searching for an unoccupied spawn point
                    listIndex = Random.Range(0, itemPossibleSpawns.Count);
                }
                
            }
        }
    }


    public void RemoveItem(Item_Apple item)
    {


        if (currentItems.Contains(item))
        { //if the item is STILL in the list when this is called
            foreach (Item_Apple apple in currentItems)
            {
                if (apple == item)
                {
                    currentlyOccupiedItemSpawns.Remove(apple.GetSpawnTransform());
                    currentItems.Remove(apple);
                    Destroy(apple.gameObject);
                    pickupPrompt.gameObject.SetActive(false);
                    break;
                }
            }
        }
    }

    void SpawnNPCs()
    {


        //Temp spawning: (still needs to be tested)
        for (int i = 0; i < numOfNPCs;i++) {
            Vector3 spawnPos = new Vector3(Random.Range(-100, 100), 1f, Random.Range(-100f, 100f));
            GameObject npc = Instantiate(NpcPrefab, spawnPos, Quaternion.identity);
            NetworkServer.Spawn(npc);
        }
        //int npcSpawnedCounter = 0;
        //attempt to spawn npcs at random point on NavMesh, which I'll need to code eventually...
        //NavMeshHit hit;
        //while (npcSpawnedCounter < numOfNPCs)
        //{
        // if (NavMesh.SamplePosition(transform.position, out hit, 50f, NavMesh.AllAreas))
        // {

        //   npcSpawnedCounter++;
        //  }
        // }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerator : MonoBehaviour
{
    public GameObject[] GroundPrefabs;
    private List<GameObject> activeGrounds = new List<GameObject>();
    private float spawnPos = 0;
    private float groundLength = 100;
    [SerializeField] private Transform player;
    private int startGrounds = 6;
    // Start is called before the first frame update
    void Start()
    {
        for (int i=0; i< startGrounds; i++)
        {
            if (i == 0)
                SpawnGround(3);
            SpawnGround(Random.Range(0, GroundPrefabs.Length));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.z-60 > spawnPos - (startGrounds * groundLength))
        {
            SpawnGround(Random.Range(0, GroundPrefabs.Length));
            DeleteGround();
        }
      
    }
    private void SpawnGround(int groundIndex)
    {
        GameObject nextGround = Instantiate(GroundPrefabs[groundIndex], transform.forward * spawnPos, transform.rotation);
        activeGrounds.Add(nextGround);
        spawnPos += groundLength;
    }
    private void DeleteGround()
    {
        Destroy(activeGrounds[0]);
        activeGrounds.RemoveAt(0);
    }
}

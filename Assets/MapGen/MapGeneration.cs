using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{

    GameObject[] Tilemaps;
    public int ChunksSpawned = 5;

    // Start is called before the first frame update
    void Start()
    {
        Tilemaps = GameObject.FindGameObjectsWithTag("GeneratedMap");
        System.Random random = new System.Random();
        for(int i = 0; i < ChunksSpawned; i++)
        {
            int selectedMap = random.Next(0, Tilemaps.Length-1);
            Tilemaps[selectedMap].gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{

    GameObject Grid;
    public int chunksSpawned = 5;
    int selectedMap;
    List<int> PreviousMaps = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        Grid = GameObject.FindGameObjectWithTag("GeneratedMapGrid");
        int mapcount = Grid.transform.childCount;
        if(chunksSpawned > mapcount)
        {
            Debug.Log("Tried to spawn more chunks than there are maps, changed chunks spawned to match mapcount");
            chunksSpawned = mapcount;
        }
        System.Random random = new System.Random();
        for(int i = 0; i < chunksSpawned; i++)
        {
            do
            {
                selectedMap = Random.Range(0, mapcount);
            } while (PreviousMaps.Contains(selectedMap) && PreviousMaps.Count < chunksSpawned);


           /* int selectedMap = random.Next(0, mapcount);
            Debug.Log(selectedMap + " " + i);
            if(PreviousMaps.Contains(selectedMap) && PreviousMaps.Count < 3)
            {
                i--;
                continue;
            } */
            Grid.gameObject.transform.GetChild(selectedMap).gameObject.SetActive(true);
            Grid.gameObject.transform.GetChild(selectedMap).transform.position = new Vector3(20*1.3f*i,0,0);
            PreviousMaps.Add(selectedMap);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

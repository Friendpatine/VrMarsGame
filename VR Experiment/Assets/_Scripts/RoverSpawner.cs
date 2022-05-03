using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoverSpawner : MonoBehaviour
{
    public int numberOfRovers;
    //public Transform SpawnCoordinates;
    public float roverSpawnRadius;
    public GameObject MarsRover;
    public Quaternion spawnRotation = new Quaternion();
    // Start is called before the first frame update
    void Start()
    {
        //spawnRotation.SetEulerAngles(0,45,0);
        //SpawnCoordinates = gameObject.GetComponent<Map>().OriginCoordinates();
        //SpawnRovers(numberOfRovers);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnRovers(Vector3 SpawnCoordinates)
	{
        float tempYoffset = 1;
        float spawnOffset = 0;
        float spawnOffsetDistance = roverSpawnRadius * (Mathf.Sqrt(2) / 2);
		for (int i = 0; i < numberOfRovers; i++)
		{
            Vector3 SpawnCoordinatesTemp = new Vector3(SpawnCoordinates.x + -spawnOffset, SpawnCoordinates.y+ tempYoffset, SpawnCoordinates.z + -spawnOffset);
            /*
            RaycastHit hit;
            Ray downRay = new Ray(SpawnCoordinatesTemp, -Vector3.up);

            if (Physics.Raycast(downRay, out hit))
            {
                SpawnCoordinatesTemp.y = (tempYoffset - hit.distance) + 1;
                    
            }*/
            
            Instantiate(MarsRover, SpawnCoordinatesTemp, spawnRotation) ;
            spawnOffset += spawnOffsetDistance;
		}
	}
}

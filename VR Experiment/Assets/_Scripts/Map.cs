using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public Vector2 worldMinBounds;
    public Vector2 woldMaxBounds;
    private Vector2 worldSize;

    public Vector2 gridDimentions;

    public GameObject GridPointMarkerObject;
    public GameObject[,] GridPointMarkers;
    //public List<GameObject> GridPointMarkers = new List<GameObject>();

    //public int numberOfRovers = 1;


    // Start is called before the first frame update
    void Start()
    {
        SpawnGridPointMarkers();
        gameObject.GetComponent<RoverSpawner>().SpawnRovers(OriginCoordinates().position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnGridPointMarkers()
	{
        GridPointMarkers = new GameObject[(int)gridDimentions.x, (int)gridDimentions.y]; 
        worldSize = woldMaxBounds - worldMinBounds;
        Vector2 GridCellSize = new Vector2(worldSize.x / gridDimentions.x, worldSize.y / gridDimentions.y);

        GameObject GridPointMarkerTemp;
        float gridPointMarkerHeight = 100;

        for (int i = 0; i < gridDimentions.x; i++)
        {
            for (int j = 0; j < gridDimentions.y; j++)
            {
                Vector2 newCoordinatePos = new Vector2((GridCellSize.x * i) + (0.5f * GridCellSize.x) + worldMinBounds.x, (GridCellSize.y * j) + (0.5f * GridCellSize.y) + worldMinBounds.y);
                Debug.Log(newCoordinatePos);
                Vector3 gridPointMarkerSpawn = new Vector3(newCoordinatePos.x, gridPointMarkerHeight, newCoordinatePos.y);

                RaycastHit hit;
                Ray downRay = new Ray(gridPointMarkerSpawn, -Vector3.up);

                if (Physics.Raycast(downRay,out hit))
                {
                    gridPointMarkerSpawn.y = gridPointMarkerHeight - hit.distance;
                }

                GridPointMarkerTemp = Instantiate(GridPointMarkerObject, gridPointMarkerSpawn, new Quaternion());
                GridPointMarkerTemp.GetComponent<GridMarker>().gridCoordinates = new Vector2(i, j);
                GridPointMarkerTemp.SetActive(true);
                GridPointMarkers[i, j] = GridPointMarkerTemp;
                //GridPointMarkers.Add(GridPointMarkerTemp);
            }
        }
    }

    public Transform OriginCoordinates()
	{
        return GridPointMarkers[0,0].transform;
	}
}

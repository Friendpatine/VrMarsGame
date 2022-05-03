using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoverInstructionsController : MonoBehaviour
{
    public List<GameObject> MarsRovers = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(MarsRovers.Count == 0)
		{
            Debug.Log("Searching for Mars Rovers");
            FindMarsRovers();
        }
		else
		{

		}
    }

    void FindMarsRovers()
	{
        GameObject[] rovers = GameObject.FindGameObjectsWithTag("Rover");

        foreach(GameObject rover in rovers)
		{
            MarsRovers.Add(rover);
		}
	}
}

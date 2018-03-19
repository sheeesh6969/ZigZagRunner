using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour {

    public GameObject roadPrefab;
    public float offset = 0.707f;
    public Vector3 lastPos;

    private int roadCount = 0;

    public void StartBuilding()
    {
        InvokeRepeating("CreateNewRoadPart", .1f, .3f);
    }

    public void CreateNewRoadPart()
    {
        Vector3 spawnPos = Vector3.zero;

        float chance = Random.Range(0, 100);
        if (chance < 50)
            spawnPos = new Vector3(lastPos.x + offset, lastPos.y, lastPos.z + offset);
        else
            spawnPos = new Vector3(lastPos.x - offset, lastPos.y, lastPos.z + offset);

        GameObject g = Instantiate(roadPrefab, spawnPos, Quaternion.Euler(0, 45, 0));

        lastPos = g.transform.position;

        //Enable the Crystal for every 5th road part
        roadCount++;

		if (roadCount % 4 == 0) {

			//cast crystal from road child
			Crystal crystal = g.transform.GetChild (0).gameObject.GetComponent<Crystal> ();

			//set different Crystal types **
			//normal   crystal: 75%       **
			//precious crystal: 14%		  **
			//toxic    crystal: 11%       **
			if (chance < 75) 
			{
				crystal.SetType (Crystal.Type.Normal);
			} 
			else if (chance >= 75 && chance < 90) 
			{
				crystal.SetType (Crystal.Type.Precious);
			}
			else if (chance >= 90) 
			{
				crystal.SetType (Crystal.Type.Toxic);
			}

			crystal.gameObject.SetActive (true);
		}
    }

}

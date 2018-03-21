using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour {

    public GameObject roadPrefab;
    public float offset = 0.707f;
    public Vector3 lastPos;
	RandomSingleObjectGenerator rndSinObjGen;
    private int roadCount = 0;

	private void Awake()
	{
		rndSinObjGen = GetComponent<RandomSingleObjectGenerator>();
	}

    public void StartBuilding()
    {
        InvokeRepeating("CreateNewRoadPart", .1f, .3f);
    }

    public void CreateNewRoadPart()
    {
        Vector3 spawnPos = Vector3.zero;

        int chance = Random.Range(0, 100);
        if (chance < 50)
            spawnPos = new Vector3(lastPos.x + offset, lastPos.y, lastPos.z + offset);
        else
            spawnPos = new Vector3(lastPos.x - offset, lastPos.y, lastPos.z + offset);

        GameObject g = Instantiate(roadPrefab, spawnPos, Quaternion.Euler(0, 45, 0));

        lastPos = g.transform.position;

        //Enable the Crystal for every 5th road part
        roadCount++;

		//spawn random crystal
		if (roadCount % 4 == 0) {

			GameObject a = rndSinObjGen.getRandomObject ();
			Vector3 v3 = new Vector3(spawnPos.x, spawnPos.y+(g.transform.localScale.y/2), spawnPos.z);
			//g.setTopObject(
			Instantiate(a, v3, Quaternion.Euler(0, 135, 0), g.transform);
			//)
		}
    }
}

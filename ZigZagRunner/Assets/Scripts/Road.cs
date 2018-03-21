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



//		GameObject a = rndSinObjGen.getRandomObject ();
//		Vector3 v3 = new Vector3(spawnPos.x, spawnPos.y+(g.transform.localScale.y/2), spawnPos.z);
//		GameObject o = Instantiate(a, v3, Quaternion.Euler(0, 45, 0));
//		o.SetActive (false);


		if (roadCount % 4 == 0) {

			GameObject a = rndSinObjGen.getRandomObject ();
			Vector3 v3 = new Vector3(spawnPos.x, spawnPos.y+(g.transform.localScale.y/2), spawnPos.z);
			GameObject o = Instantiate(a, v3, Quaternion.Euler(0, 45, 0));
			o.transform.SetParent (g.transform);

//			//cast crystal from road child
//			CrystalOld crystal = g.transform.GetChild (0).gameObject.GetComponent<CrystalOld> ();
//
//			//set rendomly a specific crystal type via the parameter - rndNumber
//			setRandomCrystal (crystal, chance);
//
//			//make the crystal visible
//			crystal.gameObject.SetActive (true);
		}
    }

	//rndNumber have to be between 0 and 100
	private void setRandomCrystal(CrystalOld crystal, int rndNumber)
	{
		if (rndNumber < 75) 
		{
			crystal.SetType (CrystalOld.Type.Normal);		// 75%
		} 
		else if (rndNumber >= 75 && rndNumber < 90) 
		{
			crystal.SetType (CrystalOld.Type.Precious);	// 14%
		}
		else if (rndNumber >= 90) 
		{
			crystal.SetType (CrystalOld.Type.Toxic);		// 11%
		}
	}

}

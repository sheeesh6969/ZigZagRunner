using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSingleObjectGenerator : MonoBehaviour {

	//Data Type
	[System.Serializable]
	public struct ObjectGenerationEntry
	{
		//[Header("SpawnInfo")]
		public GameObject SpawnObject;
		[Range(0,100)]
		public int chance;
	}

	[Header("ReadOnly")]
	[SerializeField]
	private string total;

	void OnValidate()
	{
		int maxValue = 0;

		for (int i = 0; i < objectGenerationEntrys.Length; i++) 
		{
			maxValue += objectGenerationEntrys [i].chance;
		}

		if (maxValue > 100) {
			total = "you are over 100  <" +maxValue.ToString() + ">" ;
			return;
		}

		total = maxValue.ToString();
		total += "";

	}
		

	//needed for spawning the Objects with their specific chance *setup in Inspector*
	[Header("editThis")]
	[Tooltip("Das ist der Tooltip")]
	[SerializeField]
	private ObjectGenerationEntry[] objectGenerationEntrys; 


	public GameObject getRandomObject()
	{
		return CalculateObjectFromList(Random.Range(0, GetMaxChance()));
	}
		
	private int GetMaxChance()
	{
		int maxValue = 0;
		for (int i = 0; i < objectGenerationEntrys.Length; i++) {
			maxValue += objectGenerationEntrys [i].chance;
		}

		return maxValue;
	}

	private GameObject CalculateObjectFromList(int rndNumber)
	{
		int lastValue = 0;
		for (int i = 0; i < objectGenerationEntrys.Length; i++) 
		{
			if (rndNumber < (objectGenerationEntrys [i].chance + lastValue) && rndNumber >= lastValue) {
				//Debug.Log ( "lastValue:"+lastValue+"  chance:" + chance +"  return:" + i);
				return objectGenerationEntrys [i].SpawnObject;
			} else {
				lastValue += objectGenerationEntrys [i].chance;
			}
		}

		Debug.Log ("FATAL ----ACHTUNG----");
		Debug.Log ( "lastValue:"+lastValue+"  chance:" + rndNumber );
		return null;
	}
}
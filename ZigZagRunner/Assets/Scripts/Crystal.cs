using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour {

	[SerializeField]
	private int value;
	public GameObject destroyEffect;

	public int GetValue()
	{
		return value;
	}

	public void DestroyWithEffect()
	{
		if (destroyEffect == null) {
			Destroy (this.gameObject);
			return;
		}

		//Spawn Crystal effect
		GameObject tmp_effect = Instantiate(destroyEffect, this.transform.position, Quaternion.identity);
		Destroy(tmp_effect, 2.0f);
		Destroy (this.gameObject);
	}
}

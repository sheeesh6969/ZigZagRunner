using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDown : MonoBehaviour {

	public float speed = 0.3f;
	public float range = 10.0f;
	public float delay = 1.0f;

	private bool falling = false;
	private bool falldownStarted = false;
	private Vector3 endPosition = new Vector3 ();
	
	// Update is called once per frame
	void Update () {
		if (falling) {
			transform.position = Vector3.Lerp (transform.position, endPosition, speed * Time.deltaTime);
		}
	}

	public bool isFalling()
	{
		return falldownStarted;
	}

	public void startFallDown()
	{
		falldownStarted = true;
		Invoke ("startFallDown_p", delay);
	}

	private void startFallDown_p()
	{
		if (!falling) {
			endPosition = transform.position;
			endPosition.y -= range;
			falling = true;
		}
	}
}

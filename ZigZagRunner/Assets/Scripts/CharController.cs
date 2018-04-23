using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour {

    public Transform rayStart;
    public GameObject levelUpEffect;
    public GameObject destroyEffect;
    public int runSpeed;
    private Rigidbody rb;
    private bool walkingRight = true;
    private Animator anim;
    private GameManager gameManager;
	private LevelUpSystem levelUpSystem;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
		levelUpSystem = GetComponent<LevelUpSystem>();
        gameManager = FindObjectOfType<GameManager>();
		gameManager.setCharacterLevel (levelUpSystem.getCurrentLevel());

		//set experience
		levelUpSystem.addEp(gameManager.GetStoredExperience()); 
		//set level
		gameManager.setCharacterLevel(levelUpSystem.getCurrentLevel());
    }

    void FixedUpdate () {
        if (!gameManager.gameStarted)
            return;
        else
            anim.SetTrigger("gameStarted");

        rb.transform.position = transform.position + transform.forward * runSpeed * Time.deltaTime;
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Switch();
        }

        else if (Input.touchCount > 0 && gameManager.gameStarted)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Switch();
            }
        }

        //Check if character is grounded
        RaycastHit hit;
        if (!Physics.Raycast(rayStart.position, -transform.up, out hit, Mathf.Infinity))
        {
            if (isFalling() == true)
            {
                anim.SetTrigger("isFalling");
            }
        }

        if (transform.position.y < -2)
            gameManager.EndGame();
    }

    private void Switch()
    {
        if(!gameManager.gameStarted)
            return;

        walkingRight = !walkingRight;

        if (walkingRight)
            transform.rotation = Quaternion.Euler(0, 45, 0);
        else
            transform.rotation = Quaternion.Euler(0, -45, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Crystal")
        {
			Crystal crystal = other.transform.GetComponent<Crystal> ();

			//increase Score
			gameManager.IncreaseScore(crystal.GetValue());

			//simple levelUpSystem: add ep
			bool islevelUp = levelUpSystem.addEp(crystal.GetValue());

			//if level up
			if (islevelUp) {
				execLevelUpEffekt ();
				//update ui
				gameManager.setCharacterLevel(levelUpSystem.getCurrentLevel());
			}

			//store ep(level) on disk
			int currentEp = levelUpSystem.getCurrentEp();
			gameManager.SetStoredExperience (currentEp);

			//debuff
			if (crystal.GetValue () < 0) {
				runSpeed = 4;
				//after 2sec, the running speed is reset
				Invoke ("resetRunSpeed", 2.0f);
			}

			//zerstört Kristall
			crystal.DestroyWithEffect();
        }
    }
    private GameObject lastRoadPathCollision = null;

    private void OnCollisionStay(Collision other)
    {
		//if the block has spotted in the past - return - 
		if (other.transform.gameObject == lastRoadPathCollision)
			return;

        if (gameManager.gameStarted && !isFalling())
        {
            GameObject g = Instantiate(destroyEffect, other.transform.position, Quaternion.identity);

			//for performance, the FallDown component and its update method was disabled.
			//now we enable it. //maybe, a internal controller is a mutch better way for this. fuck off. :D
			other.gameObject.GetComponent<FallDown>().enabled = true;

			//drops the block after its internal delay.
			other.gameObject.GetComponent<FallDown>().startFallDown();

			//destroy the block
            Destroy(other.transform.gameObject, 2.5f);

			//destroy the particle effect
            Destroy(g, 2.0f);

            lastRoadPathCollision = other.transform.gameObject;
        }
    }

    private bool isFalling()
    {
        return transform.position.y < 0.40f;
    }

	private void execLevelUpEffekt ()
	{
		Vector3 v3 = transform.position;
		v3.y += 1.0f;
		GameObject lvlupeffect = Instantiate(levelUpEffect, v3, Quaternion.identity);
		Destroy(lvlupeffect, 2.0f);
	}

	private void execParticleffect (GameObject effect, Vector3 pos, float destroyInSec)
	{
		if (effect == null) return;

		//Spawn Crystal effect
		GameObject tmp_effect = Instantiate(effect, pos, Quaternion.identity);
		Destroy(tmp_effect, destroyInSec);
	}

	private void resetRunSpeed()
	{
		runSpeed = 3;
	}

}

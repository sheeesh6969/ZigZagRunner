﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour {

    public Transform rayStart;
    public GameObject crystalEffect;
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
				//play effect
				execLevelUpEffekt ();
				//update ui
				gameManager.setCharacterLevel(levelUpSystem.getCurrentLevel());
			
			}

            //Spawn Crystal effect
            GameObject g = Instantiate(crystalEffect, other.transform.position, Quaternion.identity);
            Destroy(g, 2);

            Destroy(other.gameObject);
        }
    }
    private GameObject lastRoadPathCollision = null;

    private void OnCollisionStay(Collision other)
    {
        if (gameManager.gameStarted && other.transform.gameObject != lastRoadPathCollision && isFalling() == false)
        {
            GameObject g = Instantiate(destroyEffect, other.transform.position, Quaternion.identity);
            Destroy(other.transform.gameObject, 0.5f);
            Destroy(g, 0.5f);

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

}

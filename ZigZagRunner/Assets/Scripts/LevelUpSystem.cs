using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpSystem : MonoBehaviour {

	//the start Level
	public int startLevel = 1; 
	//the maximal level to reach
	public int maxLevel = 10;
	//the maximum experience to reach the maximum level 
	public int maxEp = 1000;

	//the currently available points of axperience
	private int currentEp = 0;
	//the current level
	private int currentLevel = 0;

	void Start () {
		this.currentLevel = startLevel;
	}

	public bool addEp(int ep)
	{
		// if the experience(ep) negative dont add ep
//		if (ep < 0)  
//			return false;
		 
		//if max level is reached dont add ep
		if(currentLevel == maxLevel) 
			return false;

		//if the experience(ep) going out of scope. maxEp is the limit
		if( (ep + this.currentEp) > this.maxEp) 
		{
			//set current experience(ep) to the maximum
			this.currentEp = this.maxEp;
			return tryToLevelUp();
		}
			
		this.currentEp += ep;
		return tryToLevelUp();
	}

	public int getCurrentEp()
	{
		return currentEp;
	}

	public int getCurrentLevel()
	{
		return currentLevel;
	}

	private bool tryToLevelUp()
	{
		if (currentLevel == maxLevel)
			return false;

		int levelUpStep = this.maxEp / this.maxLevel;
		int tempLevel = this.currentEp/levelUpStep;

		if (tempLevel > this.currentLevel) {
			currentLevel = tempLevel;
			return true;
		}

		return false;
	}
}

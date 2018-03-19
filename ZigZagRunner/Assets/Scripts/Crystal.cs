using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour {

	public enum Type {Normal, Precious, Toxic};

	private int mValue = 4;
	private Type mType;
	private Renderer mRend;

	public void SetType(Type type)
	{
		if (this.mType == type)
			return;

		Renderer rend = GetComponent<Renderer>();

		switch (type) 
		{
		//normal Crystal
		case Type.Normal:
			rend.material.shader = Shader.Find ("Mobile/Diffuse");
			mValue = 4;
			break;
		//big value Crystal
		case Type.Precious:
			rend.material.shader = Shader.Find("Specular");
			rend.material.SetColor ("_SpecColor",Color.yellow);
			Vector3 v3 = new Vector3 (transform.localScale.x * 1.5f, transform.localScale.y * 1.5f, transform.localScale.z * 1.5f);
			transform.localScale = v3;
			mValue = 20;
			break;
		//negative value Crystal
		case Type.Toxic:
			rend.material.shader = Shader.Find("Specular");
			rend.material.SetColor ("_SpecColor", Color.grey);
			Vector3 v31 = new Vector3 (transform.localScale.x / 2, transform.localScale.y / 2, transform.localScale.z / 2);
			transform.localScale = v31;
			mValue = -10;
			break;
		default:
			Debug.Log ("Incorrect Type");
			return;
		}

		mType = type;
	}

	public Type GetCrystalType()
	{
		return mType;
	}

	public int GetValue()
	{
		//return Crystal value
		return mValue;
	}
}

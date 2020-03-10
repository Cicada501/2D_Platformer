using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLayereffect : MonoBehaviour {

	private float lengthX, startposX;
	private float lengthY, startposY;
	public GameObject cam;
	float camPosY;
	public float parallaxEffectX;
	public float parallaxEffectY;
	// Use this for initialization
	void Start () {
		camPosY = cam.transform.position.y;
		startposX = transform.position.x;
		startposY = transform.position.y;
		lengthX = GetComponent<SpriteRenderer>().bounds.size.x;
		lengthY = GetComponent<SpriteRenderer>().bounds.size.y;
	}
	
	// Update is called once per frame
	void Update () {
		//float tempX = (cam.transform.position.x * (1 - parallaxEffectX));
		//float tempY = (cam.transform.position.y * (1 - parallaxEffectY));
		float distX = (cam.transform.position.x * parallaxEffectX);
		float distY = ((cam.transform.position.y-camPosY) * parallaxEffectY);

		transform.position = new Vector3(startposX + distX,startposY+ distY, transform.position.z);
	
		/* if(temp > startpos + length) startpos +=length;
		else if(temp < startpos - length) startpos -=length; */
		
	
	}
}

//https://www.youtube.com/watch?v=zit45k6CUMk
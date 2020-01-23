using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgrShake : MonoBehaviour {

	Animator backgroundAnimator;

	bool playerStoppedFalling = false;
	bool landing = false;
	bool landingOnPlattform1 = false;

	public float startLandingDuration =0.3f;
	private float landingDuration;

	// Use this for initialization
	void Start () {
		 backgroundAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
		backgroundAnimator.SetBool("Landing", landing);
		backgroundAnimator.SetBool("landingOnPlatform1", landingOnPlattform1);




		//if player lands shake object
        if (Player.isFalling & !playerStoppedFalling)
        {
            playerStoppedFalling = false;
			
        }
		else if(Player.isFalling){
			playerStoppedFalling = false;
		}
		//The Moment when Player.isFalling goes false, sets playerStoppedFalling true
        else if (!Player.isFalling && !playerStoppedFalling &&Player.isGrunded)
        {
				playerStoppedFalling = true;
				landing = true;
				landingDuration = startLandingDuration;
		}
		else if (!Player.isFalling && !playerStoppedFalling && Player.isOnPlatform1)
        {
				playerStoppedFalling = true;
				landingOnPlattform1 = true;
				landingDuration = startLandingDuration;
		}
        




		//Keep landing true for a Moment
		//If landing is true, wait until landingduration is 0, than landing is false.
		if((landing||landingOnPlattform1) && landingDuration>0){
			landingDuration -= Time.deltaTime;
		}
		else{
			landing = false;
			landingOnPlattform1 = false;
		}
		
	}
}

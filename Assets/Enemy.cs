using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public Animator animator;

	public int maxHealth = 100;
	int currentHealth;

	void Start () {
		currentHealth = maxHealth;		
	}

	public void TakeDamage(int damge){
		currentHealth -= damge;

		animator.SetTrigger("Hurt");

	}
	

}

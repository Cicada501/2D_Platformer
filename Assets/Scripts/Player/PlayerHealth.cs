using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

	public Slider healthBarSlider;
    public int maxHealth = 100;
    int currentHealth;

    public float flashTime;
    Color origionalColor;
    public SpriteRenderer renderer;
	Transform player;

    // Use this for initialization
    void Start()
    {
		player = GameObject.FindGameObjectWithTag("Player").transform;

        currentHealth = maxHealth;
		
        origionalColor = renderer.color;
    }

    // Update is called once per frame
    void Update()
    {
		healthBarSlider.maxValue = maxHealth;
		healthBarSlider.value = currentHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        //Let player blink red
		FlashRed();

        if (currentHealth <= 50)
        {
			player.GetComponent<Player>().GiveSuperJumps();
            //Die animation 
            //UI asking to restart 
        }
    }

    void FlashRed()
    {
        renderer.color = Color.red;
        Invoke("ResetColor", flashTime);
    }
    void ResetColor()
    {
        renderer.color = origionalColor;
    }


}

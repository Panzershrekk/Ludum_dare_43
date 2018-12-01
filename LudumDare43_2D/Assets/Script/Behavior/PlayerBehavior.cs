using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerBehavior : MonoBehaviour
{
    public PlayerStats stats;

    private Slider hitpointSlider;
    private Slider waterSlider;

    // Use this for initialization
    void Start () {
        hitpointSlider = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<Slider>();
        waterSlider = GameObject.FindGameObjectWithTag("WaterBar").GetComponent<Slider>();
        
        hitpointSlider.maxValue = stats.maxHitpoint;   
        hitpointSlider.value = stats.hitpoint;

        waterSlider.maxValue = stats.maxWater;
        waterSlider.value = stats.water;
    }
	
	// Update is called once per frame
	void Update () {
	    if (stats.isInvulnerable == true)
	    {
	        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.2f);
	        if (Time.time > stats.recoveryTime)
	        {
	            stats.isInvulnerable = false;
	            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1.0f);
	        }
	    }
    }
    public void UpdateHealth()
    {
        if (stats.hitpoint > stats.maxHitpoint)
            stats.hitpoint = stats.maxHitpoint;
        if (stats.water > stats.maxWater)
            stats.water = stats.maxWater;
        hitpointSlider.value = stats.hitpoint;
    }

    public void UpdateWater()
    {
        if (stats.hitpoint > stats.maxHitpoint)
            stats.hitpoint = stats.maxHitpoint;
        if (stats.water > stats.maxWater)
            stats.water = stats.maxWater;
        waterSlider.value = stats.water;
    }

    public void DecreaseWater()
    {
        if (stats.water <= 0)
        {
            stats.hitpoint -= (int)stats.waterDecreaseValue;
            UpdateHealth();
        }
        else
        {
            stats.water -= (int)stats.waterDecreaseValue;
            UpdateWater();
        }
    }

    public void TakeDamage(int damage)
    {
        if (stats.isDead == false)
        {
            if (stats.isInvulnerable == false)
            {
                stats.hitpoint -= damage;
                stats.recoveryTime = Time.time + stats.universalRecoveryTime;
                stats.isInvulnerable = true;
                UpdateHealth();
            }

            if (stats.hitpoint <= 0)
            {
                Die();
            }
        }
    }

    public void Die()
    {
        Debug.Log("Drop dead");
    }
}

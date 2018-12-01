using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerBehavior : MonoBehaviour
{
    public PlayerStats stats;

    private Slider hitpointSlider;
    private Slider waterSlider;
    public Inventory inventory;

    // Use this for initialization
    void Start () {
        hitpointSlider = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<Slider>();
        waterSlider = GameObject.FindGameObjectWithTag("WaterBar").GetComponent<Slider>();
        inventory = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Inventory>();

        hitpointSlider.maxValue = stats.maxHitpoint;   
        hitpointSlider.value = stats.hitpoint;

        waterSlider.maxValue = stats.maxWater;
        waterSlider.value = stats.water;

        stats.nextWaterDecreaseAllowed = Time.time + stats.waterDecreaseTick;
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

        if (Input.GetKeyDown(KeyCode.LeftControl) == true)
        {
            if (Input.GetKeyDown(KeyCode.Z) == true)
            {
                Dropitem(inventory.items[0]);
            }
        }
        DecreaseWater();
        CheckStatus();
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
        if (Time.time > stats.nextWaterDecreaseAllowed)
        {
            stats.nextWaterDecreaseAllowed = Time.time + stats.waterDecreaseTick;

            if (stats.water <= 0)
            {
                stats.hitpoint -= (int) stats.waterDecreaseValue;
                UpdateHealth();
            }
            else
            {
                stats.water -= (int) stats.waterDecreaseValue;
                UpdateWater();
            }
        }
    }

    public void CheckStatus()
    {
        if (stats.hitpoint <= 0)
        {
            Die();
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
        }
    }

    public void Die()
    {
        Debug.Log("Drop dead");
    }

    public void PickUpItem(Item item)
    {
        inventory.AddItem(item);
        ApplyItemEffect(item);
    }

    public void Dropitem(Item item)
    {
        Vector3 pos = transform.position + new Vector3(0, 1, 0);
        Quaternion rotation = transform.rotation;
        Instantiate(item.prefab, pos, rotation);

        inventory.RemoveItem(item);
        RemoveItemEffect(item);
    }

    public void ApplyItemEffect(Item item)
    {

        if (item != null)
        {
            stats.moveSpeed += item.speedModifier;
            stats.attackSpeed += item.attackSpeedModifier;
            stats.damage += item.damageModifier;
            stats.waterDecreaseTick += item.waterTickModifier;
        }
    }

    public void RemoveItemEffect(Item item)
    {

        if (item != null)
        {
            stats.moveSpeed -= item.speedModifier;
            stats.attackSpeed -= item.attackSpeedModifier;
            stats.damage -= item.damageModifier;
            stats.waterDecreaseTick -= item.waterTickModifier;
        }
    }
}

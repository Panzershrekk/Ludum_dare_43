using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerBehavior : MonoBehaviour
{
    public PlayerStats stats;

    private Vector2 strikeEnd;

    private Slider hitpointSlider;
    private Slider waterSlider;
    private Text poisonText;
    private Image poisonColor;
    public Inventory inventory;

    // Use this for initialization
    void Start () {
        hitpointSlider = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<Slider>();
        waterSlider = GameObject.FindGameObjectWithTag("WaterBar").GetComponent<Slider>();
        inventory = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Inventory>();
        poisonText = GameObject.FindGameObjectWithTag("Poisoned").GetComponent<Text>();
        poisonColor = GameObject.FindGameObjectWithTag("ColorBar").GetComponent<Image>();

        hitpointSlider.maxValue = stats.maxHitpoint;   
        hitpointSlider.value = stats.hitpoint;

        waterSlider.maxValue = stats.maxWater;
        waterSlider.value = stats.water;

        stats.nextWaterDecreaseAllowed = Time.time + stats.waterDecreaseTick;
        strikeEnd = new Vector2();
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

        if (Input.GetKeyDown(KeyCode.Alpha1) == true)
        {
            if (inventory.items[0] != null && inventory.items[0].consomable == true)
                UseItem(inventory.items[0]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) == true)
        {
            if (inventory.items[1] != null && inventory.items[1].consomable == true)
                UseItem(inventory.items[1]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) == true)
        {
            if (inventory.items[2] != null && inventory.items[2].consomable == true)
                UseItem(inventory.items[2]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) == true)
        {
            if (inventory.items[3] != null && inventory.items[3].consomable == true)
                UseItem(inventory.items[3]);
        }

        if (Input.GetKey(KeyCode.RightArrow) == true)
        {
            strikeEnd = transform.position + new Vector3(1, 0);
        }
        if (Input.GetKey(KeyCode.LeftArrow) == true)
        {
            strikeEnd = transform.position + new Vector3(-1, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow) == true)
        {
            strikeEnd = transform.position + new Vector3(0, -1);

        }
        if (Input.GetKey(KeyCode.UpArrow) == true)
        {
            strikeEnd = transform.position + new Vector3(0, 1);

        }

        Debug.DrawLine(transform.position, strikeEnd, Color.green);


        if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            Attack();
        }
        DecreaseWater();
        CheckStatus();
    }

    public void Attack()
    {
        RaycastHit2D hit = Physics2D.Linecast(transform.position, strikeEnd, 1 << LayerMask.NameToLayer("Enemy"));
        if (hit.collider != null)
        {
            CreatureBehavior enemyStat = hit.collider.GetComponent<CreatureBehavior>();
            enemyStat.stats.hitpoint -= 2;
            Debug.Log(enemyStat.stats.hitpoint);
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

    public void SufferPoison()
    {
        if (stats.isPoisonned == true)
        {
            poisonColor.GetComponent<Image>().color = new Color32(103, 0, 104, 255);
            poisonText.GetComponent<Text>().color = new Color32(103, 0, 104, 255);

            if (Time.time > stats.nextPoisonAllowed)
            {
                stats.nextPoisonAllowed = Time.time + stats.poisonTick;
                stats.hitpoint -= (int) stats.poisonValue;
                UpdateHealth();
            }
        }
        else
        {
            poisonColor.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
            poisonText.GetComponent<Text>().color = new Color32(103, 0, 104, 0);
        }
    }

    public void CheckStatus()
    {
        SufferPoison();
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

    public void UseItem(Item item)
    {
        if (item != null)
        {
            inventory.RemoveItem(item);
            ApplyItemEffect(item);
        }
    }

    public void PickUpItem(Item item)
    {
        if (item != null)
        {
            inventory.AddItem(item);
            if (item.consomable == false)
                ApplyItemEffect(item);
        }
    }

    public void RemoveItem(Item item)
    {
        if (item != null)
        {
            inventory.RemoveItem(item);
            RemoveItemEffect(item);
        }
    }

    public void Dropitem(Item item)
    {
        if (item != null)
        {
            Vector3 pos = transform.position + new Vector3(0, 1, 0);
            Quaternion rotation = transform.rotation;
            Instantiate(item.prefab, pos, rotation);

            inventory.RemoveItem(item);
            RemoveItemEffect(item);
        }
    }

    public bool TrySacrificeItem(int itemIdx)
    {
        Item i = inventory.items[itemIdx];
        if(i != null)
        {
            inventory.RemoveItem(i);
            RemoveItemEffect(i);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ApplyItemEffect(Item item)
    {

        if (item != null)
        {
            stats.moveSpeed += item.speedModifier;
            stats.attackSpeed += item.attackSpeedModifier;
            stats.damage += item.damageModifier;
            stats.waterDecreaseTick += item.waterTickModifier;

            stats.damage += (int)item.healthModifier;
            if (stats.isPoisonned == true && item.cleanse == true)
                stats.isPoisonned = false;
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
        UpdateHealth();
        UpdateWater();
    }
}

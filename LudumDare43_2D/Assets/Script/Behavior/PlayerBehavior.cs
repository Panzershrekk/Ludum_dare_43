using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerBehavior : MonoBehaviour
{
    public AudioSource move;
    public AudioSource use;
    public AudioSource take;

    public PlayerStats stats;

    public Vector2 strikeEnd;

    private Slider hitpointSlider;
    private Slider waterSlider;
    private Text poisonText;
    private Image poisonColor;
    private float nextAttack;
    public Inventory inventory;
    public Animator anim;
    public GameObject proj;

    private Vector2 rayAttack;
    private bool isAlive;

    // Use this for initialization
    void Start()
    {
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
        rayAttack = new Vector2();

        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive == true)
        {
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
                else
                {
                    Dropitem(inventory.items[0]);
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) == true)
            {
                if (inventory.items[1] != null && inventory.items[1].consomable == true)
                    UseItem(inventory.items[1]);
                else
                {
                    Dropitem(inventory.items[1]);
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha3) == true)
            {
                if (inventory.items[2] != null && inventory.items[2].consomable == true)
                    UseItem(inventory.items[2]);
                else
                {
                    Dropitem(inventory.items[2]);
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha4) == true)
            {
                if (inventory.items[3] != null && inventory.items[3].consomable == true)
                    UseItem(inventory.items[3]);
                else
                {
                    Dropitem(inventory.items[3]);
                }
            }

            if (Input.GetKey(KeyCode.RightArrow) == true)
            {
                rayAttack = new Vector3(1, 0);
                strikeEnd = transform.position + new Vector3(1, 0);
                anim.SetBool("walkRight", true);
                anim.SetBool("idle", false);
                anim.SetBool("walkBack", false);
                anim.SetBool("walkDown", false);
                anim.SetBool("walkLeft", false);

            }
            if (Input.GetKey(KeyCode.LeftArrow) == true)
            {
                rayAttack = new Vector3(-1, 0);
                strikeEnd = transform.position + new Vector3(-1, 0);
                anim.SetBool("walkLeft", true);
                anim.SetBool("idle", false);
                anim.SetBool("walkBack", false);
                anim.SetBool("walkDown", false);
                anim.SetBool("walkRight", false);
            }
            if (Input.GetKey(KeyCode.DownArrow) == true)
            {
                rayAttack = new Vector3(0, -1);
                strikeEnd = transform.position + new Vector3(0, -1);
                anim.SetBool("walkDown", true);
                anim.SetBool("idle", false);
                anim.SetBool("walkBack", false);
                anim.SetBool("walkLeft", false);
                anim.SetBool("walkRight", false);
            }
            if (Input.GetKey(KeyCode.UpArrow) == true)
            {
                rayAttack = new Vector3(0, 1);
                strikeEnd = transform.position + new Vector3(0, 1);
                anim.SetBool("walkBack", true);
                anim.SetBool("idle", false);
                anim.SetBool("walkDown", false);
                anim.SetBool("walkLeft", false);
                anim.SetBool("walkRight", false);
            }
            if (!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.RightArrow))
            {
                anim.SetBool("idle", true);
                anim.SetBool("walkBack", false);
                anim.SetBool("walkDown", false);
                anim.SetBool("walkLeft", false);
                anim.SetBool("walkRight", false);
            }

            Debug.DrawLine(transform.position, strikeEnd, Color.green);


            if (Input.GetKeyDown(KeyCode.Space) == true)
            {
                if (rayAttack.x == 0 && rayAttack.y == -1)
                {
                    anim.SetTrigger("attackDown");
                }
                if (rayAttack.x == 0 && rayAttack.y == 1)
                {
                    anim.SetTrigger("attackBack");
                }
                if (rayAttack.x == 1 && rayAttack.y == 0)
                {
                    anim.SetTrigger("attackRight");
                }
                if (rayAttack.x == -1 && rayAttack.y == 0)
                {
                    anim.SetTrigger("attackLeft");
                }
                Attack();
            }
            DecreaseWater();
            
        }

       /* if (CheckCreature(transform.position, 4.0f) == true)
        {
            MusicManager m = GameObject.FindGameObjectWithTag("Music").GetComponent<MusicManager>();
            m.LaunchDesertFight();
        }
        else
        {
            MusicManager m = GameObject.FindGameObjectWithTag("Music").GetComponent<MusicManager>();
            m.LaunchQuietDesert();
        }*/
        CheckStatus();
    }

    public bool CheckCreature(Vector3 center, float radius)
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(center, radius);
        bool isHere = false;
        int i = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].tag.Contains("Enemy"))
                isHere = true;
            i++;
        }
        return isHere;
    }

    public void Attack()
    {
        if (Time.time > nextAttack)
        {
            nextAttack = Time.time + stats.attackSpeed;
            /*RaycastHit2D hit = Physics2D.Linecast(transform.position, strikeEnd, 1 << LayerMask.NameToLayer("Enemy"));
            if (hit.collider != null)
            {
                CreatureBehavior enemyStat = hit.collider.GetComponent<CreatureBehavior>();
                enemyStat.TakeDamage(stats.damage);
                Debug.Log(enemyStat.stats.hitpoint);
            }*/

            GameObject g = GameObject.Instantiate(proj, transform.position, transform.rotation);
            Destroy(g, 0.7f);
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
                stats.hitpoint -= (int)stats.waterDecreaseValue;
                UpdateHealth();
            }
            else
            {
                stats.water -= (int)stats.waterDecreaseValue;
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
                stats.hitpoint -= (int)stats.poisonValue;
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
            isAlive = false;
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
        stats.isDead = true;
        anim.SetTrigger("dead");
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1.0f);
        Debug.Log("Drop dead");
    }

    public void UseItem(Item item)
    {
        if (item != null)
        {
            use.Play();
            inventory.RemoveItem(item);
            ApplyItemEffect(item);
        }
    }

    public void PickUpItem(Item item)
    {
        if (item != null)
        {
            take.Play();
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
        if (i != null)
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

            stats.hitpoint += (int)item.healthModifier;
            stats.water += (int)item.waterModifier;
            if (stats.isPoisonned == true && item.cleanse == true)
                stats.isPoisonned = false;
        }
        UpdateHealth();
        UpdateWater();
    }

    public void RemoveItemEffect(Item item)
    {
        if (item != null)
        {
            stats.moveSpeed -= item.speedModifier;
            stats.attackSpeed -= item.attackSpeedModifier;
            stats.damage -= item.damageModifier;
            stats.waterDecreaseTick -= item.waterTickModifier;
            stats.water -= (int)item.waterModifier;
        }
        UpdateHealth();
        UpdateWater();
    }
}

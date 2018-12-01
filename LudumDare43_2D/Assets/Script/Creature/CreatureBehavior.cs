using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureBehavior : MonoBehaviour
{
    public CreatureStats stats;
    private float nextAttackAllowed;

    // Use this for 

    void Start()
    {
        nextAttackAllowed = stats.attackSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
        if (dist > stats.range && dist < stats.rangeOfChase)
        {
            transform.position = Vector3.MoveTowards(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position, stats.moveSpeed * Time.deltaTime);
            Vector3 diff = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;
            diff.Normalize();

            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
        }
        if (dist <= stats.range)
        {
            Vector3 diff = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;
            diff.Normalize();

            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
            if (Time.time > nextAttackAllowed)
            {
                nextAttackAllowed = Time.time + stats.attackSpeed;
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehavior>().TakeDamage(stats.damage);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (stats.hitpoint > 0)
        {
            stats.hitpoint -= damage;
        }
        else
        {
            Debug.Log("Ded");
        }
    }
}
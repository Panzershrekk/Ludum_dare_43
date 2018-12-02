using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour {

    public ProjectilStats stats;

    private Vector3 targetPosition;
    private Vector3 tmp;

    private PlayerStats pStats;
    // Use this for initialization
    void Start()
    {
        tmp = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehavior>().strikeEnd;
        tmp += new Vector3(0, 0, -0.5f);
        targetPosition = (tmp - GameObject.FindGameObjectWithTag("Player").transform.position).normalized;
        pStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehavior>().stats;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)targetPosition * stats.speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            CreatureBehavior striked = col.gameObject.GetComponent<CreatureBehavior>();
            striked.TakeDamage(pStats.damage);
            Destroy(this.gameObject);
        }
    }
}

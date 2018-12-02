using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderDen : MonoBehaviour
{
    public GameObject toSpwan;
    public SpiderDenStats stats;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    float dist = Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);

	    if (dist <= stats.spawnRange)
	    {
	        if (Time.time > stats.nextSpwan)
	        {
	            stats.nextSpwan = Time.time + stats.spawnTick;
	            Instantiate(toSpwan, transform.position, transform.rotation);

	        }
	    }
	}
}

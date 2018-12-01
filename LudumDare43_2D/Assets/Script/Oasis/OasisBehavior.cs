using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OasisBehavior : MonoBehaviour
{

    public OasisStat stat;

    private PlayerBehavior player;
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehavior>();
    }
	
	// Update is called once per frame
	void Update ()
	{
	    if (stat.isRegenrating == true)
	        Regen();
	}

    public void Regen()
    {
        if (Time.time > stat.nextWaterIncreaseAllowed)
        {
            stat.nextWaterIncreaseAllowed = Time.time + stat.waterIncreaseTick;

            if (player.stats.water <= 0)
            {
                player.stats.hitpoint += (int)stat.waterIncreaseValue;
                player.UpdateHealth();
            }
            else
            {
                player.stats.water += (int)stat.waterIncreaseValue;
                player.UpdateHealth();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            stat.isRegenrating = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            stat.isRegenrating = false;
        }
    }

}

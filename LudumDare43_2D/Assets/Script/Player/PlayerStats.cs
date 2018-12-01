using UnityEngine;

[System.Serializable]
public class PlayerStats
{
    public bool isInvulnerable = false;
    public float moveSpeed;
    public int maxHitpoint;
    public int hitpoint;
    public int water;
    public int maxWater;
    public float attackSpeed;
    public float recoveryTime = 0;
    public float universalRecoveryTime = 1.5f;
    public bool isDead = false;
    public float waterDecreaseTick;
    public float waterDecreaseValue;
    public float nextWaterDecreaseAllowed;
}
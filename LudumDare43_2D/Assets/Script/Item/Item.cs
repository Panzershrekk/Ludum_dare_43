using System;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public String name;
    public Sprite sprite;

    //Effects
    public float speedModifier;
    public float waterTickModifier;
    public int damageModifier;
    public float attackSpeedModifier;
    public float reduceDamageModifier;
    public float healthModifier;
    public bool cleanse;
    public bool consomable = false;

    public GameObject prefab;
}
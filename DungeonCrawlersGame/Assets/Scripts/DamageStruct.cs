using UnityEngine;
using System.Collections;

public enum DamageType
{
	NONE,
	BURN,
	POISON,
	MUD,
	FREEZE,
    BLEED,
    BLIND
};

public struct Damage
{
	public DamageType type = DamageType.NONE;
	public int amount = 1;
    public float knockback = 0;
	public Transform fromGO = null;
	public float effectTime = 1;
	public float effectStrength = 1;
};

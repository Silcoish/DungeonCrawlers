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
	public DamageType type;
	public int amount;
    public int knockback;
	public Transform fromGO;
};

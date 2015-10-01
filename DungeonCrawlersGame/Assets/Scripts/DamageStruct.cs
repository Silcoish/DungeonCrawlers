using UnityEngine;
using System.Collections;

public enum DamgeType
{
	None,
	Fire,
	Poison,
	Mud,
	Ice
};

public struct Damage
{
	public DamgeType type;
	public int amount;
	public GameObject fromGO;
};

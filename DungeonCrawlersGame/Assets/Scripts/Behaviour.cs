using UnityEngine;
using System.Collections;

[System.Serializable]
public class Behaviour : MonoBehaviour
{

	public virtual void BehaviourStart()
	{

	}
	public virtual void BehaviourUpdate(Enemy en)
	{
		
	}

	public virtual void OnTakeDamage(Vector2 origin, int dam)
	{

	}

	public virtual void OnDeath()
	{

	}
}

using UnityEngine;
using System.Collections;

public class Quest : MonoBehaviour 
{
	public virtual bool CheckProgress() { return true; }
	public virtual void Randomise() { }
}

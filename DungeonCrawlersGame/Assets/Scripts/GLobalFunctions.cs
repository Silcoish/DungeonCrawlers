using UnityEngine;
using System.Collections;

public class GLobalFunctions : MonoBehaviour 
{

	public static void RotateVector(ref Vector2 v, float deg)
	{
		float radians = deg * Mathf.Deg2Rad;
		float sin = Mathf.Sin(radians);
		float cos = Mathf.Cos(radians);

		float tx = v.x;
		float ty = v.y;

		Vector2 temp = new Vector2(cos * tx - sin * ty, sin * tx + cos * ty);

		v.x = temp.x;
		v.y = temp.y;

	}

	public static Vector2 DegToVector(float deg)
	{
		float radians = deg * Mathf.Deg2Rad;
		return new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
	}
}

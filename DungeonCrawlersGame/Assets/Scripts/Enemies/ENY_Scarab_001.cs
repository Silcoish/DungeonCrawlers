using UnityEngine;
using System.Collections;

public class ENY_Scarab_001 : Enemy 
{
	[Header("EnemyAI")]
	public float m_moveSpeed = 5f;
	public Transform m_pathLeft;
	public Transform m_pathRight;

	protected Vector2 m_moveDirection;

	// Use this for initialization
	void Start () 
	{
		m_moveDirection = Vector2.zero;
		m_moveDirection.x = m_moveSpeed;
	}
	
	// Update is called once per frame
	public override void UpdateOverride() 
	{
		if (transform.position.x <= m_pathLeft.position.x && m_moveDirection.x < 0)
		{
			m_moveDirection.x = m_moveSpeed;
		}
		if (transform.position.x >= m_pathRight.position.x && m_moveDirection.x > 0)
		{
			m_moveDirection.x = -m_moveSpeed;
			
		}

		rb.AddForce(m_moveDirection);
		sp.transform.rotation = Quaternion.FromToRotation(Vector2.up, m_moveDirection);
	
	}
}

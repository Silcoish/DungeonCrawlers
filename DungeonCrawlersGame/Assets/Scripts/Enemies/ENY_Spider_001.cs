using UnityEngine;
using System.Collections;

public class ENY_Spider_001 : Enemy {
	[Header("EnemyAI")]
	public float m_moveSpeed = 5f;
	public Transform m_pathTop;
	public Transform m_pathBottom;

	private Vector3 m_nextPostion;

	protected Vector2 m_moveDirection;

	// Use this for initialization
	void Start()
	{
		m_moveDirection = Vector2.zero;
		m_nextPostion = m_pathBottom.position;
		m_moveDirection = (m_nextPostion - transform.position).normalized; 
	}

	// Update is called once per frame
	public override void EnemyBehaviour()
	{
		
		if (transform.position.y >= m_pathTop.position.y && m_moveDirection.y > 0)
		{
			m_nextPostion = m_pathBottom.position;
			PauseEnemy(2);
			rb.velocity = Vector2.zero;
		}
		if (transform.position.y <= m_pathBottom.position.y && m_moveDirection.y < 0)
		{
			m_nextPostion = m_pathTop.position;
			PauseEnemy(2);
			rb.velocity = Vector2.zero;
		}

		m_moveDirection = (m_nextPostion - transform.position).normalized * m_moveSpeed; 

		rb.AddForce(m_moveDirection);

	}
}

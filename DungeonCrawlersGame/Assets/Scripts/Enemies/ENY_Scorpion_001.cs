using UnityEngine;
using System.Collections;

public class ENY_Scorpion_001 : Enemy 
{
	[Header("EnemyAI")]
	public float m_moveSpeed = 5f;
	public Transform m_pathLeft;
	public Transform m_pathRight;

	public Vector2 m_shotTimeMinMax = new Vector2(1f,1.5f);
	public GameObject m_bullet;
	public GameObject m_bulletSpawn;
	public float m_bulletForce = 1;

	protected Vector2 m_moveDirection = Vector2.zero;
	protected float m_shotTime = 0;
	protected float m_timerShot = 0;

	// Use this for initialization
	void Start()
	{
		m_moveDirection.x = m_moveSpeed;
		m_shotTime = Random.Range(m_shotTimeMinMax.x,m_shotTimeMinMax.y);
	}

	// Update is called once per frame
	public override void EnemyBehaviour()
	{
		if (transform.position.x <= m_pathLeft.position.x && m_moveDirection.x < 0)
		{
			rb.velocity = Vector2.zero;
			m_moveDirection.x = m_moveSpeed;
		}
		if (transform.position.x >= m_pathRight.position.x && m_moveDirection.x > 0)
		{
			rb.velocity = Vector2.zero;
			m_moveDirection.x = -m_moveSpeed;
		}

		m_timerShot += Time.deltaTime;

		if (m_timerShot > m_shotTime)
		{
			Shoot();
			m_shotTime = Random.Range(m_shotTimeMinMax.x, m_shotTimeMinMax.y);
			m_timerShot = 0;
		}

		rb.AddForce(m_moveDirection);
		transform.rotation = Quaternion.FromToRotation(Vector2.right, m_moveDirection);

	}

	void Shoot()
	{
		Rigidbody2D bulleRB = (Instantiate(m_bullet, m_bulletSpawn.transform.position, m_bulletSpawn.transform.rotation) as GameObject).GetComponent<Rigidbody2D>();
		bulleRB.AddForce(m_bulletSpawn.transform.right * m_bulletForce, ForceMode2D.Impulse);
		rb.velocity = Vector2.zero;
		PauseEnemy(0.5f);
	}





	



}

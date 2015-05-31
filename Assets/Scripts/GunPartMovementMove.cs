using UnityEngine;
using System.Collections;

public class GunPartMovementMove : GunPartMovement
{
	public Transform m_gunPart;
	public Transform m_destination;

	private Vector3 m_basePosition;

	void Start()
	{
	}

	public override void Enter()
	{
		Animator animator = m_gunPart.GetComponent<Animator>();
		if (animator != null)
		{
			Vector3 position = m_gunPart.transform.position;
			Quaternion rotation = m_gunPart.transform.rotation;
			animator.enabled = false;
			//m_gunPart.transform.position = position;
			//m_gunPart.transform.rotation = rotation;
		}

		m_basePosition = m_gunPart.position;
	}

	public override void SetReloadProgress(float progress)
	{
		m_gunPart.position = Vector3.Lerp(m_basePosition, m_destination.position, progress);
	}
}

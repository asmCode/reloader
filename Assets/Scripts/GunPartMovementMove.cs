using UnityEngine;
using System.Collections;

public class GunPartMovementMove : GunPartMovement
{
	public Transform m_gunPart;
	public Transform m_destination;

	private Vector3 m_basePosition;

	void Start()
	{
		m_basePosition = m_gunPart.position;
	}

	public override void SetReloadProgress(float progress)
	{
		m_gunPart.position = Vector3.Lerp(m_basePosition, m_destination.position, progress);
	}
}

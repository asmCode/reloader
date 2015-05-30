using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PistolMagazine : MonoBehaviour
{
	public Transform m_firstBulletTransform;
	public Transform m_secondBulletTransform;
	public Transform m_bulletsContainer;
	public int m_capacity;

	private List<GameObject> m_bullets = new List<GameObject>();

	public int Capacity
	{
		get { return m_capacity; }
	}

	public bool IsFullyLoaded
	{
		get { return Capacity == m_bullets.Count; }
	}

	public void LoadBullet(GameObject bullet)
	{
		if (IsFullyLoaded)
			return;

		if (m_bullets.Count > 0)
			ShiftBulletsContainerDown();

		m_bullets.Add(bullet);

		bullet.transform.parent = m_bulletsContainer;
		bullet.transform.position = m_firstBulletTransform.position;
		bullet.transform.rotation = m_firstBulletTransform.rotation;
	}

	private void ShiftBulletsContainerDown()
	{
		Vector3 position = m_bulletsContainer.transform.position;
		position -= GetBulletShiftVector();
		m_bulletsContainer.transform.position = position;
	}

	private Vector3 GetBulletShiftVector()
	{
		return m_firstBulletTransform.position - m_secondBulletTransform.position;
	}
}

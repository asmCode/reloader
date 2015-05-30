using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PistolMagazine : MonoBehaviour
{
	public Transform m_firstBulletTransform;
	public Transform m_secondBulletTransform;
	public Transform m_bulletsContainer;

	private List<GameObject> m_bullets = new List<GameObject>();

	public void LoadBullet(GameObject bullet)
	{
		m_bullets.Add(bullet);

		bullet.transform.parent = m_bulletsContainer;
		bullet.transform.position = m_firstBulletTransform.position;
		bullet.transform.rotation = m_firstBulletTransform.rotation;
	}
}

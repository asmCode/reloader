using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AmmoBox : MonoBehaviour
{
	public GameObject m_bulletPrefab;
	public Transform m_firstBulletTransform;
	public Transform m_lastBulletTransform;
	public Transform m_bulletTransform;
	public int m_rows;
	public int m_columns;

	private Queue<GameObject> m_bullets = new Queue<GameObject>();

	public void RemoveBulletFromBox()
	{
		if (m_bullets.Count == 0)
			return;

		GameObject bullet = m_bullets.Dequeue();
		Destroy(bullet.gameObject);
	}

	private void Awake()
	{
		FillWithBullets(24);
	}

	private void FillWithBullets(int count)
	{
		float baseX = m_firstBulletTransform.localPosition.x;
		float baseZ = m_firstBulletTransform.localPosition.z;
		float rowShift = Mathf.Abs(m_firstBulletTransform.localPosition.z - m_lastBulletTransform.localPosition.z) / Mathf.Max(1, m_rows - 1);
		float columnShift = Mathf.Abs(m_firstBulletTransform.localPosition.x - m_lastBulletTransform.localPosition.x) / Mathf.Max(1, m_columns - 1);

		float x = baseX;
		float z = baseZ;

		for (int row = 0; row < m_rows; row++)
		{
			for (int column = 0; column < m_columns; column++)
			{
				GameObject bullet = (GameObject)Instantiate(m_bulletPrefab);
				bullet.transform.parent = transform;
				bullet.transform.localPosition = m_bulletTransform.localPosition + new Vector3(x, 0, z);
				bullet.transform.localRotation = m_bulletTransform.localRotation;

				m_bullets.Enqueue(bullet);

				x += columnShift;
			}

			x = baseX;
			z += rowShift;
		}
	}
}


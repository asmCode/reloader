using UnityEngine;
using System.Collections;

public class MoveBulletToMagazine : ReloadStage
{
	#region Inspector
	public GameObject m_ammoBox;
	public GameObject m_magazine;
	public GameObject m_bulletPrefab;
	#endregion

	private Drag m_drag;
	private GameObject m_bullet;

	private void Awake()
	{
		m_drag = new Drag(m_ammoBox, m_magazine);
	}

	private void OnEnable()
	{
		m_drag.DragStarted += HandleDragStarted;
		m_drag.DragMoved += HandleDragMoved;
		m_drag.DragOver += HandleDragOver;
		m_drag.DragEnded += HandleDragEnded;
	}

	private void OnDisable()
	{
		m_drag.DragStarted -= HandleDragStarted;
		m_drag.DragMoved -= HandleDragMoved;
		m_drag.DragOver -= HandleDragOver;
		m_drag.DragEnded -= HandleDragEnded;
	}

	private void CreateBullet(Vector3 position)
	{
		m_bullet = (GameObject)Instantiate(m_bulletPrefab, position, Quaternion.identity);
	}

	private void SetBulletPosition(Vector3 position)
	{
		if (m_bullet == null)
			return;

		m_bullet.transform.position = position;
	}

	private void HandleDragStarted(Vector3 position)
	{
		CreateBullet(position);
	}

	private void HandleDragMoved(Vector3 position)
	{
		SetBulletPosition(position);
	}

	private void HandleDragOver(Vector3 position)
	{
		Debug.Log("cipsko");
	}

	private void HandleDragEnded(Vector3 position)
	{
	}
}

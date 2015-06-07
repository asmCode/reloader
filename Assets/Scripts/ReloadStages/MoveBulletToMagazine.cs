using UnityEngine;
using System.Collections;

public class MoveBulletToMagazine : ReloadStage
{
	#region Inspector
	public GameObject m_ammoBoxObject;
	public GameObject m_magazineObject;
	public GameObject m_bulletPrefab;
	public PistolMagazine m_pistolMagazine;
	public AmmoBox m_ammoBox;
	#endregion

	private Drag m_drag;
	private GameObject m_bullet;

	private void Awake()
	{
		m_drag = new Drag(m_ammoBoxObject, m_magazineObject);
	}

	public override void Enter()
	{
		m_drag.DragStarted += HandleDragStarted;
		m_drag.DragMoved += HandleDragMoved;
		m_drag.DragOver += HandleDragOver;
		m_drag.DragEnded += HandleDragEnded;
	}

	public override void Leave()
	{
		m_drag.DragStarted -= HandleDragStarted;
		m_drag.DragMoved -= HandleDragMoved;
		m_drag.DragOver -= HandleDragOver;
		m_drag.DragEnded -= HandleDragEnded;

		m_drag.Dispose();
	}

	private void CreateBullet(Vector3 position)
	{
		m_bullet = (GameObject)Instantiate(m_bulletPrefab, position, Quaternion.Euler(26.5f, 115.6f, 78.4f));
	}

	private void DestroyBullet()
	{
		if (m_bullet != null)
			Destroy(m_bullet.gameObject);
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

		m_ammoBox.RemoveBulletFromBox();
	}

	private void HandleDragMoved(Vector3 position)
	{
		SetBulletPosition(position);
	}

	private void HandleDragOver(Vector3 position)
	{
		m_pistolMagazine.LoadBullet(m_bullet);
		m_bullet = null;

		if (m_pistolMagazine.IsFullyLoaded)
			Finish();
		//DestroyBullet();
	}

	private void HandleDragEnded(Vector3 position)
	{
		DestroyBullet();
	}
}

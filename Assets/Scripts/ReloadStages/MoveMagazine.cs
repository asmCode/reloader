using UnityEngine;
using System.Collections;

public class MoveMagazine : ReloadStage
{
	#region Inspector
	public GameObject m_srcMagazineLocation;
	public GameObject m_dstMagazineLocation;
	public GameObject m_magazine;
	#endregion

	private Drag m_drag;

	private void Awake()
	{
		m_drag = new Drag(m_srcMagazineLocation, m_dstMagazineLocation);
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

	private void SetMagazinePosition(Vector3 position)
	{
		m_magazine.transform.position = position;
	}

	private void HandleDragStarted(Vector3 position)
	{
	}

	private void HandleDragMoved(Vector3 position)
	{
		SetMagazinePosition(position);
	}

	private void HandleDragOver(Vector3 position)
	{
		m_magazine.transform.position = m_dstMagazineLocation.transform.position;
		m_magazine.transform.rotation = m_dstMagazineLocation.transform.rotation;

		Finish();
	}

	private void HandleDragEnded(Vector3 position)
	{
		// TODO: roll back position of magazine
	}
}

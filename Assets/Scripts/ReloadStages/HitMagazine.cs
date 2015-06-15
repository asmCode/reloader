using UnityEngine;
using System.Collections;

public class HitMagazine : ReloadStage
{
	#region Inspector
	public GameObject m_srcLocation;
	public GameObject m_dstLocation;
	#endregion

	private Drag m_drag;

	private void Awake()
	{
		m_drag = new Drag(m_srcLocation, m_dstLocation);
	}

	public override void Enter()
	{
		m_drag.DragOver += HandleDragOver;
	}

	public override void Leave()
	{
		m_drag.DragOver -= HandleDragOver;

		m_drag.Dispose();
	}

	private void HandleDragOver(Vector3 position)
	{
		if (m_drag.DragDuration < 0.2f)
			Finish();
	}
}

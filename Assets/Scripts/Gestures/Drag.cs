using UnityEngine;
using System.Collections;

public class Drag
{
	private Collider m_sourceObject;
	private Transform m_sourceTransform;
	private Collider m_destinationObject;
	private Transform m_destinationTransform;
	private bool m_isMoving;
	private bool m_isOver;
	private float m_distanceFromCamera;

	public event System.Action<Vector3> DragStarted;
	public event System.Action<Vector3> DragMoved;
	public event System.Action<Vector3> DragOver;
	public event System.Action<Vector3> DragEnded;

	public Drag(
		GameObject sourceObject,
		GameObject destinationObject)
	{
		m_sourceObject = sourceObject.GetComponent<Collider>();
		m_sourceTransform = sourceObject.transform;
		m_destinationObject = destinationObject.GetComponent<Collider>();
		m_destinationTransform = destinationObject.transform;

		DragGestureDetector.Instance().DragStarted += HandleDragStarted;
		DragGestureDetector.Instance().DragMoved += HandleDragMoved;
		DragGestureDetector.Instance().DragEnded += HandleDragEnded;
	}

	public void Dispose()
	{
		DragGestureDetector.Instance().DragStarted -= HandleDragStarted;
		DragGestureDetector.Instance().DragMoved -= HandleDragMoved;
		DragGestureDetector.Instance().DragEnded -= HandleDragEnded;
	}

	private void HandleDragStarted(Vector3 touchPosition)
	{
		if (HitTest(touchPosition, m_sourceObject))
		{
			m_isMoving = true;

			m_distanceFromCamera = Utils.GetPerpendicularDistance(
				Camera.main.transform.position,
				Camera.main.transform.forward,
				m_sourceTransform.position);

			if (DragStarted != null)
				DragStarted(ScreenToWorld(touchPosition));
		}
	}

	private void HandleDragMoved(Vector3 touchPosition)
	{
		if (!m_isMoving)
			return;

		if (DragMoved != null)
			DragMoved(ScreenToWorld(touchPosition));

		bool hitTest = HitTest(touchPosition, m_destinationObject);

		if (!m_isOver && hitTest)
		{
			m_isOver = true;

			if (DragOver != null)
				DragOver(ScreenToWorld(touchPosition));
		}
		else if (m_isOver && !hitTest)
		{
			m_isOver = false;
		}
	}

	private void HandleDragEnded(Vector3 touchPosition)
	{
		if (!m_isMoving)
			return;

		m_isMoving = false;

		if (DragEnded != null)
			DragEnded(ScreenToWorld(touchPosition));
	}

	private Vector3 ScreenToWorld(Vector3 screenPosition)
	{
		return Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, m_distanceFromCamera));
	}

	private bool HitTest(Vector3 screenPosition, Collider collider)
	{
		RaycastHit hit;
		return collider.Raycast(Camera.main.ScreenPointToRay(screenPosition), out hit, float.PositiveInfinity);
	}
}

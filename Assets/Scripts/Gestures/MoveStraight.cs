using UnityEngine;
using System.Collections;

public class MoveStraight
{
	private Collider m_sourceCollider;
	private bool m_isMoving;
	private Vector3 m_touchStartScreenPosition;
	private Vector3 m_srcWorldPosition;
	private Vector3 m_dstWorldPosition;
	private Vector3 m_lastTouchPosition;

	public event System.Action<Vector3> Started;
	public event System.Action<float> Moved;
	public event System.Action<Vector3> Ended;

	public MoveStraight()
	{
		DragGestureDetector.Instance().DragStarted += HandleDragStarted;
		DragGestureDetector.Instance().DragMoved += HandleDragMoved;
		DragGestureDetector.Instance().DragEnded += HandleDragEnded;
	}

	public void Start(Transform source, Transform destination)
	{
		m_sourceCollider = source.GetComponent<Collider>();

		m_srcWorldPosition = source.position;
		m_dstWorldPosition = destination.position;

		if (m_isMoving)
			OnStarted(m_lastTouchPosition);
	}

	public void SetDestination(Transform destination)
	{
		m_dstWorldPosition = destination.position;
	}

	public void Dispose()
	{
		DragGestureDetector.Instance().DragStarted -= HandleDragStarted;
		DragGestureDetector.Instance().DragMoved -= HandleDragMoved;
		DragGestureDetector.Instance().DragEnded -= HandleDragEnded;
	}

	private Vector2 GetScreenMoveVector()
	{
		Vector2 srcScreenStart = TapUtils.GetScreenPosition(m_srcWorldPosition);
		Vector2 dstScreenStart = TapUtils.GetScreenPosition(m_dstWorldPosition);
		return dstScreenStart - srcScreenStart;
	}

	private void HandleDragStarted(Vector3 touchPosition)
	{
		m_lastTouchPosition = touchPosition;

		if (TapUtils.HitTest(touchPosition, m_sourceCollider))
		{
			OnStarted(touchPosition);
		}
	}

	private void HandleDragMoved(Vector3 touchPosition)
	{
		m_lastTouchPosition = touchPosition;

		if (!m_isMoving)
			return;

		touchPosition.z = 0.0f;

		Vector2 screenMoveVector = GetScreenMoveVector();
		Vector3 touchMove = touchPosition - m_touchStartScreenPosition;

		float moveProgress = Vector3.Dot(screenMoveVector.normalized, touchMove) / screenMoveVector.magnitude;
		moveProgress = Mathf.Clamp01(moveProgress);

		if (Moved != null)
			Moved(moveProgress);
	}

	private void HandleDragEnded(Vector3 touchPosition)
	{
		m_lastTouchPosition = touchPosition;

		m_isMoving = false;

		if (Ended != null)
			Ended(touchPosition);
	}

	private void OnStarted(Vector3 touchPosition)
	{
		m_isMoving = true;
		m_touchStartScreenPosition = touchPosition;
		m_touchStartScreenPosition.z = 0.0f;

		if (Started != null)
			Started(touchPosition);
	}
}

using UnityEngine;
using System.Collections;

public class ReloadMoveStraightStage : ReloadStage
{
	public Collider m_objectToMove;
	public Transform m_moveTarget;

	private Vector3 m_baseObjectToMovePosition;
	private float m_reloadProgress;
	private bool m_isMoving;
	private Vector3 m_moveStartPosition;

	public override event System.Action Finished;
	public override event System.Action<float> ProgressChanged;

	public override void Update()
	{
		m_reloadProgress = Mathf.Clamp01(m_reloadProgress);

		if (ProgressChanged != null)
			ProgressChanged(m_reloadProgress);

		m_gunPartMovement.SetReloadProgress(m_reloadProgress);
	}

	private void Start()
	{
		m_baseObjectToMovePosition = m_objectToMove.gameObject.transform.position;
	}

	private void OnEnable()
	{
		DragGestureDetector.Instance().DragStarted += HandleDragStarted;
		DragGestureDetector.Instance().DragMoved += HandleDragMoved;
		DragGestureDetector.Instance().DragEnded += HandleDragEnded;
	}

	private void OnDisable()
	{
		DragGestureDetector.Instance().DragStarted -= HandleDragStarted;
		DragGestureDetector.Instance().DragMoved -= HandleDragMoved;
		DragGestureDetector.Instance().DragEnded -= HandleDragEnded;
	}

	private void OnFinished()
	{
		if (Finished != null)
			Finished();
	}

	private void HandleDragStarted(Vector3 touchPosition)
	{
		RaycastHit hit;
		if (m_objectToMove.Raycast(Camera.main.ScreenPointToRay(touchPosition), out hit, float.PositiveInfinity))
		{
			m_isMoving = true;
			m_moveStartPosition = touchPosition;
		}
	}

	private void HandleDragMoved(Vector3 touchPosition)
	{
		if (!m_isMoving)
			return;

		Vector3 fullMoveVector =
			GetMoveTargetScreenPosition() -
			GetObjectToMoveScreenPosition();

		touchPosition.z = 0.0f;
		m_moveStartPosition.z = 0.0f;

		Vector3 move = touchPosition - m_moveStartPosition;

		m_reloadProgress = Vector3.Dot(fullMoveVector.normalized, move) / fullMoveVector.magnitude;
	}

	private void HandleDragEnded(Vector3 touchPosition)
	{
		m_isMoving = false;
	}

	private Vector3 GetObjectToMoveScreenPosition()
	{
		Vector3 position = Camera.main.WorldToScreenPoint(m_baseObjectToMovePosition);
		position.z = 0.0f;
		return position;
	}

	private Vector3 GetMoveTargetScreenPosition()
	{
		Vector3 position = Camera.main.WorldToScreenPoint(m_moveTarget.position);
		position.z = 0.0f;
		return position;
	}
}

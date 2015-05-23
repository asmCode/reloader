using UnityEngine;
using System.Collections;

public class DragGestureDetector : MonoBehaviour
{
	private static DragGestureDetector m_instance;
	private bool m_isTouching;
	private Vector3 m_lastTouchPosition;

	public event System.Action<Vector3> DragStarted;
	public event System.Action<Vector3> DragMoved;
	public event System.Action<Vector3> DragEnded;

	public static DragGestureDetector Instance()
	{
		if (m_instance == null)
		{
			GameObject gameObject = new GameObject("DragGestureDetector", typeof(DragGestureDetector));
			m_instance = gameObject.GetComponent<DragGestureDetector>();
			DontDestroyOnLoad(gameObject);
		}

		return m_instance;
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0) && !m_isTouching)
		{
			m_isTouching = true;
			m_lastTouchPosition = Input.mousePosition;

			OnDragStarted(Input.mousePosition);
		}
		else if (Input.GetMouseButton(0) && m_isTouching)
		{
			if (m_lastTouchPosition != Input.mousePosition)
				OnDragMoved(Input.mousePosition);

			m_lastTouchPosition = Input.mousePosition;
		}
		else if (Input.GetMouseButtonUp(0) && m_isTouching)
		{
			m_isTouching = false;

			OnDragEnded(Input.mousePosition);
		}
	}

	protected void OnDragStarted(Vector3 touchPosition)
	{
		if (DragStarted != null)
			DragStarted(touchPosition);
	}

	protected void OnDragMoved(Vector3 touchPosition)
	{
		if (DragMoved != null)
			DragMoved(touchPosition);
	}

	protected void OnDragEnded(Vector3 touchPosition)
	{
		if (DragEnded != null)
			DragEnded(touchPosition);
	}
}

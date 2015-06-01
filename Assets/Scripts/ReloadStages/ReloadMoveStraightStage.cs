using UnityEngine;
using System.Collections;

public class ReloadMoveStraightStage : ReloadStage
{
	public Transform m_source;
	public Transform m_destination;

	private MoveStraight m_moveStraight;
	private float m_reloadProgress;
	private bool m_isMoving;

	private void Awake()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
		//m_moveStraight.Started -= HandleMoveStraightStarted;
		//m_moveStraight.Moved -= HandleMoveStraightMoved;
	//	m_moveStraight.Ended -= HandleMoveStraightEnded;
	}

	public override void Update()
	{
		m_reloadProgress = Mathf.Clamp01(m_reloadProgress);

		if (!m_isMoving && m_reloadProgress != 0)
		{
			m_reloadProgress = Utils.LinearDamp(m_reloadProgress, 0.0f, Time.deltaTime * 20);
		}

		SetProgress(m_reloadProgress);

		m_gunPartMovement.SetReloadProgress(m_reloadProgress);

		if (m_reloadProgress >= 1.0f)
			Finish();
	}

	public override void Enter()
	{
		if (m_gunPartMovement != null)
			m_gunPartMovement.Enter();

		m_moveStraight = new MoveStraight(m_source, m_destination);
		m_moveStraight.Started += HandleMoveStraightStarted;
		m_moveStraight.Moved += HandleMoveStraightMoved;
		m_moveStraight.Ended += HandleMoveStraightEnded;
	}

	private void OnFinished()
	{
		Finish();
	}

	private void HandleMoveStraightStarted(Vector3 screenPosition)
	{
		m_isMoving = true;
	}

	private void HandleMoveStraightMoved(float progress)
	{
		m_reloadProgress = progress;
	}

	private void HandleMoveStraightEnded(Vector3 screenPosition)
	{
		m_isMoving = false;
	}
}

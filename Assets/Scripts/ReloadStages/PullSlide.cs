using UnityEngine;
using System.Collections;

public class PullSlide : ReloadStage
{
	public Transform m_slide;
	public Transform m_basePosition;
	public Transform m_pulledPositon;

	private MoveStraight m_moveStraight;
	private bool m_isReachedMaxPull;
	private bool m_isTouching;
	private float m_gunMoveValue;

	public override void Update()
	{
		if (!m_isTouching && !m_isReachedMaxPull && m_gunMoveValue != 0.0f)
		{
			m_gunMoveValue = Utils.LinearDamp(m_gunMoveValue, 0.0f, 8.0f * Time.deltaTime);
			m_gunPartMovement.SetReloadProgress(m_gunMoveValue);
		}
		else if (!m_isTouching && m_isReachedMaxPull && m_gunMoveValue != 0.0f)
		{
			m_gunMoveValue = Utils.LinearDamp(m_gunMoveValue, 0.0f, 8.0f * Time.deltaTime);
			m_gunPartMovement.SetReloadProgress(m_gunMoveValue);

			if (m_gunMoveValue <= 0.0f)
				Finish();
		}
	}

	public override void Enter()
	{
		if (m_gunPartMovement != null)
			m_gunPartMovement.Enter();

		m_moveStraight = new MoveStraight();
		m_moveStraight.Started += HandleMoveStraightStarted;
		m_moveStraight.Moved += HandleMoveStraightMoved;
		m_moveStraight.Ended += HandleMoveStraightEnded;

		m_moveStraight.Start(m_slide, m_pulledPositon);
	}

	public override void Leave()
	{
		m_moveStraight.Started -= HandleMoveStraightStarted;
		m_moveStraight.Moved -= HandleMoveStraightMoved;
		m_moveStraight.Ended -= HandleMoveStraightEnded;
		m_moveStraight.Dispose();
	}

	private void HandleMoveStraightStarted(Vector3 screenPosition)
	{
		m_isTouching = true;
	}

	private void HandleMoveStraightMoved(float progress)
	{
		if (!m_isReachedMaxPull)
		{
			if (progress >= 1.0f)
			{
				m_isReachedMaxPull = true;

				m_moveStraight.Start(m_slide, m_basePosition);

				return;
			}
		}
		else
		{
			if (progress >= 1.0f)
				Finish();
		}

		m_gunMoveValue = !m_isReachedMaxPull ? progress : (1.0f - progress);
		m_gunPartMovement.SetReloadProgress(m_gunMoveValue);
	}

	private void HandleMoveStraightEnded(Vector3 screenPosition)
	{
		m_isTouching = false;
	}
}

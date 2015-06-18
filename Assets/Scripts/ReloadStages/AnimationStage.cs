using UnityEngine;
using System.Collections;

public class AnimationStage : ReloadStage
{
	public Animator m_animator;
	public string m_triggerName;
	public bool m_manualFinish;

	private bool m_endOfAnimFired;

	public override void Enter()
	{
		if (string.IsNullOrEmpty(m_triggerName))
			m_triggerName = "PlayTrigger";

		m_animator.enabled = true;

		m_animator.SetTrigger(m_triggerName);

		if (m_manualFinish)
			m_animator.gameObject.GetComponent<AnimationTriggerable>().AnimationTriggerFired += HandleAnimationTriggerFired;
		else
			Finish();
	}

	public override void Leave()
	{
		if (m_manualFinish)
			m_animator.gameObject.GetComponent<AnimationTriggerable>().AnimationTriggerFired -= HandleAnimationTriggerFired;
	}
	
	/*
	public override void Update()
	{
		if (m_animator.GetCurrentAnimatorStateInfo(0).IsName(m_triggerName) && !m_endOfAnimFired)
		{
			m_endOfAnimFired = true;

			OnAnimationFinished();
		}
	}
	 */

	private void OnAnimationFinished()
	{
		//Debug.Log("sssssssssssssss");
		//m_animator.enabled = false;
	}

	private void HandleAnimationTriggerFired()
	{
		Finish();
	}
}

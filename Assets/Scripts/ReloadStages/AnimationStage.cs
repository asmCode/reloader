using UnityEngine;
using System.Collections;

public class AnimationStage : ReloadStage
{
	public Animator m_animator;
	public string m_triggerName;

	public override void Enter()
	{
		m_animator.SetTrigger(string.IsNullOrEmpty(m_triggerName) ? "PlayTrigger" : m_triggerName);

		Finish();
	}
}

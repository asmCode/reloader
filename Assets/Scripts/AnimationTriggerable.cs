using UnityEngine;
using System.Collections;

public class AnimationTriggerable : MonoBehaviour
{
	public event System.Action AnimationTriggerFired;		 

	public void AnimationTrigger()
	{
		if (AnimationTriggerFired != null)
			AnimationTriggerFired();
	}
}

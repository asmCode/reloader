using UnityEngine;
using System.Collections;

public class FinishedStage : ReloadStage
{
	public GameObject m_restartButton;

	public override void Enter()
	{
		NGUITools.SetActive(m_restartButton.gameObject, true);
	}
}

using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
	public ReloadStage[] m_reloadStages;

	private int m_currentReloadStageIndex;

	public void StartReloading()
	{
		SetReloadStage(0);
	}

	private void SetReloadStage(int index)
	{
		if (m_currentReloadStageIndex != index)
			m_reloadStages[m_currentReloadStageIndex].Leave();

		for (int i = 0; i < m_reloadStages.Length; i++)
			m_reloadStages[i].gameObject.SetActive(i == index);

		m_reloadStages[index].ProgressChanged += HandleReloadStageProgressChanged;
		m_reloadStages[index].Finished += HandleReloadStageFinished;
		m_reloadStages[index].Enter();
	}

	private void NextStage()
	{
		m_currentReloadStageIndex++;

		SetReloadStage(m_currentReloadStageIndex);
	}

	private void HandleReloadStageProgressChanged(float progress)
	{

	}

	private void HandleReloadStageFinished()
	{
		NextStage();
	}
}

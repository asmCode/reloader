using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
	public ReloadStage[] m_reloadStages;

	private int m_currentReloadStageIndex;

	public void StartReloading()
	{
		for (int i = 0; i < m_reloadStages.Length; i++)
			m_reloadStages[i].Finished += HandleReloadStageFinished;

		SetReloadStage(0);
	}

	public void OnDestroy()
	{
		for (int i = 0; i < m_reloadStages.Length; i++)
			m_reloadStages[i].Finished -= HandleReloadStageFinished;
	}

	private void SetReloadStage(int index)
	{
		for (int i = 0; i < m_reloadStages.Length; i++)
			m_reloadStages[i].gameObject.SetActive(i == index);

		//m_reloadStages[index].gameObject.SetActive(true);

		m_reloadStages[index].ProgressChanged += HandleReloadStageProgressChanged;
		m_reloadStages[index].Enter();
	}

	private void NextStage()
	{
		m_reloadStages[m_currentReloadStageIndex].Leave();
		m_currentReloadStageIndex++;

		if (m_currentReloadStageIndex == m_reloadStages.Length)
			m_reloadStages[m_currentReloadStageIndex - 1].gameObject.SetActive(false);//Application.LoadLevel(Application.loadedLevel);
		else
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

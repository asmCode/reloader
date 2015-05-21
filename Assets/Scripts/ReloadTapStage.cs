using UnityEngine;
using System.Collections;

public class ReloadTapStage : ReloadStage
{
	public override event System.Action Finished;
	public override event System.Action<float> ProgressChanged;

	private float m_reloadProgress;

	public override void Update()
	{
		/*
        if (Input.GetMouseButtonDown(0))
		{
			OnFinished();
		}
		 */

		if (Input.GetKey(KeyCode.KeypadPlus))
			m_reloadProgress += Time.deltaTime * 0.5f;
		if (Input.GetKey(KeyCode.KeypadMinus))
			m_reloadProgress -= Time.deltaTime * 0.5f;

		m_reloadProgress = Mathf.Clamp01(m_reloadProgress);

		if (ProgressChanged != null)
			ProgressChanged(m_reloadProgress);

		m_gunPartMovement.SetReloadProgress(m_reloadProgress);
	}

	private void OnFinished()
	{
		if (Finished != null)
			Finished();
	}
}

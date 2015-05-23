using UnityEngine;
using System.Collections;

public class ReloadTapStage : ReloadStage
{
	public override event System.Action Finished;
	public override event System.Action<float> ProgressChanged;

	public Collider m_tapTarget;

	public override void Update()
	{
        if (Input.GetMouseButtonDown(0))
		{
			if (CheckTap(Input.mousePosition))
				OnFinished();
		}
	}

	private void OnFinished()
	{
		if (Finished != null)
			Finished();
	}

	private bool CheckTap(Vector3 tapPosition)
	{
		RaycastHit hit;
		return m_tapTarget.Raycast(Camera.main.ScreenPointToRay(tapPosition), out hit, float.PositiveInfinity);
	}
}

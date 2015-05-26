using UnityEngine;

public abstract class ReloadStage : MonoBehaviour
{
	public event System.Action Finished;
	public event System.Action<float> ProgressChanged;

	public GunPartMovement m_gunPartMovement;

	public virtual void Enter()
	{

	}

	public virtual void Leave()
	{

	}

	public virtual void Update()
	{
	}

	protected void Finish()
	{
		if (Finished != null)
			Finished();
	}

	protected void SetProgress(float progress)
	{
		if (ProgressChanged != null)
			ProgressChanged(progress);
	}
}

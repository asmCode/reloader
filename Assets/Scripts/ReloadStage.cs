using UnityEngine;

public abstract class ReloadStage : MonoBehaviour
{
	public abstract event System.Action Finished;
	public abstract event System.Action<float> ProgressChanged;

	public GunPartMovement m_gunPartMovement;

	public virtual void Update()
	{
	}
}

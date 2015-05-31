using UnityEngine;
using System.Collections;

public abstract class GunPartMovement : MonoBehaviour
{
	public abstract void Enter();
	public abstract void SetReloadProgress(float progress);
}

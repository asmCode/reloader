using UnityEngine;
using System.Collections;

public class ReloadSceneController : MonoBehaviour
{
	public Gun m_gun;

	private float m_reloadProgress;

	void Start()
	{
		m_gun.StartReloading();
	}

	void Update()
	{
	}
}

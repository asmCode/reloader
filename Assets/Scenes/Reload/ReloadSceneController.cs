using UnityEngine;
using System.Collections;

public class ReloadSceneController : MonoBehaviour
{
	public Gun m_gun;

	void Start()
	{
		m_gun.StartReloading();
	}

	void Update()
	{
	}

	public void RestartButtonPressed()
	{
		Application.LoadLevel(Application.loadedLevel);
	}
}

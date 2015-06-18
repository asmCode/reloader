using UnityEngine;
using System.Collections;

public class NextFrameTask : MonoBehaviour
{
	private static NextFrameTask m_instance;

	public static NextFrameTask Instance()
	{
		if (m_instance == null)
		{
			GameObject gameObject = new GameObject("NextFrameTask", typeof(NextFrameTask));
			DontDestroyOnLoad(gameObject);
			m_instance = gameObject.GetComponent<NextFrameTask>();
		}

		return m_instance;
	}

	private NextFrameTask()
	{

	}

	public void SetNextFrameTask(System.Action<object> callback, object userData = null)
	{
		StartCoroutine(WaitOneFrameEnumerator(callback, userData));
	}

	private IEnumerator WaitOneFrameEnumerator(System.Action<object> callback, object userData)
	{
		yield return null;

		if (callback != null)
			callback(userData);
	}
}

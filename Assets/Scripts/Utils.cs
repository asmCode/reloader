using UnityEngine;
using System.Collections;

public class Utils
{
	public static float LinearDamp(float start, float end, float change)
	{
		float diff = end - start;

		if (Mathf.Abs(diff) <= Mathf.Abs(change))
			return 0.0f;

		return start + change * Mathf.Sign(diff);
	}
}

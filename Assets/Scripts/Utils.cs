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

	public static float GetPerpendicularDistance(Vector3 p1, Vector3 p1Norm, Vector3 p2)
	{
		if (p1 == p2)
			return 0.0f;

		Vector3 dir = p2 - p1;

		return Vector3.Dot(p1Norm, dir);
	}
}

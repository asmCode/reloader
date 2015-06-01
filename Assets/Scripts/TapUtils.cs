using UnityEngine;
using System.Collections;

public class TapUtils
{
	public static bool HitTest(Vector3 screenPosition, Collider collider)
	{
		RaycastHit hit;
		return collider.Raycast(Camera.main.ScreenPointToRay(screenPosition), out hit, float.PositiveInfinity);
	}

	public static Vector2 GetScreenPosition(Vector3 worldPosition)
	{
		Vector3 position = Camera.main.WorldToScreenPoint(worldPosition);

		return new Vector2(position.x, position.y);
	}
}

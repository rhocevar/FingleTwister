using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils 
{
	public static bool IsInLayer (this GameObject toCheck, LayerMask mask)
	{
		return ((mask.value & (1 << toCheck.layer)) > 0);
	}
}

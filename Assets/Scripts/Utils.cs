using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
	public static class Utility 
	{
		public static bool IsInLayer (this GameObject toCheck, LayerMask mask)
		{
			return ((mask.value & (1 << toCheck.layer)) > 0);
		}
	}
}
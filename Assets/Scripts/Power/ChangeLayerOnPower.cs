using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Power
{
	public class ChangeLayerOnPower : BaseElectricObject 
	{
		[SerializeField]
		private SpriteRenderer sprite;
		[SerializeField]
		private string onLayer;
		[SerializeField]
		private string offLayer;

		protected override void OnPowerChanged(bool isEnabled) 
		{
			base.OnPowerChanged (isEnabled);
			sprite.sortingLayerName = isEnabled? onLayer : offLayer;	
		}
	}

}
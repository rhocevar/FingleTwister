using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Dragging
{
	public class DragInstaller : MonoInstaller
	{
		public override void InstallBindings ()
		{
			Container.BindInterfacesAndSelfTo<FingerTracker> ().FromNew ().AsSingle ();
		}
	}
}
using System.Collections;
using System.Collections.Generic;
using Dragging;
using Power;
using UnityEngine;
using Zenject;

public class LevelInstaller : MonoInstaller
{
	public override void InstallBindings ()
	{
		Container.BindInterfacesAndSelfTo<FingerTracker> ().FromNew ().AsSingle ();
		Container.Bind<PowerController> ().FromNew ().AsSingle ();
	}
}

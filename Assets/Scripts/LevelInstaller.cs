using System.Collections;
using System.Collections.Generic;
using Dragging;
using Power;
using UnityEngine;
using Zenject;

public class LevelInstaller : MonoInstaller
{
	[SerializeField]
	private float loadSceneTime;

	public override void InstallBindings ()
	{
		Container.BindInterfacesAndSelfTo<FingerTracker> ().FromNew ().AsSingle ();
		Container.BindInterfacesAndSelfTo<PowerController> ().FromNew ().AsSingle ();
		Container.Bind<UploadUIController> ().FromComponentInHierarchy ().AsSingle ();
		Container.Bind<MainframeController> ().FromComponentInHierarchy ().AsSingle ();
		Container.BindInterfacesAndSelfTo<GameManager> ().FromNew ().AsSingle ().WithArguments (loadSceneTime);
	}
}

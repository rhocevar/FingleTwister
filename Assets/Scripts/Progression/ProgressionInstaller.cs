using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using Zenject;

public class ProgressionInstaller: MonoInstaller, ICoroutineExecuter
{
	[SerializeField]
	private string[] levels;
	[SerializeField]
	private string startScene;

	public override void InstallBindings ()
	{
		Container.Bind<LevelManager> ().FromNew ().AsSingle ().WithArguments (levels, startScene);
		Container.Bind<ICoroutineExecuter> ().FromInstance (this).AsSingle ();
	}
}

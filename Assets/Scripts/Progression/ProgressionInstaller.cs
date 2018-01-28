using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using Zenject;

public class ProgressionInstaller: MonoInstaller, ICoroutineExecuter
{
	[SerializeField]
	private string[] levels;

	public override void InstallBindings ()
	{
		Container.Bind<LevelManager> ().FromNew ().AsSingle ().WithArguments (levels);
		Container.Bind<ICoroutineExecuter> ().FromInstance (this).AsSingle ();
	}
}

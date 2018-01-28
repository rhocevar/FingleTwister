using System.Collections;
using System.Collections.Generic;
using Audio;
using UnityEngine;
using Utils;
using Zenject;

public class GlobalInstaller: MonoInstaller, ICoroutineExecuter
{
	[SerializeField]
	private string[] levels;
    [SerializeField]
    private AudioManager audio;
	[SerializeField]
	private string startScene;

	public override void InstallBindings ()
	{
		Container.Bind<LevelManager> ().FromNew ().AsSingle ().WithArguments (levels, startScene);
		Container.Bind<ICoroutineExecuter> ().FromInstance (this).AsSingle ();
        Container.Bind<AudioManager>().FromInstance(audio).AsSingle();
	}
}

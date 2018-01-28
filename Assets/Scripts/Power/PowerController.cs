using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Power
{
	public class PowerController: IInitializable 
	{
		public event Action<bool> OnPowerChanged;
		
		private int totalSwitches;
		private int onSwitches;

		public bool IsOn 
		{
			get; private set;
		}

		public void AddSwitche ()
		{
			totalSwitches++;
		}

		public void Initialize()
        {
            SetPowerState (onSwitches == totalSwitches);
			
        }

		public void TurnOnSwitche ()
		{
			onSwitches++;
			if (!IsOn && onSwitches == totalSwitches)
				SetPowerState (true);
		}

        public void TurnOffSwitche ()
		{
			onSwitches--;
			if (IsOn && onSwitches != totalSwitches)
				SetPowerState (false);
		}

		private void SetPowerState(bool isOn)
        {
			IsOn = isOn;
			if (OnPowerChanged != null)
				OnPowerChanged (IsOn);
        }
    }
}
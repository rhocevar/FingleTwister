using System;
using System.Collections;
using System.Collections.Generic;
using Audio;
using UnityEngine;

namespace Power
{
	public class PowerController 
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
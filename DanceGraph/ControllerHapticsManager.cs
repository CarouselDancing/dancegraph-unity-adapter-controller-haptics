using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace DanceGraph
{
	public class ControllerHapticsManager : MonoBehaviour
	{
		public List<ActionBasedController> xrControllers = new();

		void FindAndStoreControllers()
		{
			foreach (var go in Resources.FindObjectsOfTypeAll(typeof(GameObject)).Where(go_ => go_.GetComponent<ActionBasedController>() != null).Select(go_ => ((GameObject)go_)))
			{
				var cmp = go.GetComponent<ControllerHaptics>();
				if(cmp == null)
					cmp = go.AddComponent<ControllerHaptics>();
				cmp.Init(gameObject);
			}

		}

		private void Start()
		{
			FindAndStoreControllers();
		}
	}
}
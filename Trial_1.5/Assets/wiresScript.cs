using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;

public class wiresScript : MonoBehaviour {

	// Module items
	public KMBombInfo Bomb;
	public KMAudio Audio;
	public KMSelectable wire1;
	public KMSelectable wire2;

	private static int moduleIdCounter = 1;
	private int moduleId = 0;
	private bool moduleSolved = false;

	void Start() {
		var mission = ScriptableObject.CreateInstance<KMMission>();
        mission.PacingEventsEnabled = false;
		moduleId = moduleIdCounter++;

		wire1.OnInteract += delegate () { CutWrong(wire1); return false; };
		wire2.OnInteract += delegate () { CutWire2(wire2); return false; };
	}

	//Correct wire was cut, module is solved
	void CutWire2(KMSelectable wire) {
		if (moduleSolved) return;
		Debug.Log("Wire 1 was cut! Module solved.");
		//Solved module
		wire.AddInteractionPunch();
		moduleSolved = true;
		//audioRef = GetComponent<KMAudio>().PlayGameSoundAtTransformWithRef(KMSoundOverride.SoundEffect.AlarmClockBeep, transform);
		GetComponent<KMBombModule>().HandlePass();

	}

	//Wrong wire was cut, handle strikes
	void CutWrong(KMSelectable wire) {
		if (moduleSolved) return;
		wire.AddInteractionPunch();
		//audioRef = GetComponent<KMAudio>().PlaySoundAtTransformWithRef("doublebeep125", transform);
		Debug.Log("Wrong wire was cut! You got a strike.");
		GetComponent<KMBombModule>().HandleStrike();
	}
}

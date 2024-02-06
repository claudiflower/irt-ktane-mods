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
	public KMSelectable wire3;
	public KMSelectable wire4;

	private static int moduleIdCounter = 1;
	private bool moduleSolved = false;
	private int correctWires = 0;
	private int prevCut = 0;

	void Start() {
		var mission = ScriptableObject.CreateInstance<KMMission>();
        mission.PacingEventsEnabled = false;
		moduleIdCounter++;

		wire1.OnInteract += delegate () { CutWrong(wire1); return false; };
		wire2.OnInteract += delegate () { CutWrong(wire2); return false; };
		wire3.OnInteract += delegate () { CutRight(3, wire3); return false; };
		wire4.OnInteract += delegate () { CutRight(4, wire4); return false; };
	}

	//Correct wire was cut, module is solved
	void CutRight(int selected, KMSelectable wire) {
		if (moduleSolved) return;
		wire.AddInteractionPunch();
		if (prevCut!=selected) {
			prevCut = selected;
			correctWires++;
		}
		//Solved module
		if (correctWires == 2) {
			moduleSolved = true;
			Debug.Log("Right wires were cut! Module completed.");
			GetComponent<KMBombModule>().HandlePass();
		}
	}

	//Wrong wire was cut, handle strikes
	void CutWrong(KMSelectable wire) {
		if (moduleSolved) return;
		Debug.Log("Wrong wire was cut! You got a strike.");
		wire.AddInteractionPunch();
		GetComponent<KMBombModule>().HandleStrike();
	}
}

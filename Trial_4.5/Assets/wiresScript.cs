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
	public KMSelectable wire5;

	private static int moduleIdCounter = 1;
	private bool moduleSolved = false;

	void Start() {
		moduleIdCounter++;

		wire1.OnInteract += delegate () { CutWrong(wire1); return false; };
		wire2.OnInteract += delegate () { CutWrong(wire2); return false; };
		wire3.OnInteract += delegate () { CutWrong(wire3); return false; };
		wire4.OnInteract += delegate () { CutWrong(wire4); return false; };
		wire5.OnInteract += delegate () { CutRight(wire5); return false; };
	}

	//Correct wire was cut, module is solved
	void CutRight(KMSelectable wire) {
		if (moduleSolved) return;
		wire.AddInteractionPunch();
		//Solved module
		moduleSolved = true;
		GetComponent<KMBombModule>().HandlePass();
		Debug.Log("Wire 1 was cut! Module solved.");
	}

	//Wrong wire was cut, handle strikes
	void CutWrong(KMSelectable wire) {
		if (moduleSolved) return;
		wire.AddInteractionPunch();
		Debug.Log("Wrong wire was cut! You got a strike.");
		GetComponent<KMBombModule>().HandleStrike();
	}
}

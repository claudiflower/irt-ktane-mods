using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Linq;
using KModkit;

public class colorsScript : MonoBehaviour {
	public KMBombInfo Bomb;
	public KMSelectable button1;
    public KMSelectable button2;
    public KMSelectable button3;
	private bool moduleSolved = false;
	
	// Use this for initialization
	void Start () {
        var mission = ScriptableObject.CreateInstance<KMMission>();
        mission.PacingEventsEnabled = false;
		button1.OnInteract += delegate () { OnPress(false, button1); return false; };
        button2.OnInteract += delegate () { OnPress(false, button2); return false; };
        button3.OnInteract += delegate () { OnPress(true, button3); return false; };
	}

    void OnPress(bool correctButton, KMSelectable button) {
        if (moduleSolved) return;
        Debug.Log("Pressed " + (correctButton?"correct":"wrong") + " button");
        button.AddInteractionPunch();
        if (correctButton) {
            moduleSolved = true;
            GetComponent<KMBombModule>().HandlePass();
        } else {
            GetComponent<KMBombModule>().HandleStrike();
        }
    }
		
}

using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Linq;
using KModkit;

public class colorsScript : MonoBehaviour {
	public KMBombInfo Bomb;
	public KMSelectable button1; // blue
    public KMSelectable button2; // pink
    public KMSelectable button3; // pink
    public KMSelectable button4; // orange
    public KMSelectable button5; // purple
	private bool moduleSolved = false;
    private int timesPressed = 0;
	
	// Use this for initialization
	void Start () {
        var mission = ScriptableObject.CreateInstance<KMMission>();
        mission.PacingEventsEnabled = false;
		button1.OnInteract += delegate () { OnPress(true, button1); return false; };
        button2.OnInteract += delegate () { OnPress(false, button2); return false; };
        button3.OnInteract += delegate () { OnPress(false, button3); return false; };
        button4.OnInteract += delegate () { OnPress(false, button4); return false; };
        button5.OnInteract += delegate () { OnPress(false, button5); return false; };
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

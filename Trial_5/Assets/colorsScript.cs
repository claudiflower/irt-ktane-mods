using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Linq;
using KModkit;

public class colorsScript : MonoBehaviour {
	public KMBombInfo Bomb;
	public KMSelectable button1; // orange
    public KMSelectable button2; // green
    public KMSelectable button3; // yellow
    public KMSelectable button4; // white
    public KMSelectable button5; // blue
	private bool moduleSolved = false;
    private int timesPressed = 0;
    private bool selected3 = false;
	
	// Use this for initialization
	void Start () {
        var mission = ScriptableObject.CreateInstance<KMMission>();
        mission.PacingEventsEnabled = false;

		button1.OnInteract += delegate () { OnPress(false, 1, button1); return false; };
        button2.OnInteract += delegate () { OnPress(false, 2, button2); return false; };
        button3.OnInteract += delegate () { OnPress(true, 3, button3); return false; };
        button4.OnInteract += delegate () { OnPress(true, 4, button4); return false; };
        button5.OnInteract += delegate () { OnPress(false, 5, button5); return false; };
	}

    void OnPress(bool correctButton, int x, KMSelectable button) {
        if (moduleSolved) return;
        Debug.Log("Pressed " + (correctButton?"correct":"wrong") + " button");
        
        button.AddInteractionPunch();
        if (correctButton && x==4) {
            timesPressed++;
            if (timesPressed==2 && selected3) {
                moduleSolved = true;
                GetComponent<KMBombModule>().HandlePass();
            }
        } else if (correctButton && x==3) {
            selected3=true;
            if (timesPressed==2 && selected3) {
                moduleSolved = true;
                GetComponent<KMBombModule>().HandlePass();
            }
        } else {
            GetComponent<KMBombModule>().HandleStrike();
        }
    }
		
}

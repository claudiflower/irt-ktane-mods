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
    public KMSelectable button4; 
    public KMSelectable button5;
	private bool moduleSolved = false;
    private int timesPressed = 0;
	
	// Use this for initialization
	void Start () {
        var mission = ScriptableObject.CreateInstance<KMMission>();
        mission.PacingEventsEnabled = false;

		button1.OnInteract += delegate () { OnPress(true, 1, button1); return false; };
        button2.OnInteract += delegate () { OnPress(false, 2, button2); return false; };
        button3.OnInteract += delegate () { OnPress(false, 3, button3); return false; };
        button4.OnInteract += delegate () { OnPress(false, 4, button4); return false; };
        button5.OnInteract += delegate () { OnPress(false, 5, button5); return false; };
	}

    void OnPress(bool correctButton, int x, KMSelectable button) {
        if (moduleSolved) return;
        Debug.Log("Pressed " + (correctButton?"correct":"wrong") + " button");
        
        button.AddInteractionPunch();
        if (correctButton && x==1) {
            timesPressed++;
            if (timesPressed==2) {
                moduleSolved = true;
                GetComponent<KMBombModule>().HandlePass();
            }
        } else {
            GetComponent<KMBombModule>().HandleStrike();
        }
    }
		
}

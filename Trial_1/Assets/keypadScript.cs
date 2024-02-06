using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Linq;
using KModkit;

public class keypadScript : MonoBehaviour {
    public KMBombInfo Bomb;
	public KMAudio Audio;
    public KMSelectable button1;
    public KMSelectable button2;
    public KMSelectable button3;
    public KMSelectable button4;
    public KMSelectable button5;
    public KMSelectable button6;
    public KMSelectable button7;
    public KMSelectable button8;
    public KMSelectable button9;
    public KMSelectable button0;
    public KMSelectable clear;
    int answer;
    private bool moduleSolved = false;
    [SerializeField] private TextMesh youTyped;

    void Start()
    {
        var mission = ScriptableObject.CreateInstance<KMMission>();
        mission.PacingEventsEnabled = false;
        Init();
    }

    void Init()
    {
        answer = 8;

        //TextMesh buttonText = buttons[i].GetComponentInChildren<TextMesh>();
        //buttonText.text = label;
        //int j = i;
        button9.OnInteract += delegate () { OnPress("9", button8); return false; };
        button8.OnInteract += delegate () { OnPress("8", button8); return false; };
        button7.OnInteract += delegate () { OnPress("7", button7); return false; };
        button6.OnInteract += delegate () { OnPress("6", button6); return false; };
        button5.OnInteract += delegate () { OnPress("5", button5); return false; };
        button4.OnInteract += delegate () { OnPress("4", button4); return false; };
        button3.OnInteract += delegate () { OnPress("3", button3); return false; };
        button2.OnInteract += delegate () { OnPress("2", button2); return false; };
        button1.OnInteract += delegate () { OnPress("1", button1); return false; };
        button0.OnInteract += delegate () { OnPress("0", button0); return false; };
        clear.OnInteract += delegate () { OnPress("clear", clear); return false; };
    }

    //On pressing button a looped sound will play
    void OnPress(String val, KMSelectable button) {
        youTyped.text = "";
        if (moduleSolved) return;
        // Debug.Log("Pressed " + (correctButton?"correct":"wrong") + " button");
        
        button.AddInteractionPunch();
        youTyped.text += val;
        if (val.CompareTo("8")==0) {
            moduleSolved = true;
            GetComponent<KMBombModule>().HandlePass();
        } else if(val.CompareTo("clear")==0) {
            youTyped.text = "";
        } else {
            GetComponent<KMBombModule>().HandleStrike();
        }
    }
}

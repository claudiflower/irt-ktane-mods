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
    private bool moduleSolved = false;
    private String[] entered = {"-1", "-1", "-1", "-1"};
    private String[] answer = {"E", "2", "Y", "5"};
    [SerializeField] private TextMesh youTyped;

    void Start(){
        var mission = ScriptableObject.CreateInstance<KMMission>();
        mission.PacingEventsEnabled = false;
        Init();
    } 

    void Init() {

        //TextMesh buttonText = buttons[i].GetComponentInChildren<TextMesh>();
        //buttonText.text = label;
        //int j = i;
        button9.OnInteract += delegate () { OnPress("Y", button9); return false; };
        button8.OnInteract += delegate () { OnPress("C", button8); return false; };
        button7.OnInteract += delegate () { OnPress("ZY", button7); return false; };
        button6.OnInteract += delegate () { OnPress("5", button6); return false; };
        button5.OnInteract += delegate () { OnPress("E", button5); return false; };
        button4.OnInteract += delegate () { OnPress("2", button4); return false; };
        button3.OnInteract += delegate () { OnPress("9", button3); return false; };
        button2.OnInteract += delegate () { OnPress("4", button2); return false; };
        button1.OnInteract += delegate () { OnPress("1", button1); return false; };
        button0.OnInteract += delegate () { OnPress("L", button0); return false; };
        clear.OnInteract += delegate () { OnPress("clear", clear); return false; };
    }

    void OnPress(String x,  KMSelectable button) {
        if (moduleSolved) return;
        
        button.AddInteractionPunch();
        for (int i=0; i<entered.Length; i++) {
            if ((entered[i].CompareTo("-1")==0) && (x.CompareTo("clear")!=0)) {
                entered[i] = x;
                youTyped.text += x;
                break;
            }
        }

        // Clear
        if (x.CompareTo("clear")==0) {
            Clear();
        }

        // If the array of answers does not contain -1, compare to answer
        if (Array.IndexOf(entered, "-1")<=-1) {
            for (int i=0; i<entered.Length; i++) {
                // if any value not equal to the answer, STRIKE & RESET
                if (entered[i].CompareTo(answer[i]) != 0) {
                    GetComponent<KMBombModule>().HandleStrike();
                    Debug.Log("Wrong sequence entered! Strike.");
                    Clear();
                    return;
                }
            }
            moduleSolved = true;
            Debug.Log("Correct sequence entered! Module passed.");
            GetComponent<KMBombModule>().HandlePass();
        }
    }

    void Clear() {
    Debug.Log("Cleared");
    entered[0] = "-1";
    entered[1] = "-1";
    entered[2] = "-1";
    entered[3] = "-1";
    youTyped.text = "";
    }
}

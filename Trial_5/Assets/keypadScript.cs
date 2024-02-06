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
    private String[] answer = {"symb5", "L", "6", "M"};
    [SerializeField] private TextMesh youTyped;

    void Start(){
        var mission = ScriptableObject.CreateInstance<KMMission>();
        mission.PacingEventsEnabled = false;
        Init();
    } 

    void Init() {
        // buttonText.text = label;
        // int j = i;
        button9.OnInteract += delegate () { OnPress("R", "R", button9); return false; };
        button8.OnInteract += delegate () { OnPress("S", "S", button8); return false; };
        button7.OnInteract += delegate () { OnPress("M", "M", button7); return false; };
        button6.OnInteract += delegate () { OnPress("6", "6", button6); return false; };
        button5.OnInteract += delegate () { OnPress("symb5", "[symbol]", button5); return false; };
        button4.OnInteract += delegate () { OnPress("L", "L", button4); return false; };
        button3.OnInteract += delegate () { OnPress("symb3", "[symbol]", button3); return false; };
        button2.OnInteract += delegate () { OnPress("F", "F", button2); return false; };
        button1.OnInteract += delegate () { OnPress("3", "3", button1); return false; };
        button0.OnInteract += delegate () { OnPress("symb0","[symbol]", button0); return false; };
        clear.OnInteract += delegate () { OnPress("clear", "clear", clear); return false; };

    }

    void OnPress(String tag, String display, KMSelectable button) {
        // TextMesh youTyped = button.GetComponent<TextMesh>();

        // Module already completed
        if (moduleSolved) return;
        button.AddInteractionPunch();

        // Clear
        if (tag.CompareTo("clear")==0) {
            Clear();
        }

        // Store the user's entered buttons
        for (int i=0; i<entered.Length; i++) {
            if ((entered[i].CompareTo("-1")==0) && (tag.CompareTo("clear")!=0)) {
                entered[i] = tag;
                youTyped.text += display;
                break;
            }
        }

        // If the array of answers does not contain -1, check the answer
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

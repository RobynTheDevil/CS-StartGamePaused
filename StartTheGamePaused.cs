using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using SecretHistories;
using SecretHistories.UI;
using SecretHistories.Manifestations;
using SecretHistories.Entities;
using SecretHistories.Spheres;
using SecretHistories.Abstract;
using HarmonyLib;

public class StartTheGamePaused : MonoBehaviour
{

    public static PatchTracker mainPatch {get; private set;}

    public void Start() {
        try
        {
            mainPatch = new PatchTracker("StartTheGamePaused", new MainPatch());
        }
        catch (Exception ex)
        {
          NoonUtility.LogException(ex);
        }
        NoonUtility.Log("StartTheGamePaused: Trackers Started");
    }

    public static void Initialise() {
        Harmony.DEBUG = true;
        Patch.harmony = new Harmony("robynthedevil.startthegamepaused");
		new GameObject().AddComponent<StartTheGamePaused>();
        NoonUtility.Log("StartTheGamePaused: Initialised");
	}

}


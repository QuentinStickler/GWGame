using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents
{
    public static Action<bool> OnInteractingWithMiniGame;
    public static Action OnStopInteractingWithMiniGame;
    public static Action OnFoundRightSolutionToGhostGame;
}

/*
Wieder wechseln:
Light auf Directional setzen, Intensity veringern
Lighting Settings auf Skybox setzen
Skybox in Camera auf Solid Color blau setzen
*/
using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class GameEvents
{
    public static Action<bool> OnInteractingWithMiniGame;
    public static Action OnStopInteractingWithMiniGame;
    public static Action OnFoundRightSolutionToGhostGame;
    public static Action<Dialogue> OnFinishedDialogue;
    public static Action OnPickedUpCollectible;
}

/*
Wieder wechseln:
Light auf Directional setzen, Intensity verringern
Lighting Settings auf Skybox setzen
Skybox in Camera auf Solid Color blau setzen
*/
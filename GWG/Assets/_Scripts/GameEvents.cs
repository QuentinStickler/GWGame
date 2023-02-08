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
    public static Action<bool> OnLoadScene;
}

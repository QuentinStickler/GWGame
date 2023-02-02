using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITriggerable
{
    public event Action Triggered;

    public void OnTriggered();
}

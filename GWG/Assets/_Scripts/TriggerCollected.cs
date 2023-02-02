using System;
using System.Collections;
using UnityEngine;

public class TriggerCollected : CollectibleBehaviour, ITriggerable
{
    public event Action Triggered;
    // [SerializeField] private float _dissapearTime = 0f;

    public void OnTriggered()
    {
        Triggered?.Invoke();
    }

    protected override IEnumerator DestroyText()
    {
        OnTriggered();
        // yield return new WaitForSeconds(_dissapearTime);
        // GetComponent<MeshRenderer>().enabled = false;
        yield break;
    }
}

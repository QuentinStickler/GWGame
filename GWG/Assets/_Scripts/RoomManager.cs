using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] private float _fadeOutTime = 0.5f;
    [SerializeField, Range(0f, 1f)] private float _fadeInTime = 0.5f;
    
    public static RoomManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // DontDestroyOnLoad(gameObject);
    }
    
    public void FadeOut(RoomTrigger room)
    {
        var children = gameObject
            .GetAllChildren(false)
            .Where(e => e.GetComponent<RoomTrigger>() == null)
            .ToList();
        var ignore = room.gameObject.GetAllChildren(true);
        var fade = children.Except(ignore).ToList();
        StartCoroutine(TransparencyManager.Instance.FadeOut(fade, 0f, _fadeOutTime));
    }
    
    public void FadeIn(RoomTrigger room)
    {
        var children  = room.gameObject.GetAllChildren(true);
        StartCoroutine(TransparencyManager.Instance.FadeIn(children, _fadeInTime));
    }
}

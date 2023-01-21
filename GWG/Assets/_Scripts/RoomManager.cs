using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] private float _fadeOutTime = 0.5f;
    [SerializeField, Range(0f, 1f)] private float _fadeInTime = 0.5f;
    private RoomTrigger _currentRoom;
    private List<RoomTrigger> rooms = new List<RoomTrigger>();
    private List<Renderer> renderers = new List<Renderer>();
    
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

        rooms = FindObjectsOfType<RoomTrigger>().ToList();
        foreach (var room in rooms)
        {
            room.RoomEntered += OnEnterRoom;
        }

        // DontDestroyOnLoad(gameObject);
    }

    public void OnEnterRoom(RoomTrigger room)
    {
        _currentRoom = room;
        FadeOut(room);
        FadeIn(room);
    }
    
    private void FadeOut(RoomTrigger ignoredRoom)
    {
        // var rooms = gameObject
        //         .GetComponentsInChildren<RoomTrigger>()
        //         .Where(e => e != ignoredRoom)
        //         .ToList();

        var roomTriggers = gameObject
            .GetComponentsInChildren<RoomTrigger>()
            .Select(e => e.gameObject)
            .ToList();
    
        var rooms = gameObject
            .GetAllChildren(false)
            .Where(e => !roomTriggers.Contains(e) && ((1 << e.layer) & TransparencyManager.Instance.EnvironmentMask) > 0)
            .ToList();
        
        var ignore = ignoredRoom.gameObject.GetAllChildren(true);
        var fade = rooms.Except(ignore).ToList();
        
        foreach (var obj in fade)
        {
            // TransparencyManager.Instance.StartFadeOut(obj, 0,_fadeOutTime);
            TransparencyManager.Instance.StartFade(obj, 0,_fadeOutTime);
        }
    }

    private void FadeIn(RoomTrigger room)
    {
        var children  = room.gameObject.GetAllChildren(true)
            .Where(e => ((1 << e.layer) & TransparencyManager.Instance.EnvironmentMask) > 0);
        foreach (var obj in children)
        {
            // TransparencyManager.Instance.StartFadeIn(obj, _fadeInTime);
            TransparencyManager.Instance.StartFade(obj, 1, _fadeInTime, false, true);
        }
    }

    private void OnDisable()
    {
        foreach (var room in rooms)
        {
            room.RoomEntered -= OnEnterRoom;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using RoomTransparency;
using UnityEngine;

public class WallTransparency : MonoBehaviour
{
    public Camera activeCamera;
    public event Action<Room> onRoomEnter;
    private List<Hideable> currentlyInWay = new List<Hideable>();
    private List<Hideable> currentlyTransparent = new List<Hideable>();
    private Room currentRoom;
    [SerializeField] private LayerMask _layerMask;

    // Update is called once per frame
    void Update()
    {
        collectObjectsInWay(true);
        makeWallsSolid();
        makeWallsTransparent();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.gameObject.TryGetComponent<Room>(out var newRoom) || newRoom.Equals(currentRoom))
            return;
        if (currentRoom != null && currentRoom.isVisible())
            currentRoom.showRoom(false);

        newRoom.showRoom(true);
        currentRoom = newRoom;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.TryGetComponent<Room>(out var newRoom) || newRoom.Equals(currentRoom))
            return;
        if (currentRoom != null && currentRoom.isVisible())
            currentRoom.showRoom(false);

        newRoom.showRoom(true);
        currentRoom = newRoom;
    }

    private void collectObjectsInWay(bool useBackwardRay)
    {
        currentlyInWay.Clear();
        var playerTransform = transform;
        if (playerTransform.Equals(null))
            throw new NullReferenceException("Player does not have a transform");
        var cameraPos = activeCamera.transform.position;


        var camForward = activeCamera.transform.forward;
        var direction = playerTransform.position - cameraPos;
        direction = camForward * Vector3.Dot(camForward, direction);

        var camDistanceToPlayer = Vector3.Magnitude(direction);
        
        // var forwardRay = new Ray(cameraPos, playerTransform.position - cameraPos);
        var forwardRay = new Ray(playerTransform.position - direction, direction);
        // var backwardRay = new Ray(playerTransform.position, cameraPos - playerTransform.position);
        var backwardRay = new Ray(playerTransform.position, - direction);

        var forwardHitArray = Physics.RaycastAll(forwardRay, 0.99f * camDistanceToPlayer, _layerMask);
        var backwardHitArray = Physics.RaycastAll(backwardRay, 0.99f * camDistanceToPlayer, _layerMask);

        Debug.DrawRay(playerTransform.position - direction, direction,
            forwardHitArray.Length > 0 || backwardHitArray.Length > 0 ? Color.red : Color.green,
            Time.deltaTime);


        addWalls(forwardHitArray);
        addWalls(backwardHitArray);
    }

    private void addWalls(IEnumerable<RaycastHit> array)
    {
        foreach (var raycastHit in array)
        {
            if (raycastHit.collider.transform.parent &&
                raycastHit.collider.transform.parent.gameObject.TryGetComponent(out Hideable hideable))
            {
                if (!currentlyInWay.Contains(hideable))
                    currentlyInWay.Add(hideable);
            }
        }
    }

    private void makeWallsTransparent()
    {
        for (var i = 0; i < currentlyInWay.Count; i++)
        {
            var wall = currentlyInWay[i];

            if (currentlyTransparent.Contains(wall))
                continue;
            wall.makeTransparent(true);
            currentlyTransparent.Add(wall);
        }
    }

    private void makeWallsSolid()
    {
        for (var i = currentlyTransparent.Count - 1; i >= 0; i--)
        {
            var wall = currentlyTransparent[i];
            if (currentlyInWay.Contains(wall))
                continue;
            wall.makeTransparent(false);
            currentlyTransparent.Remove(wall);
        }
    }
}
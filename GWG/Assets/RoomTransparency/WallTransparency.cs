using System;
using System.Collections;
using System.Collections.Generic;
using RoomTransparency;
using UnityEngine;

public class WallTransparency : MonoBehaviour
{
    public Camera activeCamera;
    private List<HideableWall> currentlyInWay = new List<HideableWall>();
    private List<HideableWall> currentlyTransparent = new List<HideableWall>();

    // Update is called once per frame
    void Update()
    {
        collectObjectsInWay(true);
        makeWallsSolid();
        makeWallsTransparent();
    }

    private void collectObjectsInWay(bool useBackwardRay)
    {
        currentlyInWay.Clear();
        var playerTransform = transform;
        if (playerTransform.Equals(null))
            throw new NullReferenceException("Player does not have a transform");
        var cameraPos = activeCamera.transform.position;

        var camDistanceToPlayer = Vector3.Magnitude(cameraPos - playerTransform.position);

        var forwardRay = new Ray(cameraPos, playerTransform.position - cameraPos);
        var backwardRay = new Ray(playerTransform.position, cameraPos - playerTransform.position);

        var forwardHitArray = Physics.RaycastAll(forwardRay, camDistanceToPlayer);
        var backwardHitArray = Physics.RaycastAll(backwardRay, camDistanceToPlayer);
        
        Debug.DrawRay(cameraPos, playerTransform.position - cameraPos, Color.red, Time.deltaTime);
        

        addWalls(forwardHitArray);
        addWalls(backwardHitArray);
    }

    private void addWalls(IEnumerable<RaycastHit> array)
    {
        foreach (var raycastHit in array)
        {
            if (raycastHit.collider.transform.parent && raycastHit.collider.transform.parent.gameObject.TryGetComponent(out HideableWall wall))
            {
                if (!currentlyInWay.Contains(wall))
                    currentlyInWay.Add(wall);
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
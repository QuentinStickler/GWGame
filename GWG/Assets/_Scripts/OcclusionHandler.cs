using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OcclusionHandler : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField, Range(0f, 1f)] private float _fadeOutTime = 0.2f;
    [SerializeField, Range(0f, 1f)] private float _fadeOutOpacity = 0.5f;
    [SerializeField, Range(0f, 1f)] private float _fadeInTime = 0.2f;
    private Transform _camera;
    private List<GameObject> _occludors = new List<GameObject>();
    private List<GameObject> _occludorsLast = new List<GameObject>();

    private void Awake()
    {
        _camera = Camera.main.transform;
    }

    void Update()
    {
        var camForward = _camera.forward;
        var direction = _player.position - _camera.position;
        direction = camForward * Vector3.Dot(camForward, direction);
        
        var hits =
        // Physics.RaycastAll(_player.position, -1 * direction, direction.magnitude, _layerMask);
        Physics.SphereCastAll(_player.position - direction, 0.49f , direction, direction.magnitude, TransparencyManager.Instance.EnvironmentMask);

        // For debugging purposes, comment out when not needed
        Debug.DrawLine(_player.position, _player.position - direction, hits.Length > 0 ? Color.red : Color.green);

        _occludors = hits.Select(e => e.collider.gameObject).ToList();

        var removed = _occludorsLast.Except(_occludors).ToList();
        var added = _occludors.Except(_occludorsLast).ToList();
        // var stayed = _occludors.Except(added).ToList();

        foreach (var obj in added)
        {
            foreach (var child in obj.GetAllChildren(true))
            {
                // TransparencyManager.Instance.StartFadeOut(child, _fadeOutOpacity,_fadeOutTime);
                TransparencyManager.Instance.StartFade(child, _fadeOutOpacity, _fadeOutTime, true);
            }
        }

        foreach (var obj in removed)
        {
            foreach (var child in obj.GetAllChildren(true))
            {
                // TransparencyManager.Instance.StartFadeIn(child, _fadeInTime);
                TransparencyManager.Instance.StartFade(child, 1, _fadeInTime);
            }
        }


        _occludorsLast = new List<GameObject>(_occludors);
        _occludors.Clear();
    }

    // private void OnCameraChanged()
    // {
    //     _camera = Camera.main;
    // }
}

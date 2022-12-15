using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OcclusionHandler : MonoBehaviour
{
    [SerializeField] private Transform _player;
    // private Transform _camera;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField, Range(0f, 1f)] private float _fadeOutTime = 0.2f;
    [SerializeField, Range(0f, 1f)] private float _fadeOutOpacity = 0.5f;
    [SerializeField, Range(0f, 1f)] private float _fadeInTime = 0.2f;
    private List<GameObject> occludors = new List<GameObject>();
    private List<GameObject> occludorsLast = new List<GameObject>();

    void Update()
    {
        // _camera = Camera.main.transform;
        var direction = _player.position - transform.position;
        // var direction = _player.position - _camera.position;
        var hits =
        Physics.RaycastAll(transform.position, direction, direction.magnitude, _layerMask);
        // Physics.RaycastAll(_camera.position, direction, direction.magnitude, _layerMask);

        // For debugging purposes, comment out when not needed
        Debug.DrawLine(transform.position, _player.position, hits.Length > 0 ? Color.red : Color.green);
        // Debug.DrawLine(_camera.position, _player.position, hits.Length > 0 ? Color.red : Color.green);

        occludors = hits.Select(e => e.collider.gameObject).ToList();

        var removed = occludorsLast.Except(occludors).ToList();
        var added = occludors.Except(occludorsLast).ToList();

        if(added.Count > 0)
            StartCoroutine(TransparencyManager.Instance.FadeOut(added, _fadeOutOpacity, _fadeOutTime));
        if(removed.Count > 0)
            StartCoroutine(TransparencyManager.Instance.FadeIn(removed, _fadeInTime));

        occludorsLast = new List<GameObject>(occludors);
        occludors.Clear();
    }

    // private void OnCameraChanged()
    // {
    //     _camera = Camera.main;
    // }
}

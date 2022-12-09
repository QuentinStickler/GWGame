using UnityEngine;

public class CameraController : MonoBehaviour

//Potentielles Problem: Camera Clipping, Bugs wenn man in ein Haus geht etc.
{
    #region Variables
    
    private Vector3 _offset;
    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime;
    private Vector3 _currentVelocity = Vector3.zero;
        
    #endregion
    private void Awake() => _offset = transform.position - target.position;

    private void LateUpdate()
    {
        Vector3 targetPosition = target.position + _offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, smoothTime);
    }
}
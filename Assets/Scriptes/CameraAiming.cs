using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAiming : MonoBehaviour
{
    private bool _isZoomingOut;
    private float _targetFOV;
    private float _targetOffset;
    public float zoomSpeed = 10;
    public float startFOV = 4;
    public float offsetStart = 0.45f;
    public float offsetEnd;
    public float zoomedOutFOV = 6;
    public Cinemachine.CinemachineFreeLook freeLookCamera;
    private Cinemachine.CinemachineComposer _composer;

    // Start is called before the first frame update
    void Start()
    {
        _targetFOV = startFOV;
        _targetOffset = offsetStart;
        _composer = freeLookCamera.GetRig(1).GetCinemachineComponent<Cinemachine.CinemachineComposer>();
    }

    // Update is called once per frame
    void Update()
    {
        _isZoomingOut = Input.GetKey(KeyCode.W);

        _targetFOV = _isZoomingOut ? zoomedOutFOV : startFOV;
        _targetOffset = _isZoomingOut ? offsetEnd : offsetStart;

        float newFOV = Mathf.Lerp(freeLookCamera.m_Orbits[1].m_Radius, _targetFOV,
            Time.deltaTime * zoomSpeed);
        float newOffset = Mathf.Lerp(_composer.m_TrackedObjectOffset.x, _targetOffset,
            Time.deltaTime * zoomSpeed);

        _composer.m_TrackedObjectOffset.x = newOffset;
        freeLookCamera.m_Orbits[1].m_Radius = newFOV;

    }
}

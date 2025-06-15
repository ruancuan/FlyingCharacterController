using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyController : MonoBehaviour
{
    public float moveSpeed=10;
    public float maxFloatHeight = 1000;
    public float minFloatHeight = 0f;
    public Camera freeLookCamera;
    private Animator _animator;
    private float _currentHeight;
    private float _xRotation;

    private string _flyAnimBool = "_isFlying";

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _currentHeight = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        _xRotation = freeLookCamera.transform.eulerAngles.x;

        if (Input.GetKey(KeyCode.W))
        {
            MoveCharacter();
        }
        else
        {
            DisableMovement();
        }

        _currentHeight = Mathf.Clamp(transform.position.y, _currentHeight, maxFloatHeight);
        transform.position = new Vector3(transform.position.x, _currentHeight, transform.position.z);
    }

    private void MoveCharacter()
    {
        Vector3 cameraForward = new Vector3(freeLookCamera.transform.forward.x, 0, freeLookCamera.transform.forward.z);
        transform.rotation = Quaternion.LookRotation(cameraForward);
        transform.Rotate(new Vector3(_xRotation, 0, 0), Space.Self);

        _animator.SetBool(_flyAnimBool, true);

        Vector3 forward = freeLookCamera.transform.forward;
        Vector3 flyDirection = forward.normalized;

        _currentHeight += flyDirection.y * moveSpeed * Time.deltaTime;
        _currentHeight = Mathf.Clamp(_currentHeight, minFloatHeight, maxFloatHeight);

        transform.position += flyDirection * moveSpeed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, _currentHeight, transform.position.z);

    }

    private void DisableMovement()
    {
        _animator.SetBool(_flyAnimBool, false);
        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
    }
}

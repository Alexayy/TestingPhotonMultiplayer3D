using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    [SerializeField] private GameObject CameraLookAt;
    
    public Camera myCamera;
    public float speed = 1.5f;
    public float sprintSpeed = 5f;
    private float _rotationSpeed = 15f;
    private float _animationSpeed = 0f;
    public float animationBlendSPeed = 2f;
    private bool _isSprinting = false;

    private Animator _ellenAnimator;

    private CharacterController _myController;

    private float _rotation;
    private float _speedY = 0;
    private float _gravity = -9.81f;

    private bool isJumping = false;
    public float jumpSpeed = 15;

    private PhotonView _view;

    // Start is called before the first frame update
    void Start()
    {
        myCamera = Camera.main;
        _myController = GetComponent<CharacterController>();
        _ellenAnimator = GetComponent<Animator>();
        _view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_view.IsMine)
        {
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");

            if (Input.GetButton("Jump") && !isJumping)
            {
                isJumping = true;
                _ellenAnimator.SetTrigger("Jump");
                _speedY += jumpSpeed;
            }

            if (!_myController.isGrounded)
            {
                _speedY += _gravity * Time.deltaTime;
            }
            else if (_speedY < 0)
            {
                _speedY = 0;
            }

            _ellenAnimator.SetFloat("SpeedY", _speedY / jumpSpeed);

            if (isJumping && _speedY < 0)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, Vector3.down, out hit, .5f, LayerMask.GetMask("Default")))
                {
                    isJumping = false;
                    _ellenAnimator.SetTrigger("Land");
                }
            }

            _isSprinting = Input.GetKey(KeyCode.LeftShift);

            Vector3 movement = new Vector3(x, 0, z).normalized;

            Vector3 cameraOrientationMove =
                Quaternion.Euler(0, myCamera.transform.rotation.eulerAngles.y, 0) * movement;
            Vector3 verticalMovement = Vector3.up * _speedY;

            _myController.Move((verticalMovement + (cameraOrientationMove * (_isSprinting ? sprintSpeed : speed))) *
                               Time.deltaTime);

            if (cameraOrientationMove.magnitude > 0)
            {
                _rotation = Mathf.Atan2(cameraOrientationMove.x, cameraOrientationMove.z) * Mathf.Rad2Deg;
                _animationSpeed = _isSprinting ? 1 : .5f;
            }
            else
            {
                _animationSpeed = 0;
            }

            _ellenAnimator.SetFloat("Speed",
                Mathf.Lerp(_ellenAnimator.GetFloat("Speed"), _animationSpeed, animationBlendSPeed * Time.deltaTime));

            Quaternion currentRot = transform.rotation;
            Quaternion targetRot = Quaternion.Euler(0, _rotation, 0);
            transform.rotation = Quaternion.Lerp(currentRot, targetRot, _rotationSpeed * Time.deltaTime);
        }
    }

    public GameObject LookAt()
    {
        return CameraLookAt;
    }
}
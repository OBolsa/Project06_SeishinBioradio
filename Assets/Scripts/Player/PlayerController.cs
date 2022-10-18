using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using SaveSystem;

public class PlayerController : MonoBehaviour, ISaveable
{
    [Header("Components")]
    [SerializeField] private CharacterController m_Controller;
    [SerializeField] private Transform m_Cam;
    [SerializeField] private PlayerInputActions m_PlayerControls;
    private static PlayerController PlayerControllerInstance;

    [Header("Movement")]
    [SerializeField] private float m_Speed;
    public Transform m_PlayerFeet;
    private InputAction walk;
    private Vector3 moveDir = Vector3.zero;
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    [Header("Gravity")]
    [SerializeField] private float m_GravityScale;
    [SerializeField] private float m_MaxGravity;
    private float currentGravity;

    private Vector3 gravityDirection = Vector3.down;
    private Vector3 gravityMovement;

    [Header("Impact")]
    [SerializeField] float m_PlayerMass = 3f;
    private Vector3 impact = Vector3.zero;

    private void Awake()
    {
        m_PlayerControls = new PlayerInputActions();
        PlayerControllerInstance = this;
    }

    private void OnEnable()
    {
        walk = m_PlayerControls.PlayerDefault.Walk;
        walk.Enable();
    }

    private void OnDisable()
    {
        walk.Disable();
    }

    private void Update()
    {
        if(m_Controller.enabled)
            UpdateDirection();

        if (impact.magnitude > 0.2) 
            m_Controller.Move(impact * Time.deltaTime);

        impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if(m_Controller.enabled)
            m_Controller.Move((moveDir.normalized + gravityMovement) * m_Speed/100);
    }

    public void AddForce(Vector3 direction, float force)
    {
        direction.Normalize();
        if (direction.y < 0) direction.y = -direction.y;
        impact += direction.normalized * force / m_PlayerMass;
    }

    private void UpdateDirection()
    {
        CalculateGravity();

        float horizontal = walk.ReadValue<Vector2>().x;
        float vertical = walk.ReadValue<Vector2>().y;
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if(direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + m_Cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }
        else
        {
            moveDir = Vector3.zero;
        }
    }

    public static bool IsGrounded()
    {
        return PlayerControllerInstance.m_Controller.isGrounded;
    }

    private void CalculateGravity()
    {
        if (IsGrounded())
        {
            currentGravity = 0;
        }
        else
        {
            if(currentGravity > m_MaxGravity)
            {
                currentGravity -= m_GravityScale * Time.deltaTime;
            }
            else
                currentGravity = m_MaxGravity;

            gravityMovement = gravityDirection * -currentGravity;
        }
    }

    public object CaptureState()
    {
        return new SaveData
        {
            xPos = transform.position.x,
            yPos = transform.position.y,
            zPos = transform.position.z,
            xRot = transform.rotation.x,
            yRot = transform.rotation.y,
            zRot = transform.rotation.z
        };
    }

    public void RestoreState(object state)
    {
        var savedData = (SaveData)state;

        GetComponent<CharacterController>().enabled = false;
        transform.SetPositionAndRotation(new Vector3(savedData.xPos, savedData.yPos, savedData.zPos), Quaternion.Euler(savedData.xRot, savedData.yRot, savedData.zRot));
        GetComponent<CharacterController>().enabled = true;
    }

    [System.Serializable]
    private struct SaveData
    {
        public float xPos;
        public float yPos;
        public float zPos;
        public float xRot;
        public float yRot;
        public float zRot;
    }
}
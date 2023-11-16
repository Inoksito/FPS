using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Movement variables
    [Header("Movement")]
    public float speed;
    public float jumpForce;

    [Header("Camera")]
    public float mouseSensibility;
    public float maxViewX;
    public float minViewX;
    private float rotationX;

    //Lives
    [SerializeField] private int currentLives;
    [SerializeField] private int maxLives;

    //Components
    private Rigidbody rb;
    private Camera camera;

    private WeaponController weaponController;

    public int CurrentLives { get => currentLives; set => currentLives = value; }
    public int MaxLives { get => maxLives; set => maxLives = value; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        camera =Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        weaponController=GetComponent<WeaponController>();
    }

    /// <summary>
    /// PLayer movement input controller
    /// </summary>
    private void MovePlayer()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 direction = (transform.right * x + transform.forward * z).normalized * speed;
        direction.y = rb.velocity.y;

        rb.velocity = direction;
    }

    private void FixedUpdate()
    {
        MovePlayer();
      
    }
    private void Update()
    {
        if (Time.timeScale != 0)
        {
           CameraView();
        }
     
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if (Input.GetButton("Fire1"))
        {
            if (weaponController.CanShoot())
            {
                weaponController.Shoot();
            }
        }
    }

    /// <summary>
    /// Jump action
    /// </summary>
    private void Jump()
    {
        //Throw a ray down direction from player position
        Ray ray = new Ray(transform.position,Vector3.down);

        //if the ray collide with something at 1.1m then apply force
        if (Physics.Raycast(ray, 1.1f))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    /// <summary>
    /// Control camera view with mouse
    /// </summary>
    private void CameraView()
    {
        //Get Mouse input x and y axis
        float y = Input.GetAxis("Mouse X") * mouseSensibility;
        rotationX += Input.GetAxis("Mouse Y") * mouseSensibility;

        //cut x rotation with min and max limits
        rotationX = Mathf.Clamp(rotationX,minViewX,maxViewX);

        //Rotate the camera
        camera.transform.localRotation = Quaternion.Euler(-rotationX,0,0);

        //Rotate the player
        transform.eulerAngles += Vector3.up * y;

    }
    public void DamagePlayer(int damage)
    {
        currentLives -= damage;
        HUDController.instance.UpdateHealthBar(currentLives,true);
        if (currentLives <= 0)
        {
            GameManager.instance.GameOver();
        }
    }
   
}

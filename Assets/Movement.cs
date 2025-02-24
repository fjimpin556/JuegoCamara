using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 7;

    public float groundDrag = 4;

    public float jumpForce = 12;
    public float jumpCooldown = 0.25f;
    public float airMultiplier = 0.4f;

    [Header("Comprobar suelo")]
    public float playerHeight = 2;
    public LayerMask whatIsGround;
    bool grounded;
    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    [Header("Camera control")]
    [SerializeField] GameObject camera1;
    [SerializeField] GameObject camera2;
    bool camera2D = false;

    [Header("Shooting")]
    public GameObject projectilePrefab; // Prefab del proyectil
    public Transform firePoint; // Punto desde donde se disparan los proyectiles
    public GameObject crosshair; // Referencia a la mira
    public float projectileSpeed = 20f;
    [SerializeField] GameObject weapon;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        camera2.SetActive(false);
        crosshair.SetActive(false);
        weapon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

        MyInput();
        SpeedControl();

        // handle drag
        if (grounded)
            rb.linearDamping = groundDrag;
        else
            rb.linearDamping = 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");   

        if (Input.GetKeyDown(KeyCode.E))
        {
            if(!camera2D)
            {
                camera2.SetActive(true);
                crosshair.SetActive(true);
                weapon.SetActive(true);
                camera2D = true;
            }
            else
            {
                camera2.SetActive(false);
                crosshair.SetActive(false);
                weapon.SetActive(false);
                camera2D = false;
            }
        }

        if (Input.GetMouseButtonDown(0)) // Detectar clic izquierdo
        {
            Shoot();
        }
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on ground
        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        // in air
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        // limit velocity if needed
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }

    private void Shoot()
    {
        if (projectilePrefab != null && crosshair != null)
        {
            // Instanciar el proyectil en el firePoint
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

            // Calcular dirección hacia la mira
            Vector3 direction = (crosshair.transform.position - firePoint.position).normalized;

            // Aplicar velocidad al proyectil
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = direction * projectileSpeed;
            }
        }
    }
}

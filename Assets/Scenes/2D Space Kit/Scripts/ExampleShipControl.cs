using UnityEngine;
using System.Collections;

public class ExampleShipControl : MonoBehaviour
{
    public float acceleration_amount = 1f;
    public float rotation_speed = 1f;
    public GameObject turret;
    public float turret_rotation_speed = 3f;

    // Use this for initialization
    void Start()
    {
        // Lock cursor by default (optional)
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Toggle cursor lock/unlock with ESC key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        // Movement controls
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        if (Input.GetKey(KeyCode.W))
            rb.AddForce(transform.up * acceleration_amount * Time.deltaTime);

        if (Input.GetKey(KeyCode.S))
            rb.AddForce(-transform.up * acceleration_amount * Time.deltaTime);

        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift))
            rb.AddForce(-transform.right * acceleration_amount * 0.6f * Time.deltaTime);

        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift))
            rb.AddForce(transform.right * acceleration_amount * 0.6f * Time.deltaTime);

        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.LeftShift))
            rb.AddTorque(-rotation_speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.LeftShift))
            rb.AddTorque(rotation_speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.C))
        {
            rb.angularVelocity = Mathf.Lerp(rb.angularVelocity, 0, rotation_speed * 0.06f * Time.deltaTime);
            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, Vector2.zero, acceleration_amount * 0.06f * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.H))
            transform.position = Vector3.zero;
    }
}

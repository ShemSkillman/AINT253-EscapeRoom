using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] float moveSpeed = 0.1f;
    [SerializeField] float rotationSpeed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * Input.GetAxis("Vertical") * moveSpeed, Space.Self);

        transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * moveSpeed, Space.Self);

        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * rotationSpeed);

        Camera.main.transform.Rotate(Vector3.right * -Input.GetAxis("Mouse Y") * rotationSpeed);

    }
}

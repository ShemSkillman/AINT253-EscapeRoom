using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] float moveSpeed = 0.1f;
    [SerializeField] float rotationSpeed = 0.5f;
    [SerializeField] Transform playerCharacter;
    [SerializeField] Transform eyes;
    [SerializeField] Camera playerCam;

    bool isCrouched = false;

    public bool IsFrozen { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        IsFrozen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsFrozen == true) return;

        if (Input.GetKey(KeyCode.LeftControl))
        {
            Cursor.visible = true;
            return;
        }

        transform.Translate(Vector3.forward * Input.GetAxis("Vertical") * moveSpeed, Space.Self);
        transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * moveSpeed, Space.Self);

        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * rotationSpeed);
        Camera.main.transform.Rotate(Vector3.right * -Input.GetAxis("Mouse Y") * rotationSpeed);

        Cursor.visible = false;
        
        if (Input.GetKeyDown(KeyCode.C))
        {
            isCrouched = !isCrouched;

            if (isCrouched)
            {
                playerCharacter.Rotate(new Vector3(90, 0, 0));
                moveSpeed /= 2;
            }
            else
            {
                moveSpeed *= 2;
                playerCharacter.Rotate(new Vector3(-90, 0, 0));
            }

            playerCam.transform.position = eyes.position;
        }
        
    }
}

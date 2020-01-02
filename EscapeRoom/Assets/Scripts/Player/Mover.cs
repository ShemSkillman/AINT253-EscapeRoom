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
    [SerializeField] float jumpForce = 10f;
    [SerializeField] float jumpCoolDown = 1.5f;

    Rigidbody rb;

    bool canJump = true;
    bool isCrouched = false;

    public bool IsFrozen { get; set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

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

        Cursor.visible = false;
        
        if (Input.GetKeyDown(KeyCode.C))
        {
            isCrouched = !isCrouched;

            if (isCrouched)
            {
                playerCharacter.localScale = new Vector3(playerCharacter.localScale.x * 0.5f, playerCharacter.localScale.y * 0.25f, 
                    playerCharacter.localScale.z * 0.5f);
                moveSpeed /= 2;
            }
            else
            {
                moveSpeed *= 2;
                playerCharacter.localScale = new Vector3(playerCharacter.localScale.x / 0.5f, playerCharacter.localScale.y / 0.25f,
                    playerCharacter.localScale.z / 0.5f);
            }

            playerCam.transform.position = eyes.position;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Jump();
        }
        
    }

    private void Jump()
    {
        if (!canJump || isCrouched) return;
        rb.AddForce(Vector3.up * 10, ForceMode.Impulse);

        StartCoroutine(JumpCoolDown());
    }

    private IEnumerator JumpCoolDown()
    {
        canJump = false;

        yield return new WaitForSeconds(jumpCoolDown);

        canJump = true;
    }


    void FixedUpdate()
    {
        if (IsFrozen) return;

        rb.velocity = (transform.forward * Input.GetAxis("Vertical") * moveSpeed) + (transform.right * Input.GetAxis("Horizontal") * moveSpeed)
            + new Vector3(0, rb.velocity.y, 0);

        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * rotationSpeed);
        Camera.main.transform.Rotate(Vector3.right * -Input.GetAxis("Mouse Y") * rotationSpeed);
    }
}

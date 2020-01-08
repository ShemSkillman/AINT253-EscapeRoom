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
    [SerializeField] RandomAudioPlayer crouchPlayer;
    [SerializeField] RandomAudioPlayer jumpPlayer;

    AudioSource audioSource;

    Rigidbody rb;

    bool canJump = true;
    bool isCrouched = false;
    bool isMoving = false;

    public bool IsFrozen { get; set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        IsFrozen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsFrozen == true)
        {
            audioSource.Pause();
            return;
        }


        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            Cursor.visible = false;
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            audioSource.Pause();
            Cursor.visible = true;
            return;
        }

        print(isMoving);

        if ((Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0) && !isMoving)
        {
            isMoving = true;
            audioSource.Play();
            print("Playing");
        }
        else if (Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") == 0)
        {
            audioSource.Pause();
            isMoving = false;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            isCrouched = !isCrouched;

            if (isCrouched)
            {
                playerCharacter.localScale = new Vector3(playerCharacter.localScale.x * 0.5f, playerCharacter.localScale.y * 0.25f, 
                    playerCharacter.localScale.z * 0.5f);
                moveSpeed /= 2;

                crouchPlayer.PlayRandomAudio();
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

        jumpPlayer.PlayRandomAudio();

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

        if (Input.GetKey(KeyCode.LeftControl))
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            return;
        }

        rb.velocity = (transform.forward * Input.GetAxis("Vertical") * moveSpeed) + (transform.right * Input.GetAxis("Horizontal") * moveSpeed)
            + new Vector3(0, rb.velocity.y, 0);

        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * rotationSpeed);
        Camera.main.transform.Rotate(Vector3.right * -Input.GetAxis("Mouse Y") * rotationSpeed);
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
     [Header("Movement")]
    public Transform Cam;
   public CharacterController controller;

   public float speed = 6f;

   public float turnSmoothTime = 0.1f;

   public float turnSmoothVelocity;
   private Vector3 lastGroundedPosition;
   public float yLimit = -25;
   

 [Header("Jumping")]
     private Vector3 playerVelocity;
    private bool groundedPlayer;
    public float jumpHeight = 1.0f;
    public float gravityValue = -9.81f;

    [Header("Bubble")]
    public GameObject bubblePrefab;
     GameObject currentBubble;
    public Transform bubbleSpawnParent;
    
    [Header("Animations")]
    public Animator playerAnimator;
    
void Start()
{
    if(SceneManager.GetActiveScene().buildIndex == 0)
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
    // Update is called once per frame
    void Update()
    {

#region movement and jumping
float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if(direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y , targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0);

            Vector3 moveDir = Quaternion.Euler(0f,targetAngle, 0f) * Vector3.forward;

            controller.Move(moveDir.normalized * speed * Time.deltaTime);
            playerAnimator.SetBool("isRunning", true);
        }
        else
        {
                    playerAnimator.SetBool("isRunning", false);
        }


        groundedPlayer = controller.isGrounded;
        
        // Jump 
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
            lastGroundedPosition = transform.position;
            
        }

        playerAnimator.SetBool("isGrounded", groundedPlayer);
        


        // Changes the height position of the player..
        if (Input.GetButton("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            playerAnimator.SetTrigger("Jump");
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        #endregion

        #region  restartcurrentScene
        if(Input.GetKeyDown(KeyCode.R))
        {Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }
        #endregion


#region bubble spawner
        if (Input.GetMouseButtonDown(1))
        {
            currentBubble =  Instantiate(bubblePrefab, bubbleSpawnParent.position, bubbleSpawnParent.rotation,bubbleSpawnParent);
            playerAnimator.SetLayerWeight(1,1);
        }
        else if(Input.GetMouseButtonUp(1))
        {
            currentBubble.transform.parent = null;
            currentBubble = null;
             playerAnimator.SetLayerWeight(1,0);
            
        }
        #endregion

        if(transform.position.y < yLimit )
        {
            transform.position = lastGroundedPosition;
        }


    }

    public void cursorHide()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

}

/*
Adrift Among Infinite Stars by Scott Buckley - BackgroundMusic
https://www.youtube.com/watch?v=svWYKzJfVJc ambient sound
*/
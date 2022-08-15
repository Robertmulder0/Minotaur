using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private PlayerInput playerInput;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Transform cameraTransform;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Transform bulletParent;
    [SerializeField]
    private Transform slingTransform;
    
    [SerializeField]
    private float playerSpeed = 5.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;
    [SerializeField]
    private float rotationSpeed = 10f;

    public int slingShotAmmo;
    public TextMeshProUGUI ammoText;

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction shootAction;
    private InputAction leftMouseClick;

    public GameController gameController;

    private void Awake()
    {
        controller = gameObject.GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        cameraTransform = Camera.main.transform;
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        leftMouseClick = new InputAction(binding: "<Mouse>/leftButton");
        leftMouseClick.performed += ctx => ShootGun();
        leftMouseClick.Enable();
    }

    private void onEnable() 
    {
        shootAction.performed += _ => ShootGun();
    }

    private void onDisable()
    {
        shootAction.performed -= _ => ShootGun();
    }

    private void LeftMouseClicked(){
        print ("LeftMouseClicked");
    }

    private void ShootGun() {
        if (slingShotAmmo > 0){
            //raycast to reticle to determine where to shoot shot
            RaycastHit hit;
            GameObject bullet = GameObject.Instantiate(bulletPrefab, slingTransform.position, Quaternion.identity, bulletParent);
            BulletController bulletController = bullet.GetComponent<BulletController>();
            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, Mathf.Infinity)) 
            {
                bulletController.target = hit.point;
                bulletController.hit = true;
            } else {
                bulletController.target = cameraTransform.position + cameraTransform.forward * 25f;
                bulletController.hit = false;
            }
            slingShotAmmo--;
        }
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);
        move = move.x * cameraTransform.right.normalized + move.z * cameraTransform.forward.normalized;
        move.y = 0f;
        controller.Move(move * Time.deltaTime * playerSpeed);


        // Changes the height position of the player
        if (jumpAction.triggered && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        //rotate camera towards player aim
        float targetAngle = cameraTransform.eulerAngles.y;
        Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        //update ammo text
        ammoText.text = "Ammo: " + slingShotAmmo;

        //freeze if game over
        if (gameController.playerLost || gameController.gameWon){
            Disable();
        }
    }

    private void Disable()
    {
        enabled = false;
    }
}
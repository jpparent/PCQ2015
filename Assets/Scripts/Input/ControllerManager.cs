using XboxCtrlrInput;
using UnityEngine;
using System.Collections;

public enum PlayerNumber { player_1, player_2, player_3, player_4 };

public class ControllerManager : MonoBehaviour
{
    

    private Animator animator;

    public float moveSpeed = 5f;
    public float dashBonus = 1f;
    public PlayerNumber playerNum;
    Rigidbody rb;
    public int controllerNum;
    public int hotspot;

    // set controller number pour le hat
    public Chase chase;
    public Hat hat;
    public bool isHat;

    // cooldown de tackle
    public float nextTackle;
    public float tackleRate;

    // Vibration Modifiers
    public float deadzone;
    public float vibrationDuration;
    public float leftMotor;
    public float rightMotor;
    public float vibrationItensity;
    private bool canVibrate;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Use this for initialization
    void Awake()
    {
        // Player Controlls
        tackleRate = 2f;
        nextTackle = 0f;

        // Vibration settings
        deadzone = .95f;
        hotspot = GameObject.FindGameObjectWithTag("RoundManager").GetComponent<RoundManager>().hotspotIndex[0];
        vibrationItensity = .65f;
        vibrationDuration = 3;
        leftMotor = 0f;
        rightMotor = 0f;


        if (isHat)
        {
            gameObject.tag = "PlayerHat";
            hat = gameObject.AddComponent<Hat>() as Hat;
        }
        else
        {
            gameObject.tag = "Player";
            chase = gameObject.AddComponent<Chase>() as Chase;
        }

        // Set Player controllers
        switch (playerNum)
        {
            case PlayerNumber.player_1:
                controllerNum = 1;
                break;
            case PlayerNumber.player_2:
                controllerNum = 2;
                break;
            case PlayerNumber.player_3:
                controllerNum = 3;
                break;
            case PlayerNumber.player_4:
                controllerNum = 4;
                break;
        }

        XCI.DEBUG_LogControllerNames();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        LeftAxisManager();
        VibrationManager();
    }

    void Update()
    {
        if (isHat)
        {
            HatPlayer();
        }
        else
        {
            ChaseTeam();
        }
    }

    void LeftAxisManager()
    {
        Vector3 newPos = transform.position;
        float axisX = XCI.GetAxis(XboxAxis.LeftStickX, controllerNum);
        float axisY = XCI.GetAxis(XboxAxis.LeftStickY, controllerNum);

        // Variable afin de verifier si le stick est activer, peu importe la direction de l'axe
        // A utiliser avec le mecanim
        float axis = Mathf.Abs(axisX) > Mathf.Abs(axisY) ? axisX : axisY;

        animator.SetFloat("PlayerMovementSpeed", Mathf.Abs(axis));


        float newPosX = axisX * moveSpeed * dashBonus;
        float newPosZ = axisY * moveSpeed * dashBonus;
        Vector3 thrust = new Vector3(newPosX, 0, newPosZ);
        //float thrust = moveSpeed * dashBonus;

        newPos = new Vector3(newPosX, newPos.y, newPosZ);

        rb.velocity = thrust;
        //transform.eulerAngles = new Vector3(transform.eulerAngles.x, Mathf.Atan2(axisX, axisY) * Mathf.Rad2Deg, transform.eulerAngles.z);
        if (Mathf.Abs(axis) > 0.2f)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, Mathf.Atan2(axisX, axisY) * Mathf.Rad2Deg, transform.eulerAngles.z);
        }

        //transform.position = newPos;
    }

    void VibrationManager()
    {
        if (canVibrate)
        {
            leftMotor = vibrationItensity;
            rightMotor = vibrationItensity;
            canVibrate = false;
        }

        leftMotor = Mathf.Lerp(leftMotor, 0f, Time.deltaTime * vibrationDuration);
        rightMotor = Mathf.Lerp(rightMotor, 0f, Time.deltaTime * vibrationDuration);

        XCI.SetVibration(controllerNum, leftMotor, rightMotor);
    }

    // Methode pour verifier le spot a aller.
    void HatPlayer()
    {
        float axisX = XCI.GetAxis(XboxAxis.RightStickX, controllerNum);
        float axisY = XCI.GetAxis(XboxAxis.RightStickY, controllerNum);

        bool down = XCI.GetDPad(XboxDPad.Down, controllerNum);
        bool up = XCI.GetDPad(XboxDPad.Up, controllerNum);
        bool left = XCI.GetDPad(XboxDPad.Left, controllerNum);
        bool right = XCI.GetDPad(XboxDPad.Right, controllerNum);

        switch (hotspot)
        {
            case 1:
                if (axisY > deadzone || up)
                    canVibrate = true;
                break;
            case 2:
                if (axisX > deadzone || down)
                    canVibrate = true;
                break;
            case 3:
                if (axisY < -deadzone || left)
                    canVibrate = true;
                break;
            case 4:
                if (axisX < -deadzone || right)
                    canVibrate = true;
                break;
        }
    }

    void ChaseTeam()
    {
        // Assigner a A ou X... A verifier.
        if ((XCI.GetButton(XboxButton.X, controllerNum) || XCI.GetButton(XboxButton.A, controllerNum)) && !isHat && Time.time > nextTackle)
        {
            animator.SetTrigger("PlayerIsTackling");

            gameObject.GetComponent<Chase>().Tackling();
            nextTackle = Time.time + tackleRate;
        }
    }

    void SetHat()
    {
        isHat = true;
    }

    public void setDashSpeed(float value)
    {
        dashBonus = value;
    }
}

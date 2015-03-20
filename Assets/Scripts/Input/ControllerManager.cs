#define DEBUG

using XboxCtrlrInput;
using UnityEngine;
using System.Collections;

public class ControllerManager : MonoBehaviour
{
    public enum PlayerNumber { player_1, player_2, player_3, player_4 };

    public float moveSpeed = 20f;
    public float moveSpeedModifier;
    public float moveSpeedModifierDuration;
    public PlayerNumber playerNum;
    public int controllerNum;
    public int hotspot;

    // set controller number pour le hat
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

    public Animator anim;
    private Vector3 moveDir;

    // Use this for initialization
    void Awake()
    {
        // Player Controlls
        isHat = false;
        tackleRate = 5f;
        nextTackle = 0f;

        // Vibration settings
        deadzone = .95f;
        hotspot = 2;
        vibrationItensity = .65f;
        vibrationDuration = 3;
        leftMotor = 0f;
        rightMotor = 0f;

        gameObject.tag = "Player";
        moveDir = Vector3.zero;

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
            HatPlayer();

        ChaseTeam();
    }

    void LeftAxisManager()
    {
        Vector3 newPos = transform.position;
        float axisX = XCI.GetAxis(XboxAxis.LeftStickX, controllerNum);
        float axisY = XCI.GetAxis(XboxAxis.LeftStickY, controllerNum);
        float axis = Mathf.Abs(axisX) > Mathf.Abs(axisY) ? axisX : axisY;


        float newPosX = newPos.x + (axisX * moveSpeed * Time.deltaTime);
        float newPosZ = newPos.y + (axisY * moveSpeed * Time.deltaTime);

        newPos = new Vector2(newPosX, newPosZ);
        //transform.eulerAngles = new Vector3(transform.eulerAngles.x, Mathf.Atan2(axisX, axisY) * Mathf.Rad2Deg, transform.eulerAngles.z);
        transform.position = newPos;
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
        float axisX = XCI.GetAxis(XboxAxis.RightStickX);
        float axisY = XCI.GetAxis(XboxAxis.RightStickY);


        switch (hotspot)
        {
            case 1:
                if (axisY > deadzone)
                    canVibrate = true;
                break;
            case 2:
                if (axisY < -deadzone)
                    canVibrate = true;
                break;
            case 3:
                if (axisX > deadzone)
                    canVibrate = true;
                break;
            case 4:
                if (axisX < -deadzone)
                    canVibrate = true;
                break;
        }

        if (axisX > .2f || axisX < -.2f || axisY > .2f || axisY < -.2f)
        {
#if DEBUG
            Debug.Log("I am hat");
            Debug.Log("Axis X: " + axisX + " Axis Y: " + axisY);
#endif
        }
    }

    void ChaseTeam()
    {
        // Assigner a A ou X... A verifier.
        bool tackle = false;
        if (!isHat)
            tackle = XCI.GetButton(XboxButton.X) || XCI.GetButton(XboxButton.A);

        if (tackle && Time.time > nextTackle) {
#if DEBUG
        
            Debug.Log("I haz tackled");
#endif
            nextTackle = Time.time + tackleRate;    
        }
    }

    void SetHat()
    {
        isHat = true;
    }

    // Reinitialize all
    void Reset()
    {
        isHat = false;
    }
}

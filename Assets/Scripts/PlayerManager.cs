using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Transform playerCam;
    [SerializeField] private Transform uiFacingAngle;
    
    [Header("Switching Hand")]
    [SerializeField] private InputAction rayHandSwitch;
    [SerializeField] private Transform rayHand, normalHand;

    
    [Header("UI Actions")]
    [SerializeField] private InputAction timerAction;
    [SerializeField] private InputAction objectiveAction;
    [SerializeField] private Transform timerUI, objectiveUI;
    private void Awake()
    {
        rayHandSwitch.Enable();
        timerAction.Enable();
        objectiveAction.Enable();
        
        rayHandSwitch.started += EnableRayHandSwitch;
        rayHandSwitch.canceled += EnableRayHandSwitch;

        timerAction.started += ShowTimer;
        timerAction.canceled += ShowTimer;

        objectiveAction.started += ShowObjectives;
        objectiveAction.canceled += ShowObjectives;
    }

    private void ShowObjectives(InputAction.CallbackContext obj)
    {
        objectiveUI.gameObject.SetActive(obj.started);
        timerUI.gameObject.SetActive(false);
        uiFacingAngle.LookAt(playerCam);
    }

    private void ShowTimer(InputAction.CallbackContext obj)
    {
        timerUI.gameObject.SetActive(obj.started);
        uiFacingAngle.LookAt(playerCam);
    }

    private void EnableRayHandSwitch(InputAction.CallbackContext obj)
    {
        if (obj.started)
        {
            rayHand.gameObject.SetActive(true);
            normalHand.gameObject.SetActive(false);
        }
        else if(obj.canceled)
        {
            rayHand.gameObject.SetActive(false);
            normalHand.gameObject.SetActive(true);
        }
    }
}

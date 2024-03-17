using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;

public class touchInput : MonoBehaviour
{
    [SerializeField] private TMP_Text debugText;
    [SerializeField] private ARCameraManager arCamera;

    public void SingleTap(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Performed)
        {
            var touchPos = ctx.ReadValue<Vector2>();
            debugText.text = touchPos.ToString();
        }
    }

    public void DoubleTap(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Performed)
        {
            debugText.text = "Change Camera";

            if (arCamera.currentFacingDirection == CameraFacingDirection.World)
            {
                GetComponent<ARRaycastManager>().enabled = false;
                GetComponent<ARPlaneManager>().enabled = false;
                GetComponent<ARFaceManager>().enabled = true;
                arCamera.requestedFacingDirection = CameraFacingDirection.User;
            }
        }
        if (arCamera.currentFacingDirection == CameraFacingDirection.World)
        {
            GetComponent<ARRaycastManager>().enabled = true;
            GetComponent<ARPlaneManager>().enabled = true;
            GetComponent<ARFaceManager>().enabled = false;
            arCamera.requestedFacingDirection = CameraFacingDirection.World;
        }
    }
}
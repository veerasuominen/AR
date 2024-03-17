using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;

public class face : MonoBehaviour
{
    [SerializeField] private TMP_Text debugText;
    [SerializeField] private ARCameraManager ARCamera;
    [SerializeField] private ARFace ARFaces;
    [SerializeField] private ARFaceManager faceManager;

    private void OnEnable()
    {
        faceManager.facesChanged += FacesChanged;
    }

    private void OnDisable()
    {
        faceManager.facesChanged -= FacesChanged;
    }

    private List<ARFace> faces = new List<ARFace>();

    private void FacesChanged(ARFacesChangedEventArgs eventArgs)
    {
        foreach (var newFace in eventArgs.added)
        {
            faces.Add(newFace);
        }

        foreach (var lostFace in eventArgs.removed)
        { faces.Remove(lostFace); }
    }

    // Update is called once per frame
    private void Update()
    {
        if (ARCamera.currentFacingDirection == CameraFacingDirection.User)
        {
            if (faces.Count > 0)
            {
                Vector3 lowerLipPos = faces[0].vertices[14];//Gets position of lower lip
                debugText.text = lowerLipPos.ToString("F3"); //Add to debug text
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraManager : MonoBehaviour
{
    Camera cameraMain;
    [SerializeField]
    Vector3 basePosition;

    [SerializeField]
    float interpolationMultiplier;

    private void Awake()
    {
        cameraMain = GetComponent<Camera>();
    }

    public void UpdateCameraBasedOnPlayerInput(float relativeDistanceOfCueBallFromCentre, Vector3 playerEndStickPosition)
    {
        //Debug.Log(relativeDistanceOfCueBallFromCentre);
        //transform.position = basePosition + playerEndStickPosition.SetY(0) * relativeDistanceOfCueBallFromCentre * interpolationMultiplier;
        //
        //transform.position = transform.position.SetY((basePosition + playerEndStickPosition.SetY(0)).magnitude * interpolationMultiplier * 10 * relativeDistanceOfCueBallFromCentre);
    }

    public void ResetCameraPosition()
    {
        transform.position = basePosition;
    }
}

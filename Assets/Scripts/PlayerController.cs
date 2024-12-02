using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{    
    [SerializeField]
    CueBall cueBall;
    [SerializeField]
    Rigidbody cueBallRigidBody;
    [SerializeField]
    Light cueBallLight;

    [SerializeField]
    AnimationCurve cueBallLightIntensityFromVelocityCurve;
    [SerializeField]
    AnimationCurve cueBallLightRangeFromVelocityCurve;

    [SerializeField]
    float cueBallLightIntensityMultiplier;
    [SerializeField]
    float cueBallLightRangeMultiplier;

    [SerializeField]
    bool holdingCueBall = false;

    [SerializeField]
    float speedMultiplier = 1.0f;
    [SerializeField]
    float slowDownMultiplier = 0.9f;

    [SerializeField]
    LineRenderer stickStrengthVisualization;

    [SerializeField]
    CameraManager cameraManager;

    // Start is called before the first frame update
    void Start()
    {

    }

    public float ballSpeedLimit;

    // Update is called once per frame
    void Update()
    {
        float ballSpeed = new Vector2(cueBallRigidBody.velocity.x, cueBallRigidBody.velocity.z).magnitude;
        cueBallLight.intensity = cueBallLightIntensityFromVelocityCurve.Evaluate(ballSpeed) * cueBallLightIntensityMultiplier;
        cueBallLight.range = cueBallLightRangeFromVelocityCurve.Evaluate(ballSpeed) * cueBallLightRangeMultiplier;

        if (ballSpeed > ballSpeedLimit)
        { 
            if (holdingCueBall)
            {
                StoppedPressingCueBall();
            }

            cueBall.ChangeColorToShot();

            return;
        }
        else
        {
            cueBallRigidBody.velocity *= 1 - Time.deltaTime * (1 - slowDownMultiplier);
        }

        cueBall.ChangeColorToWaiting();

        if (GameInput.TouchesAndPointersCount == 1)
        {
            if (GameInput.JustClicked)
            {
                GameObject ballUnderPointer = GameInput.ObjectUnderPointer;

                if (ballUnderPointer == cueBall.gameObject)
                {
                    ClickedCueBall();
                }

                Debug.Log(GameInput.ObjectUnderPointer);
            }

            if (holdingCueBall)
            {
                Vector3 endStickPosition = GameInput.WorldPointerPositionSetGroundPlane(5);
                StickVisualization(cueBall.transform.position, GameManager.BallRadius, endStickPosition);

                float relativeCueBallPositionPercentage = GameManager.Instance.RelativeDistanceOfPositionFromCentre(cueBall.transform.position);
                cameraManager.UpdateCameraBasedOnPlayerInput(relativeCueBallPositionPercentage, endStickPosition);
            }

            //cueBall.transform.position = new Vector3(GameInput.WorldPointerPosition.x, cueBall.transform.position.y, GameInput.WorldPointerPosition.z);
        }
        else if (GameInput.JustStoppedClicking)
        {
            if (holdingCueBall)
            {
                if (GameInput.ObjectUnderPointer != cueBall.gameObject)
                {
                    Vector3 baseWorldPointerPosition = GameInput.WorldPointerPosition;
                    Vector3 pointerPosition = new Vector3(baseWorldPointerPosition.x, cueBall.transform.position.y, baseWorldPointerPosition.z);

                    Vector3 newVelocity = (pointerPosition - cueBallRigidBody.position) * speedMultiplier;

                    newVelocity = newVelocity.normalized * Mathf.Pow(newVelocity.magnitude, 0.8f);

                    cueBallRigidBody.velocity = newVelocity;
                }

                StoppedPressingCueBall();
            }
        }
    }

    void StickVisualization(Vector3 cueBallPosition, float cueBallRadius, Vector3 endStickPosition)
    {
        float distanceFromCentre = Vector3.Distance(cueBallPosition, endStickPosition);

        if (distanceFromCentre < cueBallRadius)
        {
            stickStrengthVisualization.enabled = false;
            return;
        }

        stickStrengthVisualization.enabled = true;

        stickStrengthVisualization.SetPosition(0, cueBall.transform.position);
        stickStrengthVisualization.SetPosition(1, endStickPosition);
    }

    void ClickedCueBall()
    {
        Debug.Log(true);

        cueBallRigidBody.velocity = Vector3.zero;

        holdingCueBall = true;
    }

    void StoppedPressingCueBall()
    {
        Debug.Log(false);

        holdingCueBall = false;

        stickStrengthVisualization.enabled = false;

        cameraManager.ResetCameraPosition();
    }
}

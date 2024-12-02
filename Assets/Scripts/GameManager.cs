using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    BallsSetup ballsSetup;
    [SerializeField]
    TableSetup tableSetup;

    [SerializeField]
    Vector2 dimensions;
    [SerializeField]
    Vector3 triangleStart;

    public static float BallRadius { get => Instance.ballsSetup.BallRadius; }

    public CueBall CueBall { get => cueBall; }
    [SerializeField]
    CueBall cueBall;

    [SerializeField]
    int shotsPerTurn;
    [SerializeField]
    int shotsLeft;

    [SerializeField]
    float timeScale;

    private void Awake()
    {
        Instance = this;
    }

    public bool blueTurn = true;

    void Start()
    {
        tableSetup.Setup(dimensions);
        ballsSetup.Setup(triangleStart);

        shotsLeft = shotsPerTurn;

        Time.timeScale = timeScale;
    }

    void Update()
    {
        if (CueBall.transform.position.y < -5)
        {
            ResetCueBall();
        }
    }

    void ResetCueBall()
    {
        CueBall.transform.position = Vector3.zero;

        CueBall.ResetVelocity();

        SwitchTurn();
    }

    void SwitchTurn()
    {
        blueTurn = !blueTurn;
    }

    public float RelativeDistanceOfPositionFromCentre(Vector3 position)
    {
        position = new Vector3(Mathf.Abs(position.x), Mathf.Abs(position.y), position.z);

        return Mathf.Max(dimensions.y / 2 - position.y, dimensions.x / 2 - position.x);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsSetup : MonoBehaviour
{
    [SerializeField]
    List<GameObject> blueBalls;
    [SerializeField]
    List<GameObject> redBalls;

    [SerializeField]
    GameObject blackBall;

    public float BallRadius { get => ballRadius; }
    [SerializeField]
    float ballRadius;

    public void Setup(Vector3 startPosition)
    {
        List<GameObject> allBalls = new List<GameObject>();
        allBalls.AddRange(blueBalls);
        allBalls.AddRange(redBalls);

        int ballsPerLine = 1;
        for (int line = 1; line <= 5; line++)
        {
            for (int i = 0; i < ballsPerLine; i++)
            {
                GameObject ball = (line == 3 && i == 1) ? blackBall : MathExt.PullRandomFrom(allBalls);

                Vector3 position = GetBallPosition(line, i, ballRadius);

                ball.transform.position = startPosition + position;

                ball.GetComponent<Rigidbody>().freezeRotation = true;
            }

            ballsPerLine++;
        }
    }

    public Vector3 GetBallPosition(int line, int lineIndex, float ballRadius)
    {
        float startBallLineIndex = (float)(line - 1) / 2;

        return new Vector3(Mathf.Sqrt(3) * ballRadius * line, 0, ballRadius * (lineIndex - startBallLineIndex) * 2);
    }
}

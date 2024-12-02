using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableSetup : MonoBehaviour
{
    [SerializeField]
    Transform floor;

    [SerializeField]
    Transform wallLeft;
    [SerializeField]
    Transform wallRight;
    [SerializeField]
    Transform wallUp;
    [SerializeField]
    Transform wallDown;

    [SerializeField]
    float wallWidth;
    [SerializeField]
    float wallHeight;

    public void Setup(Vector2 dimensions)
    {
        floor.localScale = new Vector3(dimensions.x,  dimensions.y, 2);

        float width = dimensions.x;
        float height = dimensions.y;

        Vector3 verticalWallsSize = new Vector3(wallWidth, height, wallHeight);
        wallLeft.localScale = verticalWallsSize;
        wallRight.localScale = verticalWallsSize;

        wallLeft.position = new Vector3(-width / 2, wallLeft.position.y, 0);
        wallRight.position = new Vector3(width / 2, wallRight.position.y, 0);

        Vector3 horizontalWallsSize = new Vector3(width, wallWidth, wallHeight);
        wallUp.localScale = horizontalWallsSize;
        wallDown.localScale = horizontalWallsSize;

        wallUp.position = new Vector3(0, wallUp.position.y, height / 2);
        wallDown.position = new Vector3(0, wallDown.position.y, -height / 2);
    }
}

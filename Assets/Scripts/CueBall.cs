using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueBall : MonoBehaviour
{
    [SerializeField]
    Color waitingToShootColor = Color.white;
    [SerializeField]
    Color shotColor = Color.white;
    [SerializeField]
    Material material;

    Rigidbody rigidBody;

    [SerializeField]
    float maxVerticalUpVelocity = 0.1f;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

        rigidBody.freezeRotation = true;
    }

    private void Update()
    {
        if (rigidBody.transform.position.y > maxVerticalUpVelocity)
        {
            rigidBody.transform.position = rigidBody.transform.position.SetY(maxVerticalUpVelocity * 0.5f);
        }
    }

    public void ResetVelocity()
    {
        rigidBody.velocity = Vector3.zero;
    }

    public void ChangeColorToWaiting()
    {
        material.SetColor("_EmissionColor", waitingToShootColor);
    }

    public void ChangeColorToShot()
    {
        material.SetColor("_EmissionColor", shotColor);
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    [Header("References")]
    [SerializeField] InputReader inputReader;
    [SerializeField] Transform bodyTransform;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] ParticleSystem dustTrail;

    [Header("Settings")]
    [SerializeField] float movementSpeed = 4f;
    [SerializeField] float turningRate = 30f;
    [SerializeField] float emissionRate = 10f;

    private ParticleSystem.EmissionModule emissionModule;
    private Vector2 previousMovementInput;
    private Vector3 previousPos;

    private const float ParticleStopThreshold = 0.005f;

    private void Awake()
    {
        emissionModule = dustTrail.emission;
    }

    public override void OnNetworkSpawn()
    {
        if(!IsOwner) { return; };

        inputReader.MoveEvent += HandleMove;
    }

    public override void OnNetworkDespawn()
    {
        if (!IsOwner) { return; };

        inputReader.MoveEvent -= HandleMove;
    }

    private void Update()
    {
        if(!IsOwner) { return; }

        float zRotation = previousMovementInput.x * -turningRate * Time.deltaTime;
        bodyTransform.Rotate(0f, 0f, zRotation);
    }

    private void FixedUpdate()
    {
        if ((transform.position - previousPos).sqrMagnitude > ParticleStopThreshold)
        {
            emissionModule.rateOverTime = emissionRate;
        }
        else
        {
            emissionModule.rateOverTime = 0;
        }

        previousPos = transform.position;

        if (!IsOwner) { return; }

        rb.velocity = (Vector2)bodyTransform.up * previousMovementInput.y * movementSpeed;
    }

    private void HandleMove(Vector2 movementInput)
    {
        previousMovementInput = movementInput;
    }
}

using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class TankPlayer : NetworkBehaviour
{
    [Header("References")]
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [SerializeField] SpriteRenderer minimapIconRenderer;
    [SerializeField] Texture2D crosshair;
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public CoinWallet Wallet { get; private set; }

    [Header("Settings")]
    [SerializeField] int ownerPriority = 15;
    [SerializeField] Color ownerColor;
    public NetworkVariable<FixedString32Bytes> PlayerName = new NetworkVariable<FixedString32Bytes>();

    public static event Action<TankPlayer> OnPlayerSpawned;
    public static event Action<TankPlayer> OnPlayerDespawned;

    public override void OnNetworkSpawn()
    {
        if(IsServer)
        {
            UserData userData = HostSingleton.Instance.GameManager.NetworkServer.GetUserDataByClientId(OwnerClientId);

            PlayerName.Value = userData.userName;

            OnPlayerSpawned?.Invoke(this);
        }

        if(IsOwner)
        {
            virtualCamera.Priority = ownerPriority;

            minimapIconRenderer.color = ownerColor;

            Cursor.SetCursor(crosshair, new Vector2(crosshair.width / 2, crosshair.height / 2), CursorMode.Auto);
        }
    }

    public override void OnNetworkDespawn()
    {
        if(IsServer)
        {
            OnPlayerDespawned?.Invoke(this);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class HealingZone : NetworkBehaviour
{
    [Header("References")]
    [SerializeField] Image healPowerBar;

    [Header("Settings")]
    [SerializeField] int maxHealPower = 30;
    [SerializeField] float healCooldown = 60f;
    [SerializeField] float healTickRate = 1f;
    [SerializeField] int coinsPerTick = 10;
    [SerializeField] int healthPerTick = 10;

    private List<TankPlayer> playersInZone = new List<TankPlayer>();

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(!IsServer) { return; }

        if(!col.attachedRigidbody.TryGetComponent<TankPlayer>(out TankPlayer player)) { return; }

        playersInZone.Add(player);

        Debug.Log($"Entered: {player.PlayerName.Value}");
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if(!IsServer) { return; }

        if (!col.attachedRigidbody.TryGetComponent<TankPlayer>(out TankPlayer player)) { return; }

        playersInZone.Remove(player);

        Debug.Log($"Left: {player.PlayerName.Value}");
    }
}

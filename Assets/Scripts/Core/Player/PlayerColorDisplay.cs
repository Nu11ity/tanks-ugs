using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColorDisplay : MonoBehaviour
{
    [SerializeField] TeamColorLookup teamColorLookup;
    [SerializeField] TankPlayer player;
    [SerializeField] SpriteRenderer[] playerSprites;

    private void Start()
    {
        HandleTeamChanged(-1, player.TeamIndex.Value);

        player.TeamIndex.OnValueChanged += HandleTeamChanged;
    }
    private void OnDestroy()
    {
        player.TeamIndex.OnValueChanged -= HandleTeamChanged;
    }

    private void HandleTeamChanged(int oldTeamIndex, int newTeamIndex)
    {
        Color teamColor = teamColorLookup.GetTeamColor(newTeamIndex);

        foreach(SpriteRenderer sprite in playerSprites)
        {
            sprite.color = teamColor;
        }
    }
}

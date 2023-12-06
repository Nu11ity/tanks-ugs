using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] TMP_Text queueStatusText;
    [SerializeField] TMP_Text queueTimerText;
    [SerializeField] TMP_Text findMatchButtonText;
    [SerializeField] TMP_InputField joinCodeField;

    private bool isMatchmaking;//!
    private bool isCancelling;//!

    private void Start()
    {
        if(ClientSingleton.Instance == null) { return; }

        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

        queueStatusText.text = string.Empty;
        queueTimerText.text = string.Empty;
    }

    public async void FindMatchPressed()//!
    {
        if(isCancelling) { return; }

        if(isMatchmaking)
        {
            queueStatusText.text = "Cancelling...";
            isCancelling = true;
            // Cancel matchmaking
            isCancelling = false;
            isMatchmaking = false;
            findMatchButtonText.text = "Find Match";
            queueStatusText.text = string.Empty;
            return;
        }

        // Start queue
        findMatchButtonText.text = "Cancel";
        queueStatusText.text = "Searching...";
        isMatchmaking = true;
    }

    public async void StartHost()
    {
        await HostSingleton.Instance.GameManager.StartHostAsync();
    }

    public async void StartClient()
    {
        await ClientSingleton.Instance.GameManager.StartClientAsync(joinCodeField.text);
    }
}

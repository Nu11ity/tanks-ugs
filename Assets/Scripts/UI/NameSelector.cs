using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NameSelector : MonoBehaviour
{
    [SerializeField] TMP_InputField nameField;
    [SerializeField] Button connectButton;
    [SerializeField] int minNameLength = 1;
    [SerializeField] int maxNameLength = 12;

    public const string PlayerNameKey = "PlayerName";

    private void Start()
    {
        if(SystemInfo.graphicsDeviceType == UnityEngine.Rendering.GraphicsDeviceType.Null)
        {
            LoadNextSceneIndex();
            return;
        }
        nameField.text = PlayerPrefs.GetString(PlayerNameKey, string.Empty);
        HandleNameChanged();
    }

    public void HandleNameChanged()
    {
        connectButton.interactable =
            nameField.text.Length >= minNameLength &&
            nameField.text.Length <= maxNameLength;
    }

    public void Connect()
    {
        PlayerPrefs.SetString(PlayerNameKey, nameField.text);

        LoadNextSceneIndex();
    }

    private void LoadNextSceneIndex()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

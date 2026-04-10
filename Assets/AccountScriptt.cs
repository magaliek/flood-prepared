using UnityEngine;

public class AccountScriptt : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;

    public GameObject loginPanel;
    public GameObject registerPanel;

    private string currentUsername;
    private string currentPassword;

    // Called when Play button is pressed
    public void OnPlayPressed()
    {
        currentUsername = usernameInput.text;
        currentPassword = passwordInput.text;

        string savedUser = PlayerPrefs.GetString("username", "");
        string savedPass = PlayerPrefs.GetString("password", "");

        if (currentUsername == savedUser && currentPassword == savedPass)
        {
            LoadGame();
        }
        else
        {
            // Show register panel
            loginPanel.SetActive(false);
            registerPanel.SetActive(true);
        }
    }

    // YES button
    public void OnYesPressed()
    {
        // Save new account
        PlayerPrefs.SetString("username", currentUsername);
        PlayerPrefs.SetString("password", currentPassword);
        PlayerPrefs.Save();

        LoadGame();
    }

    // NO button
    public void OnNoPressed()
    {
        registerPanel.SetActive(false);
        loginPanel.SetActive(true);
    }

    void LoadGame()
    {
        SceneManager.LoadScene("GameScene"); // change to your scene name
    }
}

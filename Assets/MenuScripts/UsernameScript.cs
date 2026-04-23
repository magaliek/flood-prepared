using UnityEngine;
using TMPro;
using MenuScripts;

namespace MenuScripts
{
public class UsernameScript : MonoBehaviour
{
    [SerializeField] private GameObject makeAccountPanel;
    [SerializeField] private TMP_InputField usernameInput;
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private TMP_Text errorText;

    // Called by your Play button
    public void OnPlayClick()
    {
        string username = usernameInput.text.Trim();
        string password = passwordInput.text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            errorText.text = "Please enter a username and password.";
            return;
        }

        errorText.text = "";

        GameManager.Instance.TryLogin(
            username,
            password,
            onError: msg => errorText.text = msg,
            showMakeAccountPanel: () =>
            {
                makeAccountPanel.SetActive(true);
                makeAccountPanel.transform.SetAsLastSibling();
            }
        );
    }
}
}
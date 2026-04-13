using UnityEngine;
using System.Collections;
using System.Security.Cryptography;
using System.Text;
using maggies_awesome_score_system;
using UnityEngine.SceneManagement;
using TMPro;

namespace MenuScripts
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public string PlayerId { get; private set; }
        public string Username { get; private set; }
        public string PendingUsername { get; private set; }
        public string PendingPassword { get; private set; }

        [SerializeField] private GameObject makeAccountPanel;
        [SerializeField] private GameObject usernamePanel;
        [SerializeField] private GameObject instructionsPanel;
        [SerializeField] private GameObject bPreparedPanel;

        [SerializeField] private TMP_InputField usernameInput;
        [SerializeField] private TMP_InputField passwordInput;

        [SerializeField] private TMP_Text usernameErrorText;
        [SerializeField] private TMP_Text makeAccountErrorText;

        void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        void Start()
        {
            makeAccountPanel.SetActive(false);
            usernamePanel.SetActive(false);
            instructionsPanel.SetActive(false);
            bPreparedPanel.SetActive(false);
        }

        // Called by Play button on usernamePanel
        public void OnPlayClick()
        {
            string username = usernameInput.text.Trim();
            string password = passwordInput.text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                usernameErrorText.text = "Please enter a username and password.";
                return;
            }

            StartCoroutine(Login(username, password));
        }

        // Called by Yes button on makeAccountPanel
        public void OnYesClick()
        {
            StartCoroutine(Register(PendingUsername, PendingPassword));
        }

        // Called by No button on makeAccountPanel
        public void OnNoClick()
        {
            makeAccountPanel.SetActive(false);
            makeAccountErrorText.text = "";
        }

        private IEnumerator Login(string username, string password)
        {
            string hash = HashPassword(password);
            string query = $"username=eq.{username}&password_hash=eq.{hash}";

            yield return StartCoroutine(SupabaseClient.Instance.Get("users", query, response =>
            {
                if (response == null)
                {
                    usernameErrorText.text = "Something went wrong. Try again.";
                    return;
                }

                if (response == "[]")
                {
                    StartCoroutine(CheckIfUsernameExists(username));
                }
                else
                {
                    var userData = ParseUser(response);
                    SetPlayer(userData.id, userData.username);
                    SceneManager.LoadScene("Hallway");
                }
            }));
        }

        private IEnumerator CheckIfUsernameExists(string username)
        {
            string query = $"username=eq.{username}";

            yield return StartCoroutine(SupabaseClient.Instance.Get("users", query, response =>
            {
                if (response == "[]")
                {
                    SetPendingCredentials(usernameInput.text.Trim(), passwordInput.text);
                    makeAccountPanel.SetActive(true);
                    makeAccountPanel.transform.SetAsLastSibling();
                    makeAccountErrorText.text = "";
                }
                else
                {
                    usernameErrorText.text = "Incorrect password.";
                }
            }));
        }

        private IEnumerator Register(string username, string password)
        {
            string hash = HashPassword(password);
            string json = $"{{\"username\":\"{username}\",\"password_hash\":\"{hash}\"}}";

            yield return StartCoroutine(SupabaseClient.Instance.Post("users", json, success =>
            {
                if (success)
                {
                    StartCoroutine(LoginAfterRegister(username, password));
                }
                else
                {
                    makeAccountErrorText.text = "Failed to create account. Try again.";
                }
            }));
        }

        private IEnumerator LoginAfterRegister(string username, string password)
        {
            string hash = HashPassword(password);
            string query = $"username=eq.{username}&password_hash=eq.{hash}";

            yield return StartCoroutine(SupabaseClient.Instance.Get("users", query, response =>
            {
                if (response != null && response != "[]")
                {
                    string trimmed = response.Trim('[', ']');
                    var userData = JsonUtility.FromJson<UserData>(trimmed);
                    SetPlayer(userData.id, userData.username);
                    SceneManager.LoadScene("Hallway");
                }
                else
                {
                    makeAccountErrorText.text = "Login after register failed. Try again.";
                }
            }));
        }

        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder sb = new StringBuilder();
            foreach (byte b in bytes) sb.Append(b.ToString("x2"));
            return sb.ToString();
        }

        private UserData ParseUser(string response)
        {
            string trimmed = response.Trim('[', ']');
            return JsonUtility.FromJson<UserData>(trimmed);
        }

        [System.Serializable]
        private class UserData
        {
            public string id;
            public string username;
        }

        public void SetPlayer(string id, string username)
        {
            PlayerId = id;
            Username = username;
        }

        public void SetPendingCredentials(string username, string password)
        {
            PendingUsername = username;
            PendingPassword = password;
        }
    }
}
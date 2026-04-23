using UnityEngine;
using System.Collections;
using System.Security.Cryptography;
using System.Text;
using maggies_awesome_score_system;
using UnityEngine.SceneManagement;

namespace MenuScripts
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public string PlayerId { get; private set; }
        public string Username { get; private set; }
        public string PendingUsername { get; private set; }
        public string PendingPassword { get; private set; }

        void Awake()
        {
            if (Instance != null) { Destroy(gameObject); return; }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        // Called by UsernameScript with its own field references passed in
        public void TryLogin(string username, string password,
            System.Action<string> onError,
            System.Action showMakeAccountPanel)
        {
            StartCoroutine(Login(username, password, onError, showMakeAccountPanel));
        }

        public void TryRegister(System.Action<string> onError)
        {
            StartCoroutine(Register(PendingUsername, PendingPassword, onError));
        }

        private IEnumerator Login(string username, string password,
            System.Action<string> onError,
            System.Action showMakeAccountPanel)
        {
            string hash = HashPassword(password);
            string query = $"username=eq.{username}&password_hash=eq.{hash}";

            yield return StartCoroutine(SupabaseClient.Instance.Get("users", query, response =>
            {
                if (response == null)
                {
                    onError?.Invoke("Something went wrong. Try again.");
                    return;
                }

                if (response == "[]")
                {
                    // Credentials don't match — check if username exists at all
                    StartCoroutine(CheckIfUsernameExists(username, password,
                        onError, showMakeAccountPanel));
                }
                else
                {
                    var userData = ParseUser(response);
                    SetPlayer(userData.id, userData.username);
                    SceneManager.LoadScene("Hallway");
                }
            }));
        }

        private IEnumerator CheckIfUsernameExists(string username, string password,
            System.Action<string> onError,
            System.Action showMakeAccountPanel)
        {
            string query = $"username=eq.{username}";

            yield return StartCoroutine(SupabaseClient.Instance.Get("users", query, response =>
            {
                if (response == "[]")
                {
                    // Username doesn't exist at all — offer to register
                    // Save credentials NOW before touching any UI
                    SetPendingCredentials(username, password);
                    showMakeAccountPanel?.Invoke();
                }
                else
                {
                    // Username exists but password was wrong
                    onError?.Invoke("Incorrect password.");
                }
            }));
        }

        private IEnumerator Register(string username, string password,
            System.Action<string> onError)
        {
            string hash = HashPassword(password);
            string json = $"{{\"username\":\"{username}\",\"password_hash\":\"{hash}\"}}";

            yield return StartCoroutine(SupabaseClient.Instance.Post("users", json, success =>
            {
                if (success)
                    StartCoroutine(LoginAfterRegister(username, password, onError));
                else
                    onError?.Invoke("Failed to create account. Try again.");
            }));
        }

        private IEnumerator LoginAfterRegister(string username, string password,
            System.Action<string> onError)
        {
            string hash = HashPassword(password);
            string query = $"username=eq.{username}&password_hash=eq.{hash}";

            yield return StartCoroutine(SupabaseClient.Instance.Get("users", query, response =>
            {
                if (response != null && response != "[]")
                {
                    var userData = ParseUser(response);
                    SetPlayer(userData.id, userData.username);
                    SceneManager.LoadScene("Hallway");
                }
                else
                {
                    onError?.Invoke("Login after register failed. Try again.");
                }
            }));
        }

        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            var sb = new System.Text.StringBuilder();
            foreach (byte b in bytes) sb.Append(b.ToString("x2"));
            return sb.ToString();
        }

        private UserData ParseUser(string response)
        {
            string trimmed = response.Trim('[', ']');
            return JsonUtility.FromJson<UserData>(trimmed);
        }

        [System.Serializable]
        private class UserData { public string id; public string username; }

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
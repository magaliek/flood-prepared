using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace maggies_awesome_score_system
{
    public class SupabaseClient : MonoBehaviour
    {
        public static SupabaseClient Instance { get; private set; }

        public void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            };
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public IEnumerator Post(string table, string json, System.Action<bool> callback)
        {
            string url = $"{SupaConfig.Url}/rest/v1/{table}";

            using var request = new UnityWebRequest(url, "POST");
            
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("apikey", SupaConfig.AnonKey);
            request.SetRequestHeader("Authorization", $"Bearer {SupaConfig.AnonKey}");
            request.SetRequestHeader("Prefer", "return=minimal");

            byte[] body = Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(body);
            request.downloadHandler = new DownloadHandlerBuffer();

            yield return request.SendWebRequest();

            bool success = request.result == UnityWebRequest.Result.Success;
            if (!success) Debug.LogError($"Supabase POST error: {request.error} - {request.downloadHandler.text}");
            callback?.Invoke(success);
        }

        public IEnumerator Get(string table, string query, System.Action<string> callback)
        {
            string url = $"{SupaConfig.Url}/rest/v1/{table}?{query}";

            using var request = new UnityWebRequest(url, "GET");
            
            request.SetRequestHeader("apikey", $"{SupaConfig.AnonKey}");
            request.SetRequestHeader("Authorization", $"Bearer {SupaConfig.AnonKey}");

            request.downloadHandler = new DownloadHandlerBuffer();

            yield return request.SendWebRequest();

            bool success = request.result == UnityWebRequest.Result.Success;

            if (success)
            {
                callback?.Invoke(request.downloadHandler.text);
            }
            else
            {
                Debug.LogError($"Supabase GET error: {request.error}");
                callback?.Invoke(null);
            }
        }
    }
}
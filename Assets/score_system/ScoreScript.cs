using System.Collections.Generic;
using MenuScripts;
using maggies_awesome_score_system;
using packing_scripts;
using UnityEngine;
using System;

namespace score_system
{
    public enum MapChoice {
        None,
        Basement,
        NearRiver,
        Hill,
        DesignatedShelter
    }
    public class ScoreScript : MonoBehaviour
    {
        public static ScoreScript Instance { get; private set; }

        private int _windowPoints;
        private int _waterValveTask;
        private int _drawerTask;
        private int _fuseboxTask;
        private int _mapPoints;
        private int _backpackPoints;
        private int _notebookPoints;
        private int _notificationPoints;
        private int _takingBackpack;
        private int _outBeforeTimer;

        public MapChoice chosenShelter;
        public bool packingDone;
        public bool notebookDone;
        public bool notebookTaken;
        public bool mapDone;
        public bool mapTaken;
        public bool windowDone;
        public bool valveDone;
        public bool drawerDone;
        public bool fuseboxDone;
        public bool notificationsDone;
        public bool leftOnTime;
        public bool tookBackpack;

        public bool phase2;

        public List<DocumentData> takenDrawerDocs = new List<DocumentData>();

        public void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(this);
        }
        
        
        private void AddWindowPoints() {
            if (windowDone) _windowPoints += 15;
        }
        private void AddValvePoints()
        {
            if (valveDone) _waterValveTask += 15;
        }
        private void AddDrawerPoints()
        {
            if (drawerDone) _drawerTask += 25;
        }
        private void AddFuseboxPoints()
        {
            if (fuseboxDone) _fuseboxTask += 25;
        }
        private void AddNotificationPoints()
        {
            if (notificationsDone) _notificationPoints += 15;
        }

        private void AddTimerPoints()
        {
            if (leftOnTime) _outBeforeTimer += 50;
        }
        
        private void AddTakingBackpackPoints(IReadOnlyList<PackingItem> packed)
        {
            if (!tookBackpack) return;
            if (packed.Count <= 0)
            {
                _backpackPoints = 0;
                return;
            }

            _takingBackpack += 35;
        }

        private void AddNotebookPoints()
        {
            if (!notebookDone || !notebookTaken) return;
            _notebookPoints += 15;
        }

        private void AddPackingPoints(IReadOnlyList<PackingItem> packed)
        {
            if (!packingDone) return;
            

            
            {
                foreach (var item in packed)
                {
                    _backpackPoints += item.GetPoints();
                }
            }
        }

        private void AddMapPoints()
        {
            if (!mapDone || !mapTaken) return;
            switch (chosenShelter)
            {
                case MapChoice.Hill:
                    if (packingDone) _mapPoints += 15;
                    break;
                case MapChoice.DesignatedShelter:
                    _mapPoints += 100;
                    break;
            }
        }

        private void ResetPoints()
        {
            _windowPoints = 0;
            _waterValveTask = 0;
            _drawerTask = 0;
            _fuseboxTask = 0;
            _mapPoints = 0;
            _backpackPoints = 0;
            _notebookPoints = 0;
            _notificationPoints = 0;
            _takingBackpack = 0;
            _outBeforeTimer = 0;
        }
        
        public int TotalScore()
        {
            ResetPoints(); AddDrawerPoints();
            AddFuseboxPoints(); AddMapPoints();
            AddNotebookPoints(); AddNotificationPoints();
            
            var packed = Backpack.Instance != null ? Backpack.Instance.Packed : new List<PackingItem>();

            AddTakingBackpackPoints(packed);
            AddTimerPoints(); AddValvePoints();
            AddWindowPoints(); AddPackingPoints(packed);
            
            int totalPoints = _windowPoints + _waterValveTask +
            _drawerTask + _fuseboxTask +
            _mapPoints + _backpackPoints +
            _notebookPoints + _notificationPoints +
            _takingBackpack + _outBeforeTimer;

            return totalPoints;
        }

        public string GetEndingText()
        {
            if (!leftOnTime)
                return Endings.Ending2;

            if (!mapDone || !mapTaken)
                return UnityEngine.Random.value < 0.5f ? Endings.Ending1 : Endings.Ending6;

            switch (chosenShelter)
            {
                case MapChoice.DesignatedShelter:
                    return (windowDone && fuseboxDone && valveDone && drawerDone)
                        ? Endings.Ending3
                        : Endings.Ending5;

                case MapChoice.Hill:
                    if (!packingDone || !tookBackpack) return Endings.Ending4;
                    return (windowDone && fuseboxDone && valveDone && drawerDone)
                        ? Endings.Ending3
                        : Endings.Ending5;

                default:
                    return Endings.Ending1;
            }
        }

        public void SaveToSupabase()
        {
            int total = TotalScore();
            string ending = GetEndingText();
            string playerId = GameManager.Instance.PlayerId;
            string timestamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");

            string json = $@"{{
        ""player_id"": ""{playerId}"",
        ""created_at"": ""{timestamp}"",
        ""score_window"": {_windowPoints},
        ""score_water_valve"": {_waterValveTask},
        ""score_drawer"": {_drawerTask},
        ""score_fusebox"": {_fuseboxTask},
        ""score_map_task"": {_mapPoints},
        ""score_notebook_task"": {_notebookPoints},
        ""score_packing"": {_backpackPoints},
        ""score_notification"": {_notificationPoints},
        ""score_backpack_phase2"": {_takingBackpack},
        ""score_escaped_in_time"": {_outBeforeTimer},
        ""score_total"": {total},
        ""ending"": {GetEndingNumber(ending)}
    }}";

            StartCoroutine(SupabaseClient.Instance.Post("game_sessions", json, success =>
            {
                if (success) Debug.Log("Session saved!");
                else Debug.LogError("Failed to save session!");
            }));
        }
        
        private int GetEndingNumber(string ending)
        {
            if (ending == Endings.Ending1) return 1;
            if (ending == Endings.Ending2) return 2;
            if (ending == Endings.Ending3) return 3;
            if (ending == Endings.Ending4) return 4;
            if (ending == Endings.Ending5) return 5;
            if (ending == Endings.Ending6) return 6;
            Debug.LogError($"Unrecognised ending string: '{ending}'");
            return 1;
        }
    }
}

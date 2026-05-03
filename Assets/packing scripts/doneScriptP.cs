using score_system;
using UnityEngine;

namespace packing_scripts
{
    public class DoneScriptP : MonoBehaviour
    {
        [SerializeField] private GameObject panel;

        public void OnDoneClicked()
        {
            float tCal=0;
            float tCarb=0;
            float tProtein=0;
            float tFat=0;
            float tSodium=0;
            float tWater=0;

            foreach (var item in Backpack.Instance.Packed)
            {
                tCal += item.Calories;
                tCarb += item.Carbs;
                tProtein += item.Protein;
                tFat += item.Fat;
                tSodium += item.Sodium;
                tWater += item.Water;
                
                if (item is Notebook) ScoreScript.Instance.notebookTaken = true;
                if (item is Map) ScoreScript.Instance.mapTaken = true;
            }
            
            var meetsReq = (tCal >= (1190 * 3)) &&
                           (tCarb >= (130 * 3)) &&
                           (tProtein >= (56 * 3)) &&
                           (tFat >= (49.5 * 3)) &&
                           (tSodium >= (500 * 3)) &&
                           (tWater >= (2.5 * 3));

            if (meetsReq) ScoreScript.Instance.packingDone = true;
            
            panel.SetActive(false);
        }
    }
}
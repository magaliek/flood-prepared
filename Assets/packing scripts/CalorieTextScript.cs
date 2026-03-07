using TMPro;
using UnityEngine;

namespace packing_scripts
{
    public class CalorieTextScript : MonoBehaviour
    {
        [SerializeField]
        TMP_Text myText;

        [SerializeField] private Backpack backpack;

        public void UpdateText()
        {
            myText.text = $"Calories Packed: {backpack.Calories}";
        }
    }
}

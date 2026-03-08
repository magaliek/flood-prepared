using TMPro;
using UnityEngine;

namespace packing_scripts
{
    public class MacroText : MonoBehaviour
    {
        [SerializeField] TMP_Text myText;
        
        [SerializeField] private Backpack backpack;
        
        
        public void UpdateText(string text)
        {
            myText.text = text;
        }
    }
}
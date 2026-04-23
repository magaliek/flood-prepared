using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace packing_scripts
{
    public class ResetScript : MonoBehaviour
    {
        [SerializeField] private Backpack backpack;
        [SerializeField] private TMP_Text text;

        public void OnResetClicked()
        {
            foreach (PackingItem item in backpack.Packed.ToList())
            {
                backpack.RemoveItem(item);
                backpack.ResetMacros();
            }
        }
    }
}
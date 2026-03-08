using UnityEngine;
using UnityEngine.UI;

namespace packing_scripts
{
    public class Notebook : PackingItem
    {
        [SerializeField] private Sprite doneNotebookImage;
        
        private Image _myNotebook;
        
        
        private void Awake()
        {
            _myNotebook = GetComponent<Image>();
        }

        public void ChangeImage()
        {
            _myNotebook.sprite = doneNotebookImage;
        }
    }
}
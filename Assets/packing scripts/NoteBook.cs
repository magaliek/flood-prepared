using UnityEngine;
using UnityEngine.UI;

namespace packing_scripts
{
    public class Notebook : PackingItem
    {
        [SerializeField] private Sprite doneNotebookImage;
        private bool _done;
        [SerializeField] private bool value; //remove

        private Image _myImage;
        private Sprite _originalSprite;
        
        
        private void Start()
        {
            _myImage = GetComponent<Image>();
            _originalSprite = _myImage.sprite;
        }

        public void SetDone(bool val)
        {
            _done = val;
            ChangeImage();
        }

        private void Update()
        {
            ChangeImage();
        }

        private void ChangeImage()
        {
            _myImage.sprite = value ? doneNotebookImage : _originalSprite; //change it
        }
    }
}
using UnityEngine;
using UnityEngine.UI;
using score_system;

namespace packing_scripts
{
    public class Notebook : PackingItem
    {
        [SerializeField] private Sprite doneNotebookImage;
        
        private Image _myNotebook;
        
        
        protected override void Awake()
        {
            base.Awake();
            _myNotebook = GetComponent<Image>();
        }

        public void ChangeImage()
        {
            _myNotebook.sprite = doneNotebookImage;
        }
        
        public override int GetPoints() 
        {
            points = ScoreScript.Instance?.notebookDone == true ? 3 : 0;
            return points;
        }
    }
}
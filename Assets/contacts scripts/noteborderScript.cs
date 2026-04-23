using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace contacts_scripts
{
    public class NoteborderScript : MonoBehaviour, IDropHandler
    {
        private RectTransform _contentParent;
        private Outline _outline;

        public bool isEmpty=true;

        [SerializeField] private string zoneId;
        [SerializeField] PencilAnim pencilAnimator;
        [SerializeField] private TMP_FontAsset sketchFont;
        
        public void Awake()
        {
            _contentParent = transform as RectTransform;
            _outline = GetComponent<Outline>();
            _outline.enabled = false;
        }
        
        public void OnDrop(PointerEventData eventData)
        {
            GameObject draggedObj = eventData.pointerDrag;
            
            ContactScript incomingContact = draggedObj.GetComponent<ContactScript>();
            NoteborderScript previousZone = incomingContact.GetCurrentZone();
            
            if (!isEmpty)
            {
                ContactScript occupant = GetComponentInChildren<ContactScript>();
                if (occupant != null)
                {
                    if (previousZone != null)
                    {
                        previousZone.AcceptContact(occupant, previousZone);
                    }
                    else
                    {
                        occupant.SnapToOrigin();
                    }
                }
            }
            AcceptContact(incomingContact, this);
        }
            
        public void AcceptContact(ContactScript contact, NoteborderScript zone)
        {
            TMP_Text text = contact.text;
            text.font = sketchFont;
            text.color = Color.black;
            contact.transform.SetParent(_contentParent, false);

            RectTransform draggedRect = contact.GetComponent<RectTransform>();
            draggedRect.anchorMin = new Vector2(0.5f, 0.5f);
            draggedRect.anchorMax = new Vector2(0.5f, 0.5f);
            draggedRect.pivot     = new Vector2(0.5f, 0.5f);
            draggedRect.anchoredPosition = Vector2.zero;
            draggedRect.localRotation = Quaternion.identity;
            draggedRect.localScale = Vector3.one;

            isEmpty = false;

            contact.SetCurrentZone(zone);
            
            if (!contact.IsValidZone(zoneId))
            {
                _outline.enabled = true;
                _outline.effectColor = Color.red;
            }
            else if (contact.IsValidZone(zoneId))
            {
                _outline.enabled = true;
                _outline.effectColor = Color.green;
            }
            else
            {
                _outline.enabled = false;
            }
            pencilAnimator.PlayAnimate(text);
        }
    }
}

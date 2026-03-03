using UnityEngine;

[CreateAssetMenu(menuName = "FloodGame/Document")]
public class DocumentData : ScriptableObject
{
    public string id;            // "passport"
    public string displayName;   // "Passport"
    public Sprite icon;          // optional UI icon
}
using UnityEngine;
using TMPro;
using MenuScripts;

namespace MenuScripts
{
public class MakeAccountScript : MonoBehaviour
{
    [SerializeField] private GameObject makeAccountPanel;
    [SerializeField] private TMP_Text errorText;

    public void OnYesClick()
    {
        errorText.text = "";
        GameManager.Instance.TryRegister(
            onError: msg => errorText.text = msg
        );
    }

    public void OnNoClick()
    {
        makeAccountPanel.SetActive(false);
        errorText.text = "";
    }
}
}
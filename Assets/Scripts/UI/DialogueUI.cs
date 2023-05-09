using UnityEngine;
using Dialogue;
using TMPro;
using UnityEngine.UI;

namespace UI
{
    public class DialogueUI : MonoBehaviour
    {
        private PlayerConversant _playerConversant;
        [SerializeField] private TextMeshProUGUI aIText;
        [SerializeField]private Button nextButton;
        [SerializeField] private Transform choiceRoot;
        [SerializeField] private GameObject choicePrefab;
        
        // Start is called before the first frame update
        void Start()
        {
            _playerConversant = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();
            nextButton.onClick.AddListener(Next);
            
            UpdateUI();
        }

        void Next()
        {
            _playerConversant.Next();
            UpdateUI();
        }

        void UpdateUI()
        {
            aIText.text = _playerConversant.GetText();
            nextButton.gameObject.SetActive(_playerConversant.HasNext());
            foreach (Transform item in choiceRoot)
            {
                Destroy(item.gameObject);
            }

            foreach (string choiceText in _playerConversant.GetChoices())
            {
                GameObject choiceInstance = Instantiate(choicePrefab,choiceRoot);
                var textComp = choiceInstance.GetComponentInChildren<TextMeshProUGUI>();
                textComp.text = choiceText;
            }
        }
    }
}
 
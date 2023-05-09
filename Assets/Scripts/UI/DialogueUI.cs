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
        [SerializeField] private GameObject AIResponse;
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
            AIResponse.SetActive(!_playerConversant.IsChoosing());
            choiceRoot.gameObject.SetActive(_playerConversant.IsChoosing());

            if (_playerConversant.IsChoosing())
            {
                BuildChoiceList();
            }
            else
            {
                aIText.text = _playerConversant.GetText();
                nextButton.gameObject.SetActive(_playerConversant.HasNext());
            }
        }

        private void BuildChoiceList()
        {
            foreach (Transform item in choiceRoot)
            {
                Destroy(item.gameObject);
            }

            foreach (DialogueNode choice in _playerConversant.GetChoices())
            {
                GameObject choiceInstance = Instantiate(choicePrefab, choiceRoot);
                var textComp = choiceInstance.GetComponentInChildren<TextMeshProUGUI>();
                textComp.text = choice.GetText();
                Button button = choiceInstance.GetComponentInChildren<Button>();
                button.onClick.AddListener(() =>
                {
                    _playerConversant.SelectChoice(choice);
                    UpdateUI();
                });
            }
        }
    }
}
 
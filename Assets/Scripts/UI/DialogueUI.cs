using UnityEngine;
using Dialogue;
using TMPro;

namespace UI
{
    public class DialogueUI : MonoBehaviour
    {
        private PlayerConversant _playerConversant;
        [SerializeField]
        private TextMeshProUGUI aIText;
        
        // Start is called before the first frame update
        void Start()
        {
            _playerConversant = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();
            aIText.text = _playerConversant.GetText();
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
 
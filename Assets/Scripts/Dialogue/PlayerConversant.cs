using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Dialogue
{
    public class PlayerConversant : MonoBehaviour
    {
        [SerializeField] private Dialogue currentDialogue;
        private DialogueNode _currentNode = null;
        private bool _isChoosing = false;

        private void Awake()
        {
            _currentNode = currentDialogue.GetRootNode();
        }

        public bool IsChoosing()
        {
            return _isChoosing;
        }

        public string GetText()
        {
            if (currentDialogue == null)
            {
                return "";
            }

            return _currentNode.GetText();
        }

        public IEnumerable<DialogueNode> GetChoices()
        {
            return currentDialogue.GetPlayerChildren(_currentNode);
        }

        public void Next()
        {
            int numPlayerResponses =  currentDialogue.GetPlayerChildren(_currentNode).Count();
            if (numPlayerResponses > 0)
            {
                _isChoosing = true;
                return;
            }
            
            DialogueNode[] children = currentDialogue.GetAIChildren(_currentNode).ToArray();
            int randomIndex = Random.Range(0, children.Length);
            _currentNode = children[randomIndex];
        }

        public bool HasNext()
        {
            return currentDialogue.GetAllChildren(_currentNode).Count() > 0;
        }
    }
}

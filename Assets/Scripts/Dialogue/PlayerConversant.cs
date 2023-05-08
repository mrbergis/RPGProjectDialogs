using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Dialogue
{
    public class PlayerConversant : MonoBehaviour
    {
        [SerializeField] private Dialogue currentDialogue;
        private DialogueNode _currentNode = null;

        private void Awake()
        {
            _currentNode = currentDialogue.GetRootNode();
        }

        public string GetText()
        {
            if (currentDialogue == null)
            {
                return "";
            }

            return _currentNode.GetText();
        }

        public void Next()
        {
            DialogueNode[] children = currentDialogue.GetAllChildren(_currentNode).ToArray();
            int randomIndex = Random.Range(0, children.Length);
            _currentNode = children[randomIndex];
        }

        public bool HasNext()
        {
            return currentDialogue.GetAllChildren(_currentNode).Count() > 0;
        }
    }
}

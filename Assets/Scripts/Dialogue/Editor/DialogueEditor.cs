using UnityEditor;
using UnityEditor.Callbacks;

namespace Dialogue.Editor
{
    public class DialogueEditor : EditorWindow
    {
        private Dialogue _selectedDialogue = null;
        
        [MenuItem("Window/Dialogue Editor")]
        public static void ShowEditorWindow()
        {
            GetWindow(typeof(DialogueEditor), false, "Dialogue Editor");
        }

        [OnOpenAsset(1)]
        public static bool OnOpenAsset(int instanceID, int line)
        {
            Dialogue dialogue = EditorUtility.InstanceIDToObject(instanceID) as Dialogue;
            if (dialogue != null)
            {
                ShowEditorWindow();
                return true;
            }
            return false;
        }

        private void OnEnable()
        {
            Selection.selectionChanged += OnSelectionChange;
        }

        private void OnSelectionChange()
        {
            Dialogue newDialogue =  Selection.activeObject as Dialogue;
            if (newDialogue != null)
            {
                _selectedDialogue = newDialogue;
                Repaint();
            }
        }

        private void OnGUI()
        {
            if (_selectedDialogue == null)
            {
                EditorGUILayout.LabelField("No Dialogue Selected.");
            }
            else
            {
                foreach (DialogueNode node in _selectedDialogue.GetAllNodes())
                {
                    string newText = EditorGUILayout.TextField(node.text);
                    if (newText != node.text)
                    {
                        node.text = newText;
                        EditorUtility.SetDirty(_selectedDialogue);
                    }
                }
            }
        }
    }
} 
  



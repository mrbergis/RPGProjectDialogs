using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Dialogue.Editor
{
    public class DialogueEditor : EditorWindow
    {
        private Dialogue _selectedDialogue = null;
        private GUIStyle _nodeStyle;
        private bool _dragging = false;
        
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

            _nodeStyle = new GUIStyle();
            _nodeStyle.normal.background = EditorGUIUtility.Load("node0") as Texture2D;
            _nodeStyle.normal.textColor = Color.white;
            _nodeStyle.padding = new RectOffset(20,20,20,20);
            _nodeStyle.border = new RectOffset(12, 12, 12, 12);
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
                ProcessEvents();
                foreach (DialogueNode node in _selectedDialogue.GetAllNodes())
                {
                    OnGUINode(node);
                }
            }
        }

        private void ProcessEvents()
        {
            if (Event.current.type == EventType.MouseDown && !_dragging)
            {
                _dragging = true;
            }
            else if (Event.current.type == EventType.MouseDrag && _dragging)
            {
                Undo.RecordObject(_selectedDialogue, "Move Dialogue Node");
                _selectedDialogue.GetRootNode().rect.position = Event.current.mousePosition;
                //Repaint();
                GUI.changed = true;//это семантически точнее чем использовать Repaint()
            }
            else if (Event.current.type == EventType.MouseUp && _dragging)
            {
                _dragging = false;
            }
        }

        private void OnGUINode(DialogueNode node)
        {
            GUILayout.BeginArea(node.rect, _nodeStyle);
            
            EditorGUI.BeginChangeCheck();

            EditorGUILayout.LabelField("Node:", EditorStyles.whiteLabel);
            string newText = EditorGUILayout.TextField(node.text);
            string newUniqueID = EditorGUILayout.TextField(node.uniqueID);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(_selectedDialogue, "Update Dialogue Text");

                node.text = newText;
                node.uniqueID = newUniqueID;
            }
            
            GUILayout.EndArea();
        }
    }
} 
  


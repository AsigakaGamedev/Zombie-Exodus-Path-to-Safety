using System;
using UnityEditor;
using UnityEngine;

namespace UIEM
{
    public class Node
    {
        public Rect rect;
        public string title = "";
        public string oldTitle = "";
        public bool isDragged = false;
        public bool isSelected = false;

        private bool Renaming = false;
        private bool selectCheck = false;
        private Texture2D runTexture;
        private bool runAtStart = false;
        private Texture2D loopTexture;
        private bool loop = false;

        public ConnectionPoint inPoint;
        public ConnectionPoint outPoint;

        private GUIStyle style;
        private GUIStyle defaultNodeStyle;
        private GUIStyle selectedNodeStyle;

        private Action<Node>[] nodeActions; //0: Rename, 1: Copy, 2: Duplicate, 3: Delete

        public Node(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Texture2D _runTexture, Texture2D _loopTexture, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<Node>[] _nodeActions, bool _runAtStart, bool _loop)
        {
            rect = new Rect(position.x, position.y, width, height);
            style = nodeStyle;
            inPoint = new ConnectionPoint(this, ConnectionPointType.In, inPointStyle, OnClickInPoint);
            outPoint = new ConnectionPoint(this, ConnectionPointType.Out, outPointStyle, OnClickOutPoint);
            defaultNodeStyle = nodeStyle;
            selectedNodeStyle = selectedStyle;
            nodeActions = _nodeActions;
            runAtStart = _runAtStart;
            runTexture = _runTexture;
            loopTexture = _loopTexture;
            loop = _loop;
            nodeActions[0](this);
        }

        public void Drag(Vector2 delta)
        {
            rect.position += delta;
        }

        public void Draw()
        {
            inPoint.Draw();
            outPoint.Draw();

            GUI.Box(rect, title, style);

            if (runAtStart)
                GUI.DrawTexture(new Rect(rect.position.x + 12f, rect.position.y + 8f, 9f, 9f), runTexture);
            if (loop)
                GUI.DrawTexture(new Rect(rect.position.x + 179f, rect.position.y + 8f, 10f, 9f), loopTexture);

            if (Renaming)
            {
                GUI.SetNextControlName("renameField");
                title = GUI.TextField(rect, title, style);

                if (runAtStart)
                    GUI.DrawTexture(new Rect(rect.position.x + 12f, rect.position.y + 8f, 9f, 9f), runTexture);
                if (loop)
                    GUI.DrawTexture(new Rect(rect.position.x + 179f, rect.position.y + 8f, 10f, 9f), loopTexture);

                GUI.FocusControl("renameField");
                if (!selectCheck)
                {
                    var te = (TextEditor)GUIUtility.GetStateObject(typeof(TextEditor), GUIUtility.keyboardControl);
                    te.selectIndex = oldTitle.Length;
                    te.cursorIndex = 0;
                    selectCheck = true;
                }
            }
            else if (selectCheck) selectCheck = false;
        }

        public bool ProcessEvents(Event e)
        {
            switch (e.type)
            {
                case EventType.MouseDown:
                    if (e.button == 0)
                    {
                        if (rect.Contains(e.mousePosition))
                        {
                            isDragged = true;
                            GUI.changed = true;
                            isSelected = true;
                            style = selectedNodeStyle;
                        }
                        else
                        {
                            GUI.changed = true;
                            isSelected = false;
                            if (Renaming)
                            {
                                if (oldTitle != title)
                                {
                                    nodeActions[0](this);
                                }
                            }
                            Renaming = false;
                            style = defaultNodeStyle;
                        }
                    }
                    if (e.button == 1 && isSelected && rect.Contains(e.mousePosition))
                    {
                        ProcessContextMenu();
                        e.Use();
                    }
                    break;
                case EventType.MouseUp:
                    isDragged = false;
                    break;
                case EventType.MouseDrag:
                    if (e.button == 0 && isDragged)
                    {
                        Drag(e.delta);
                        e.Use();
                        return true;
                    }
                    break;
                case EventType.KeyDown:
                    if (isSelected && e.keyCode == KeyCode.Delete)
                    {
                        nodeActions[3](this);
                        e.Use();
                    }
                    break;
            }

            return false;
        }

        private void ProcessContextMenu()
        {
            GenericMenu genericMenu = new GenericMenu();
            genericMenu.AddItem(new GUIContent("Rename"), false, OnClickRenameNode);
            genericMenu.AddItem(new GUIContent("Copy"), false, () => nodeActions[1](this));
            genericMenu.AddItem(new GUIContent("Duplicate"), false, () => nodeActions[2](this));
            genericMenu.AddItem(new GUIContent("Delete"), false, OnClickRemoveNode);
            genericMenu.ShowAsContext();
        }

        private void OnClickRenameNode()
        {
            oldTitle = title;
            Renaming = true;
        }

        private void OnClickRemoveNode()
        {
            if (EditorUtility.DisplayDialog("Delete Effect", "Are you sure you want to delete " + title + " ?", "Delete", "Cancel"))
            {
                nodeActions[3](this);
            }
        }
    }
}
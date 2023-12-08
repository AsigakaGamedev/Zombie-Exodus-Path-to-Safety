using System;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace UIEM
{
    public class NodeBasedEditor : EditorWindow
    {
        private List<Node> nodes;
        private List<Connection> connections;

        private GUIStyle nodeStyle;
        private GUIStyle selectedNodeStyle;
        private GUIStyle inPointStyle;
        private GUIStyle outPointStyle;
        private Texture2D runTexture;
        private Texture2D loopTexture;

        private EditorZoomer zoomer = new EditorZoomer();

        private ConnectionPoint selectedInPoint;
        private ConnectionPoint selectedOutPoint;

        private Vector2 offset;
        private Vector2 drag;

        private UIEffectsManager manager;
        private int seeker = 0;
        private Vector2 origin = Vector2.zero;
        private Vector2 lastOffset = Vector2.zero;

        public static void OpenWindow(UIEffectsManager _manager)
        {
            if (!HasOpenInstances<NodeBasedEditor>())
            {
                NodeBasedEditor editor = GetWindow<NodeBasedEditor>(_manager.gameObject.name + " UI Effects");
                editor.manager = _manager;

                for (int i = 0; i < editor.manager.Settings.Count; i++)
                {
                    editor.AddNode(editor.manager.Settings[i].nodePos, editor.manager.Settings[i].RunAtStart, editor.manager.Settings[i].Loop);
                    editor.nodes[i].title = editor.manager.Settings[i].Name;
                }

                for (int i = 0; i < editor.manager.Settings.Count; i++)
                {
                    if (editor.manager.Settings[i].Outputs.Count > 0)
                    {
                        editor.selectedOutPoint = editor.nodes[i].outPoint;
                        foreach (string outputName in editor.manager.Settings[i].Outputs)
                        {
                            for (int j = 0; j < editor.nodes.Count; j++)
                            {
                                if (outputName == editor.nodes[j].title)
                                {
                                    editor.selectedInPoint = editor.nodes[j].inPoint;
                                    if (editor.connections == null)
                                    {
                                        editor.connections = new List<Connection>();
                                    }
                                    editor.connections.Add(new Connection(editor.selectedInPoint, editor.selectedOutPoint, editor.OnClickRemoveConnection));
                                }
                            }
                        }
                    }
                }
                editor.ClearConnectionSelection();
            }
			else
				GetWindow<NodeBasedEditor>();
        }

        private void OnEnable()
        {
            nodeStyle = new GUIStyle();
            nodeStyle.normal.textColor = Color.white;
            nodeStyle.alignment = TextAnchor.MiddleCenter;
            nodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1.png") as Texture2D;
            nodeStyle.border = new RectOffset(12, 12, 12, 12);

            selectedNodeStyle = new GUIStyle();
            selectedNodeStyle.normal.textColor = Color.white;
            selectedNodeStyle.alignment = TextAnchor.MiddleCenter;
            selectedNodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1 on.png") as Texture2D;
            selectedNodeStyle.border = new RectOffset(12, 12, 12, 12);

            inPointStyle = new GUIStyle();
            inPointStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left.png") as Texture2D;
            inPointStyle.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left on.png") as Texture2D;
            inPointStyle.border = new RectOffset(4, 4, 12, 12);

            outPointStyle = new GUIStyle();
            outPointStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right.png") as Texture2D;
            outPointStyle.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right on.png") as Texture2D;
            outPointStyle.border = new RectOffset(4, 4, 12, 12);

            runTexture = EditorGUIUtility.Load(AssetDatabase.GetAssetPath(MonoScript.FromScriptableObject(this)).Replace("Scripts/Editor/NodeBasedEditor.cs", "Textures/uiemPlay.png")) as Texture2D;
            loopTexture = EditorGUIUtility.Load(AssetDatabase.GetAssetPath(MonoScript.FromScriptableObject(this)).Replace("Scripts/Editor/NodeBasedEditor.cs", "Textures/uiemAgain.png")) as Texture2D;
        }

        private void OnGUI()
        {
            DrawGrid(20 * zoomer.zoom, 0.2f, Color.gray);
            DrawGrid(100 * zoomer.zoom, 0.4f, Color.gray);

            zoomer.Begin();

            DrawNodes();
            DrawConnections();
            DrawConnectionLine(Event.current);

            ProcessNodeEvents(Event.current);
            ProcessEvents(Event.current);

            zoomer.End();

            if (GUI.Button(new Rect(position.width - 150, 0, 150, 25), "Find the Next Node") && nodes != null && nodes.Count > 0)
            {
                if (seeker >= nodes.Count)
                    seeker = 0;
                drag = position.size / (2 * zoomer.zoom) - new Vector2(100f, 25f) - (nodes[seeker].rect.position);
                for (int i = 0; i < nodes.Count; i++)
                {
                    nodes[i].Drag(drag);
                    SetEffectPos(i);
                }
                seeker++;
                GUI.changed = true;
            }

            if (GUI.changed) Repaint();
        }

        private void DrawGrid(float gridSpacing, float gridOpacity, Color gridColor)
        {
            int widthDivs = Mathf.CeilToInt(position.width / gridSpacing);
            int heightDivs = Mathf.CeilToInt(position.height / gridSpacing);

            Handles.BeginGUI();
            Handles.color = new Color(gridColor.r, gridColor.g, gridColor.b, gridOpacity);

            offset += drag * 0.5f * zoomer.zoom;
            Vector3 newOffset = new Vector3(offset.x % gridSpacing, offset.y % gridSpacing, 0);

            for (int i = 0; i < widthDivs; i++)
            {
                Handles.DrawLine(new Vector3(gridSpacing * i, -gridSpacing, 0) + newOffset, new Vector3(gridSpacing * i, position.height, 0f) + newOffset);
            }

            for (int j = 0; j < heightDivs; j++)
            {
                Handles.DrawLine(new Vector3(-gridSpacing, gridSpacing * j, 0) + newOffset, new Vector3(position.width, gridSpacing * j, 0f) + newOffset);
            }

            Handles.color = Color.white;
            Handles.EndGUI();
        }

        private void DrawNodes()
        {
            if (lastOffset != Vector2.zero && lastOffset - zoomer.GetContentOffset() != Vector2.zero)
                origin += lastOffset - zoomer.GetContentOffset();

            if (nodes != null)
            {
                for (int i = 0; i < nodes.Count; i++)
                {
                    nodes[i].Draw();
                    if (lastOffset != Vector2.zero && lastOffset - zoomer.GetContentOffset() != Vector2.zero)
                    {
                        nodes[i].Drag(lastOffset - zoomer.GetContentOffset());
                        SetEffectPos(i);
                    }
                }
            }

            lastOffset = zoomer.GetContentOffset();
        }

        private void DrawConnections()
        {
            if (connections != null)
            {
                for (int i = 0; i < connections.Count; i++)
                {
                    connections[i].Draw();
                }
            }
        }

        private void ProcessEvents(Event e)
        {
            drag = Vector2.zero;
            switch (e.type)
            {
                case EventType.MouseDown:
                    if (e.button == 0)
                    {
                        ClearConnectionSelection();
                    }
                    if (e.button == 1)
                    {
                        ProcessContextMenu(e.mousePosition);
                    }
                    break;
                case EventType.MouseDrag:
                    if (e.button == 0)
                    {
                        OnDrag(e.delta);
                    }
                    break;
                case EventType.MouseUp:
                    if (e.button == 0 && nodes != null)
                    {
                        for (int i = 0; i < nodes.Count; i++)
                        {
                            SetEffectPos(i);
                        }
                    }
                    break;
            }
        }

        private void ProcessNodeEvents(Event e)
        {
            if (nodes != null)
            {
                for (int i = nodes.Count - 1; i >= 0; i--)
                {
                    bool guiChanged = nodes[i].ProcessEvents(e);
                    if (guiChanged)
                    {
                        GUI.changed = true;
                    }
                }
            }
        }

        private void DrawConnectionLine(Event e)
        {
            if (selectedInPoint != null && selectedOutPoint == null)
            {
                Handles.DrawBezier(
                    selectedInPoint.rect.center,
                    e.mousePosition,
                    selectedInPoint.rect.center + Vector2.left * 50f,
                    e.mousePosition - Vector2.left * 50f,
                    new Color(1.0f, 0.64f, 0.0f),
                    null,
                    3f
                );

                GUI.changed = true;
            }

            if (selectedOutPoint != null && selectedInPoint == null)
            {
                Handles.DrawBezier(
                    selectedOutPoint.rect.center,
                    e.mousePosition,
                    selectedOutPoint.rect.center - Vector2.left * 50f,
                    e.mousePosition + Vector2.left * 50f,
                    new Color(1.0f, 0.64f, 0.0f),
                    null,
                    3f
                );

                GUI.changed = true;
            }
        }

        private void ProcessContextMenu(Vector2 mousePosition)
        {
            GenericMenu genericMenu = new GenericMenu();
            genericMenu.AddItem(new GUIContent("Add Effect"), false, () => OnClickAddEffect(mousePosition, false, false));
            if (EditorGUIUtility.systemCopyBuffer.StartsWith("VUlFZmZlY3RzTWFrZXI= "))
                genericMenu.AddItem(new GUIContent("Paste"), false, OnClickPaste);
            else
                genericMenu.AddDisabledItem(new GUIContent("Paste"));
            genericMenu.ShowAsContext();
        }

        private void OnDrag(Vector2 delta)
        {
            drag = delta;

            if (nodes != null)
            {
                for (int i = 0; i < nodes.Count; i++)
                {
                    nodes[i].Drag(delta);
                }
            }

            GUI.changed = true;
        }

        private void OnClickAddEffect(Vector2 mousePosition, bool runAtStart, bool loop)
        {
            AddNode(mousePosition, runAtStart, loop);
            manager.Settings.Add(new UIEffect(nodes[nodes.Count - 1].title, mousePosition - origin));
        }

        private void AddNode(Vector2 mousePosition, bool runAtStart, bool loop)
        {
            if (nodes == null)
            {
                nodes = new List<Node>();
            }

            nodes.Add(new Node(mousePosition, 200, 50, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, runTexture, loopTexture, OnClickInPoint, OnClickOutPoint, new Action<Node>[] { OnClickRenameNode, OnClickCopyNode, OnClickDupNode, OnClickRemoveNode }, runAtStart, loop));
        }

        private void OnClickPaste()
        {
            try
            {
                string[] copiedVariables = EditorGUIUtility.systemCopyBuffer.TrimStart("VUlFZmZlY3RzTWFrZXI= ".ToCharArray()).Split(' ');
                string[] vectorOrColor;
                OnClickAddEffect(Vector2.zero, copiedVariables[0] == "1", copiedVariables[1] == "1");
                UIEffect effect = manager.Settings[manager.Settings.Count - 1];
                effect.EffectType = (UIEffect.effectTypes)int.Parse(copiedVariables[2]);
                effect.Delay = float.Parse(copiedVariables[3]);
                effect.Speed = float.Parse(copiedVariables[4]);
                effect.initialState = (UIEffect.initialStates)int.Parse(copiedVariables[5]);
                vectorOrColor = copiedVariables[6].Split('|');
                effect.startVector = new Vector3(float.Parse(vectorOrColor[0]), float.Parse(vectorOrColor[1]), float.Parse(vectorOrColor[2]));
                vectorOrColor = copiedVariables[7].Split('|');
                effect.targetVector = new Vector3(float.Parse(vectorOrColor[0]), float.Parse(vectorOrColor[1]), float.Parse(vectorOrColor[2]));
                effect.Duration = float.Parse(copiedVariables[8]);
                vectorOrColor = copiedVariables[9].Split('|');
                effect.color = new Color(float.Parse(vectorOrColor[0]), float.Parse(vectorOrColor[1]), float.Parse(vectorOrColor[2]));
                effect.RotationType = (UIEffect.rotationTypes)int.Parse(copiedVariables[10]);
                effect.RotationDirection = (UIEffect.rotDirections)int.Parse(copiedVariables[11]);
                effect.FadeType = (UIEffect.fadeTypes)int.Parse(copiedVariables[12]);
                effect.ApplyToChildren = copiedVariables[13] == "1";
                effect.startAlpha = int.Parse(copiedVariables[14]);
                vectorOrColor = copiedVariables[15].Split('|');
                effect.startColor = new Color(float.Parse(vectorOrColor[0]), float.Parse(vectorOrColor[1]), float.Parse(vectorOrColor[2]));
                effect.BrightnessDuration = float.Parse(copiedVariables[16]);
                effect.ShakeOrJellyDirection = (UIEffect.shakeDirections)int.Parse(copiedVariables[17]);
                effect.Amplitude = float.Parse(copiedVariables[18]);
                manager.Settings[manager.Settings.Count - 1] = effect;
            }
            catch {};
        }

        private void OnClickInPoint(ConnectionPoint inPoint)
        {
            selectedInPoint = inPoint;
            if (selectedOutPoint != null)
            {
                if (selectedOutPoint.node != selectedInPoint.node)
                {
                    CreateConnection();
                    ClearConnectionSelection();
                }
                else
                {
                    ClearConnectionSelection();
                }
            }
        }

        private void OnClickOutPoint(ConnectionPoint outPoint)
        {
            selectedOutPoint = outPoint;
            if (selectedInPoint != null)
            {
                if (selectedOutPoint.node != selectedInPoint.node)
                {
                    CreateConnection();
                    ClearConnectionSelection();
                }
                else
                {
                    ClearConnectionSelection();
                }
            }
        }

        private void OnClickRemoveNode(Node node)
        {
            if (connections != null)
            {
                List<Connection> connectionsToRemove = new List<Connection>();

                for (int i = 0; i < connections.Count; i++)
                {
                    if (connections[i].inPoint == node.inPoint || connections[i].outPoint == node.outPoint)
                    {
                        connectionsToRemove.Add(connections[i]);
                    }
                }

                for (int i = 0; i < connectionsToRemove.Count; i++)
                {
                    OnClickRemoveConnection(connectionsToRemove[i]);
                }
            }

            RemoveEffect(node);
            nodes.Remove(node);
        }

        private void OnClickRemoveConnection(Connection connection)
        {
            RemoveConnection(connection.outPoint.node, connection.inPoint.node.title);
            connections.Remove(connection);
        }

        private void RemoveConnection(Node outNode, string toRemove)
        {
            int index = nodes.IndexOf(outNode);
            UIEffect effect = manager.Settings[index];
            effect.Outputs.Remove(toRemove);
            manager.Settings[index] = effect;
        }

        private void CreateConnection()
        {
            if (connections == null)
            {
                connections = new List<Connection>();
            }

            foreach (Connection conn in connections)
            {
                if (conn.inPoint.node == selectedInPoint.node && conn.outPoint.node == selectedOutPoint.node)
                    return;
            }
            connections.Add(new Connection(selectedInPoint, selectedOutPoint, OnClickRemoveConnection));
            SetConnection(selectedInPoint, selectedOutPoint);
        }

        private void ClearConnectionSelection()
        {
            selectedInPoint = null;
            selectedOutPoint = null;
        }

        private void SetConnection(ConnectionPoint inPoint, ConnectionPoint outPoint)
        {
            int index = nodes.IndexOf(outPoint.node);
            UIEffect effect = manager.Settings[index];
            effect.Outputs.Add(inPoint.node.title);
            manager.Settings[index] = effect;
        }

        private void OnClickRenameNode(Node node)
        {
            if (node.title == "")
            {
                int counter = 1;
                while (true)
                {
                    bool contains = false;
                    foreach (Node _node in nodes)
                    {
                        if (_node.title == "effect" + counter.ToString())
                        {
                            contains = true;
                            break;
                        }
                    }
                    if (contains)
                        counter++;
                    else
                    {
                        node.title = "effect" + counter.ToString();
                        if (node.oldTitle != "")
                        {
                            RenameEffect(node);
                        }
                        return;
                    }
                }
            }
            else
            {
                foreach (Node _node in nodes)
                {
                    if (_node.title == node.title && _node != node)
                    {
                        node.title = node.oldTitle;
                        return;
                    }
                }
                RenameEffect(node);
            }
        }

        private void RenameEffect(Node node)
        {
            for (int i = 0; i < manager.Settings.Count; i++)
            {
                if (manager.Settings[i].Outputs.Contains(node.oldTitle))
                {
                    manager.Settings[i].Outputs.Remove(node.oldTitle);
                    manager.Settings[i].Outputs.Add(node.title);
                }
                if (node.oldTitle == manager.Settings[i].Name)
                {
                    UIEffect effect = manager.Settings[i];
                    effect.Name = node.title;
                    manager.Settings[i] = effect;
                }
            }
        }

        private void OnClickCopyNode(Node node)
        {
            UIEffect effect = manager.Settings[nodes.IndexOf(node)];
            EditorGUIUtility.systemCopyBuffer = "VUlFZmZlY3RzTWFrZXI= " +
                Convert.ToInt32(effect.RunAtStart).ToString() + " " +
                Convert.ToInt32(effect.Loop).ToString() + " " +
                ((int)effect.EffectType).ToString() + " " +
                effect.Delay.ToString() + " " +
                effect.Speed.ToString() + " " +
                ((int)effect.initialState).ToString() + " " +
                effect.startVector.x.ToString() + "|" + effect.startVector.y.ToString() + "|" + effect.startVector.z.ToString() + " " +
                effect.targetVector.x.ToString() + "|" + effect.targetVector.y.ToString() + "|" + effect.targetVector.z.ToString() + " " +
                effect.Duration.ToString() + " " +
                effect.color.r.ToString() + "|" + effect.color.g.ToString() + "|" + effect.color.b.ToString() + " " +
                ((int)effect.RotationType).ToString() + " " +
                ((int)effect.RotationDirection).ToString() + " " +
                ((int)effect.FadeType).ToString() + " " +
                Convert.ToInt32(effect.ApplyToChildren).ToString() + " " +
                effect.startAlpha.ToString() + " " +
                effect.startColor.r.ToString() + "|" + effect.startColor.g.ToString() + "|" + effect.startColor.b.ToString() + " " +
                effect.BrightnessDuration.ToString() + " " +
                ((int)effect.ShakeOrJellyDirection).ToString() + " " +
                effect.Amplitude.ToString();
        }

        private void OnClickDupNode(Node node)
        {
            UIEffect effect = manager.Settings[nodes.IndexOf(node)];
            OnClickAddEffect(Vector2.zero, effect.RunAtStart, effect.Loop);
            effect.nodePos = manager.Settings[manager.Settings.Count - 1].nodePos;
            effect.Name = manager.Settings[manager.Settings.Count - 1].Name;
            effect.Outputs = manager.Settings[manager.Settings.Count - 1].Outputs;
            effect.OnStart = null;
            effect.OnFinished = null;
            manager.Settings[manager.Settings.Count - 1] = effect;
        }

        private void RemoveEffect(Node node)
        {
            int index = nodes.IndexOf(node);
            if (seeker - 1 == index)
                seeker--;
            manager.Settings.RemoveAt(index);
        }

        private void SetEffectPos(int index)
        {
            UIEffect effect = manager.Settings[index];
            effect.nodePos = new Vector2(nodes[index].rect.x - origin.x, nodes[index].rect.y - origin.y);
            manager.Settings[index] = effect;
        }
    }
}
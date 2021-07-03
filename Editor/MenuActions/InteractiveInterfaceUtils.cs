﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using AlephVault.Unity.Support.Utils;
using AlephVault.Unity.MenuActions.Utils;

namespace GameMeanMachine.Unity.GabTab
{
    namespace MenuActions
    {
        using GameMeanMachine.Unity.GabTab.Authoring.Behaviours;
            
        /// <summary>
        ///   Menu actions to create <see cref="Behaviours.InteractiveInterface"/> with the inner
        ///     message component.
        /// </summary>
        public static class InteractiveInterfaceUtils
        {
            private class CreateInteractiveInterfaceWindow : EditorWindow
            {
                public Transform selectedTransform;
                private string interactiveInterfaceObjectName = "New Interactive Interface";
                private float interfaceHeight = 0.3f;
                private Color interfaceTint = Color.white;
                private Color messageTint = new Color(15/16f, 15/16f, 15/16f);
                private Color messageContentColor = Color.black;

                private void OnGUI()
                {
                    GUIStyle longLabelStyle = MenuActionUtils.GetSingleLabelStyle();

                    titleContent = new GUIContent("Gab Tab - Creating a new Interactive Interface");

                    EditorGUILayout.BeginVertical();

                    EditorGUILayout.LabelField("This wizard will create an Interactive Interface inside a HUD, at the bottom.", longLabelStyle);

                    EditorGUILayout.LabelField("This is the name the Interactive Interface game object will have when added to the hierarchy.", longLabelStyle);
                    interactiveInterfaceObjectName = MenuActionUtils.EnsureNonEmpty(EditorGUILayout.TextField("Int. Interface name", interactiveInterfaceObjectName), "New Interactive Interface");

                    EditorGUILayout.LabelField("Interactive Interface height is a fraction of the height of the parent canvas.", longLabelStyle);
                    interfaceHeight = EditorGUILayout.Slider("Int. Interface height", interfaceHeight, 0f, 1f);
                    interfaceTint = EditorGUILayout.ColorField("Int. Interface tint", interfaceTint);
                    messageTint = EditorGUILayout.ColorField("Int. Message tint", messageTint);
                    messageContentColor = EditorGUILayout.ColorField("Int. Message color", messageContentColor);

                    if (GUILayout.Button("Create Interactive Interface")) Execute();
                    EditorGUILayout.EndVertical();
                }

                private void Execute()
                {
                    Canvas selectedCanvas = selectedTransform.GetComponent<Canvas>();
                    Rect canvasRect = selectedCanvas.GetComponent<RectTransform>().rect;
                    float messageOffset = Values.Max(canvasRect.height, canvasRect.width) * 0.01f;
                    Sprite background = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Background.psd");

                    // Creating the object
                    GameObject interactiveInterfaceObject = new GameObject(interactiveInterfaceObjectName);
                    interactiveInterfaceObject.SetActive(false);
                    interactiveInterfaceObject.transform.parent = selectedTransform;
                    Image interactiveInterfaceImageComponent = AlephVault.Unity.Layout.Utils.Behaviours.AddComponent<Image>(interactiveInterfaceObject);
                    interactiveInterfaceImageComponent.sprite = background;
                    interactiveInterfaceImageComponent.type = Image.Type.Sliced;
                    interactiveInterfaceImageComponent.color = interfaceTint;
                    interactiveInterfaceImageComponent.fillCenter = true;
                    RectTransform interactiveInterfaceRectTransform = interactiveInterfaceObject.GetComponent<RectTransform>();
                    interactiveInterfaceRectTransform.anchorMin = Vector2.zero;
                    interactiveInterfaceRectTransform.anchorMax = new Vector2(1f, interfaceHeight);
                    interactiveInterfaceRectTransform.pivot = new Vector2(0.5f, 0);
                    interactiveInterfaceRectTransform.sizeDelta = Vector2.zero;
                    interactiveInterfaceRectTransform.localScale = Vector3.one;
                    Authoring.Behaviours.InteractiveInterface interactiveInterfaceComponent = AlephVault.Unity.Layout.Utils.Behaviours.AddComponent<Authoring.Behaviours.InteractiveInterface>(interactiveInterfaceObject);
                        
                    // Create the interactive message
                    GameObject interactiveMessageObject = new GameObject("Message");
                    interactiveMessageObject.transform.parent = interactiveInterfaceObject.transform;
                    Image interactiveMessageImageComponent = AlephVault.Unity.Layout.Utils.Behaviours.AddComponent<Image>(interactiveMessageObject);
                    interactiveMessageImageComponent.sprite = background;
                    interactiveMessageImageComponent.type = Image.Type.Sliced;
                    interactiveMessageImageComponent.color = messageTint;
                    interactiveMessageImageComponent.fillCenter = true;
                    RectTransform interactiveMessageRectTransform = interactiveMessageObject.GetComponent<RectTransform>();
                    interactiveMessageRectTransform.localScale = Vector2.one;
                    interactiveMessageRectTransform.anchorMin = new Vector2(0f, 0.30f);
                    interactiveMessageRectTransform.anchorMax = new Vector2(1f, 1f);
                    interactiveMessageRectTransform.pivot = new Vector2(0.5f, 0);
                    interactiveMessageRectTransform.sizeDelta = Vector2.zero;
                    interactiveMessageRectTransform.offsetMax = Vector2.one * -messageOffset;
                    interactiveMessageRectTransform.offsetMin = Vector2.one * messageOffset;
                    InteractiveMessage interactiveMessageComponent = AlephVault.Unity.Layout.Utils.Behaviours.AddComponent<InteractiveMessage>(interactiveMessageObject);

                    // Create the interactive message content
                    GameObject interactiveMessageContentObject = new GameObject("Message Content");
                    interactiveMessageContentObject.transform.parent = interactiveMessageObject.transform;
                    InteractiveMessageContent interactiveMessageContentComponent = AlephVault.Unity.Layout.Utils.Behaviours.AddComponent<InteractiveMessageContent>(interactiveMessageContentObject);
                    Text interactiveMessageContentTextComponent = interactiveMessageContentObject.GetComponent<Text>();
                    interactiveMessageContentTextComponent.alignment = TextAnchor.UpperLeft;
                    interactiveMessageContentTextComponent.horizontalOverflow = HorizontalWrapMode.Wrap;
                    interactiveMessageContentTextComponent.verticalOverflow = VerticalWrapMode.Overflow;
                    interactiveMessageContentTextComponent.text = "Lorem ipsum dolor sit amet consectetur adipiscing elit.";
                    interactiveMessageContentTextComponent.fontSize = (int)(interactiveMessageRectTransform.rect.height / 2.4f);
                    interactiveMessageContentTextComponent.color = messageContentColor;
                    ContentSizeFitter interactiveMessageContentContentSizeFitterComponent = interactiveMessageContentObject.GetComponent<ContentSizeFitter>();
                    interactiveMessageContentContentSizeFitterComponent.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
                    interactiveMessageContentContentSizeFitterComponent.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
                    RectTransform interactiveMessageContentRectTransform = interactiveMessageContentObject.GetComponent<RectTransform>();
                    interactiveMessageContentRectTransform.localScale = Vector3.one;
                    interactiveMessageContentRectTransform.anchorMin = new Vector2(0f, 1f);
                    interactiveMessageContentRectTransform.anchorMax = new Vector2(0f, 1f);
                    interactiveMessageContentRectTransform.pivot = new Vector2(0f, 1f);
                    // I don't know - perhaps a bug or something.
                    interactiveMessageContentRectTransform.sizeDelta = new Vector2(interactiveMessageRectTransform.rect.size.x - 2 * messageOffset, 0);
                    interactiveMessageContentRectTransform.offsetMin = new Vector2(messageOffset, 0);

                    // Set the message content into the message
                    AlephVault.Unity.Layout.Utils.Behaviours.SetObjectFieldValues(interactiveMessageComponent, new Dictionary<string, object>()
                    {
                        { "messageContent", interactiveMessageContentComponent }
                    });

                    // Set the message into the interface
                    AlephVault.Unity.Layout.Utils.Behaviours.SetObjectFieldValues(interactiveInterfaceComponent, new Dictionary<string, object>()
                    {
                        { "interactiveMessage", interactiveMessageComponent }
                    });
                    interactiveInterfaceObject.SetActive(true);
                    Undo.RegisterCreatedObjectUndo(interactiveInterfaceObject, "Create Interactive Interface");
                    Close();
                }
            }

            /// <summary>
            ///   This method is used in the menu action: GameObject > Gab Tab > Create Interactive Interface.
            ///   It creates an <see cref="InteractiveInterface"/>, with their inner message component, in the scene.
            /// </summary>
            [MenuItem("GameObject/Gab Tab/Create Interactive Interface", false, 11)]
            public static void CreateInteractiveInterface()
            {
                CreateInteractiveInterfaceWindow window = ScriptableObject.CreateInstance<CreateInteractiveInterfaceWindow>();
                window.maxSize = new Vector2(550, 186);
                window.minSize = window.maxSize;
                window.selectedTransform = Selection.activeTransform;
                window.ShowUtility();
            }

            /// <summary>
            ///   Validates the menu item: GameObject > Gab Tab > Create Interactive Interface.
            ///   It enables such menu option when a canvas is selected in the scene hierarchy.
            /// </summary>
            [MenuItem("GameObject/Gab Tab/Create Interactive Interface", true)]
            public static bool CanCreateInteractiveInterface()
            {
                return Selection.activeTransform != null && Selection.activeTransform.GetComponent<Canvas>();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Gamekit3D;

namespace Gamekit3D
{
    [CustomEditor(typeof(TransitionPoint))]
    public class TransitionStartEditor : Editor
    {
        SerializedProperty m_TransitioningGameObjectProp;
        SerializedProperty m_TransitionTypeProp;
        SerializedProperty m_NewSceneNameProp;
        SerializedProperty m_TransitionDestinationTagProp;
        SerializedProperty m_DestinationTransformProp;
        SerializedProperty m_TransitionWhenProp;
        SerializedProperty m_RequiresInventoryCheckProp;
        SerializedProperty m_InventoryControllerProp;
        SerializedProperty m_InventoryCheckProp;
        SerializedProperty m_InventoryItemsProp;
        SerializedProperty m_OnHasItemProp;
        SerializedProperty m_OnDoesNotHaveItemProp;

        GUIContent[] m_InventoryControllerItems = new GUIContent[0];

        void OnEnable()
        {
            m_TransitioningGameObjectProp = serializedObject.FindProperty("transitioningGameObject");
            m_TransitionTypeProp = serializedObject.FindProperty("transitionType");
            m_NewSceneNameProp = serializedObject.FindProperty("newSceneName");
            m_TransitionDestinationTagProp = serializedObject.FindProperty("transitionDestinationTag");
            m_DestinationTransformProp = serializedObject.FindProperty("destinationTransform");
            m_TransitionWhenProp = serializedObject.FindProperty("transitionWhen");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(m_TransitioningGameObjectProp);

            EditorGUILayout.PropertyField(m_TransitionTypeProp);
            EditorGUI.indentLevel++;
            if ((TransitionPoint.TransitionType)m_TransitionTypeProp.enumValueIndex == TransitionPoint.TransitionType.SameScene)
            {
                EditorGUILayout.PropertyField(m_DestinationTransformProp);
            }
            else
            {
                EditorGUILayout.PropertyField(m_NewSceneNameProp);
                EditorGUILayout.PropertyField(m_TransitionDestinationTagProp);
            }
            EditorGUI.indentLevel--;

            EditorGUILayout.PropertyField(m_TransitionWhenProp);

            //EditorGUILayout.PropertyField(m_RequiresInventoryCheckProp);
            //if (m_RequiresInventoryCheckProp.boolValue)
            //{
            //    EditorGUI.indentLevel++;

            //    EditorGUI.BeginChangeCheck();
            //}

            serializedObject.ApplyModifiedProperties();
        }

    }
}
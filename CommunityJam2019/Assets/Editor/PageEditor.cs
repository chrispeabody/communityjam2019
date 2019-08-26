using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Page))]
public class PageEditor : Editor {

    public override void OnInspectorGUI() {
    	//base.OnInspectorGUI();

    	serializedObject.Update();
    	Page page = (Page) target;

    	Beat beatToRemove = null;
    	foreach(Beat beat in page.beats) {
    		GUILayout.BeginHorizontal();

    		if (GUILayout.Button("X", GUILayout.Width(18))) {
    			beatToRemove = beat;
    		}

    		beat.setWords(EditorGUILayout.TextArea(beat.getWords()));
    		GUILayout.EndHorizontal();
    	}
    	if (beatToRemove != null) {page.beats.Remove(beatToRemove);}

    	if (GUILayout.Button("Add beat")) {
    		page.beats.Add(new Beat(""));
    	}

		Choice choiceToRemove = null;
    	foreach(Choice choice in page.choices) {
    		GUILayout.BeginHorizontal();

    		if (GUILayout.Button("X", GUILayout.Width(18))) {
    			choiceToRemove = choice;
    		}

    		choice.setWords(EditorGUILayout.TextField("Choice:", choice.getWords()));
    		GUILayout.EndHorizontal();

    		choice.setLink((Page) EditorGUILayout.ObjectField("Page link:", choice.getLink(), typeof(Page), false));
    	}
    	if (choiceToRemove != null) {page.choices.Remove(choiceToRemove);}

    	if (page.choices.Count < 4) {
	    	if (GUILayout.Button("Add choice")) {
	    		page.choices.Add(new Choice("", null));
	    	}
    	}

    	serializedObject.ApplyModifiedProperties();
    	EditorUtility.SetDirty(page);
    }
}

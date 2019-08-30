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

    	EditorGUILayout.LabelField("Beats");

    	Beat beatToRemove = null;
    	foreach(Beat beat in page.beats) {
    		string moodStr = "";
    		if (beat.getRequiredMood() != Mood.None) {moodStr = " ["+beat.getRequiredMood()+" "+beat.getRequiredMoodAmount()+"]";}  
    		beat.expandInEditor = EditorGUILayout.Foldout(beat.expandInEditor, beat.getWords() + moodStr, true);

    		if (beat.expandInEditor) {
	    		GUILayout.BeginHorizontal();

	    		if (GUILayout.Button("X", GUILayout.Width(18))) {
	    			beatToRemove = beat;
	    		}

	    		beat.setWords(EditorGUILayout.TextArea(beat.getWords()));
	    		GUILayout.EndHorizontal();

	    		GUILayout.BeginHorizontal();
	    		beat.setRequiredMood((Mood)EditorGUILayout.EnumPopup("Required:", beat.getRequiredMood()));
	    		beat.setRequiredMoodAmount(EditorGUILayout.IntField(beat.getRequiredMoodAmount()));
	    		GUILayout.EndHorizontal();  
	    	}		
    	}
    	if (beatToRemove != null) {page.beats.Remove(beatToRemove);}

    	if (GUILayout.Button("Add beat")) {
    		page.beats.Add(new Beat(""));
    	}

    	EditorGUILayout.LabelField("Choices");

		Choice choiceToRemove = null;
    	foreach(Choice choice in page.choices) {
    		string moodStr = "";
    		if (choice.getRequiredMood() != Mood.None) {moodStr = " ["+choice.getRequiredMood()+" "+choice.getRequiredMoodAmount()+"]";}  
    		choice.expandInEditor = EditorGUILayout.Foldout(choice.expandInEditor, choice.getWords() + moodStr, true);

    		if (choice.expandInEditor) {
	    		GUILayout.BeginHorizontal();

	    		if (GUILayout.Button("X", GUILayout.Width(18))) {
	    			choiceToRemove = choice;
	    		}

	    		choice.setWords(EditorGUILayout.TextField("Choice:", choice.getWords()));
	    		GUILayout.EndHorizontal();
	    		
	    		GUILayout.BeginHorizontal();
	    		choice.setRequiredMood((Mood)EditorGUILayout.EnumPopup("Required:", choice.getRequiredMood()));
	    		choice.setRequiredMoodAmount(EditorGUILayout.IntField(choice.getRequiredMoodAmount()));
	    		GUILayout.EndHorizontal();  

	    		GUILayout.BeginHorizontal();
	    		choice.setMood((Mood)EditorGUILayout.EnumPopup("Add:",choice.getMood()));
	    		choice.setMoodMod(EditorGUILayout.IntField(choice.getMoodMod()));
	    		GUILayout.EndHorizontal();

	    		choice.setLink((Page) EditorGUILayout.ObjectField("Page link:", choice.getLink(), typeof(Page), false));

	    		GUILayout.BeginHorizontal();
	    		choice.setAltMood((Mood)EditorGUILayout.EnumPopup("Alt link:",choice.getAltMood()));
	    		choice.setAltMoodReq(EditorGUILayout.IntField(choice.getAltMoodReq()));
	    		choice.setAltLink((Page) EditorGUILayout.ObjectField(choice.getAltLink(), typeof(Page), false));
	    		GUILayout.EndHorizontal();
			}
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

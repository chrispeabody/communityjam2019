using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Beat {
	[SerializeField] protected string _words;
	[SerializeField] protected Mood _requiredMood;
	[SerializeField] protected int _requiredMoodAmount;
	public bool expandInEditor; // This is used only for the editor

	public Beat (string words) {
		_words = words;
	}

	public string getWords() {return _words;}
	public void setWords(string words) {_words = words;}
	public Mood getRequiredMood() {return _requiredMood;}
	public void setRequiredMood(Mood requiredMood) {_requiredMood = requiredMood;}
	public int getRequiredMoodAmount() {return _requiredMoodAmount;}
	public void setRequiredMoodAmount(int requiredMoodAmount) {_requiredMoodAmount = requiredMoodAmount;}
}

[System.Serializable]
public class Choice : Beat {
	[SerializeField] private Page _link;
	[SerializeField] private Mood _mood;
	[SerializeField] private int _moodMod;

	[SerializeField] private Mood _altMood;
	[SerializeField] private int _altMoodReq;
	[SerializeField] private Page _altLink;

	public Choice (string words, Page link) : base(words) {
		_link = link;
	}

	public Page getLink() {return _link;}
	public void setLink(Page link) {_link = link;}
	public Mood getMood() {return _mood;}
	public void setMood(Mood mood) {_mood = mood;}
	public int getMoodMod() {return _moodMod;}
	public void setMoodMod(int moodMod) {_moodMod = moodMod;}

	public Page getAltLink() {return _altLink;}
	public void setAltLink(Page link) {_altLink = link;}
	public Mood getAltMood() {return _altMood;}
	public void setAltMood(Mood mood) {_altMood = mood;}
	public int getAltMoodReq() {return _altMoodReq;}
	public void setAltMoodReq(int altMoodReq) {_altMoodReq = altMoodReq;}
}

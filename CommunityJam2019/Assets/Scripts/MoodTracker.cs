using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoodTracker {
	Dictionary<Mood,int> _moods = new Dictionary<Mood,int>();

	public int getMood(Mood mood) {
		if (_moods.ContainsKey(mood)) {
			return _moods[mood];
		} else {
			return 0;
		}
	}

	public int addToMood(Mood mood, int x) {
		if (_moods.ContainsKey(mood)) {
			Debug.Log("Added "+x+" to mood "+mood);
			_moods[mood] += x;
		} else {
			Debug.Log("Created mood "+mood);
			_moods.Add(mood,x);
		}

		Debug.Log("Total is now "+_moods[mood]);
		return _moods[mood];
	}

	public Dictionary<Mood,int> getMoods() {return _moods;}
}

public enum Mood {
	None,
	Agitated,
	Appeased
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beat {
	protected string _words;

	public Beat (string words) {
		_words = words;
	}

	public string getWords() {return _words;}
	public void setWords(string words) {_words = words;}
}

public class Choice : Beat {
	private Page _link;

	public Choice (string words, Page link) : base(words) {
		_link = link;
	}

	public Page getLink() {return _link;}
	public void setLink(Page link) {_link = link;}
}

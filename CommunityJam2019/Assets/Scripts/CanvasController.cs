using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour {
    // GameObjects
    public GameObject _mainTextbox;
    public GameObject _option1Button;
    public GameObject _option2Button;
    public GameObject _option3Button;
    public GameObject _option4Button;
    public Page startingPage;

    // Visual components
	private Text _words;
	private Text _option1;
	private Text _option2;
	private Text _option3;
	private Text _option4;
    private Page _page;

    // Other
    private int _currentBeat;
    private int _finalBeat;

    // Start is called before the first frame update
    void Start() {
        _words = _mainTextbox.GetComponent<Text>();
        _option1 = _option1Button.GetComponentInChildren<Text>();
        _option2 = _option2Button.GetComponentInChildren<Text>();
        _option3 = _option3Button.GetComponentInChildren<Text>();
        _option4 = _option4Button.GetComponentInChildren<Text>();

        updatePage(startingPage);
    }

    // Update is called once per frame
    void Update() {
        if (Input.anyKeyDown) {
        	Debug.Log("current: "+_currentBeat);
        	Debug.Log("final: "+_finalBeat);
        	if (_currentBeat < _finalBeat) {
        		nextBeat();
        	}
        }
    }

    private void updatePage (Page page) {
    	_page = page;
    	_words.text = _page.beats[0].getWords();

    	_currentBeat = 0;
    	_finalBeat = _page.beats.Count-1;

    	checkDisplayButtons();
    }

    private void nextBeat () {
    	_currentBeat++;
    	_words.text = _page.beats[_currentBeat].getWords();

    	checkDisplayButtons();
    }

    private void checkDisplayButtons() {
    	if (_currentBeat == _finalBeat) {
    		_option1Button.SetActive(true);
    		_option2Button.SetActive(true);
    		_option3Button.SetActive(true);
    		_option4Button.SetActive(true);
    	} else {
    		_option1Button.SetActive(false);
    		_option2Button.SetActive(false);
    		_option3Button.SetActive(false);
    		_option4Button.SetActive(false);
    	}
    }

    public Page getPage() {return _page;}
    public void setPage(Page page) {_page = page;}
}

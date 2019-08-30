﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour {
    // GameObjects & public stuff
    public GameObject _mainTextbox;
    public GameObject _option1Button;
    public GameObject _option2Button;
    public GameObject _option3Button;
    public GameObject _option4Button;
    public GameObject _soundManagerGO;
    public Page startingPage;
    public SoundCollection _selectionSound;
    public SoundCollection _newBeatSound;


    // Visual components
    private List<GameObject> _optionGOList = new List<GameObject>();
    private List<Text> _optionList = new List<Text>();
	private Text _words;
    private Page _page;

    // Other
    private SoundManager _soundManager;
    private int _currentBeat;
    private int _finalBeat;
    private MoodTracker _moodTracker = new MoodTracker();
    
    //Things for the lightning flash
    public RawImage _lightningBackground;
    public RawImage _rainBackground;
    public bool _isLightning = false;
    private float _curTimeForFlash = 0.4f;
    private float _totTimeForFlash = 0.4f;
    private Color _lastColor;
    private float _doubleStrikeBuffer = 0f;
    public SoundCollection _thunderSounds;

    // Start is called before the first frame update
    void Start() {
        _words = _mainTextbox.GetComponent<Text>();

        // It's ugly, I know -cp
        _optionGOList.Add(_option1Button);
        _optionGOList.Add(_option2Button);
        _optionGOList.Add(_option3Button);
        _optionGOList.Add(_option4Button);

        foreach (GameObject go in _optionGOList) {
        	_optionList.Add(go.GetComponentInChildren<Text>());
        }

        _soundManager = _soundManagerGO.GetComponent<SoundManager>();

        updatePage(startingPage);
    }

    // Update is called once per frame
    void Update() {
        if (Input.anyKeyDown) {
        	if (_currentBeat < _finalBeat) {
        		nextBeat();
        	}
        }

        float lightningChance = Random.Range(0f,1f);
        if (lightningChance < 0.002f && _doubleStrikeBuffer <= 0f) {
            _isLightning = true;
            _doubleStrikeBuffer = 4f;
        } else if (_doubleStrikeBuffer > 0f) {
            _doubleStrikeBuffer -= Time.deltaTime;
        }

        handleLightning();
    }

    private void updatePage (Page page) {
    	_page = page.clone();

        // GET RID OF BEATS THAT WE DON'T MEET THE REQUIREMENT FOR
        List<Beat> beatsToRemove = new List<Beat>();
        foreach (Beat beat in _page.beats) {
            if (beat.getRequiredMood() != Mood.None) {
                if (_moodTracker.getMood(beat.getRequiredMood()) < beat.getRequiredMoodAmount()) {
                    beatsToRemove.Add(beat);
                }
            }
        }

        foreach (Beat beat in beatsToRemove) {
            _page.beats.Remove(beat);
        }

        // GET RID OF CHOICES THAT WE DON'T MEET THE REQUIREMENT FOR
        List<Choice> choicesToRemove = new List<Choice>();
        foreach (Choice choice in _page.choices) {
            if (choice.getRequiredMood() != Mood.None) {
                if (_moodTracker.getMood(choice.getRequiredMood()) < choice.getRequiredMoodAmount()) {
                    choicesToRemove.Add(choice);
                }
            }
        }

        foreach (Choice choice in choicesToRemove) {
            _page.choices.Remove(choice);
        }


        // UPDATE CURRENT DISPLAY
    	_words.text = _page.beats[0].getWords();

    	_currentBeat = 0;
    	_finalBeat = _page.beats.Count-1;

    	for (int i = 0; i < _page.choices.Count; i++) {
    		_optionList[i].text = _page.choices[i].getWords();
    	}

    	checkDisplayButtons();
    }


    private void nextBeat () {      
        _currentBeat++;

    	_words.text = _page.beats[_currentBeat].getWords();
        if (_newBeatSound != null) {
    	   _soundManager.playSoundCollection(_newBeatSound);
        }

    	checkDisplayButtons();
    }

    private void checkDisplayButtons() {
    	if (_currentBeat == _finalBeat) {
    		for (int i = 0; i < _page.choices.Count; i++) {
    			_optionGOList[i].SetActive(true);
    		}
    		for (int i = _page.choices.Count; i < 4; i++) {
    			_optionGOList[i].SetActive(false);
    		}
    	} else {
    		foreach (GameObject button in _optionGOList) {
    			button.SetActive(false);
    		}
    	}
    }

    public void selectOption(int selection) {
        Choice choice = _page.choices[selection];
    	
        Page nextPage;
        if (choice.getAltMood() != Mood.None && _moodTracker.getMood(choice.getAltMood()) >= choice.getAltMoodReq()) {
            nextPage = choice.getAltLink();
        } else {
            nextPage = choice.getLink();
        }

    	if (nextPage != null) {
            if (choice.getMood() != Mood.None) {
                _moodTracker.addToMood(choice.getMood(),choice.getMoodMod());
            }

            if (_selectionSound != null) {
    		  _soundManager.playSoundCollection(_selectionSound);
            }
    		updatePage(nextPage);
    	}
    }
    //lightningStrike causes the background chosen (variable lightningBackground) to flash bright and then dim
    public void handleLightning() {
        
    	if (_isLightning)
        {
            
            _curTimeForFlash -= Time.deltaTime;
        

	        if (_thunderSounds != null) {
	        	_soundManager.playSoundCollection(_thunderSounds);
	        }
    
            float sameColorChance = Random.Range(0f,1f);

            if (sameColorChance < 0.3 || _lastColor == null) {
                float timeFraction = _curTimeForFlash / _totTimeForFlash;
                float lightFraction = Random.Range(timeFraction/2,timeFraction);

                _lastColor = new Color(lightFraction, lightFraction, lightFraction, 1);

                _lightningBackground.color = _lastColor;
                _rainBackground.color = _lastColor;
            } else {
                _lightningBackground.color = _lastColor;
                _rainBackground.color = _lastColor;
            }

            if (_curTimeForFlash <= 0) {
                _lightningBackground.color = new Color(0.1f, 0.1f, 0.1f, 1);
                _rainBackground.color = new Color(0.05f, 0.05f, 0.05f, 1);
                _isLightning = false;
                _totTimeForFlash = Random.Range(0.5f, 0.8f);
                _curTimeForFlash = _totTimeForFlash;
            }
    	}
    }

    // FOR TESTING
    public void returnToStart() {
    	updatePage(startingPage);
    }

    public Page getPage() {return _page;}
    public void setPage(Page page) {_page = page;}
}

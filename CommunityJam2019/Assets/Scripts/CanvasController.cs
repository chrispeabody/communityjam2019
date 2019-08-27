using System.Collections;
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
    
    //Things for the lightning flash
    public RawImage _lightningBackground;
    public bool _isLightning = false;
    private float _timeForFlash = 0.1f;
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

        if (_isLightning)
        {
            lightningStrike();
            _timeForFlash -= Time.deltaTime;
        }
    }

    private void updatePage (Page page) {
    	_page = page;
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
    	_soundManager.playSoundCollection(_newBeatSound);

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
    	Page nextPage = _page.choices[selection].getLink();
    	if (nextPage != null) {
    		_soundManager.playSoundCollection(_selectionSound);
    		updatePage(nextPage);
    	}
    }
    //lightningStrike causes the background chosen (variable lightningBackground) to flash bright and then dim
    public void lightningStrike() {
        Color _brightFlash = new Color(1, 1, 1, 1);
        Color _halfFlash = new Color(0.5f, 0.5f, 0.5f, 1);
        Color _noFlash = new Color(0, 0, 0, 1);
        _soundManager.playSoundCollection(_thunderSounds);
        _lightningBackground.color = _brightFlash;
        if(_timeForFlash <= 0)
        {
            _lightningBackground.color = _noFlash;
        }
        if(_timeForFlash <= -0.05f)
        {
            _lightningBackground.color = _halfFlash;
        }
        if (_timeForFlash <= -0.2f)
        {
            _lightningBackground.color = _noFlash;
            _isLightning = false;
            _timeForFlash = 0.1f;
        }
    }

    // FOR TESTING
    public void returnToStart() {
    	updatePage(startingPage);
    }

    public Page getPage() {return _page;}
    public void setPage(Page page) {_page = page;}
}

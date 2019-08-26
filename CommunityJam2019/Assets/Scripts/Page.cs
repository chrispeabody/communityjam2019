using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Page")]
public class Page : ScriptableObject {
	[SerializeField] public List<Beat> beats;
	[SerializeField] public List<Choice> choices;
}

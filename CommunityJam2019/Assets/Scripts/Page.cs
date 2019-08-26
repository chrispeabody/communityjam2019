using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Page")]
public class Page : ScriptableObject {
	[SerializeField] public List<Beat> beats = new List<Beat>();
	[SerializeField] public List<Choice> choices = new List<Choice>();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FourxUI : MonoBehaviour {

	/*
	public Text 
	private int population;
	private int jobs;
	private int materials;
	private int products;
	private int merchandise;
	private int culture;

	private float happiness;
	private float wealth;
	*/

	public Text tooltipText;
	public Text happinessText;
	public Text wealthText;

	private string happinessPrefix = "happiness: ";
	private string wealthPrefix = "wealth: ";

	private static FourxUI monolith;

	public static FourxUI Instance(){
		if (monolith == null) {
			monolith = new FourxUI ();
		}

		return monolith;
	}

	void Awake(){
		if (monolith == null) {
			monolith = this;
		} else {
			Destroy (this);
		}
	}

	public void UpdateUI(float happiness, float wealth){
		wealthText.text = wealthPrefix + wealth.ToString ();
		happinessText.text = happinessPrefix + happiness.ToString ();
	}

	public void UpdateUI(string tooltip){
		tooltipText.text = tooltip;
	}

	public void UpdateUI(float happiness, float wealth, string tooltip){
		UpdateUI (tooltip);
		UpdateUI (happiness, wealth);
	}

	public void ClearTooltip(){
		tooltipText.text = "";
	}
}

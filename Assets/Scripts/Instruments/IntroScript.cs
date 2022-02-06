using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

public class IntroScript : MonoBehaviour
{
	[SerializeField] private Text IntroTextComponent;

	[SerializeField] private string[] TextForIntro;

	[SerializeField] private GameObject EndingObject;

	[SerializeField] private GameObject IntroTextObject;

	private int ShowingText;

	[SerializeField] private float introSpeed, endLogoTime;

	private bool _isAFirstText = true;

	private void Start() => StartCoroutine(Intro());

	IEnumerator Intro() 
	{
		if (_isAFirstText == false) yield return new WaitForSeconds(introSpeed);

		else _isAFirstText = false;

		if (ShowingText < TextForIntro.Length) 
		{
			IntroTextObject.SetActive(false);
			yield return new WaitForSeconds(1.2f);
			IntroTextObject.SetActive(true);

			IntroTextComponent.text = TextForIntro[ShowingText];

			ShowingText++;
			StartCoroutine(Intro());
		}
		else 
		{
			StartCoroutine(Ending());
		}
	}

	IEnumerator Ending() 
	{
		EndingObject.SetActive(true);
		IntroTextObject.SetActive(false);
		yield return new WaitForSeconds(endLogoTime);
		SceneManager.LoadScene("Menu");
	}
}
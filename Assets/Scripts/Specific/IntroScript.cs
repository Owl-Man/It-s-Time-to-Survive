using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

public class IntroScript : MonoBehaviour
{
	public Text IntroTextComponent;

	public string[] TextForIntro;

	public GameObject EndingObject;

	public GameObject IntroTextObject;

	private int ShowingText = 0;

	public float introSpeed;

	public float endLogoTime;

	private bool isAFirstText = true;

	private void Start() => StartCoroutine(Intro());

	IEnumerator Intro() 
	{
		if (isAFirstText == false) yield return new WaitForSeconds(introSpeed);

		else isAFirstText = false;

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
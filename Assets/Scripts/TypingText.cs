using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class TypingText : MonoBehaviour
{
	public float delay;
	public string fullText;
	private string currentText = "";
	public float decay;
	
	public void TypeText(string newText)
    {
		fullText = newText;
		StartCoroutine(ShowText());
    }

	IEnumerator ShowText()
	{
		for (int i = 0; i < fullText.Length; i++)
		{
			currentText = fullText.Substring(0, i);
			this.GetComponent<TextMeshProUGUI>().text = currentText;
			yield return new WaitForSeconds(delay);
		}
		yield return new WaitForSeconds(decay);

		this.GetComponent<TextMeshProUGUI>().text = "";
	}
}
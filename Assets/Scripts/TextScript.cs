using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScript : MonoBehaviour
{

	private Text _textComponent;
	private bool _atEnd = false;

	public void PlayText(string text)
	{
        GameControllerScript.GetActiveGameController().DemandFocus();
		StartCoroutine(RollingText(text));
	}

	public IEnumerator RollingText(string text)
	{

		_textComponent = GetComponent<Text>();
		var length = text.Length;
		string currentText = "";
        float speed = 0.1f;
		while (currentText.Length < length)
		{
			currentText += text[currentText.Length];
			_textComponent.text = currentText;
			yield return new WaitForSeconds(speed);
		}

		_atEnd = true;
	}

	public static TextScript Build()
	{
        var prefab = Resources.Load("Prefabs/PopUpText") as GameObject;
        var instance = Instantiate(prefab);
        return instance.transform.Find("Panel/Text").GetComponent<TextScript>();
	}
	
    void Update()
    {
        if (_atEnd && Input.GetButtonDown("A")) {
            GameControllerScript.GetActiveGameController().ReleaseFocus();
            Destroy(gameObject.transform.parent.parent.gameObject);
        }
    }
    
	 
}

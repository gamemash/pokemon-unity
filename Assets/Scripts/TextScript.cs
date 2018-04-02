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
		var currentCharacter = 0.0f;
		var velocity = 20.0f;
		while (currentCharacter < text.Length)
		{
			currentCharacter += velocity * Time.fixedDeltaTime;
			_textComponent.text = text.Substring(0, (int)currentCharacter);
			yield return new WaitForFixedUpdate();
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

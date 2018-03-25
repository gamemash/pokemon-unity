using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

	public bool ReceiveInput = true;
	public string[] menuItems =new string[]{ "BAG", "OLI", "SAVE", "OPTION", "EXIT" };
	private float menuPadding = 25f; // The size of the border of this menu
	private float textPaddingTop = 15f;
	private float textPaddingLeft = 33f;
	private int fontSize = 14;
	private float menuWidth = 160f;
	private int highlightedItem = 0;
	private RectTransform selectorRect;

	public GameObject selector;

	public bool canMoveUp = true;
	public bool canMoveDown = true;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < menuItems.Length; i++)
		{
			GameObject text = CreateText (transform, 0f, i * -(textPaddingTop + fontSize) - menuPadding, menuItems[i], fontSize, Color.black);
		}

		RectTransform trans = gameObject.GetComponent<RectTransform> ();
		trans.sizeDelta = new Vector2 (menuWidth, (menuItems.Length * (textPaddingTop + fontSize)) + menuPadding * 1.2f);

		selector = GameObject.Find ("Selector");

		selectorRect = selector.GetComponent<RectTransform> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (ReceiveInput) {
			if (canMoveUp && Input.GetKeyDown (KeyCode.W)) {
				canMoveUp = false;
				highlightedItem -= 1;
				if (highlightedItem < 0) {
					highlightedItem = menuItems.Length - 1;
				}
			}

			if (Input.GetKeyUp (KeyCode.W)) {
				canMoveUp = true;
			}	

			if (canMoveDown && Input.GetKeyDown (KeyCode.S)) {
				canMoveDown = false;
				highlightedItem += 1;
				if (highlightedItem > menuItems.Length - 1) {
					highlightedItem = 0;
				}
			}

			if (Input.GetKeyUp (KeyCode.S)) {
				canMoveDown = true;
			}			
		}


		selectorRect.anchoredPosition = new Vector2 (selectorRect.anchoredPosition.x, -29f * (highlightedItem + 1));
	}

	GameObject CreateText(Transform canvas_transform, float x, float y, string text_to_print, int font_size, Color text_color)
	{
		GameObject UItextGO = new GameObject(text_to_print);
		UItextGO.transform.SetParent(canvas_transform);


		RectTransform trans = UItextGO.AddComponent<RectTransform>();
		trans.anchoredPosition = new Vector2(x + textPaddingLeft, y);
		trans.sizeDelta = new Vector2 (menuWidth, fontSize);

		Text text = UItextGO.AddComponent<Text>();
		text.rectTransform.localScale = new Vector3 (1, 1, 1);
		text.rectTransform.pivot = new Vector2 (0, 1);
		text.rectTransform.anchorMin = new Vector2 (0, 1);
		text.rectTransform.anchorMax = new Vector2 (0, 1);
		text.text = text_to_print;
		text.fontSize = font_size;
		text.font = (Font)Resources.Load("Fonts/Pokemon GB");
		text.color = text_color;

		return UItextGO;
	}
}

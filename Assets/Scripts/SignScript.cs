using lib;
using UnityEngine;

public class SignScript : Interactable
{
    public string Text;

    private bool _active;

    private void Start()
    {
    }

    public override void Interact()
    {
        _active = true;
        TextScript.Build().PlayText(Text);
    }


}

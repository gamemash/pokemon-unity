using System.Collections;
using lib;
using UnityEngine;

public class DoorScript : Entrance
{
    // Use this for initialization


    private float Open()
    {
        GetComponent<Animator>().SetBool("Open", true);
        return 0.5f;
    }
    
    private float Close()
    {
        GetComponent<Animator>().SetBool("Open", false);
        return 0.5f;
    }

    public override IEnumerator PlayArrivalAnimation(GameObject gameObject)
    {
        var player = gameObject.GetComponent<PlayerControllerScript>();
        player.SetPosition((Vector2)transform.position - new Vector2(0.5f, 0.5f) +  Vector2.up);
        player.SetDirection(PlayerControllerScript.Direction.Down);
        yield return new WaitForSeconds(Open());
        yield return new WaitForSeconds(player.MoveForward(2));
        yield return new WaitForSeconds(Close());
    }

    public override IEnumerator PlayEntranceAnimation(GameObject gameObject)
    {
        var player = gameObject.GetComponent<PlayerControllerScript>();
        
        yield return new WaitForSeconds(Open());
        yield return new WaitForSeconds(player.MoveForward(2));
        yield return new WaitForSeconds(Close());
        LoadScene();
    }
}
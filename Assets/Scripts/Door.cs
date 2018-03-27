using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator _animator;


    public void Start()
    {

        _animator = GetComponent<Animator>();

    }

    public void FixedUpdate()
    {
        
    }

    public void Open()
    {
        GetComponent<Animator>().SetBool("Open", true);
    }
    
    public void Close()
    {
        GetComponent<Animator>().SetBool("Open", false);
    }
}
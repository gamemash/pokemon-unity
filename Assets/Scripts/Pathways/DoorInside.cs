using System.Collections;
using UnityEngine;

namespace Pathways
{
    public class DoorInside : Entrance
    {
        // Use this for initialization


        public override IEnumerator PlayArrivalAnimation(GameObject gameObject)
        {
            var player = gameObject.GetComponent<PlayerControllerScript>();
            player.SetPosition((Vector2)transform.position - new Vector2(0.5f, 0.5f) + Vector2.up);
            yield return null;
        }

        public override IEnumerator PlayEntranceAnimation(GameObject gameObject)
        {
            var player = gameObject.GetComponent<PlayerControllerScript>();
            
            yield return new WaitForSeconds(player.MoveForward(1));
            LoadScene();
        }
    }
}
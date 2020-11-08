using Assets.MyGame.Scripts.Builder;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Characters characters;

    private void OnTriggerEnter(Collider other)
    {
        if (characters)
        {
            if (other.gameObject.GetComponent<Characters>())
            {
                if (other.gameObject == characters.gameObject || other.gameObject == characters.gameObject.GetComponentInChildren<Transform>().gameObject)
                {
                    return;
                }
                if (other.gameObject.GetComponent<Characters>() && characters.Name.name != other.gameObject.GetComponent<Characters>().Name.name)
                {
                    other.gameObject.SendMessage("ApplyDamage", characters.Demage.demage);
                }
            }
            else
            {
                characters.gameObject.SendMessage("Miss");
            }
        }
           
        if(!other.gameObject.GetComponent<Bullet>())
        Destroy(gameObject);
    }
}

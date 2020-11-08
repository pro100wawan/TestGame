using Assets.MyGame.Scripts.Builder;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnTriger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Characters>())
            if(other.gameObject.GetComponent<Characters>().Name.name == "Player")
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

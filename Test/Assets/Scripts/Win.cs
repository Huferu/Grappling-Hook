using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    public GameObject winScreen;
    public float delaybeforeShow;

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Player") {
         //   collision.gameObject.GetComponent<Player>().Win();
            Invoke("ShowWinScreen", delaybeforeShow);
        }
    }

    private void ShowWinScreen() {
        winScreen.SetActive(true);
    }
}

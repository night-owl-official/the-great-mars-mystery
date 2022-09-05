using UnityEngine;

public class EndCredits : MonoBehaviour {
    private void Start() {
        SoundManager sm = FindObjectOfType<SoundManager>();
        sm.StopPlayingSound("tune");
        sm.Play("Theme");
    }

    public void ReloadToMainMenu() {
        FindObjectOfType<GameManager>().GoBackToMainMenu();
    }
}

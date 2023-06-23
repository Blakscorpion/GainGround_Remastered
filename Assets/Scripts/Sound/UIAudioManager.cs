using UnityEngine;

public class InterfaceSoundManager : MonoBehaviour
{
    public AudioClip cursorSound;  // Son du curseur
    public AudioClip buttonSound;  // Son du bouton

    private AudioSource audioSource;

    private bool isCursorOverButton = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (isCursorOverButton && Input.GetMouseButtonDown(0))
        {
            PlaySound(buttonSound);
        }
    }

    private void OnMouseEnter()
    {
        isCursorOverButton = true;
        PlaySound(cursorSound);
    }

    private void OnMouseExit()
    {
        isCursorOverButton = false;
    }

    private void PlaySound(AudioClip soundClip)
    {
        if (soundClip != null)
        {
            audioSource.clip = soundClip;
            audioSource.Play();
        }
    }
}

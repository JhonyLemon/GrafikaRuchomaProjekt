using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicChanger : MonoBehaviour
{
    public AudioClip[] tracks; // tablica z utworami do odtworzenia
    public KeyCode switchTrackKey; // klawisz do prze��czania utwor�w
    public bool playOnStart; // czy odtwarza� utw�r po uruchomieniu skryptu
    public bool loopTracks; // czy zap�tli� odtwarzanie utwor�w
    public bool shuffleTracks; // czy losowa� utwory z listy


    private AudioSource audioSource; // komponent AudioSource do odtwarzania muzyki
    private int currentTrackIndex; // indeks bie��cego utworu

    // Start is called before the first frame update
    void Start()
    {
        // Pobierz komponent AudioSource
        audioSource = GetComponent<AudioSource>();

        // Je�li ustawiono flag� shuffleTracks na true, losuj indeks bie��cego utworu z listy
        if (shuffleTracks)
        {
            currentTrackIndex = Random.Range(0, tracks.Length);
        }
        else
        {
            // W przeciwnym razie ustaw indeks bie��cego utworu na pierwszy element tablicy
            currentTrackIndex = 0;
        }

        // Je�li ustawiono flag� playOnStart na true, odtw�rz utw�r
        if (playOnStart)
        {
            PlayCurrentTrack();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Je�li naci�ni�to klawisz switchTrackKey, prze��cz na nast�pny utw�r
        if (Input.GetKeyDown(switchTrackKey))
        {
            SwitchTrack();
        }

        // Je�li bie��cy utw�r si� sko�czy�, prze��cz na nast�pny
        if (!audioSource.isPlaying && loopTracks)
        {
            SwitchTrack();
        }
    }

    // Metoda do prze��czania na nast�pny utw�r
    void SwitchTrack()
    {
        if (shuffleTracks)
        {
            currentTrackIndex = Random.Range(0, tracks.Length);
        }
        else
        {
            // W przeciwnym razie zwi�ksz indeks bie��cego utworu
            currentTrackIndex++;
        }

        // Je�li indeks wychodzi poza granice tablicy, przewi� go na pocz�tek
        if (currentTrackIndex >= tracks.Length)
        {
            currentTrackIndex = 0;
        }

        // Odtw�rz bie��cy utw�r
        PlayCurrentTrack();
    }

    // Metoda do odtwarzania bie��cego utworu
    void PlayCurrentTrack()
    {
        // Ustaw bie��cy utw�r jako �r�d�o d�wi�ku
        audioSource.clip = tracks[currentTrackIndex];
        // Zresetuj pozycj� odtwarzania do pocz�tku utworu
        audioSource.time = 0;
        // Odtw�rz utw�r
        audioSource.Play();
    }
}


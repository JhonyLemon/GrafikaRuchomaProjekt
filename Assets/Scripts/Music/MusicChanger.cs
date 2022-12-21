using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicChanger : MonoBehaviour
{
    public AudioClip[] tracks; // tablica z utworami do odtworzenia
    public KeyCode switchTrackKey; // klawisz do prze³¹czania utworów
    public bool playOnStart; // czy odtwarzaæ utwór po uruchomieniu skryptu
    public bool loopTracks; // czy zapêtliæ odtwarzanie utworów
    public bool shuffleTracks; // czy losowaæ utwory z listy


    private AudioSource audioSource; // komponent AudioSource do odtwarzania muzyki
    private int currentTrackIndex; // indeks bie¿¹cego utworu

    // Start is called before the first frame update
    void Start()
    {
        // Pobierz komponent AudioSource
        audioSource = GetComponent<AudioSource>();

        // Jeœli ustawiono flagê shuffleTracks na true, losuj indeks bie¿¹cego utworu z listy
        if (shuffleTracks)
        {
            currentTrackIndex = Random.Range(0, tracks.Length);
        }
        else
        {
            // W przeciwnym razie ustaw indeks bie¿¹cego utworu na pierwszy element tablicy
            currentTrackIndex = 0;
        }

        // Jeœli ustawiono flagê playOnStart na true, odtwórz utwór
        if (playOnStart)
        {
            PlayCurrentTrack();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Jeœli naciœniêto klawisz switchTrackKey, prze³¹cz na nastêpny utwór
        if (Input.GetKeyDown(switchTrackKey))
        {
            SwitchTrack();
        }

        // Jeœli bie¿¹cy utwór siê skoñczy³, prze³¹cz na nastêpny
        if (!audioSource.isPlaying && loopTracks)
        {
            SwitchTrack();
        }
    }

    // Metoda do prze³¹czania na nastêpny utwór
    void SwitchTrack()
    {
        if (shuffleTracks)
        {
            currentTrackIndex = Random.Range(0, tracks.Length);
        }
        else
        {
            // W przeciwnym razie zwiêksz indeks bie¿¹cego utworu
            currentTrackIndex++;
        }

        // Jeœli indeks wychodzi poza granice tablicy, przewiñ go na pocz¹tek
        if (currentTrackIndex >= tracks.Length)
        {
            currentTrackIndex = 0;
        }

        // Odtwórz bie¿¹cy utwór
        PlayCurrentTrack();
    }

    // Metoda do odtwarzania bie¿¹cego utworu
    void PlayCurrentTrack()
    {
        // Ustaw bie¿¹cy utwór jako Ÿród³o dŸwiêku
        audioSource.clip = tracks[currentTrackIndex];
        // Zresetuj pozycjê odtwarzania do pocz¹tku utworu
        audioSource.time = 0;
        // Odtwórz utwór
        audioSource.Play();
    }
}


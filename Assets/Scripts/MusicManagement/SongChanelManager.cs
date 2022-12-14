using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SongChanelManager : MonoBehaviour
{
    public string inputName;
    public double beatInAdvance = 2.0d;
    public NoteDisplayer displayer;
    public int defaultPoolId = 0;
    public AudioSource audioSource;
    public AudioClip hitSound;

    public UnityEvent onWarnHideInstrument;
    public UnityEvent onWarnShowInstrument;
    public UnityEvent onHideInstrument;
    public UnityEvent onShowInstrument;
    public UnityEvent onEndSong;

    [SerializeField]
    public TimingEvaluator timingEvaluator;


    private Note[] notes;
    private int lastNoteIndex;

    private List<Note> instancedNotes;


    void Start()
    {
        instancedNotes = new List<Note>();
        lastNoteIndex = 0;
        if (notes == null)
        {
            notes = new Note[0];
        }
    }

    void Update()
    {
        if (Conductor.Instance.musicPlaying)
        {
            UpdateInstanciedNotes();
            SpawnNewNotes();
        }
    }

    public void SetNotes(Note[] notes)
    {
        this.notes = notes;
    }

    private void UpdateInstanciedNotes()
    {
        InputSystem.Inputs input = InputSystem.GetInput(inputName);
        bool press = input.Pressed;
        bool release = input.Released;
        for (int n = 0; n < instancedNotes.Count; n++)
        {
            Note note = instancedNotes[n];
            if (press)
            {
                if (note.OnPlayPress(input))
                {
                    press = false;
                }
            }
            if (release)
            {
                if (note.OnPlayRelease(input))
                {
                    release = false;
                }
            }
            note.Update();
            if (!note.IsPlaying)
            {
                instancedNotes.RemoveAt(n);
                n--;
            }
        }
    }

    private void SpawnNewNotes()
    {
        double songPos = Conductor.Instance.songPositionInBeats;
        bool spawnNewNote = true;

        while (lastNoteIndex < notes.Length && spawnNewNote)
        {
            Note note = notes[lastNoteIndex];
            spawnNewNote = songPos >= note.Beat - beatInAdvance;
            if (spawnNewNote)
            {
                note.Start();
                instancedNotes.Add(note);
                lastNoteIndex++;
            }
        }
    }
}

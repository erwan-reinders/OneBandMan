using System.Collections.Generic;
using UnityEngine;

public class SongChanelManager : MonoBehaviour
{
    public string inputName;
    public double beatInAdvance = 2.0d;
    public NotePool pool;
    public AudioSource audioSource;
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
        UpdateInstanciedNotes();
        SpawnNewNotes();
    }

    public void SetNotes(Note[] notes)
    {
        this.notes = notes;
    }

    private void UpdateInstanciedNotes()
    {
        InputSystem.Inputs input = InputSystem.GetInput(inputName);
        pool.DisplayInput(input);
        bool press = input.Pressed;
        bool release = input.Released;
        for (int n = 0; n < instancedNotes.Count; n++)
        {
            Note note = instancedNotes[n];
            if (press)
            {
                if (note.OnPlayPress())
                {
                    press = false;
                }
            }
            if (release)
            {
                if (note.OnPlayRelease())
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

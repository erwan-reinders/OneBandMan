using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongManager : MonoBehaviour
{
    public static SongManager instance;

    public NotePool pool;
    [SerializeField]
    public TimingEvaluator timingEvaluator;

    private Note[] notes;
    private int lastNoteIndex;

    private List<Note> instancedNotes;


    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Error : Only one SongManager can exist");
        }

        notes = new Note[16];
        for (int i = 0; i < 16; i++)
        {
            notes[i] = new SimpleNote(i+5, pool);
        }

        instancedNotes = new List<Note>();
        lastNoteIndex = 0;
    }

    void Update()
    {
        UpdateInstanciedNotes();
        SpawnNewNotes();
    }

    private void UpdateInstanciedNotes()
    {
        //Todo remplacer Input par un InputManager plus complexe
        bool press = Input.GetKeyDown(KeyCode.Space);
        bool release = Input.GetKeyUp(KeyCode.Space);
        for (int n = 0; n < instancedNotes.Count; n++)
        {
            Note note = instancedNotes[n];
            if (press)
            {
                note.OnPlayPress();
                //press = false;
            }
            if (release)
            {
                note.OnPlayRelease();
                //release = false;
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
        //Todo get real value
        double beatInAdvance = 2.0d;
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

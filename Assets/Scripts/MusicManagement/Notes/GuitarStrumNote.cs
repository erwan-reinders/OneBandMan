using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuitarStrumNote : SimpleNote
{
    public GuitarStrumNote(double beat, SongChanelManager songChanelManager) : base(beat, songChanelManager, 1)
    {
    }

    public override bool OnPlayPress(InputSystem.Inputs input)
    {
        if (input.Type == GuitarInput.STRUMMING_TYPE)
        {
            return base.OnPlayPress(input);
        }
        return false;
    }
}

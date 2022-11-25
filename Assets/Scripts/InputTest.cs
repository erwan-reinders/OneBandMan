using UnityEngine;

public class InputTest : MonoBehaviour
{
    public float angleIncrement = 90f;
    public int rotation = 0;

    public float inputLatency;
    public float inputMoyLatency;

    public int nbInput = 10;
    public float[] inputsLatency;

    private void Start()
    {
        inputsLatency = new float[nbInput];
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rotation++;

            float inputTime = Conductor.Instance.songPosition;
            float inputBeat = Conductor.Instance.songPositionInBeats;

            float floorBeat, fracBeat;
            Util.GetFloorFrac(inputBeat, out floorBeat, out fracBeat);

            if (fracBeat > 0.5f)
            {
                // Too soon
                inputLatency = inputTime - (float)Conductor.Instance.BeatToTime(floorBeat + 1.0f);
            }
            else
            {
                //Too late
                inputLatency = inputTime - (float)Conductor.Instance.BeatToTime(floorBeat);
            }

            inputsLatency[rotation % nbInput] = inputLatency;

            if (rotation >= nbInput)
            {
                inputMoyLatency = 0f;
                foreach (float input in inputsLatency)
                {
                    inputMoyLatency += input;
                }
                inputMoyLatency /= nbInput;
            }
        }

        transform.rotation = Quaternion.Euler(0, 0, angleIncrement * rotation);
    }
}

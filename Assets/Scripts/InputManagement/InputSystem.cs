using System.Collections.Generic;

public static class InputSystem
{
    public static Dictionary<string, Inputs> inputs = new Dictionary<string, Inputs>();

    /*    public static void Reset()
        {
            inputs = new Dictionary<string, Inputs>();
        }*/

    /*    public static List<Inputs> GetNewChannels(int numberOfChannels)
        {
            List<Inputs> newChannels = new List<Inputs>();

            for (int i = 0; i < numberOfChannels; i++)
            {
                Inputs input = new Inputs();
                inputs.Add(input);
                newChannels.Add(input);
            }

            return newChannels;
        }*/

    public static Inputs GetInput(string inputName)
    {
        return inputs[inputName];
    }

    public class Inputs
    {
        public bool Active;
        public bool Pressed;
        public bool Released;
    }
}

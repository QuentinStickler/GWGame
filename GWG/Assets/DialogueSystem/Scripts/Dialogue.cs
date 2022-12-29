using UnityEngine;

namespace DefaultNamespace
{
    [System.Serializable]
    public class Dialogue
    {
        public SpokenWord[] sentences;
    }

    [System.Serializable]
    public class SpokenWord
    {
        public string name;

        [TextArea(3, 10)]
        public string sentence;
    }
}
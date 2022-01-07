using System.Collections.Generic;

namespace WordleBot.App
{
    public class GuessResult
    {
        public GuessResult()
        {
            CorrectCharsInPosition = new Dictionary<int, char>();
            CorrectCharsOutOfPosition = new Dictionary<int, char>();
            IncorrectChars = new List<char>();
        }

        public void Add(char curChar, int pos, ResultValue curCharRes)
        {
            switch (curCharRes) {
                case ResultValue.Incorrect:
                    IncorrectChars.Add(curChar);
                    break;
                case ResultValue.Correct:
                    CorrectCharsInPosition.Add(pos, curChar);
                    break;
                case ResultValue.OutOfPosition:
                    CorrectCharsOutOfPosition.Add(pos, curChar);
                    break;
            }
        }

        public bool IsCorrect()
        {
            return CorrectCharsInPosition.Keys.Count == 5;
        }

        public List<char> IncorrectChars { get; set; }
        public Dictionary<int, char> CorrectCharsInPosition { get; set; }
        public Dictionary<int, char> CorrectCharsOutOfPosition { get; set; }
    }

    public enum ResultValue
    {
        Incorrect = 0,
        Correct = 1,
        OutOfPosition = 2
    }
}

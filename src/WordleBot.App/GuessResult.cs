using System.Collections.Generic;

namespace WordleBot.App
{
    public class GuessResult
    {
        public List<CharacterGuess> CharacterGuesses { get; set; }
    }

    public class CharacterGuess
    {
        public char Character { get; set; }
        public int Position { get; set; }
        public ResultValue Result { get; set; }
    }

    public enum ResultValue
    {
        Correct,
        Incorrect,
        OutOfPosition
    }
}

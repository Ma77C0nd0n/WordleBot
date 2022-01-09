using System.Collections.Generic;
using System.Linq;

namespace WordleBot.App
{
    public class WordFilterService
    {
        public List<string> FilterWordsBasedOnResult(
            List<string> words,
            GuessResult result)
        {
            words.RemoveAt(0);
            var includedList = new List<string>();
            foreach(string word in words)
            {
                if (!WordHasUnusedCharacter(result, word))
                {
                    for (int index = 0; index < 5; index++)
                    {
                        var curChar = word[index];
                        if (result.CorrectCharsInPosition.TryGetValue(index, out char resChar) && (resChar != curChar))
                            break;
                        if (result.CorrectCharsOutOfPosition.TryGetValue(index, out resChar) && resChar == curChar)
                            break;
                        if (index == 4 && NumberOfOutOfPositionIsCorrect(
                            result.CorrectCharsOutOfPosition.Values.ToList(), 
                            result.CorrectCharsInPosition.Keys, 
                            word
                        ))
                        {
                            includedList.Add(word);
                        }
                    }
                }
            }
            return includedList;
        }

        private bool WordHasUnusedCharacter(GuessResult result, string word)
        {
            var intersectingChars = word.Intersect(result.IncorrectChars);
            return intersectingChars.Any(c 
                => !CharacterExistsInOtherPosition(c, result.CorrectCharsInPosition.Values) && 
                !CharacterExistsInOtherPosition(c, result.CorrectCharsOutOfPosition.Values));


            static bool CharacterExistsInOtherPosition(char curChar, IEnumerable<char> correctPositionChars) => correctPositionChars.Contains(curChar);
        }

        private bool NumberOfOutOfPositionIsCorrect(List<char> charactersOutOfPos, IEnumerable<int> positionsOfCorrectChars, string word)
        {
            var wordWithoutCorrectPos = word;
            positionsOfCorrectChars.OrderByDescending(i => i).ToList().ForEach(p => wordWithoutCorrectPos = wordWithoutCorrectPos.Remove(p));
            return word.Intersect(charactersOutOfPos).Count() == charactersOutOfPos.Count;
        }
    }
}

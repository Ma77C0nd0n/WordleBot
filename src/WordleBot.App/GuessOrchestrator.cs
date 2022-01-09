using System;
using System.Collections.Generic;
using System.Linq;

namespace WordleBot.App
{
    public class GuessOrchestrator
    {
        public void MakeGuess(List<string> possibleWords, string correctWord, out int numberOfGuesses)
        {
            var WordSortingSvc = new WordSortingService();
            var WordFilterSvc = new WordFilterService();

            var sortedWords = WordSortingSvc.GetSortedWords(possibleWords, applyDuplicateMultiplier: true);

            var guessIsCorrect = false;
            numberOfGuesses = 0;
            while (!guessIsCorrect)
            {
                if (!sortedWords.Any())
                {
                    Console.WriteLine($"No words left to guess. Exiting.");
                    numberOfGuesses = -1;
                    return;
                }

                string guessedWord = sortedWords[0];

                //Console.WriteLine(guessedWord);

                var result = GetResultStringFromCorrectWordOrInput(guessedWord, correctWord);

                //Console.WriteLine(result);

                var guessResult = ConvertToGuessResult(guessedWord, result);

                guessIsCorrect = guessResult.IsCorrect();
                numberOfGuesses++;

                if (guessIsCorrect)
                    break;
                else
                    sortedWords = WordFilterSvc.FilterWordsBasedOnResult(sortedWords, guessResult);
            }
            Console.WriteLine($"Guess \"{sortedWords[0]}\" was correct after {numberOfGuesses} attempts");
        }

        private string GetResultStringFromCorrectWordOrInput(string guessedWord, string correctWord)
        {
            if (string.IsNullOrWhiteSpace(correctWord))
            {
                return Console.ReadLine();
            }
            return GetResultStringFromCorrectWord(guessedWord, correctWord);
        }

        private string GetResultStringFromCorrectWord(string guessedWord, string correctWord)
        {
            var resultStr = new char[5];
            var possibleOutOfPos = new Dictionary<int, char>();
            var correctPosChars = new List<char>();

            for (int i = 0; i < 5; i++)
            {
                if (guessedWord[i] == correctWord[i])
                {
                    resultStr[i] = '1';
                    correctPosChars.Add(guessedWord[i]);
                }

                else if (correctWord.Contains(guessedWord[i]))
                {
                    possibleOutOfPos.Add(i, guessedWord[i]);
                }
                else
                    resultStr[i] = '0';
            }

            EvaluateForOutOfPos(correctWord, possibleOutOfPos, correctPosChars, resultStr);

            return new string(resultStr);
        }

        private void EvaluateForOutOfPos(string correctWord, Dictionary<int, char> possibleOutOfPos, List<char> correctPosChars, char[] resultStr)
        {
            var charsSet = new List<char>();
            foreach(var outOfPosKeyValuePair in possibleOutOfPos)
            {
                if (correctPosChars.Contains(outOfPosKeyValuePair.Value))
                {
                    if (HasSufficientMatches(outOfPosKeyValuePair.Value))
                        SetOutOfPosResultStrValue(charsSet, outOfPosKeyValuePair, resultStr, correctWord, correctPosChars);
                    else
                        resultStr[outOfPosKeyValuePair.Key] = '0';
                }
                else
                    SetOutOfPosResultStrValue(charsSet, outOfPosKeyValuePair, resultStr, correctWord, correctPosChars);
            }

            bool HasSufficientMatches(char outOfPosChar) => correctWord.Count(x => x == outOfPosChar) > correctPosChars.Count(x => x == outOfPosChar);

        }

        private void SetOutOfPosResultStrValue(List<char> charsSet, KeyValuePair<int, char> outOfPosKeyValuePair, char[] resultStr, string correctWord, List<char> correctPosChars)
        {
            if (!charsSet.Any())
            {
                resultStr[outOfPosKeyValuePair.Key] = '2';
                charsSet.Add(outOfPosKeyValuePair.Value);
            }
            else if (IsExcessDuplicate(outOfPosKeyValuePair.Value))
                resultStr[outOfPosKeyValuePair.Key] = '0';
            else
                resultStr[outOfPosKeyValuePair.Key] = '2';

            bool IsExcessDuplicate(char outOfPosChar) => correctWord.Count(x => x == outOfPosChar) == charsSet.Count(x => x == outOfPosChar) + correctPosChars.Count(x => x == outOfPosChar);
        }

        private GuessResult ConvertToGuessResult(string input, string result)
        {
            var guessRes = new GuessResult();
            for (int i = 0; i < 5; i++)
            {
                var curChar = input[i];
                var curCharRes = (ResultValue) char.GetNumericValue(result[i]);
                    guessRes.Add(curChar, i, curCharRes);
            }

            return guessRes;
        }
    }
}

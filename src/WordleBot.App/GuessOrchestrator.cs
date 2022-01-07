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

                //Console.WriteLine($"Guessed word: {guessedWord}");

                var result = GetResultStringFromCorrectWordOrInput(guessedWord, correctWord);

                var guessResult = ConvertToGuessResult(guessedWord, result);

                guessIsCorrect = guessResult.IsCorrect();
                numberOfGuesses++;

                sortedWords = WordFilterSvc.FilterWordsBasedOnResult(
                    sortedWords,
                    guessResult
                );
            }
            //Console.WriteLine("Guess was correct");
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
            for (int i = 0; i < 5; i++)
            {
                if(guessedWord[i] == correctWord[i])
                    resultStr[i] = '1';
                
                else if (correctWord.Contains(guessedWord[i]))
                    resultStr[i] = '2';
                
                else
                    resultStr[i] = '0';
            }
            return new string(resultStr);
        }

        private static GuessResult ConvertToGuessResult(string input, string result)
        {
            var guessRes = new GuessResult();
            for (int i = 0; i < 5; i++)
            {
                var curChar = input[i];
                var curCharRes = (ResultValue)char.GetNumericValue(result[i]);
                guessRes.Add(curChar, i, curCharRes);
            }
            return guessRes;
        }
    }
}

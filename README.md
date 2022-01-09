# WordleBot
Bot for solving [wordle game](https://www.powerlanguage.co.uk/wordle/)

## Proposed Solution

1. Get all 5 letter word list
2. Sort by letter frequency
3. For each word in sorted list
   1. Send first word
      1. Filter word list based on result where:
      2. For each letter
         1. If correct in correct position, filter out all without correct letter in correct position
         2. If incorrect - filter out all words with that letter
         3. If correct but position incorrect
            1. filter out all words with that letter in that position
            2. filter out all words not containing that letter

Sample of working solution (side-by-side with wordle game)
![wordle_solved](https://user-images.githubusercontent.com/10655290/148473027-0b95a751-885e-4d5c-9772-07f47627db1c.gif)

Success rate 90.9% (in autoplay, using all 5 letter words :: max guesses is 6):
![image](https://user-images.githubusercontent.com/10655290/148687896-e9f73446-f975-48bf-8b05-1e3889821a72.png)


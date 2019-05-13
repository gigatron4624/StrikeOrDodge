using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Judge
{
    /* Program: Judgment.cs
     * This console application is a modified simulation of Judge, a Nintendo game that Gunpei Yokoi created in 1980. 
     * The original Judge game involves two players drawing cards with random numbers from 1 through 9. Each player can choose to strike
     * his or her opponent with a hammer, or dodge the opponent's hammer strike. Striking an opponent after drawing the higher number
     * or dodging the opponent's strike after drawing a lower number results in a win. Striking an opponent after drawing the lower number or dodging
     * after drawing a higher number results in a loss. This simulation includes a "hard mode" where the random number pool increases from 9 to 18.
     * The simulation is similar, but not fully identical to the original game. Please see the Functionality description for more details. */

    class Judgment
    {
        /* Fields */
        private char winGame = 'w'; // winGame = win
        private char loseGame = 'l'; // loseGame = loss
        private char tieGame = 't'; // tieGame = tie, stalemate, or draw
        private int gameCount = 3; // best of 3 rounds

        /* TwoPlayer decides whether the opponent is CPU or human. 
         * opponentOption gives you a choice: A = 1 player vs CPU, while B = 2 players vs each other
         * returns true if playing against other users and false if playing against the CPU */
        bool TwoPlayer(char opponentOption)
        {
            int playerCount; // How many human players?
            if (opponentOption == 'A')
                playerCount = 1; // solo gameplay
            else if (opponentOption == 'B')
                playerCount = 2; // dual gameplay (default option)
            else
                playerCount = 2; // 2 player mode is the default option.
            return playerCount == 2; // returns true for 2 player mode and false for 1 player mode
        }

        /* DifficultyMode decides what difficulty level the user wants to play. The user has two choices: easy or hard.
         * easyOrHard represents the difficulty level chosen.
         * return 1 for easy mode, 2 for hard mode */
        int DifficultyMode(char level)
        {
            int easyOrHard; // This variable is self-explanatory. 
            switch (level)
            {
                case 'H':
                    easyOrHard = 2; // set difficulty level to hard
                    break;
                default:
                    easyOrHard = 1; // set difficulty level to easy
                    break;
            }
            return easyOrHard; // return the setting of the difficulty level
        }

        /* StrikeOrDodge determines whether the user wants to strike the opponet or dodge.
         * userChoice is a character representing each player's decision: strike (s) or dodge (any other char) 
         * return a 1 for a strike and a 2 for a dodge */
        int StrikeOrDodge(char userChoice)
        {
            int userMove; // an integer indicating a strike (1) or dodge (2)
            switch (userChoice)
            {
                case 's':
                    userMove = 1; // strike opponent
                    break;
                default:
                    userMove = 2; // dodge opponent
                    break;
            }
            return userMove; // return the user's decision
        }

        /* Magnitude compares the numbers on the cards drawn by each player. 
         * n1 is the number drawn by Player 1.
         * n2 is the number drawn by Player 2.
         * returns an integer depending on who drew the larger number */
        int Magnitude(int n1, int n2)
        {
            int cardNumber; // three outcomes
            if (n1 == n2)
                cardNumber = 0; // Each player drew the same number.
            else if (n1 > n2)
                cardNumber = 1; // Player 1 drew a larger number.
            else
                cardNumber = -1; // Player 2 drew a larger number.
            return cardNumber; // return the number on the card
        }

        /* Duel generates the outcome of each round as a string. 
         * n1 is the number that Player 1 drew.
         * n2 is the number that Player 2 drew.
         * c1 is the choice that Player 1 made (strike or dodge).
         * c2 is the choice that Player 2 made (strike or dodge).
         * returns a string indicating who won the round. */
        string Duel(int n1, int n2, char c1, char c2)
        {
            String winnerOfRound = ""; // This is a string that indicates who won each round. 
            int playerOneMove = StrikeOrDodge(c1); // Player 1 either struck or dodged.
            int playerTwoMove = StrikeOrDodge(c2); // Player 2 either struck or dodged.
            int biggerNumber = Magnitude(n1, n2); // Whose number is bigger?
            if (biggerNumber == 1) // Player 1 drew a higher number.
            {
                if (playerOneMove == 1 || playerTwoMove == 1) // Player 1 struck Player 2 or vice-versa.
                    winnerOfRound = "Player 1"; // The round goes to Player 1.
                else
                    winnerOfRound = "Player 2"; // The round goes to Player 2.
            }
            else if (biggerNumber == -1) // Player 2 drew a higher number. 
            {
                if (playerOneMove == 1 || playerTwoMove == 1) // Player 2 struck Player 1 or vice-versa.
                    winnerOfRound = "Player 2"; // The round goes to Player 2.
                else
                    winnerOfRound = "Player 1"; // The round goes to Player 1.
            }
            else
                winnerOfRound = "nobody"; // The round is a draw/tie.
            return winnerOfRound; // the outcome of the round
        }

        /* WinLossOrTie generates characters indicating wins, losses, and ties by player 1.  
         * n1 is the number that Player 1 drew.
         * n2 is the number that Player 2 drew.
         * c1 is the choice that Player 1 made (strike or dodge).
         * c2 is the choice that Player 2 made (strike or dodge).
         * returns a character indicating that Player 1 won, lost, or tied. */
        char WinLossOrTie(int n1, int n2, char c1, char c2)
        {
            char duelResult = ' '; // initialize outcome for Player 1
            int playerOneMove = StrikeOrDodge(c1); // Player 1 either struck or dodged.
            int playerTwoMove = StrikeOrDodge(c2); // Player 2 either struck or dodged.
            int higherNumber = Magnitude(n1, n2); // Whose number is bigger?
            if (higherNumber == 1) // Player 1 drew a higher number.
            {
                if (playerOneMove == 1 || playerTwoMove == 1) // Player 1 struck Player 2 or vice-versa.
                    duelResult = winGame; // Player 1 gets a 'w'.
                else
                    duelResult = loseGame; // Either Player 1 or Player 2 dodged. Player 1 gets a 'l'. 
            }
            else if (higherNumber == -1) // Player 2 drew a higher number. 
            {
                if (playerOneMove == 1 || playerTwoMove == 1) // Player 2 struck Player 1 or vice-versa.
                    duelResult = loseGame; // Player 1 gets a 'l'.
                else
                    duelResult = winGame; // Either Player 1 or Player 2 dodged. Player 1 gets a 'w'.
            }
            else
                duelResult = tieGame; // Both players drew the same number. Player 1 gets a 't'.
            return duelResult; // returns a 'w', 'l', or 't'
        }

        /* PlayerTwoRecord stores the wins, losses, and ties for Player 2 in an array. 
         * playerOneRecord is an array that stores the wins, losses, and ties for Player 1.
         * returns an array with the records for Player 2 */
        char[] PlayerTwoRecord(char[] playerOneRecord)
        {
            char[] recordForP2 = new char[gameCount]; // initialize win-loss-tie record for player 2
            for (int j = 0; j < playerOneRecord.Length; j++)
            {
                if (playerOneRecord[j] == winGame)
                    recordForP2[j] = loseGame; // Player 1 won, so Player 2 lost.
                else if (playerOneRecord[j] == loseGame)
                    recordForP2[j] = winGame; // Player 1 lost, so Player 2 won.
                else
                    recordForP2[j] = tieGame; // It's a tie. 
            }
            return recordForP2; // return the win-loss-tie array for Player 2
        }

        /* ScoreKeeper calculates the final score of the three rounds played. 
         * playerStandings represent the win-loss-tie record for each player. 
         * returns the player's total score */
        int ScoreKeeper(char[] playerStandings)
        {
            int score = 0; // initialize variable for player score
            for (int i = 0; i < playerStandings.Length; i++)
            {
                if (playerStandings[i] == winGame)
                    score = score + 2; // 2 points for a 'w'
                else if (playerStandings[i] == tieGame)
                    score++; // 1 point for a 't'
                else
                    score = score + 0; // no points for a 'l'
            }
            return score; // returns the cumulative score of all the rounds
        }

        /* FinalResults compares scores and declares the champion. 
         * score1 is the score for Player 1. 
         * score2 is the score for Player 2. 
         * returns the winner of the game */
        string FinalResults(int score1, int score2)
        {
            string theChampIs = " "; // initialize empty string
            if (score1 == score2)
                theChampIs = "It's a draw!"; // No sudden death overtime for you!
            else if (score1 > score2)
                theChampIs = "Player 1 is the champ!"; // Player 1 wins the game! 
            else
                theChampIs = "Player 2 is the champ!"; // Player 2 wins the game!
            return theChampIs; // returns the winner
        }

        /* This method begins the beguine - I mean game, with apologies to Cole Porter. */
        void LetsPlay()
        {
            int playerOneNumber; // number drawn by Player 1
            int playerTwoNumber; // number drawn by Player 2
            char playerOneDecision; // decision made by Player 1
            char playerTwoDecision; // decision made by Player 2
            char versus = ' '; // user can choose solo mode (vs CPU) or dual mode (vs another player)
            char[] playerOneStandings = new char[gameCount]; // initialize win-loss-tie record for Player 1
            char[] playerTwoStandings = new char[gameCount]; // initialize win-loss-tie record for Player 2
            string fight = ""; // This variable indicates the outcome of each round. 

            Random rn = new Random(); // instantiate random number
            int i; // i = round
            int maxNumber = 9; // standard maximum judge score
            WriteLine("Type H or h for hard mode. "); // easy mode or hard mode
            char stage = Convert.ToChar(ReadLine().ToUpper());

            if (DifficultyMode(stage) == 2) // hard mode activated
                maxNumber = 18; // maximum number drawn goes up to 18
            try
            {
                WriteLine("Now, will it be 1 player (A) or 2 players (B)?"); // prompt user for 1 or 2 players
                versus = Convert.ToChar(ReadLine().ToUpper()); // A for solo, B for dual
            }
            catch (FormatException fe)
            {
                WriteLine("That is not a character. " + fe.Message); // Yes, there is exception handling. 
                versus = 'B'; // automatically defaults to two-player mode
            }

            // A warning pops up if versus = 'C' or some other character. 
            if (versus != 'A' && versus != 'B')
                WriteLine("Warning: There is no option " + versus + ". "); // choose A or B

            for (i = 1; i <= gameCount; i++)
            {
                if (i == gameCount) // i == 3
                    WriteLine("Final Round"); // Round 3
                else
                    WriteLine("Round " + i); // example: Round 1 (i = 1)

                if (TwoPlayer(versus))
                {
                    playerOneNumber = rn.Next(1, maxNumber); // draw a random number
                    playerTwoNumber = rn.Next(1, maxNumber); // draw a random number
                    WriteLine("P1: Strike or Dodge? "); // s to strike, any other key to dodge
                    playerOneDecision = Convert.ToChar(ReadLine().ToLower());  // Player 1 makes a move.
                    WriteLine("P2: Strike or Dodge? "); // Your turn, Player 2! 
                    playerTwoDecision = Convert.ToChar(ReadLine().ToLower()); // Player 2 makes a move. 

                    WriteLine("P1 drew " + playerOneNumber + ". P2 drew " + playerTwoNumber + "."); // numbers drawn
                    fight = Duel(playerOneNumber, playerTwoNumber, playerOneDecision, playerTwoDecision); // winner of round
                    WriteLine("Round " + i + " goes to " + fight + "."); // Print winner of round to console.

                    // Update records for Player 1. 
                    playerOneStandings[i - 1] = WinLossOrTie(playerOneNumber, playerTwoNumber, playerOneDecision, playerTwoDecision);
                }

                else
                {
                    playerOneNumber = rn.Next(1, maxNumber); // draw a random number
                    playerTwoNumber = rn.Next(1, maxNumber); // draw a random number
                    WriteLine("Will you strike or dodge? "); // s to strike, any other key to dodge
                    playerOneDecision = Convert.ToChar(ReadLine().ToLower());

                    if (playerTwoNumber > maxNumber - 4)
                        // The cpu will strike if the number drawn is more than 5 (easy mode) or more than 14 (hard mode).
                        playerTwoDecision = 's'; // CPU strikes
                    else
                        playerTwoDecision = 'd'; // CPU dodges

                    WriteLine("You drew " + playerOneNumber + ". The CPU drew " + playerTwoNumber + "."); // numbers drawn
                    fight = Duel(playerOneNumber, playerTwoNumber, playerOneDecision, playerTwoDecision); // winner of round
                    WriteLine("Round " + i + " goes to " + fight + "."); // Print winner of round to console.

                    // Update records for Player 1. 
                    playerOneStandings[i - 1] = WinLossOrTie(playerOneNumber, playerTwoNumber, playerOneDecision, playerTwoDecision);
                }
            }

            playerTwoStandings = PlayerTwoRecord(playerOneStandings); // Update records for Player 2.
            int playerOneScore = ScoreKeeper(playerOneStandings); // Calculate score for Player 1.
            int playerTwoScore = ScoreKeeper(playerTwoStandings); // Calculate score for Player 2.
            WriteLine("Player 1 has a score of " + playerOneScore + "."); // Print score to console.
            WriteLine("Player 2 has a score of " + playerTwoScore + "."); // Print score to console.
            string whoWon = FinalResults(playerOneScore, playerTwoScore); // final results - who won?
            WriteLine(whoWon); // Print the results the console.
        }

        /* Main Method */
        /* This method includes the welcome message and starts the game. */
        static void Main(string[] args)
        {
            WriteLine("Welcome fellow gamer!"); // introductory message
            Judgment pound = new Judgment(); // instantiate Judgment object
            pound.LetsPlay(); // play the game
            WriteLine("Game Over Yeah!"); // end of game (phrase credited to Takenobu Mitsuyoshi)
            ReadKey(); // press any key to close game
        }
    }
}

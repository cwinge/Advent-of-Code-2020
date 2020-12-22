using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Day_22
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");
            var player1 = input.Skip(1).TakeWhile(x => !String.IsNullOrWhiteSpace(x)).Select(x => int.Parse(x));
            var player2 = input.SkipWhile(x => !x.Equals("Player 2:")).Skip(1).Select(x => int.Parse(x));
            PartOne(new Queue<int>(player1), new Queue<int>(player2));
            PartTwo(new Queue<int>(player1), new Queue<int>(player2));
        }

        static void PartOne(Queue<int> playerOne, Queue<int> playerTwo)
        {
            while (playerOne.Count != 0 && playerTwo.Count != 0)
            {
                if (playerOne.Peek() < playerTwo.Peek())
                {
                    playerTwo.Enqueue(playerTwo.Dequeue());
                    playerTwo.Enqueue(playerOne.Dequeue());
                }
                else
                {
                    playerOne.Enqueue(playerOne.Dequeue());
                    playerOne.Enqueue(playerTwo.Dequeue());
                }
            }

            Queue<int> winner = playerOne.Count == 0 ? playerTwo : playerOne;
            long score = 0;
            for (int i = winner.Count; i > 0; i--)
            {
                score += (i * winner.Dequeue());
            }

            Console.WriteLine($"Part one score: {score}");
        }

        static void PartTwo(Queue<int> playerOne, Queue<int> playerTwo)
        {
            long score = 0;
            Queue<int> winner = PlayerOneWins(playerOne, playerTwo) ? playerOne : playerTwo;
            for (int i = winner.Count; i > 0; i--)
            {
                score += (i * winner.Dequeue());
            }

            Console.WriteLine($"Part two score: {score}");
        }

        static bool PlayerOneWins(Queue<int> playerOne, Queue<int> playerTwo)
        {
            HashSet<string> PlayerOneHistory = new HashSet<string>();
            HashSet<string> PlayerTwoHistory = new HashSet<string>();
            while (playerOne.Any() && playerTwo.Any())
            {
                var playerOneCurrentDeck = string.Join(",", playerOne.Select(c => c));
                var playerTwoCurrentDeck = string.Join(",", playerTwo.Select(c => c));
                if (PlayerOneHistory.Contains(playerOneCurrentDeck) && PlayerTwoHistory.Contains(playerTwoCurrentDeck))
                {
                    return true;
                }
                PlayerOneHistory.Add(playerOneCurrentDeck);
                PlayerTwoHistory.Add(playerTwoCurrentDeck);

                var topOne = playerOne.Dequeue();
                var topTwo = playerTwo.Dequeue();

                bool oneWon;
                if(topOne <= playerOne.Count && topTwo <= playerTwo.Count)
                {
                    oneWon = PlayerOneWins(new Queue<int>(playerOne.Take(topOne)), new Queue<int>(playerTwo.Take(topTwo)));
                }else{

                    oneWon = topOne > topTwo;
                }

                if (oneWon)
                {
                    playerOne.Enqueue(topOne);
                    playerOne.Enqueue(topTwo);
                }
                else
                {
                    playerTwo.Enqueue(topTwo);
                    playerTwo.Enqueue(topOne);
                }
            }
            return playerOne.Any();
        }
    }
}

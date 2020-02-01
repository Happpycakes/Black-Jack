using System;
using System.Threading;
using System.Collections.Generic;

namespace BlackJack
{
    class Program
    {
        static void Main(string[] args)
        {
            int choice = Menu();

            while (1 == 1)
            {
                Console.Clear();
                IDictionary<int, int> card = CreateCard();
                IDictionary<int, int> cardTwo = CreateCard();
                PlayBlackjack(CardNumber(card), CardNumber(cardTwo));
                Console.ReadLine();
            }
        }

        static int Menu()
        {
            TypeOut("Hello and welcome to the program, please enter your name: ");
            string name = Console.ReadLine();

            Console.Clear();
            TypeOut("Please select the NUMBER of one of the games below to play");

            Console.Write("\n\n1.) Blackjack ");
            int choice = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

            return choice;
        }

        static IDictionary<int, int> CreateCard()
        {
            IDictionary<int, int> card = new Dictionary<int, int>();

            Random cardNumGen = new Random();
            Random cardClassGen = new Random();

            int cardNumber = cardNumGen.Next(2, 15);
            int cardClass = cardClassGen.Next(0, 4);

            card.Add(cardNumber, cardClass);

            //foreach (KeyValuePair<int, int> item in card)
            //{
            //    Console.WriteLine("Card Number: {0} Card Class: {1}", item.Key, item.Value);
            //}

            return card;
        }

        static int CardNumber(IDictionary<int, int> card)
        {
            foreach (KeyValuePair<int, int> item in card)
                return item.Key;

            throw new NotImplementedException();
        }

        static void PlayBlackjack(int card, int cardTwo)
        {
            #region Declarations
            Random dealerCards = new Random();
            int dealerCard = CardNumber(CreateCard());
            int dealerCardTwo = CardNumber(CreateCard());
            int dealerCardThree = CardNumber(CreateCard());
            int dealerCardFour = CardNumber(CreateCard());
            int dealerCardFive = CardNumber(CreateCard());
            int cardThree = CardNumber(CreateCard());
            int cardFour = CardNumber(CreateCard());
            int cardFive = CardNumber(CreateCard());
            int hits = 0;
            int totalScore = 0;
            int dealerScore = dealerCard + dealerCardTwo;
            string cardOneString = GetCardString(card);
            string cardTwoString = GetCardString(cardTwo);
            string cardThreeString = GetCardString(cardThree); 
            string cardFourString = GetCardString(cardFour);
            string cardFiveString = GetCardString(cardFive);
            string dealerCardOneString = GetCardString(dealerCard);
            string dealerCardTwoString = GetCardString(dealerCardTwo);
            string dealerCardThreeString = GetCardString(dealerCardThree); ;
            string dealerCardFourString = GetCardString(dealerCardFour);
            string dealerCardFiveString = GetCardString(dealerCardFive);
            bool playerAutoWin = false;
            bool dealerAutoWin = false;
            bool dealerHit = false;
            bool playing = true;
            bool hit = false;
            bool validAnswer;
            #endregion

            #region SetFaceValue
            if (card == 12 || card == 13 || card == 14)
                card = 10;
            if (cardTwo == 12 || cardTwo == 13 || cardTwo == 14)
                cardTwo = 10;
            if (cardThree == 12 || cardThree == 13 || cardThree == 14)
                cardThree = 10;
            if (cardFour == 12 || cardFour == 13 || cardFour == 14)
                cardFour = 10;
            if (cardFive == 12 || cardFive == 13 || cardFive == 14)
                cardFive = 10;

            if (dealerCard == 12 || dealerCard == 13 || dealerCard == 14)
                dealerCard = 10;
            if (dealerCardTwo == 12 || dealerCardTwo == 13 || dealerCardTwo == 14)
                dealerCardTwo = 10;
            if (dealerCardThree == 12 || dealerCardThree == 13 || dealerCardThree == 14)
                dealerCardThree = 10;
            if (dealerCardFour == 12 || dealerCardFour == 13 || dealerCardFour == 14)
                dealerCardFour = 10;
            if (dealerCardFive == 12 || dealerCardFive == 13 || dealerCardFive == 14)
                dealerCardFive = 10;

            #endregion

            TypeOut("Your cards: " + cardOneString + ", and " + cardTwoString + "\n");
            TypeOut("The dealer is showing a " + dealerCardOneString + "\n");
            Thread.Sleep(500);

            while (playing)
            {
                validAnswer = false;
                TypeOut("Hit or stay? ");

                while (validAnswer == false)
                {
                    string hitOrStay = Console.ReadLine().ToUpper();

                    if (hitOrStay == "HIT")
                    {
                        hit = true;
                        validAnswer = true;
                        hits++;
                    }

                    else if (hitOrStay == "STAY")
                    {
                        hit = false;
                        validAnswer = true;
                    }

                    else
                    {
                        Console.WriteLine("Please enter either hit or stay.");
                    }
                }

                if (!hit)
                {
                    playing = false;

                    if (hits == 0)
                    {
                        totalScore = card + cardTwo;
                    }

                    dealerHit = DealerLogic(dealerScore);

                    TypeOut("The dealer flips over their other card. They now have a " + dealerCardOneString + " and a " + dealerCardTwoString);
                    Console.ReadLine();

                    if(dealerHit)
                    {
                        TypeOut("The dealer hit. They now have a " + dealerCardOneString + ", " + dealerCardTwoString + " and a "+ dealerCardThreeString);
                        dealerScore += dealerCardThree;
                        if ((dealerCardOneString == "A" || dealerCardTwoString == "A" || dealerCardThreeString == "A") && (totalScore > 21))
                            dealerScore -= 10;

                        Console.ReadLine();
                        dealerHit = DealerLogic(dealerScore);

                        if (dealerHit)
                        {
                            TypeOut("The dealer hit again. they now have a " + dealerCardOneString + ", " + dealerCardTwoString + ", " + dealerCardThreeString + " and a " + dealerCardFourString);
                            dealerScore += dealerCardFour;
                            if ((dealerCardOneString == "A" || dealerCardTwoString == "A" || dealerCardThreeString == "A" || dealerCardFourString == "A") && (totalScore > 21))
                                dealerScore -= 10;

                            Console.ReadLine();
                            dealerHit = DealerLogic(dealerScore);

                            if (dealerHit)
                            {
                                TypeOut("The dealer hit AGAIN. They now have a " + dealerCardOneString + ", " + dealerCardTwoString + ", " + dealerCardThreeString + ", " + dealerCardFourString + " and a " + dealerCardFiveString);
                                dealerScore += dealerCardFive;
                                if ((dealerCardOneString == "A" || dealerCardTwoString == "A" || dealerCardThreeString == "A" || dealerCardFourString == "A" || dealerCardFiveString == "A") && (totalScore > 21))
                                    dealerScore -= 10;

                                Console.ReadLine();
                                dealerAutoWin = true;
                            }
                        }
                    }

                    EndGame(totalScore, dealerScore, playerAutoWin, dealerAutoWin);
                }

                else if (hits == 1)
                {
                    Console.WriteLine("Your new cards are, {0}, {1}, {2}", cardOneString, cardTwoString, cardThreeString);
                    totalScore = card + cardTwo + cardThree;

                    if((cardOneString == "A" || cardTwoString == "A" || cardThreeString == "A") && (totalScore > 21))
                        totalScore -= 10;

                    if (totalScore > 21)
                    {
                        Thread.Sleep(1000);
                        EndGame(totalScore, dealerScore, playerAutoWin, dealerAutoWin);
                        playing = false;
                    }

                    hit = false;
                }

                else if (hits == 2)
                {
                    Console.WriteLine("Your new cards are, {0}, {1}, {2}, {3}", cardOneString, cardTwoString, cardThreeString, cardFourString);
                    totalScore = card + cardTwo + cardThree + cardFour;

                    if ((cardOneString == "A" || cardTwoString == "A" || cardThreeString == "A" || cardFourString == "A") && (totalScore > 21))
                        totalScore -= 10;

                    if (totalScore > 21)
                    {
                        Thread.Sleep(1000);
                        EndGame(totalScore, dealerScore, playerAutoWin, dealerAutoWin);
                        playing = false;
                    }

                    hit = false;
                }

                else if (hits == 3)
                {
                    Console.WriteLine("Your new cards are, {0}, {1}, {2}, {3}, {4}", cardOneString, cardTwoString, cardThreeString, cardFourString, cardFiveString);
                    totalScore = card + cardTwo + cardThree + cardFour + cardFive;

                    if ((cardOneString == "A" || cardTwoString == "A" || cardThreeString == "A" || cardFourString == "A" || cardFiveString == "A") && (totalScore > 21))
                        totalScore -= 10;

                    if (totalScore > 21)
                    {
                        Thread.Sleep(1000);
                        EndGame(totalScore, dealerScore, playerAutoWin, dealerAutoWin);
                        playing = false;
                    }

                    playerAutoWin = true;
                    hit = false;
                    Console.WriteLine("You MUST select stay this round.");
                }
            }
        }

        static void EndGame(int finalScore, int dealerScore, bool playerAutoWin, bool DealerAutoWin)
        {
            Console.Clear();

            TypeOut("Game finished. Your final score was, " + finalScore + ". The dealer finished with a " + dealerScore + "\n");
            if (finalScore == dealerScore || playerAutoWin && DealerAutoWin)
                TypeOut("The dealer won on tie. YOU LOSE");
            else if (playerAutoWin)
                TypeOut("Congrats you survived with 5 cards. YOU WIN!");
            else if (DealerAutoWin)
                TypeOut("The dealer survived with 5 cards. YOU LOSE!");
            else if (finalScore > dealerScore && finalScore <= 21)
                TypeOut("YOU WIN");
            else if (finalScore > 21)
                TypeOut("You busted. YOU LOSE");
            else if (finalScore < dealerScore && dealerScore <= 21)
                TypeOut("The dealer outplayed you. YOU LOSE");
            else if (finalScore <= 21 && dealerScore > 21)
                TypeOut("The dealer busted. YOU WIN!");
        }

        static bool DealerLogic(int dealerTotal)
        {
            if (dealerTotal <= 16)
                return true;
            else if (dealerTotal > 16)
                return false;
            else throw new NotImplementedException();
        }

        static void TypeOut(string message)
        {
           foreach (char c in message)
            {
                Console.Write(c);
                Thread.Sleep(40);
            }
        }

        static string GetCardString(int card)
        {
            if (card == 11)
                return "A";
            else if (card == 12)
                return "J";
            else if (card == 13)
                return "Q";
            else if (card == 14)
                return "K";
            else return Convert.ToString(card);
        }
    }
}

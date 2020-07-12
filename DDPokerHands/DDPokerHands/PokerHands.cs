using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DDPokerHands
{
    public class PokerHands
    {
        /// <summary>
        /// Read the string, divide them between players, get the suits, gets the values and sort, 
        /// Group by values and count, check the range, decide the winner
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public string ReadFile(string line)
        {
            Dictionary<int, int> player1;
            Dictionary<int, int> player2;
            Dictionary<int, int> rankList1;
            Dictionary<int, int> rankList2;
            ChooseWinnerByRank chooseWinner = new ChooseWinnerByRank();
            string winner = string.Empty;
            try
            {
                string player1Cards = line.Substring(0, 14);
                string player2Cards = line.Substring(15, 14);

                string[] player1CardsSuits = player1Cards.Split(' ');
                string[] player2CardsSuits2 = player2Cards.Split(' ');

                player1 = GroupCardByValueCount(player1Cards);
                player2 = GroupCardByValueCount(player2Cards);

                List<string> numberSuit = GetValuesfromCards(player1CardsSuits);
                List<string> numberSuit2 = GetValuesfromCards(player2CardsSuits2);

                List<int> numbers = GetSuitsfromCards(player1CardsSuits);
                List<int> numbers2 = GetSuitsfromCards(player2CardsSuits2);

                numbers.Sort();
                numbers2.Sort();

                rankList1 = chooseWinner.CheckRanks(player1, numbers, numberSuit);
                rankList2 = chooseWinner.CheckRanks(player2, numbers2, numberSuit2);

                //Check the rank2 has the rank1
                int maxRankP1 = rankList1.Count != 0 ? rankList1.Keys.Max() : 0;
                int maxRankP2 = rankList2.Count != 0 ? rankList2.Keys.Max() : 0;
                if (maxRankP1 > maxRankP2)
                {
                    winner = "player1";
                }
                else if (maxRankP2 > maxRankP1)
                {
                    winner = "player2";
                }
                else if (maxRankP2 == maxRankP1)
                {
                    int maxCard1 = rankList1.Where(s => s.Key == maxRankP1).Select(s => s.Value).FirstOrDefault();
                    int maxCard2 = rankList2.Where(s => s.Key == maxRankP2).Select(s => s.Value).FirstOrDefault();

                    if (maxCard1 > maxCard2)
                    {
                        winner = "player1";
                    }
                    else if (maxCard2 > maxCard1)
                    {
                        winner = "player2";
                    }
                }
                if (winner == string.Empty)
                { winner = RemoveHighestValueFromBoth(player1, player2); }
                return winner;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<int> GetSuitsfromCards(string[] playerCardsSuits)
        {
            List<int> numbers = new List<int>();
            try
            {
                foreach (var item in playerCardsSuits.ToList())
                {
                    if (item.Contains('K')) { numbers.Add(13); }
                    else if (item.Contains('T')) { numbers.Add(10); }
                    else if (item.Contains('J')) { numbers.Add(11); }
                    else if (item.Contains('Q')) { numbers.Add(12); }
                    else if (item.Contains('A')) { numbers.Add(14); }
                    else { numbers.Add(Convert.ToInt32(item.Substring(0, 1))); }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return numbers;
        }

        private List<string> GetValuesfromCards(string[] playerCardsSuits)
        {
            List<string> numberSuit = new List<string>();
            foreach (var item in playerCardsSuits.ToList())
            {
                numberSuit.Add(item.Substring(1));
            }
            return numberSuit;
        }

        /// <summary>
        /// When all the rules doesnt match, check the last one- get the highest value among palyers to decide winner
        /// </summary>
        /// <param name="player1"></param>
        /// <param name="player2"></param>
        /// <returns></returns>
        public string RemoveHighestValueFromBoth(Dictionary<int, int> player1, Dictionary<int, int> player2)
        {
            string winner = string.Empty;

            int maxp1 = player1.Keys.Max();
            int maxp2 = player2.Keys.Max();

            if (Convert.ToInt32(maxp1) > Convert.ToInt32(maxp2))
            { winner = "player1"; }
            else if (Convert.ToInt32(maxp2) > Convert.ToInt32(maxp1))
            { winner = "player2"; }
            else if (Convert.ToInt32(maxp2) == Convert.ToInt32(maxp1))
            {
                player2.Remove(maxp2);
                player1.Remove(maxp1);
                if (winner == string.Empty)
                {
                    winner = RemoveHighestValueFromBoth(player1, player2);
                };
            }
            return winner;
        }
        /// <summary>
        /// Replace the values of alphabets, group by the counts
        /// </summary>
        /// <param name="player1"></param>
        /// <returns></returns>
        public Dictionary<int, int> GroupCardByValueCount(string player1)
        {
            Dictionary<int, int> stringNum = new Dictionary<int, int>();
            if (player1.Contains("2"))
            {
                int count = player1.Count(x => x == '2');
                stringNum.Add(2, count);
            }
            if (player1.Contains("3"))
            {
                int count = player1.Count(x => x == '3');
                stringNum.Add(3, count);
            }
            if (player1.Contains("4"))
            {
                int count = player1.Count(x => x == '4');
                stringNum.Add(4, count);
            }
            if (player1.Contains("5"))
            {
                int count = player1.Count(x => x == '5');
                stringNum.Add(5, count);
            }
            if (player1.Contains("6"))
            {
                int count = player1.Count(x => x == '6');
                stringNum.Add(6, count);
            }
            if (player1.Contains("7"))
            {
                int count = player1.Count(x => x == '7');
                stringNum.Add(7, count);
            }
            if (player1.Contains("8"))
            {
                int count = player1.Count(x => x == '8');
                stringNum.Add(8, count);
            }
            if (player1.Contains("9"))
            {
                int count = player1.Count(x => x == '9');
                stringNum.Add(9, count);
            }
            if (player1.Contains("T"))
            {
                int count = player1.Count(x => x == 'T');
                stringNum.Add(10, count);
            }
            if (player1.Contains("J"))
            {
                int count = player1.Count(x => x == 'J');
                stringNum.Add(11, count);
            }
            if (player1.Contains("Q"))
            {
                int count = player1.Count(x => x == 'Q');
                stringNum.Add(12, count);
            }
            if (player1.Contains("K"))
            {
                int count = player1.Count(x => x == 'K');
                stringNum.Add(13, count);
            }
            if (player1.Contains("A"))
            {
                int count = player1.Count(x => x == 'A');
                stringNum.Add(14, count);
            }
            return stringNum;
        }
       
    }
}


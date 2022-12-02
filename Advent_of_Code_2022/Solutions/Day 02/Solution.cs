namespace Advent_of_Code_2022.Solutions.Day_02;

public class Solution: ISolution
{
    
    public static IEnumerable<(PlayOption myHand, PlayOption theirHand)> ParseInputPartOne(string input)
    {
        return input.Split('\n')
            .Where(line => !line.Equals(""))
            .Select(line => line.Split(' '))
            .Select(items => new [] {items[0][0], items[1][0]})
            .Select(hands => (
                myHand: (PlayOption) (hands[1] - 'X' + 1), 
                theirHand: (PlayOption) (hands[0] - 'A' + 1))
            );
    }
    
    public static IEnumerable<(PlayOption theirHand, Outcome outcome)> ParseInputPartTwo(string input)
    {
        return input.Split('\n')
            .Where(line => !line.Equals(""))
            .Select(line => line.Split(' '))
            .Select(items => new [] {items[0][0], items[1][0]})
            .Select(hands => (
                theirHand: (PlayOption) (hands[0] - 'A' + 1), 
                outcome: (Outcome) ((hands[1] - 'X') * 3))
            );
    }

    public enum PlayOption
    {
        Rock = 1, Paper = 2, Scissors = 3
    }

    public enum Outcome
    {
        Win = 6, Draw = 3, Lose = 0
    }

    public static Outcome OutcomeForMatch(PlayOption myHand, PlayOption theirHand)
    {
        if (myHand == theirHand) return Outcome.Draw;

        return myHand switch
        {
            PlayOption.Rock => theirHand == PlayOption.Scissors ? Outcome.Win : Outcome.Lose,
            PlayOption.Paper => theirHand == PlayOption.Rock ? Outcome.Win : Outcome.Lose,
            PlayOption.Scissors => theirHand == PlayOption.Paper ? Outcome.Win : Outcome.Lose
        };
    }

    public static PlayOption PlayOptionForMatch(PlayOption theirHand, Outcome outcome)
    {
        return outcome switch
        {
            Outcome.Draw => theirHand,
            Outcome.Lose => theirHand switch
            {
                PlayOption.Rock => PlayOption.Scissors,
                PlayOption.Paper => PlayOption.Rock,
                PlayOption.Scissors => PlayOption.Paper
            },
            Outcome.Win => theirHand switch
            {
                PlayOption.Rock => PlayOption.Paper,
                PlayOption.Paper => PlayOption.Scissors,
                PlayOption.Scissors => PlayOption.Rock
            }
        };
    }

    public string GetFirstAnswer()
    {
        return ParseInputPartOne(File.ReadAllText("Solutions/Day 02/input.txt"))
            .Select(hands => (myHand: hands.myHand, outcome: OutcomeForMatch(hands.myHand, hands.theirHand)))
            .Select(match => (int) match.outcome + (int) match.myHand)
            .Sum()
            .ToString();
    }

    public string GetSecondAnswer()
    {
        return ParseInputPartTwo(File.ReadAllText("Solutions/Day 02/input.txt"))
            .Select(handAndOutcome => (myHand: PlayOptionForMatch(handAndOutcome.theirHand, handAndOutcome.outcome), outcome: handAndOutcome.outcome))
            .Select(match => (int) match.outcome + (int) match.myHand)
            .Sum()
            .ToString();
    }
}
namespace Advent_of_Code_2022.Solutions.Day_02;

public class Solution: ISolution
{
    
    public static IEnumerable<(PlayOption myHand, PlayOption theirHand)> ParseInput(string input)
    {
        return input.Split('\n')
            .Where(line => !line.Equals(""))
            .Select(line => line.Split(' '))
            .Select(hands => (myHand: _optionsMap[hands[0]], theirHand: _optionsMap[hands[1]]));
    }

    public enum PlayOption
    {
        Rock,
        Paper,
        Scissors
    }

    private static Dictionary<PlayOption, int> _playOptionScore = new()
    {
        {PlayOption.Rock, 1},
        {PlayOption.Paper, 2},
        {PlayOption.Scissors, 3}
    };

    public enum Outcome
    {
        Win,
        Lose,
        Draw
    }

    private static Dictionary<Outcome, int> _outcomeScore = new()
    {
        { Outcome.Lose, 0},
        { Outcome.Draw, 3},
        { Outcome.Win, 6},
    };

    private static Dictionary<string, PlayOption> _optionsMap = new()
    {
        { "A", PlayOption.Rock },
        { "B", PlayOption.Paper },
        { "C", PlayOption.Scissors },
        { "X", PlayOption.Rock },
        { "Y", PlayOption.Paper },
        { "Z", PlayOption.Scissors },
    };

    public static Outcome OutcomeForMatch(PlayOption myHand, PlayOption theirHand)
    {
        if (myHand == theirHand) return Outcome.Draw;

        return myHand switch
        {
            PlayOption.Rock => theirHand == PlayOption.Scissors ? Outcome.Win : Outcome.Lose,
            PlayOption.Paper => theirHand == PlayOption.Rock ? Outcome.Win : Outcome.Lose,
            PlayOption.Scissors => theirHand == PlayOption.Paper ? Outcome.Win : Outcome.Lose,
            _ => throw new ArgumentOutOfRangeException(nameof(myHand), myHand, null)
        };
    } 

    public string GetFirstAnswer()
    {
        var parsedInput = ParseInput(File.ReadAllText("Solutions/Day 02/input.txt"));

        var score = parsedInput
            .Select(hands => (hands, outcome: OutcomeForMatch(hands.myHand, hands.theirHand)))
            .Select(match => _outcomeScore[match.outcome] + _playOptionScore[match.hands.myHand])
            .Sum();
            
        
        return score.ToString();
    }

    public string GetSecondAnswer()
    {
        throw new NotImplementedException();
    }
}
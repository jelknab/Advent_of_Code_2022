namespace Advent_of_Code_2022.Solutions.Day_02;

public class Solution: ISolution
{
    
    public static IEnumerable<(PlayOption myHand, PlayOption theirHand)> ParseInputPartOne(string input)
    {
        return input.Split('\n')
            .Where(line => !line.Equals(""))
            .Select(line => line.Split(' '))
            .Select(hands => (myHand: OptionsMap[hands[1]], theirHand: OptionsMap[hands[0]]));
    }
    
    public static IEnumerable<(PlayOption theirHand, Outcome outcome)> ParseInputPartTwo(string input)
    {
        return input.Split('\n')
            .Where(line => !line.Equals(""))
            .Select(line => line.Split(' '))
            .Select(hands => (theirHand: OptionsMap[hands[0]], outcome: OutcomeMap[hands[1]]));
    }

    public enum PlayOption
    {
        Rock,
        Paper,
        Scissors
    }
    
    private static readonly Dictionary<string, PlayOption> OptionsMap = new()
    {
        { "A", PlayOption.Rock },
        { "B", PlayOption.Paper },
        { "C", PlayOption.Scissors },
        { "X", PlayOption.Rock },
        { "Y", PlayOption.Paper },
        { "Z", PlayOption.Scissors },
    };

    private static readonly Dictionary<PlayOption, int> PlayOptionScore = new()
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
    private static readonly Dictionary<string, Outcome> OutcomeMap = new()
    {
        { "X", Outcome.Lose },
        { "Y", Outcome.Draw },
        { "Z", Outcome.Win }
    };

    private static readonly Dictionary<Outcome, int> OutcomeScore = new()
    {
        { Outcome.Lose, 0},
        { Outcome.Draw, 3},
        { Outcome.Win, 6},
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

    public static PlayOption PlayOptionForMatch(PlayOption theirHand, Outcome outcome)
    {
        return outcome switch
        {
            Outcome.Draw => theirHand,
            Outcome.Lose => theirHand switch
            {
                PlayOption.Rock => PlayOption.Scissors,
                PlayOption.Paper => PlayOption.Rock,
                PlayOption.Scissors => PlayOption.Paper,
                _ => throw new ArgumentOutOfRangeException(nameof(theirHand), theirHand, null)
            },
            Outcome.Win => theirHand switch
            {
                PlayOption.Rock => PlayOption.Paper,
                PlayOption.Paper => PlayOption.Scissors,
                PlayOption.Scissors => PlayOption.Rock,
                _ => throw new ArgumentOutOfRangeException(nameof(theirHand), theirHand, null)
            },
            _ => throw new ArgumentOutOfRangeException(nameof(outcome), outcome, null)
        };
    }

    public string GetFirstAnswer()
    {
        var parsedInput = ParseInputPartOne(File.ReadAllText("Solutions/Day 02/input.txt"));

        var score = parsedInput
            .Select(hands => (myHand: hands.myHand, outcome: OutcomeForMatch(hands.myHand, hands.theirHand)))
            .Select(match => OutcomeScore[match.outcome] + PlayOptionScore[match.myHand])
            .Sum();
        
        return score.ToString();
    }

    public string GetSecondAnswer()
    {
        var parsedInput = ParseInputPartTwo(File.ReadAllText("Solutions/Day 02/input.txt"));
        
        var score = parsedInput
            .Select(hands => (myHand: PlayOptionForMatch(hands.theirHand, hands.outcome), outcome: hands.outcome))
            .Select(match => OutcomeScore[match.outcome] + PlayOptionScore[match.myHand])
            .Sum();
        
        return score.ToString();
    }
}
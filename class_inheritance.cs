// Project objectives: Create a C# console app that has class inheritance and polymorphism.
// Inputs: User input a football player name
// Output: The League and name of team and name of player in the console
// This project consists of 3 parts (part A, partB, partC)

// Part A: Create the parent class FootballLeague
// Consist of 1 properties: LeagueName
// Properties are public
// Consist of 1 method: DisplayLeague()
// Method is public and virtual
// Method will display the league name
// The properties are: "English Premier League", "La Liga", "Bundesliga", "Serie A"

// Part B: Create the child class FootballTeam that inherits from FootballLeague
// Consist of 1 properties: TeamName
// Properties are public
// Consist of 1 method: DisplayTeam()
// Method is public and virtual
// Method will display the team name
// The properties are: "Manchester United", "Real Madrid", "Bayern Munich", "Juventus"
// Mapping : Manchester United - English Premier League, Real Madrid - La Liga, Bayern Munich - Bundesliga, Juventus - Serie A

// Part C: Create the child class FootballPlayer that inherits from FootballTeam
// Consist of 1 properties: FootballPlayerName
// Properties are public
// Consist of 1 method: PlayerName()
// Method is public and virtual
// Method will display the football player name
// The properties are: "Cantona", "Zidane", "Kane", "Baggio"
// Mapping : Cantona - Manchester United, Zidane - La Liga, Kane - Bayern Munich, Baggio - Juventus

using System;
using System.Collections.Generic;
using System.IO;

public class FootballLeague
{
    public string LeagueName { get; set; }

    public virtual void DisplayLeague()
    {
        Console.WriteLine("League: " + LeagueName);
    }
}

public class FootballTeam : FootballLeague
{
    public string TeamName { get; set; }

    public override void DisplayLeague()
    {
        base.DisplayLeague();
        Console.WriteLine("Team: " + TeamName);
    }

    public virtual void DisplayTeam()
    {
        Console.WriteLine("Team: " + TeamName);
    }
}

public class FootballPlayer : FootballTeam
{
    public string FootballPlayerName { get; set; }

    public override void DisplayLeague()
    {
        base.DisplayLeague();
        Console.WriteLine("Player: " + FootballPlayerName);
    }

    public override void DisplayTeam()
    {
        base.DisplayTeam();
        Console.WriteLine("Player: " + FootballPlayerName);
    }

    public virtual void DisplayPlayer()
    {
        Console.WriteLine("Player: " + FootballPlayerName);
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            Console.WriteLine("Enter the name of a football player: ");
            string playerName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(playerName))
            {
                throw new ArgumentException("Player name cannot be empty or whitespace.");
            }

            Dictionary<string, (string League, string Team)> playerMappings = LoadMappings("repos\\dwightfonseka\\copilot_net\\dictionary.txt");

            if (!playerMappings.TryGetValue(playerName, out var mapping))
            {
                Console.WriteLine("Player not found.");
                return;
            }

            FootballPlayer player = new FootballPlayer
            {
                LeagueName = mapping.League,
                TeamName = mapping.Team,
                FootballPlayerName = playerName
            };

            player.DisplayLeague();
            player.DisplayTeam();
            player.DisplayPlayer();
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("An unexpected error occurred: " + ex.Message);
        }
    }

    private static Dictionary<string, (string League, string Team)> LoadMappings(string filePath)
    {
        var mappings = new Dictionary<string, (string League, string Team)>();

        foreach (var line in File.ReadLines(filePath))
        {
            var parts = line.Split('-');
            if (parts.Length == 3)
            {
                var playerName = parts[0].Trim();
                var leagueName = parts[1].Trim();
                var teamName = parts[2].Trim();
                mappings[playerName] = (leagueName, teamName);
            }
        }

        return mappings;
    }
}
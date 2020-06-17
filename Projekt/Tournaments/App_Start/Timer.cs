using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using Tournaments.App_Start;
using System.Diagnostics;
using Tournaments.Models;
using Microsoft.Ajax.Utilities;
using System.Data.Entity;

public static class AspNetTimer
{
    private static readonly ApplicationDbContext db = new ApplicationDbContext();
    private static readonly Timer _timer = new Timer(OnTimerElapsed);
    private static readonly JobHost _jobHost = new JobHost();

    public static void Start()
    {
        _timer.Change(TimeSpan.Zero, TimeSpan.FromMinutes(10));
    }

    private static void OnTimerElapsed(object sender)
    {
        _jobHost.DoWork(() => Matcher.matchPlayers());
    }
}

public static class Matcher
{
    private static readonly ApplicationDbContext db = new ApplicationDbContext();

    public static void matchPlayers()
    {
        IList<Tournament> tournaments = db.Tournaments.Where(t => (t.Deadline < DateTime.Now) && !t.PlayersMatched).ToList();
        foreach (Tournament tournament in tournaments) {
            List<ApplicationUser> enrolledPlayers = new List<ApplicationUser>();
            List<Enrollment> enrollments = tournament.Enrollments.OrderBy(e => e.Ranking).ToList();
            foreach (Enrollment e in enrollments)
                enrolledPlayers.Add(e.ApplicationUser);
            ICollection<Battle> battles = new List<Battle>();
            switch (tournament.Discipline.MatchType)
            {
                case "ALL_VERSUS_ALL":
                    battles = allVersusAllMatch(enrolledPlayers, tournament);
                    break;
                case "BEST_WITH_WORST":
                    battles = bestWithWorstMatch(enrolledPlayers, tournament);
                    break;
                case "PAIRS_FROM_TOP":
                    battles = pairsFromTopMatch(enrolledPlayers, tournament);
                    break;
                default:
                    break;
            }
            tournament.PlayersMatched = true;
            db.Battles.AddRange(battles);
            db.SaveChanges();
        }
        db.Dispose();
    }

    private static ICollection<Battle> allVersusAllMatch(List<ApplicationUser> players, Tournament tournament)
    {
        ICollection<Battle> battles = new List<Battle>();
        for (int i = 0; i < players.Count; ++i)
        {
            for (int j = i + 1; j < players.Count; ++j)
            {
                battles.Add(new Battle(players[i], players[j], tournament));
            }
        }
        return battles;
    }

    private static ICollection<Battle> bestWithWorstMatch(List<ApplicationUser> players, Tournament tournament)
    {
        ICollection<Battle> battles = new List<Battle>();
        for (int i = 0; i < players.Count / 2; ++i)
        {
            battles.Add(new Battle(players[i], players[players.Count() - 1 - i], tournament));
        }
        if (players.Count % 2 == 1)
            battles.Add(new Battle(players[(int)(players.Count / 2) + 1], players[(int)(players.Count / 2) + 1], tournament));
        return battles;
    }

    private static ICollection<Battle> pairsFromTopMatch(List<ApplicationUser> players, Tournament tournament)
    {
        ICollection<Battle> battles = new List<Battle>();
        for (int i = 0; i < players.Count - 1; i += 2)
        {
            battles.Add(new Battle(players[i], players[i + 1], tournament));
        }
        if (players.Count % 2 == 1)
            battles.Add(new Battle(players[players.Count - 1], players[players.Count - 1], tournament));
        return battles;
    }
}
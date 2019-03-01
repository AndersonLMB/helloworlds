using System;
using System.Collections.Generic;
using System.Linq;

namespace SnakeSchedule
{
    class Program
    {
        static void Main(string[] args)
        {

            List<string> ls = new List<string>() {
                "Manchester City","Liverpool","Tottenham Hotspur","Manchester United","Arsenal","Chelsea","Wolverhampton Wanderers","Watford","Everton","West Ham United","AFC Bournemouth","Leicester City","Crystal Palace","Brighton and Hove Albion","Burnley","Newcastle United","Cardiff City","Southampton","Fulham","Huddersfield Town"
            };
            GenSchedule(ls);
        }
        public static void GenSchedule(ICollection<string> vs)
        {
            GenSchedule(vs, new ScheduleType()
            {
                HomeAndAway = true
            });
        }

        public static void GenSchedule(ICollection<string> vs, ScheduleType scheduleType)
        {
            //奇数
            if (vs.Count % 2 != 0)
            {
                vs.Add("EMPTY");
            }

            //偶数
            else
            {

            }
            var teams = vs.Select((s) => { return new Team(s); });
            var countOfTeams = teams.Count();
            int countOfRoundsOfSchedule = CalCountOfRounds(countOfTeams, scheduleType);
            int countOfMatchesPerRound = countOfTeams / 2;
            var schedule = new Schedule() { Rounds = new List<Round>() };
            for (int indexOfThisRound = 0; indexOfThisRound < countOfRoundsOfSchedule; indexOfThisRound++)
            {
                Round round = new Round() { Matches = new List<Match>() };
                //是第0轮
                if (indexOfThisRound == 0)
                {
                    for (int indexOfMatchInRound = 0; indexOfMatchInRound < countOfMatchesPerRound; indexOfMatchInRound++)
                    {
                        Match match = new Match();
                        match.HomeTeam = teams.ToList()[indexOfMatchInRound * 2];
                        match.AwayTeam = teams.ToList()[indexOfMatchInRound * 2 + 1];
                        round.Matches.Add(match);
                    }
                }
                //不是第0轮
                else
                {
                    //前一轮
                    var previosRound = schedule.Rounds[indexOfThisRound - 1];
                    for (int indexOfMatchInRound = 0; indexOfMatchInRound < countOfMatchesPerRound; indexOfMatchInRound++)
                    {
                        Match match = new Match();
                        if (indexOfMatchInRound == 0)
                        {
                            match.HomeTeam = teams.ToList()[0];
                            match.AwayTeam = previosRound.Matches[indexOfMatchInRound + 1].AwayTeam;
                        }
                        else
                        {
                            //是第二轮吗
                            match.HomeTeam = indexOfMatchInRound == 1 ? previosRound.Matches[0].AwayTeam : previosRound.Matches[indexOfMatchInRound - 1].HomeTeam;
                            //是最后一轮吗
                            match.AwayTeam = indexOfMatchInRound == countOfMatchesPerRound - 1 ? previosRound.Matches.Last().HomeTeam : previosRound.Matches[indexOfMatchInRound + 1].AwayTeam;
                        }
                        round.Matches.Add(match);
                    }
                }
                schedule.Rounds.Add(round);
            }
            ;


            schedule.CrossMatches();
            schedule.Print();
            ; ;
        }

        private static int CalCountOfRounds(int countOfTeams, ScheduleType scheduleType)
        {
            return (scheduleType.HomeAndAway ? 2 : 1) * (countOfTeams - 1);
        }
    }



    public class Schedule
    {
        public List<Round> Rounds { get; set; }

        public void CrossMatches()
        {
            var lastHalfRounds = Rounds.GetRange(Rounds.Count / 2, Rounds.Count / 2);
            lastHalfRounds.ForEach(round => round.ReverseHomeAndAwayForMatches());

            //Round[] oldRounds = new Round[Rounds.Count];

            //Rounds.CopyTo(oldRounds);

            var halfCountOfRounds = (Rounds.Count) / 2;

            for (int i = 0; i < halfCountOfRounds; i++)
            {

                if (i % 2 == 0)
                {

                }
                else
                {
                    var tempRound = Rounds[i];
                    Rounds[i] = Rounds[i + halfCountOfRounds];
                    Rounds[i + halfCountOfRounds] = tempRound;
                }
            }



            //var newRounds = new List<Round>();

            //var halfCountOfRounds = (Rounds.Count) / 2;

            //for (int i = 0; i < Rounds.Count; i++)
            //{
            //    if (i % 2 == 0)
            //    {
            //        newRounds.Add(oldRounds[i])
            //    }
            //    else
            //    {
            //        newrou
            //    }


            //    newRounds.Add(oldRounds[i]);
            //    newRounds.Add(oldRounds[i + halfCountOfRounds]);
            //}
            //Rounds = newRounds;

        }

        public void Print()
        {
            Rounds.ForEach((round) =>
            {
                var displayIndexOfThisRound = Rounds.IndexOf(round) + 1;
                round.Matches.ForEach((match) =>
                {
                    Console.WriteLine($"Round {displayIndexOfThisRound:D2} {match.HomeTeam.Name:12} vs {match.AwayTeam.Name:12}");
                });
                Console.WriteLine("--------------");



            });
        }

    }

    public class Round
    {
        public string RoundNumber { get; set; }
        public List<Match> Matches { get; set; }

        public void ReverseHomeAndAwayForMatches()
        {
            Matches.ForEach(match => match.ReverseHomeAndAway());
        }



    }

    public class Match
    {
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
        public void ReverseHomeAndAway()
        {
            var temp = HomeTeam;
            HomeTeam = AwayTeam;
            AwayTeam = temp;
        }
        public override string ToString()
        {
            return $"Match {HomeTeam} vs {AwayTeam}";
            //return base.ToString();
        }
    }
    public class Team
    {
        public Team(string name)
        {
            this.Name = name;
        }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"Team {Name}";
        }


    }

    public class ScheduleType
    {
        public bool HomeAndAway { get; set; }
        public bool SwapHomeAndAway { get; set; } = true;
        public bool Cross { get; set; }
    }




}

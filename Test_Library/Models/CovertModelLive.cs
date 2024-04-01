using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Library.Models
{
    public class CovertModelLive
    {
    }
    public class Datum
    {
        public string matchId { get; set; }
        public int leagueType { get; set; }
        public string leagueId { get; set; }
        public string leagueName { get; set; }
        public string leagueShortName { get; set; }
        public string leagueColor { get; set; }
        public string subLeagueId { get; set; }
        public string subLeagueName { get; set; }
        public int matchTime { get; set; }
        public int halfStartTime { get; set; }
        public int status { get; set; }
        public string homeId { get; set; }
        public string homeName { get; set; }
        public string awayId { get; set; }
        public string awayName { get; set; }
        public int homeScore { get; set; }
        public int awayScore { get; set; }
        public int homeHalfScore { get; set; }
        public int awayHalfScore { get; set; }
        public int homeRed { get; set; }
        public int awayRed { get; set; }
        public int homeYellow { get; set; }
        public int awayYellow { get; set; }
        public int homeCorner { get; set; }
        public int awayCorner { get; set; }
        public string homeRank { get; set; }
        public string awayRank { get; set; }
        public string season { get; set; }
        public string stageId { get; set; }
        public string round { get; set; }
        public string group { get; set; }
        public string location { get; set; }
        public string weather { get; set; }
        public string temperature { get; set; }
        public string explain { get; set; }
        public ExtraExplain extraExplain { get; set; }
        public bool hasLineup { get; set; }
        public bool neutral { get; set; }
        public int injuryTime { get; set; }
        public int updateTime { get; set; }
    }

    public class ExtraExplain
    {
        public int kickOff { get; set; }
        public int minute { get; set; }
        public int homeScore { get; set; }
        public int awayScore { get; set; }
        public int extraTimeStatus { get; set; }
        public int extraHomeScore { get; set; }
        public int extraAwayScore { get; set; }
        public int penHomeScore { get; set; }
        public int penAwayScore { get; set; }
        public int twoRoundsHomeScore { get; set; }
        public int twoRoundsAwayScore { get; set; }
        public int winner { get; set; }
    }

    public class Root
    {
        public int code { get; set; }
        public string message { get; set; }
        public List<Datum> data { get; set; }
    }
}

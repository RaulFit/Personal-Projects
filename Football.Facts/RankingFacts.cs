using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football.Facts
{
    public class RankingFacts
    {
        [Fact]
        public void GetTeam_InvalidPosition_ShouldReturnNull()
        {
            List<Team> teams = new List<Team>();
            Ranking ranking = new Ranking(teams);
            ranking.AddTeam(new Team("France"));
            ranking.AddTeam(new Team("Italy"));
            ranking.AddTeam(new Team("Spain"));
            Assert.Null(ranking.GetTeam(4));
        }

        [Fact]
        public void GetTeam_ValidPosition_ShouldReturnTeam()
        {
            List<Team> teams = new List<Team>();
            Ranking ranking = new Ranking(teams);
            Team france = new Team("France");
            ranking.AddTeam(france);
            ranking.AddTeam(new Team("Italy"));
            ranking.AddTeam(new Team("Spain"));
            Assert.Equal(france, ranking.GetTeam(1));
        }

        [Fact]
        public void CanGetPosition()
        {
            List<Team> teams = new List<Team>();
            Ranking ranking = new Ranking(teams);
            Team france = new Team("France");
            ranking.AddTeam(france);
            ranking.AddTeam(new Team("Italy"));
            ranking.AddTeam(new Team("Spain"));
            Assert.Equal(1, ranking.GetPosition(france));
        }

        [Fact]
        public void CanUpdateRankingBasedOnOneGame()
        {
            List<Team> teams = new List<Team>();
            Ranking ranking = new Ranking(teams);
            Team france = new Team("France");
            ranking.AddTeam(france);
            Team spain = new Team("Spain");
            ranking.AddTeam(spain);
            Game game = new Game(ranking.GetTeam(2), ranking.GetTeam(1), false);
            ranking.UpdateRanking(game);
            Assert.Equal(1, ranking.GetPosition(spain));
            Assert.Equal(2, ranking.GetPosition(france));
        }

        [Fact]
        public void CanUpdateRankingBasedOnMultipleGame()
        {
            List<Team> teams = new List<Team>();
            Ranking ranking = new Ranking(teams);
            Team france = new Team("France");
            ranking.AddTeam(france);
            Team spain = new Team("Spain");
            ranking.AddTeam(spain);
            Team italy = new Team("Italy");
            ranking.AddTeam(italy);
            Game game1 = new Game(ranking.GetTeam(1), ranking.GetTeam(2), true);
            Game game2 = new Game(ranking.GetTeam(2), ranking.GetTeam(3), false);
            Game game3 = new Game(ranking.GetTeam(2), ranking.GetTeam(1), false);
            ranking.UpdateRanking(game1);
            ranking.UpdateRanking(game2);
            ranking.UpdateRanking(game3);
            Assert.Equal(1, ranking.GetPosition(spain));
            Assert.Equal(2, ranking.GetPosition(france));
            Assert.Equal(3, ranking.GetPosition(italy));
        }


    }
}

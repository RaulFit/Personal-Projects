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
            Ranking ranking = new Ranking();
            ranking.AddTeam(new Team("France"));
            ranking.AddTeam(new Team("Italy"));
            ranking.AddTeam(new Team("Spain"));
            Assert.Null(ranking.GetTeam(4));
        }

        [Fact]
        public void GetTeam_ValidPosition_ShouldReturnTeam()
        {
            Ranking ranking = new Ranking();
            Team france = new Team("France");
            ranking.AddTeam(france);
            ranking.AddTeam(new Team("Italy"));
            ranking.AddTeam(new Team("Spain"));
            Assert.Equal(france, ranking.GetTeam(1));
        }

        [Fact]
        public void CanGetPosition()
        {
            Ranking ranking = new Ranking();
            Team france = new Team("France");
            ranking.AddTeam(france);
            ranking.AddTeam(new Team("Italy"));
            ranking.AddTeam(new Team("Spain"));
            Assert.Equal(1, ranking.GetPosition(france));
        }

        [Fact]
        public void CanUpdateRankingBasedOnOneGame()
        {
            Ranking ranking = new Ranking();
            Team france = new Team("France");
            ranking.AddTeam(france);
            Team spain = new Team("Spain");
            ranking.AddTeam(spain);
            Game game = new Game(france, spain, 10, 15);
            ranking.UpdateRanking(game);
            Assert.Equal(1, ranking.GetPosition(spain));
            Assert.Equal(2, ranking.GetPosition(france));
        }

        [Fact]
        public void CanUpdateRankingBasedOnMultipleGame()
        {
            Ranking ranking = new Ranking();
            Team france = new Team("France");
            ranking.AddTeam(france);
            Team spain = new Team("Spain");
            ranking.AddTeam(spain);
            Team italy = new Team("Italy");
            ranking.AddTeam(italy);
            Game game1 = new Game(france, spain, 10, 10);
            Game game2 = new Game(spain, italy, 15, 10);
            Game game3 = new Game(spain, france, 30, 20);
            ranking.UpdateRanking(game1);
            ranking.UpdateRanking(game2);
            ranking.UpdateRanking(game3);
            Assert.Equal(1, ranking.GetPosition(spain));
            Assert.Equal(2, ranking.GetPosition(france));
            Assert.Equal(3, ranking.GetPosition(italy));
        }
    }
}

using Xunit;

namespace Football.Facts
{
    public class GameFacts
    {
        [Fact]
        public void AwardPoints_GameIsDraw_ShouldAwardOnePointToBothTeams()
        {
            Team france = new Team("France");
            Team spain = new Team("Spain");
            Game game = new Game(france, spain, 10, 10);
            game.AwardPoints();
            Assert.False(france.HasLessPoints(spain));
            Assert.False(spain.HasLessPoints(france));
        }

        [Fact]
        public void AwardPoints_HomeTeamWon_ShouldAwardPointsToHomeTeam()
        {
            Team france = new Team("France");
            Team spain = new Team("Spain");
            Game game = new Game(france, spain, 20, 10);
            game.AwardPoints();
            Assert.True(spain.HasLessPoints(france));
        }

        [Fact]
        public void AwardPoints_AwayTeamWon_ShouldAwardPointsToAwayTeam()
        {
            Team france = new Team("France");
            Team spain = new Team("Spain");
            Game game = new Game(france, spain, 10, 20);
            game.AwardPoints();
            Assert.True(france.HasLessPoints(spain));
        }
    }
}

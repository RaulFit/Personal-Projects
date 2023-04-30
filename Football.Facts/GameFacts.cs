using Xunit;

namespace Football.Facts
{
    public class GameFacts
    {
        [Fact]
        public void GameIsNotDraw()
        {
            Team winner = new Team("France");
            Team loser = new Team("Spain");
            Game game = new Game(winner, loser, false);
            Assert.False(game.IsDraw());
        }

        [Fact]
        public void GameCanBeDraw()
        {
            Team winner = new Team("France");
            Team loser = new Team("Spain");
            Game game = new Game(winner, loser, true);
            Assert.True(game.IsDraw());
        }

        [Fact]
        public void IsWinner_ShouldReturnTrue()
        {
            Team winner = new Team("France");
            Team loser = new Team("Spain");
            Game game = new Game(winner, loser, false);
            Assert.True(game.IsWinner(new Team("France")));
        }

        [Fact]
        public void IsNotWinner_ShouldReturnFalse()
        {
            Team winner = new Team("France");
            Team loser = new Team("Spain");
            Game game = new Game(winner, loser, false);
            Assert.False(game.IsWinner(new Team("US")));
        }

        [Fact]
        public void IsLoser_ShouldReturnTrue()
        {
            Team winner = new Team("France");
            Team loser = new Team("Spain");
            Game game = new Game(winner, loser, false);
            Assert.True(game.IsLoser(new Team("Spain")));
        }

        [Fact]
        public void IsNotLoser_ShouldReturnFalse()
        {
            Team winner = new Team("France");
            Team loser = new Team("Spain");
            Game game = new Game(winner, loser, false);
            Assert.False(game.IsLoser(new Team("Germany")));
        }

    }
}

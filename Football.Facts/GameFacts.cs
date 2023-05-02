using Xunit;

namespace Football.Facts
{
    public class GameFacts
    {
        [Fact]
        public void IsWinner_ShouldReturnTrue()
        {
            Team france = new Team("France");
            Team italy = new Team("Italy");
            Game game = new Game(france, italy, false);
            Assert.True(game.IsWinner(france));
        }

        [Fact]
        public void IsNotWinner_ShouldReturnFalse()
        {
            Team france = new Team("France");
            Team spain = new Team("Spain");
            Game game = new Game(france, spain, false);
            Assert.False(game.IsWinner(spain));
        }

        [Fact]
        public void IsLoser_ShouldReturnTrue()
        {
            Team france = new Team("France");
            Team italy = new Team("Italy");
            Game game = new Game(france, italy, false);
            Assert.True(game.IsLoser(italy));
        }

        [Fact]
        public void IsNotLoser_ShouldReturnFalse()
        {
            Team france = new Team("France");
            Team italy = new Team("Italy");
            Game game = new Game(france, italy, false);
            Assert.False(game.IsLoser(france));
        }

    }
}

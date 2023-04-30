using Xunit;

namespace Football.Facts
{
    public class TeamFacts
    {
        [Fact]
        public void CompareNames_SameNames_ShoulReturnTrue()
        {
            Team firstTeam = new Team("Germany");
            Team secondTeam = new Team("Germany");
            Assert.True(firstTeam.CompareNames(secondTeam));
        }

        [Fact]
        public void CompareNames_DifferentNames_ShoulReturnFalse()
        {
            Team firstTeam = new Team("Germany");
            Team secondTeam = new Team("England");
            Assert.False(firstTeam.CompareNames(secondTeam));
        }

        [Fact]
        public void AddPoints_ShouldAddPointsToTheTeam()
        {
            Team firstTeam = new Team("Germany");
            Team secondTeam = new Team("England");
            Assert.False(firstTeam.HasLessPoints(secondTeam));
            secondTeam.AddPoints(3);
            Assert.True(firstTeam.HasLessPoints(secondTeam));
        }
    }
}

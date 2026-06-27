namespace GuildWars2.Tests.Features.Exploration.PointsOfInterest;

public class PointOfInterestCoordinateAssertions
{
    [Test]
    public async Task Coordinate_values_can_be_compared_with_tolerance()
    {
        float expectedX = 52657.7f;
        float expectedY = 32978.8f;
        float actualX = 52657.7005f;
        float actualY = 32978.8002f;

        await Assert.That(actualX).IsEqualTo(expectedX).Within(0.001f);
        await Assert.That(actualY).IsEqualTo(expectedY).Within(0.001f);
    }
}

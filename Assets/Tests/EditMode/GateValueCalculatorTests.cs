using NUnit.Framework;

public class GateValueCalculatorTests
{
    [Test]
    public void ApplyBulletHit_IncreasesValueByOne()
    {
        Assert.AreEqual(-99, GateValueCalculator.ApplyBulletHit(-100));
        Assert.AreEqual(1, GateValueCalculator.ApplyBulletHit(0));
        Assert.AreEqual(6, GateValueCalculator.ApplyBulletHit(5));
    }

    [Test]
    public void ApplyBulletHit_CanTransitionFromNegativeToZeroToPositive()
    {
        int value = -1;

        value = GateValueCalculator.ApplyBulletHit(value);
        Assert.AreEqual(0, value);

        value = GateValueCalculator.ApplyBulletHit(value);
        Assert.AreEqual(1, value);
    }
}

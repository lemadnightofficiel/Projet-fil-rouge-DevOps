namespace ProjetDevOps.UnitsTests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        const bool varTrue = true;
        Assert.True(varTrue);
    }

    [Fact]
    public void Test2()
    {
        const bool varFalse = false;
        Assert.False(varFalse);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Test3(object value)
    {
        Assert.IsType<bool>(value);
    }
}

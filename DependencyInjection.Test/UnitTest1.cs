using System;
using Xunit;

namespace DependencyInjection.Test
{
    public class UnitTest1
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public void IsEven(int value)
        {
            Assert.True(value % 2 == 0);
        }
    }
}

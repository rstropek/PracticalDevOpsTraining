using AspNetCore1Angular2Intro.Services;
using System;
using Xunit;

namespace Tests
{
    public class TestClass
    {
        [Fact]
        public void TestAdd()
        {
            Assert.True(1 == 1);
        }

        [Fact]
        public void SomeDemoTest()
        {
            Assert.True(1 == 1);
        }

        [Fact(Skip = "Demo")]
        public void IgnoredTest()
        {
            throw new NotImplementedException();
        }

        public void GenerateDemoTitle()
        {
            var ng = new NameGenerator();
            Assert.True(ng.GenerateRandomBookTitle().Length > 0);
        }
    }
}
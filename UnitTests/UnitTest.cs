using System;
using Xunit;
using ExpleoTestApp;
namespace UnitTests
{
    public class UnitTest
    {
        [Fact] //Anv�nder mig av Xunit f�r att det �r jag mest bekant med.
        public void DetectAnagram()
        {        
            Assert.True(Anagram.IsAnagram("Ma ry","arm y"));
            Assert.True(Anagram.IsAnagram("Listen","siLent"));
            Assert.True(Anagram.IsAnagram("heart","earth"));
            Assert.False(Anagram.IsAnagram("Jaguar", "January"));
            Assert.False(Anagram.IsAnagram("Jaguar", "Raguje"));
        }

        [Fact] //Anv�nder mig av Xunit f�r att det �r jag mest bekant med.
        public void DetectAnagramWithoutLinq()
        {
            Assert.True(Anagram.IsAnagram("Ma ry", "arm y"));
            Assert.True(Anagram.IsAnagram("L isten", "si Lent"));
            Assert.True(Anagram.IsAnagram("heart", "earth"));
            Assert.False(Anagram.IsAnagram("Jaguar", "Jan uar"));
            Assert.False(Anagram.IsAnagram("Jaguar", "Raguje"));
        }

    }
}

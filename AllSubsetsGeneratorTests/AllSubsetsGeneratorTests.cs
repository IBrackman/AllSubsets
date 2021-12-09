using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AllSubsets;

namespace AllSubsetsTests
{
    [TestClass]
    public class AllSubsetsGeneratorTests
    {
        private static bool AreEqual<T>(IReadOnlyList<List<T>> expected, IReadOnlyList<List<T>> actual)
        {
            if (expected.Count != actual.Count)
                return false;

            for (var i = 0; i < expected.Count; i++)
            {
                if (expected[i].Count != actual[i].Count)
                    return false;

                for (var j = 0; j < expected[i].Count; j++)
                    if (!expected[i][j].Equals(actual[i][j]))
                        return false;
            }

            return true;
        }

        [TestMethod]
        public void AllIntSubsetsShouldBeEqualToExpected()
        {
            var allSubsetsGenerator = new AllSubsetsGenerator<int>();

            var set = new List<int>(new[] {1, 2});

            var expected = new List<List<int>>(new[]
            {
                new List<int>(new int[] { }),
                new List<int>(new[] {2}),
                new List<int>(new[] {1, 2}),
                new List<int>(new[] {1})
            });

            var actual = allSubsetsGenerator.GenerateAllSubsets(set);

            Assert.IsTrue(AreEqual(expected, actual));
        }

        [TestMethod]
        public void AllStringSubsetsShouldBeEqualToExpected()
        {
            var allSubsetsGenerator = new AllSubsetsGenerator<string>();

            var set = new List<string>(new[] {"abc", ""});

            var expected = new List<List<string>>(new[]
            {
                new List<string>(new string[] { }),
                new List<string>(new[] {""}),
                new List<string>(new[] {"abc", ""}),
                new List<string>(new[] {"abc"})
            });

            var actual = allSubsetsGenerator.GenerateAllSubsets(set);

            Assert.IsTrue(AreEqual(expected, actual));
        }

        [TestMethod]
        public void SubsetsShouldBeNotEqualToExpected()
        {
            var allSubsetsGenerator = new AllSubsetsGenerator<int>();

            var set = new List<int>(new[] { 1, 2 });

            var expected = new List<List<int>>(new[]
            {
                new List<int>(new[] {2}),
                new List<int>(new[] {1, 2}),
                new List<int>(new[] {1})
            });

            var actual = allSubsetsGenerator.GenerateAllSubsets(set);

            Assert.IsFalse(AreEqual(expected, actual));
        }

        [TestMethod]
        public void SubsetsShouldContainOnlySet()
        {
            var allSubsetsGenerator = new AllSubsetsGenerator<int>();

            var set = new List<int>();

            var expected = new List<List<int>>(new[]
            {
                new List<int>()
            });

            var actual = allSubsetsGenerator.GenerateAllSubsets(set);

            Assert.IsTrue(AreEqual(expected, actual));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void SubsetsGeneratorShouldThrowArgumentOutOfRangeException()
        {
            var allSubsetsGenerator = new AllSubsetsGenerator<int>();

            allSubsetsGenerator.GenerateAllSubsets(null);
        }
    }
}
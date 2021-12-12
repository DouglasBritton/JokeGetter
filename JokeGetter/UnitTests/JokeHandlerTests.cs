using JokeGetterCommon;
using JokeGetterCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace UnitTests
{
    /// <summary>
    /// Defines test class JokeHandlerTests.
    /// </summary>
    [TestClass]
    public class JokeHandlerTests
    {
        /// <summary>
        /// Test that recalculating the similarity scores gives the same results.
        /// </summary>
        [TestMethod]
        public void CalculateSimilarityScoresProducesConsistentResults()
        {
            JokeHandler jokeHandler = new JokeHandler(new Mock<IJokeDataSource>().Object);

            jokeHandler.Jokes.Add(new JokeDetails("Joke 1."));
            jokeHandler.Jokes.Add(new JokeDetails("Joke 2."));
            jokeHandler.Jokes.Add(new JokeDetails("Joke 3."));
            jokeHandler.Jokes.Add(new JokeDetails("Joke 4."));
            jokeHandler.Jokes.Add(new JokeDetails("Joke 5."));
            jokeHandler.CalculateSimilarityScores();

            List<JokeDetails>? expectedResults = jokeHandler.Jokes;

            jokeHandler.CalculateSimilarityScores();

            Assert.AreEqual(expectedResults, jokeHandler.Jokes);
        }
        
        /// <summary>
        /// Tests that the similarity scores are being calculated properly.
        /// </summary>
        [TestMethod]
        public void CalculateSimilarityScoresProducesValidResults()
        {
            JokeHandler jokeHandler = new JokeHandler(new Mock<IJokeDataSource>().Object);

            jokeHandler.Jokes.Add(new JokeDetails("Joke 1."));
            jokeHandler.Jokes.Add(new JokeDetails("Joke 1."));
            jokeHandler.Jokes.Add(new JokeDetails("Joke 1."));
            jokeHandler.Jokes.Add(new JokeDetails("Joke 1."));
            jokeHandler.Jokes.Add(new JokeDetails("Joke 1."));

            jokeHandler.CalculateSimilarityScores();

            for (int i = 0; i < jokeHandler.Jokes.Count; i++)
            {
                for (int j = i + 1; j < jokeHandler.Jokes.Count; j++)
                {
                    Assert.AreEqual(jokeHandler.Jokes[i].SimilarityScore, jokeHandler.Jokes[j].SimilarityScore);
                }
            }
        }

        /// <summary>
        /// Checks that the comparison works for large collections.
        /// </summary>
        [TestMethod]
        public void CalculateSimilarityScoresProducesResultsForLargeCollection ()
        {
            JokeHandler jokeHandler = new JokeHandler(new Mock<IJokeDataSource>().Object);

            for(double i = 0; i < 1000; i++)
            {
                jokeHandler.Jokes.Add(new JokeDetails("Joke " + i + "."));
            }
            jokeHandler.CalculateSimilarityScores();

            Assert.IsTrue(jokeHandler.Jokes.Count == 1000);
        }

        /// <summary>
        /// Checks that no joke's similarity score percentage exceeds 100%.
        /// </summary>
        [TestMethod]
        public void SimilarityScorePercentage()
        {
            JokeHandler jokeHandler = new JokeHandler(new Mock<IJokeDataSource>().Object);

            jokeHandler.Jokes.Add(new JokeDetails("Joke 1."));
            jokeHandler.Jokes.Add(new JokeDetails("Joke 2."));
            jokeHandler.Jokes.Add(new JokeDetails("Joke 3."));
            jokeHandler.Jokes.Add(new JokeDetails("Joke 4."));
            jokeHandler.Jokes.Add(new JokeDetails("Joke 5."));
            jokeHandler.CalculateSimilarityScores();

            double maxPercentage = 0;
            foreach(JokeDetails joke in jokeHandler.Jokes)
            {
                if(maxPercentage < joke.SimilarityScorePercentage)
                {
                    maxPercentage = joke.SimilarityScorePercentage;
                }
            }

            Assert.AreEqual(100, maxPercentage);
        }
    }
}
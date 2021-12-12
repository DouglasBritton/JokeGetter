using JokeGetterCore;
using SimMetrics.Net.Metric;

namespace JokeGetterCommon
{
    /// <summary>
    /// A class that handles jokes.
    /// </summary>
    public class JokeHandler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JokeHandler"/> class.
        /// </summary>
        public JokeHandler(IJokeDataSource jokeDataSource)
        {
            JokeDataSource = jokeDataSource;
        }

        /// <summary>
        /// The joke data source
        /// </summary>
        private IJokeDataSource JokeDataSource;

        /// <summary>
        /// Contains the stored jokes associated with this object.
        /// </summary>
        public List<JokeDetails> Jokes = new List<JokeDetails>();


        /// <summary>
        /// Generates a random joke, adds it the stored jokes and returns the new joke details.
        /// </summary>
        /// <returns>The new joke details.</returns>
        public JokeDetails GenerateRandomJoke()
        {
            JokeDetails newJokeDetails = new JokeDetails(JokeDataSource.RandomJoke);
            Jokes.Add(newJokeDetails);

            return newJokeDetails;
        }


        /// <summary>
        /// Calculates the similarity scores (absolute and percentage of maximum similarity score).
        /// </summary>
        public void CalculateSimilarityScores()
        {
            // Ensure that the similarity scores of the jokes are reset to 0.
            foreach (JokeDetails joke in Jokes)
            {
                joke.SimilarityScore = 0;
                joke.SimilarityScorePercentage = 0;
            }

            // Calculate the similarity score of the jokes.
            Levenstein levenstein = new Levenstein();
            for (int i = 0; i < Jokes.Count; i++)
            {
                for (int j = i + 1; j < Jokes.Count; j++)
                {
                    double similarity = levenstein.GetSimilarity(Jokes[i].Joke, Jokes[j].Joke);

                    Jokes[i].SimilarityScore += similarity;
                    Jokes[j].SimilarityScore += similarity;
                }
            }

            // Express the similarity score of the jokes as a percentage of the maximum joke similarity score.
            double maxSimilarityScore = MaxSimilarityScore;
            foreach (JokeDetails joke in Jokes)
            {
                joke.SimilarityScorePercentage = Math.Round( (joke.SimilarityScore / maxSimilarityScore) * 100, 0 );
            }
        }

        /// <summary>
        /// Gets the maximum similarity score.
        /// </summary>
        /// <value>The maximum similarity score.</value>
        private double MaxSimilarityScore
        {
            get
            {
                double max = 0;
                foreach (JokeDetails joke in Jokes)
                {
                    if (max < joke.SimilarityScore)
                    {
                        max = joke.SimilarityScore;
                    }
                }

                return max;
            }
        }
    }

    /// <summary>
    /// Contains the details of a joke. 
    /// </summary>
    public class JokeDetails
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JokeDetails"/> class.
        /// </summary>
        /// <param name="joke">The joke.</param>
        /// <exception cref="System.ArgumentException">Cannot create a JokeDetails object from a null joke.</exception>
        public JokeDetails(string joke)
        {
            if (joke == null)
            {
                throw new ArgumentException("Cannot create a JokeDetails object from a null joke.", joke);
            }

            Joke = joke;
        }

        /// <summary>
        /// Gets the joke.
        /// </summary>
        /// <value>The joke.</value>
        public string Joke
        { get; private set; }

        /// <summary>
        /// Gets or sets the similarity score.
        /// </summary>
        /// <value>The similarity score.</value>
        public double SimilarityScore
        { get; set; } = 0;

        /// <summary>
        /// Gets or sets the similarity score percentage.
        /// </summary>
        /// <value>The similarity score percentage.</value>
        public double SimilarityScorePercentage
        { get; set; } = 0;
    }

}
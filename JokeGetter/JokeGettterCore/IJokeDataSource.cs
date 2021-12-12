namespace JokeGetterCore
{
    /// <summary>
    /// An interface for joke data sources.
    /// </summary>
    public interface IJokeDataSource
    {
        /// <summary>
        /// Get a random joke.
        /// </summary>
        public string RandomJoke { get; }
    }
}

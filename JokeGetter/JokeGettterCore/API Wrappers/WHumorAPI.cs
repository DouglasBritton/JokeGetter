using humorapi.Model;
using com.humorapi;
using System.Configuration;

namespace JokeGetterCore
{
    /// <summary>
    /// A wrapper for the humor api implementing the required functionality.
    /// </summary>
    public class WHumorAPI : IJokeDataSource
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="WHumorAPI"/> class.
        /// </summary>
        /// <exception cref="System.Collections.Generic.KeyNotFoundException">The humor api key cannot be found.</exception>
        public WHumorAPI()
        {
            string apiKey = ConfigurationManager.AppSettings.Get("humorApiKey");
            if (apiKey == null)
            {
                throw new KeyNotFoundException("The humor api key cannot be found.");
            }

            CreateApiInstance(apiKey);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WHumorAPI"/> class.
        /// </summary>
        /// <param name="apiKey">The API key.</param>
        public WHumorAPI(string apiKey)
        {
            CreateApiInstance(apiKey);
        }

        /// <summary>
        /// Creates the API instance.
        /// </summary>
        /// <param name="apiKey">The API key.</param>
        private void CreateApiInstance(string apiKey)
        {
            Org.OpenAPITools.Client.Configuration.ApiKey.Add("api-key", apiKey);
            ApiInstance = new JokesApi();
        }

        /// <summary>
        /// The API instance
        /// </summary>
        private JokesApi ApiInstance;

        /// <summary>
        /// Get a random joke.
        /// </summary>
        /// <value>The random joke.</value>
        /// <exception cref="System.Exception">Exception when calling JokesApi.RandomJoke: " + ex.Message</exception>
        public string RandomJoke
        {
            get
            {
                try
                {
                    InlineResponse2004 result = ApiInstance.RandomJoke(null, null, null, 10 /* filter for the best rated jokes */, null);

                    return result.Joke;
                }
                catch (Exception ex)
                {
                    throw new Exception("Exception when calling HumorApi: " + ex.Message);
                }
            }
        }
    }
}

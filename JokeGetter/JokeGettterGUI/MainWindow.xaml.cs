using System;
using System.Windows;
using JokeGetterCommon;
using JokeGetterCore;
using Microsoft.VisualBasic;

namespace JokeGetterGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// The joke handler
        /// </summary>
        public JokeHandler JokeHandler;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            try
            {
                InitializeComponent();

                JokeHandler = new JokeHandler(new WHumorAPI());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "An issue has occurred");

                string apiKey = Interaction.InputBox("The Humor API instance could not be created. Please enter a HumorAPI key.", "Enter Humor API key");
                if(string.IsNullOrWhiteSpace(apiKey))
                {
                    Close();
                }

                JokeHandler = new JokeHandler(new WHumorAPI(apiKey));
            }
        }

        /// <summary>
        /// Handles the Click event of the AddRandomJoke control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void AddRandomJoke_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                JokeDataGrid.Items.Add(JokeHandler.GenerateRandomJoke());
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "An issue has occurred");
            }
        }

        /// <summary>
        /// Handles the Click event of the RefreshTable control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void RefreshTable_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                JokeDataGrid.Items.Clear();
                
                JokeHandler.CalculateSimilarityScores();
                foreach (var joke in JokeHandler.Jokes)
                {
                    JokeDataGrid.Items.Add(joke);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "An issue has occurred");
            }
        }
    }
}

using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TicTacToeWPFApplication
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Members

        /// <summary>
        ///     Holds the current results of cells in the active game
        /// </summary>
        private MarkType[] mResults;

        /// <summary>
        ///     True if player 1's turn (X). False if player 2's turn (O)
        /// </summary>
        private bool mPlayerOneTurn;

        /// <summary>
        ///     True if Game has ended
        /// </summary>
        private bool mGameEnded;

        #endregion Private Members

        #region Constructors
        /// <summary>
        ///     Default Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }
        #endregion Constructors

        /// <summary>
        ///     Starts a new game and clear's all values back to the start
        /// </summary>
        private void NewGame()
        {
            // Create new blank array of free cells
            mResults = new MarkType[9];

            // set Every position to free
            for(var i = 0; i< mResults.Length; i++)
            {
                mResults[i] = MarkType.Free;
            }

            // Make sure player 1 starts the game
            mPlayerOneTurn = true;

            // Iterate Every button on the grid
            TicTacToeBoardContainer.Children.Cast<Button>().ToList().ForEach(button => 
            {
                // Change Background, foreground and content to default values
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });

            // Make Sure the game hasn't finished
            mGameEnded = false;

            // Set Player 1's Turn
            WhoseTurnTextBlock.Foreground = Brushes.Blue;
            WhoseTurnTextBlock.Text = "Player 1's Turn";
        }


        /// <summary>
        ///     Handles a button click event
        /// </summary>
        /// <param name="sender">The button that is clicked</param>
        /// <param name="e">The event of the click</param>
        private void TicTacToeCellButton_Click(object sender, RoutedEventArgs e)
        {
            // Start a new game on the clcik after it finished
            if (mGameEnded)
            {
                NewGame();
                return;
            }

            if (mPlayerOneTurn)
            {
                WhoseTurnTextBlock.Foreground = Brushes.Red;
                WhoseTurnTextBlock.Text = "Player 2's Turn";

            }
            else
            {
                WhoseTurnTextBlock.Foreground = Brushes.Blue;
                WhoseTurnTextBlock.Text = "Player 1's Turn";
            }

            // Cast the sender to a button
            var button = (Button)sender;

            // Find the button position in the array
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            // get position from 2D array to a 1D array
            var index = column + (row * 3); // 3 because we have 3 columns

            // Don't do anything if the cell already has Nought or Cross
            if(mResults[index] != MarkType.Free)
            {
                // Nothing to do
                return;
            }
            else
            {
                // Set the cell value on which player's turn it is
                if (mPlayerOneTurn)
                {
                    mResults[index] = MarkType.Cross;
                }
                else
                {
                    mResults[index] = MarkType.Nought;
                }
                // On line Compact if else statement
                // mResults[index] = mPlayerOneTurn ? MarkType.Cross : MarkType.Nought; // Does Exactly Same as above if else does

                // set text to button result
                button.Content = mPlayerOneTurn ? "X" : "O";

                // Change Noughts to green
                if (!mPlayerOneTurn)
                {
                    button.Foreground = Brushes.Red;
                }

                // Change the player turn
                if (mPlayerOneTurn)
                {
                    mPlayerOneTurn = false;

                    // Check for Winner
                    CheckForWinner();
                }
                else
                {
                    mPlayerOneTurn = true;

                    // Check for Winner
                    CheckForWinner();
                }
                // Do the Same thing using bitwise operator. TOggle the true, false
                // i.e It will make it false if true, true if false
                // mPlayerOneTurn ^= true;

                
            }
        }

        /// <summary>
        ///     Check the winner of a three line straight
        /// </summary>
        private void CheckForWinner()
        {
            #region Horizontal Win Check

            // Check for horizontal win
            var HorizontalSame1 = (mResults[0] & mResults[1] & mResults[2]) == mResults[0]; // & (Single and) is a bitwise operator
            var HorizontalSame2 = (mResults[3] & mResults[4] & mResults[2]) == mResults[3]; // & (Single and) is a bitwise operator
            var HorizontalSame3 = (mResults[6] & mResults[7] & mResults[8]) == mResults[6]; // & (Single and) is a bitwise operator

            if(mResults[0] != MarkType.Free && HorizontalSame1)
            {
                // Game Ends
                mGameEnded = true;

                // Highlight Winning Cels
                Button_0_0.Background = Button_0_1.Background = Button_0_2.Background = Brushes.Bisque;
            }
            if(mResults[3] != MarkType.Free && HorizontalSame2)
            {
                // Game Ends
                mGameEnded = true;

                // Highlight Winning Cels
                Button_1_0.Background = Button_1_1.Background = Button_1_2.Background = Brushes.Bisque;
            }
            if(mResults[6] != MarkType.Free && HorizontalSame3)
            {
                // Game Ends
                mGameEnded = true;

                // Highlight Winning Cels
                Button_2_0.Background = Button_2_1.Background = Button_2_2.Background = Brushes.Bisque;
            }

            #endregion Horizontal Win Check


            #region Vertical Win Check

            // Check if Vertical Win
            var VerticallSame1 = (mResults[0] & mResults[3] & mResults[6]) == mResults[0];
            var VerticallSame2 = (mResults[1] & mResults[4] & mResults[7]) == mResults[1];
            var VerticallSame3 = (mResults[2] & mResults[5] & mResults[8]) == mResults[2];

            if (mResults[0] != MarkType.Free && VerticallSame1)
            {
                // Game Ends
                mGameEnded = true;

                // Highlight Winning Cels
                Button_0_0.Background = Button_1_0.Background = Button_2_0.Background = Brushes.Bisque;
                // Button_0_0.Foreground = Button_1_0.Foreground = Button_2_0.Foreground = Brushes.DarkBlue;
            }
            if (mResults[1] != MarkType.Free && VerticallSame2)
            {
                // Game Ends
                mGameEnded = true;

                // Highlight Winning Cels
                Button_0_1.Background = Button_1_1.Background = Button_2_1.Background = Brushes.Bisque;
                // Button_0_0.Foreground = Button_1_0.Foreground = Button_2_0.Foreground = Brushes.DarkBlue;
            }
            if (mResults[2] != MarkType.Free && VerticallSame3)
            {
                // Game Ends
                mGameEnded = true;

                // Highlight Winning Cels
                Button_0_2.Background = Button_1_2.Background = Button_2_2.Background = Brushes.Bisque;
                // Button_0_0.Foreground = Button_1_0.Foreground = Button_2_0.Foreground = Brushes.DarkBlue;
            }

            #endregion Vertical Win Check


            #region Diagonal Win Check

            // Check for Diagonal Win
            var DiagonalSame1 = (mResults[0] & mResults[4] & mResults[8]) == mResults[0];
            var DiagonalSame2 = (mResults[2] & mResults[4] & mResults[6]) == mResults[2];

            if (mResults[0] != MarkType.Free && DiagonalSame1)
            {
                // Game Ends
                mGameEnded = true;

                // Highlight Winning Cels
                Button_0_0.Background = Button_1_1.Background = Button_2_2.Background = Brushes.Bisque;
                // Button_0_0.Foreground = Button_1_0.Foreground = Button_2_0.Foreground = Brushes.DarkBlue;
            }
            if (mResults[2] != MarkType.Free && DiagonalSame2)
            {
                // Game Ends
                mGameEnded = true;

                // Highlight Winning Cels
                Button_0_2.Background = Button_1_1.Background = Button_2_0.Background = Brushes.Bisque;
                // Button_0_0.Foreground = Button_1_0.Foreground = Button_2_0.Foreground = Brushes.DarkBlue;
            }

            #endregion Diagonal Win Check


            #region Check For Draw

            // Check For Draw
            if (!mResults.Any(f => f == MarkType.Free))
            {
                // Game Ended with a draw
                mGameEnded = true;

                // Turn all Cells to Orange
                TicTacToeBoardContainer.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Background = Brushes.Orange;
                });
            }

            #endregion Check For Draw

        }
    }
}

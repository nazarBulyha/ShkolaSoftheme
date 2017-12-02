using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HW6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int userGuess = 0, iteration = 2, randNumberValue;
        Random rnd;

        public MainWindow()
        {
            InitializeComponent();
            rnd = new Random();
            randNumberValue = rnd.Next(1, 10);
            //randNumber.Text = randNumberValue.ToString(); //Cheat:)
        }

        private void GuessNumber(object sender, RoutedEventArgs e)
        {
            try
            {
                Int32.TryParse(userNumber.Text, out userGuess);
                if (userNumber.Text == String.Empty) throw new ArgumentException();
                if (userGuess < 1 || userGuess > 10) throw new OverflowException();
            }
            catch (ArgumentException)
            {
                MessageBox.Show($"Please try again.", "Overflow error");
                return;
            }
            catch (OverflowException)
            {
                MessageBox.Show($"Number must be only from 1 to 10, please try again.", "Overflow error");
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Exception caught");
                return;
            }

            if (iteration == 0)
            {
                var result = MessageBox.Show("Do you want to try again?", "Game over", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK) Reset();
                else Close();
            }
            else if (userGuess != randNumberValue)
            {
                MessageBox.Show($"Attempts allowed : {iteration}", "Wrong guess", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                iteration--;
            }
            else if (userGuess == randNumberValue) //check it because if iteration == 0 go first in the last check
            {
                MessageBox.Show("Congratulations!!!", "Win!", MessageBoxButton.OK, MessageBoxImage.Asterisk, MessageBoxResult.OK);
                Reset();
            }
        }

        private void Reset()
        {
            iteration = 2;
            randNumberValue = rnd.Next(1, 10);
            //randNumber.Text = randNumberValue.ToString(); //Cheat:)
        }

        void EnterClicked(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                GuessNumber(sender, e);
                e.Handled = true;
            }
        }
    }
}
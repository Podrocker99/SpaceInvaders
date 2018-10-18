using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using System.Collections;

namespace SpaceInvaders
{
    ///
    /// The purpose of this class is to initialize the window
    /// and and game play, as well as to take care of the keyboard
    /// inputs used to drive the game
    ///
    public partial class MainWindow : Window
    {
        private GamePlay gamePlay;

        public MainWindow()
        {
            InitializeComponent();
            gamePlay = new GamePlay(PlayArea, menu);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left || e.Key == Key.A)
                gamePlay.setMoveLeft(true);

            if (e.Key == Key.Right || e.Key == Key.D)
                gamePlay.setMoveRight(true);

            if (e.Key == Key.Space)
                gamePlay.fireBullet();

            if (e.Key == Key.LeftShift || e.Key == Key.Escape)
                pauseResume();

        }//end Keypress down

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left || e.Key == Key.A)
                gamePlay.setMoveLeft(false);

            if (e.Key == Key.Right || e.Key == Key.D)
                gamePlay.setMoveRight(false);

            /**
            if(e.Key == Key.Space)
            {
                fire = true;
            }
            **/
        }//end Keypress up

        private void btn_start_Click(object sender, RoutedEventArgs e)
        {
            pauseResume();
        }

        private void pauseResume()
        {   
            if (gamePlay.player == null)
                gamePlay.initiaizeGame();

            gamePlay.toggleTimers();
        }

        private void btn_exit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Now closing Space Invaders!", "Exiting Application");
            Environment.Exit(0);
        }

        private void mnuAboutInfo_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Authors: Mark Stepchin and Tarren Selig\r\n" +
                "Version: 1.0.0\r\n" +
                "Creation Kit: Visual Studio 2017\r\n" +
                ".NET Framework: 4.6\r\n" +
                "Format: 64x", "Information");
        }

        private void mnuGameplayStartStop_Click(object sender, RoutedEventArgs e)
        {
            pauseResume();
        }

        private void mnuGameplayControls_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Left Shift: Pause/Resume\r\n" +
                "Left Arrow/A Key: Move Left\r\n" +
                "Right Arrow/D Key: Move Right\r\n" +
                "Space Bar: Fire Ship's Guns", "Controls");
        }

        private void mnuGameplayPoints_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bottom Aliens: 10pts\r\n" +
                "Middle Aliens: 20pts\r\n" +
                "Top Aliens: 30pts\r\n" +
                "Mothership: 100pts", "Points System");
        }

        private void btn_highScores_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("This feature is not quite yet implimented! Sorry for the inconvenience", "Apologies");
        }
    }//end Window
}//end namespace
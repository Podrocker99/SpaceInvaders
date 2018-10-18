using System;
using System.Windows;
using System.Collections;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SpaceInvaders
{
    class GamePlay
    {
        #region Global Variables
        private DispatcherTimer movePlayer = new DispatcherTimer();
        private DispatcherTimer moveAliens = new DispatcherTimer();
        private int alien_direction;
        private Random rng = new Random();

        private ArrayList bullets = new ArrayList();
        private ArrayList aBullets = new ArrayList();
        private ArrayList segments;
        private ArrayList aliens;
        private ArrayList motherships = new ArrayList();
        public Player player;

        private int fireRate = 3000;
        private bool fire = true;

        private double scoreVal = 0;
        private double level = 1;

        private Label lblScore;
        private Label lblLives;
        private Label lblLevel;

        private const int INITIAL_PLAYER_LIVES = 3;
        private int playerLives;

        private Canvas PlayArea;
        private StackPanel menu;

        #endregion
        #region Setters
        public void setMoveLeft(bool moveLeft)
        {
            if (movePlayer.IsEnabled == true) { player.setMoveLeft(moveLeft); }

            else { return; }
        }
        public void setMoveRight(bool moveRight)
        {
            if (movePlayer.IsEnabled == true) { player.setMoveRight(moveRight); }

            else { return; }
        }
        #endregion

        public GamePlay(Canvas PlayArea, StackPanel menu)
        {
            this.PlayArea = PlayArea;
            this.menu = menu;

            movePlayer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            movePlayer.Tick += MovePlayer_Tick;
            movePlayer.Tick += MoveBullet_Tick;

            moveAliens.Interval = new TimeSpan(0, 0, 0, 0, 25);
            moveAliens.Tick += MoveAlien_Tick;
            moveAliens.Tick += MoveAlienBullet_Tick;
            moveAliens.Tick += MoveMotherShip_Tick;
            alien_direction = -1;
        }//end GamePlay

        public void initiaizeGame()
        {
            PlayArea.Children.Clear();

            this.playerLives = INITIAL_PLAYER_LIVES;

            GameSetup gameSetup = new GameSetup(PlayArea);
            
            this.lblScore = new Label {
                Content = "Score:   " + scoreVal,
                Width = 500,
                Height = 30,
                FontSize = 16,
                FontWeight = FontWeights.Bold,
                Foreground = Brushes.LimeGreen,
                Margin = new Thickness(0, 0, 60, 30),
            };

            this.lblLives = new Label
            {
                Content = "Lives:   " + playerLives,
                Width = 100,
                Height = 30,
                FontSize = 16,
                FontWeight = FontWeights.Bold,
                Foreground = Brushes.LimeGreen,
                Margin = new Thickness(PlayArea.Width - 160, 0, PlayArea.Width - 110, 30)
            };

            this.lblLevel = new Label
            {
                Content = "Level:   " + level,
                Width = 100,
                Height = 30,
                FontSize = 16,
                FontWeight = FontWeights.Bold,
                Foreground = Brushes.LimeGreen,
                Margin = new Thickness(PlayArea.Width/2 - 50, 0, PlayArea.Width/2 + 50, 30),
            };

            PlayArea.Children.Add(lblScore);
            PlayArea.Children.Add(lblLives);
            PlayArea.Children.Add(lblLevel);
            
            this.segments = gameSetup.buildShields();
            this.aliens = gameSetup.createAliens();
            this.player = new Player(PlayArea);
        }//end initializeGame

        public void nextLevel()
        {
            GameSetup gameSetup = new GameSetup(PlayArea);

            if (playerLives <= 3)
            {
                ++playerLives;
                lblLives.Content = "Lives:   " + playerLives;
            }

            this.fireRate -= 200;
            this.aliens = gameSetup.createAliens();

            
        }

        private void MovePlayer_Tick(object sender, EventArgs e)
        {
            player.movePlayer();

            if(aliens.Count == 0)
            {
                nextLevel();
            }
        }

        private void MoveAlien_Tick(object sender, EventArgs e)
        {
            bool dropDown = false;

            if (changeDirection())
            {
                this.alien_direction *= -1; //change direction
                dropDown = true;            //drop down 1 row
            }

            //tell all aliens to move
            foreach (Alien a in this.aliens)
            {
                int shoot = rng.Next(0,fireRate);
                a.moveAlien(alien_direction, dropDown);
                AlienShootBullet(a, shoot);
            }
        }//end MoveAlien timer 

        private void MoveMotherShip_Tick(object sender, EventArgs e)
        {
            ArrayList mShipToRemove = new ArrayList();

            if (motherships.Count == 0)
            {
                GameSetup gameSetup = new GameSetup(PlayArea);
                int send = rng.Next(0, 1000);

                if (send >= 999)
                {
                    this.motherships = gameSetup.createMotherships();
                }//end send condition
            }//end if no motherships

            if(motherships.Count > 0)
            {
                foreach(Alien mothership in motherships)
                {
                    if (mothership.reachedWall() == false)
                    {
                        mothership.moveMothership();
                    }//end mothership movement

                    else
                    {
                        PlayArea.Children.Remove(mothership.image);
                        mShipToRemove.Add(mothership);
                    }//end if mothership has hit the right wall

                }//end move mothership
            }//end if mothership is spawned

            foreach (Alien mothership in mShipToRemove)
                this.motherships.Remove(mothership);

        }//end MoveMothership_Tick

        public void AlienShootBullet(Alien a, int shoot)
        {
            if (shoot >= fireRate-1)
            {
                alienBullet bullet = a.shootBullet();
                aBullets.Add(bullet);

                shoot = 0;
            }//end if
        }//end AlienShootBullet

        private bool changeDirection()
        {
            foreach (Alien a in this.aliens)
                if (a.reachedWall())
                    return true;

            return false;
        }//end changeDirection

        private void MoveBullet_Tick(object sender, EventArgs e)
        {
            if (bullets.Count <= 0)   // no bullets to move
                return;

            ArrayList bulletsToRemove = new ArrayList();
            ArrayList segsToRemove    = new ArrayList();
            ArrayList aliensToRemove  = new ArrayList();

            foreach (Bullet bullet in this.bullets)
            {
                if (bullet.reachedTop())
                {
                    bullet.removeFromCanvas();
                    bulletsToRemove.Add(bullet);
                    fire = true;
           
                }//end if bullet hits top of play area

                foreach (Segment segment in segments)
                {
                    if (bullet.top <= segment.top + segment.seg.Width
                        && bullet.left + bullet.b.Width >= segment.left
                        && bullet.left <= segment.left + segment.seg.Width)
                    {
                        if (segment.life > 0)
                        {
                            PlayArea.Children.Remove(bullet.b);
                            bulletsToRemove.Add(bullet);
                            segment.seg.Opacity *= (.75);
                            segment.life = segment.life - 1;
                        }//if the segment still has life left
                        else
                        {
                            PlayArea.Children.Remove(bullet.b);
                            bulletsToRemove.Add(bullet);
                            segsToRemove.Add(segment);
                            PlayArea.Children.Remove(segment.seg);
                        } //if the segment runs out of life

                        fire = true;
                    }
                }//end bullet hitting shield segment

                foreach (Alien alien in this.aliens)
                {
                    if (bullet.top <= alien.top + alien.image.Width
                        && bullet.left + bullet.b.Width >= alien.left
                        && bullet.left <= alien.left + alien.image.Width)
                    {
                        PlayArea.Children.Remove(bullet.b);
                        PlayArea.Children.Remove(alien.image);

                        bulletsToRemove.Add(bullet);
                        aliensToRemove.Add(alien);

                        fire = true;

                        scoreVal += alien.score;
                        lblScore.Content = "Score:   " + scoreVal;
                    }//end if bullet hits alien
                }//end bullet hitting alien

                foreach (Alien mothership in this.motherships)
                {
                    if (bullet.top <= mothership.top + mothership.image.Width
                        && bullet.left + bullet.b.Width >= mothership.left
                        && bullet.left <= mothership.left + mothership.image.Width)
                    {
                        PlayArea.Children.Remove(bullet.b);
                        PlayArea.Children.Remove(mothership.image);

                        bulletsToRemove.Add(bullet);
                        aliensToRemove.Add(mothership);

                        fire = true;

                        scoreVal += mothership.score;
                        lblScore.Content = "Score:   " + scoreVal;
                    }//end if bullet hits mothership
                }//end bullet hitting mothership

                bullet.moveBullet();

            }//end bullet collision detection

            foreach (Bullet bullet in bulletsToRemove)
                this.bullets.Remove(bullet);

            foreach (Segment seg in segsToRemove)
                this.segments.Remove(seg);

            foreach (Alien alien in aliensToRemove)
                this.aliens.Remove(alien);

        }//end MoveBullet_Tick

        public void fireBullet()
        {
            if (movePlayer.IsEnabled == true)
            {
                if (fire)
                {
                    Bullet bullet = player.shootBullet();
                    bullets.Add(bullet);

                    fire = false;
                }
            }

            else { return; }
        }//end fireBullet

        private void MoveAlienBullet_Tick(object sender, EventArgs e)
        {
            if (aBullets.Count <= 0)   // no bullets to move
                return;

            ArrayList alienBulletsToRemove = new ArrayList();
            ArrayList segsToRemove = new ArrayList();

            bool removeAlienBullets = false;

            foreach (alienBullet bullet in this.aBullets)
            {
                foreach (Segment segment in segments)
                {
                    if (bullet.top + bullet.HEIGHT >= segment.top
                        && bullet.left + bullet.b.Width >= segment.left
                        && bullet.left <= segment.left + segment.seg.Width)
                    {
                        if (segment.life > 0)
                        {
                            PlayArea.Children.Remove(bullet.b);
                            alienBulletsToRemove.Add(bullet);
                            segment.seg.Opacity *= (.75);
                            segment.life = segment.life - 1;
                        }//if the segment still has life left
                        else
                        {
                            PlayArea.Children.Remove(bullet.b);
                            alienBulletsToRemove.Add(bullet);
                            segsToRemove.Add(segment);
                            PlayArea.Children.Remove(segment.seg);
                        } //if the segment runs out of life
                    }
                }//end bullet hitting shield segment

                //checking if alien bullet hit the player
                if (bullet.top + bullet.HEIGHT >= player.top
                        && bullet.left + bullet.b.Width >= player.left//checking right side
                        && bullet.left <= player.left + player.image.Width) // checking left side
                {
                    if (playerLives > 0)
                    {
                        player.setPlayerMiddle();
                        --this.playerLives;
                        lblLives.Content = "Lives:   " + this.playerLives;
                        removeAlienBullets = true;

                        PlayArea.Children.Remove(bullet.b);
                        alienBulletsToRemove.Add(bullet);
                    }
                    else
                        restartGame();

                }//end if

                if (bullet.reachedBottom())
                {
                    bullet.removeFromCanvas();
                    alienBulletsToRemove.Add(bullet);
                }//end if bullet hits top of play area

                bullet.moveABullet();
            }//end foreach loop

            if (removeAlienBullets)
            {
                foreach (alienBullet a in this.aBullets)
                    a.removeFromCanvas();
                this.aBullets.Clear();
            }

            foreach (alienBullet bullet in alienBulletsToRemove)
                this.aBullets.Remove(bullet);

            foreach (Segment seg in segsToRemove)
                this.segments.Remove(seg);

        }//end MoveABullet_Tick

        private void restartGame()
        {
            toggleTimers();
            //if one of top scores, prompt for user to enter their name
            
            initiaizeGame();
        }//end restartGame

        public void toggleTimers()
        {
            if (movePlayer.IsEnabled == true)
            {
                menu.Visibility = Visibility.Visible;

                movePlayer.IsEnabled = false;
                moveAliens.IsEnabled = false;

            }//end pause game
            else
            {
                menu.Visibility = Visibility.Collapsed;

                movePlayer.IsEnabled = true;
                moveAliens.IsEnabled = true;

            }//end start game
        }//end toggleTimers
    }
}

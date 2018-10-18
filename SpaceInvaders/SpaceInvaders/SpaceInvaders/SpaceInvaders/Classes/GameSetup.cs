using System.Windows.Controls;
using System.Collections;
using System;
using System.Windows.Media.Imaging;
using System.Windows;

namespace SpaceInvaders
{
    class GameSetup
    {
        private Canvas PlayArea;
        private const int NUM_SHIELDS = 4;
        private const int SHIELD_HEIGHT = 600 - 75;

        public GameSetup(Canvas PlayArea)
        {
            this.PlayArea = PlayArea;
        }

        public ArrayList buildShields()
        {
            ArrayList segments = new ArrayList();

            double left;
            int livesPerSegment = 3;

            double offset = .15;

            for (int i = 0; i < NUM_SHIELDS; i++)
            {
                left = PlayArea.Width * offset;

                Shield shield = new Shield(PlayArea);
                shield.buildShield(SHIELD_HEIGHT, left, livesPerSegment);
                addSegments(shield, segments);

                offset += .2;
            }

            return segments;
        }

        private void addSegments(Shield shield, ArrayList segments)
        {
            foreach (Segment s in shield.getSegments())
                segments.Add(s);
        }

        public ArrayList createAliens()
        {
            ArrayList aliens = new ArrayList();

            int i = 18;
            double bottom = PlayArea.Height / 2.25;
            double Leftl1 = PlayArea.Width - 180;
            double seperation = 0;

            while (i >= 0)
            {
                Alien a = new Alien(1, bottom, Leftl1 - seperation, PlayArea);
                Alien a2 = new Alien(1, a.top - a.WIDTH, Leftl1 - seperation, PlayArea);
                Alien b = new Alien(2, a2.top - a.WIDTH, Leftl1 - seperation, PlayArea);
                Alien b2 = new Alien(2, b.top - a.WIDTH, Leftl1 - seperation, PlayArea);
                Alien c = new Alien(3, b2.top - a.WIDTH, Leftl1 - seperation, PlayArea);
                Alien c2 = new Alien(3, c.top - a.WIDTH, Leftl1 - seperation, PlayArea);

                fillAliens(a, aliens);
                fillAliens(a2, aliens);
                fillAliens(b, aliens);
                fillAliens(b2, aliens);
                fillAliens(c, aliens);
                fillAliens(c2, aliens);

                seperation += 40;
                --i;
            }

            return aliens;

        }//end createAliens

        private void fillAliens(Alien a, ArrayList aliens)
        {
            PlayArea.Children.Add(a.image);
            Canvas.SetTop(a.image, a.top);
            Canvas.SetLeft(a.image, a.left);
            aliens.Add(a);
        }//end fill aliens array

        public ArrayList createMotherships()
        {
            ArrayList motherships = new ArrayList();

            Alien m = new Alien(0, 45, 1, PlayArea);

            PlayArea.Children.Add(m.image);
            Canvas.SetTop(m.image, m.top);
            Canvas.SetLeft(m.image, m.left);
            motherships.Add(m);

            return motherships;
        }//end createMotherships

        public Image setupLives(double left)
        {
            Image life = new Image
            {
                Width = 30,
                Height = 30,
                Source = new BitmapImage(new Uri(@".\Resources\Player.png")),
                Margin = new Thickness(PlayArea.Width - left, 0, PlayArea.Width - (left + 30), 30),
            };

            return life;
        }
    }
}

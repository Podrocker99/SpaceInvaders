using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;

namespace SpaceInvaders
{
    class Bullet
    {
        private Canvas PlayArea;

        public Rectangle b;
        public double top;
        public double left;

        private const int bdy = 4;

        public Bullet(Canvas PlayArea, double top, double left)
        {
            this.b = new Rectangle()
            {
                Width = 3,
                Height = 25,
                Fill = Brushes.DarkRed,
                Stroke = Brushes.Red,
                StrokeThickness = 1,
            };
            this.top = top;
            this.left = left;

            this.PlayArea = PlayArea;

            PlayArea.Children.Add(b);
            Canvas.SetTop(b, top);
            Canvas.SetLeft(b, left);
        }

        public void moveBullet()
        {
            top -= bdy;
            Canvas.SetTop(b, top);
        }

        public bool reachedTop()
        {
            return top <= 0;
        }
        public void removeFromCanvas()
        {
            PlayArea.Children.Remove(b);
        }
    }
}

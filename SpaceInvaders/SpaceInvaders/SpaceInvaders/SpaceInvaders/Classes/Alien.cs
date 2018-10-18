using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SpaceInvaders
{
    class Alien
    {
        private const string alien1 = @".\Resources\Alien1.png";
        private const string alien2 = @".\Resources\Alien2.png";
        private const string alien3 = @".\Resources\Alien3.png";
        private const string motherShip = @".\Resources\Mothership.png";

        public Image image;
        public double top;
        public double left;
        private double dy = 40;
        private double dx = 3;

        public double score;

        public int WIDTH = 40;

        private Canvas PlayArea;


        public Alien(int type, double top, double left, Canvas PlayArea)
        {
            string uri = chooseUri(type);
            this.image = generateImage(uri);

            this.top  = top;
            this.left = left;

            this.dy = WIDTH/2;
            this.dx = 1;

            this.PlayArea = PlayArea;
        }
    
        public bool reachedWall()
        {
            return left <= 0 || left >= PlayArea.Width - image.Width;
        }

        public void moveAlien(int direction, bool goDown)
        {
            if (goDown) this.top += dy;
            this.left += dx*direction;

            Canvas.SetLeft(this.image, this.left);
            Canvas.SetTop(this.image, this.top);
        }

        public void moveMothership()
        {
            this.left += dx*3;

            Canvas.SetLeft(this.image, this.left);
            Canvas.SetTop(this.image, this.top);
        }

        private string chooseUri(int type)
        {
            switch (type)
            {
                case 1:
                    score = 10;
                    return alien1;
                case 2:
                    score = 20;
                    return alien2;
                case 3:
                    score = 30;
                    return alien3;
                default:
                    score = 100;
                    return motherShip;
            }
        }

        private Image generateImage(string uri)
        {
            Image i = new Image();
            i.Width = WIDTH;

            BitmapImage myBitmapImage = new BitmapImage();     // creating the source
            myBitmapImage.BeginInit();                         // BitmapImage.UriSource must be in a BeginInit/EndInit block

            myBitmapImage.UriSource = new Uri(uri, UriKind.Relative);

            /*  To save significant application memory, set the DecodePixelWidth or  
            DecodePixelHeight of the BitmapImage value of the image source to the desired 
            height or width of the rendered image. If you don't do this, the application will 
            cache the image as though it were rendered as its normal size rather then just 
            the size that is displayed.

            Note: In order to preserve aspect ratio, set DecodePixelWidth
            or DecodePixelHeight but not both.  */
            myBitmapImage.DecodePixelWidth = WIDTH;
            myBitmapImage.EndInit();

            i.Source = myBitmapImage;

            return i;
        }

        public alienBullet shootBullet()
        {
            double middle = left + (WIDTH / 2);
            double bottom = top + WIDTH;
            return new alienBullet(PlayArea, bottom, middle);
        }

 
    }//and Alien Class

    class alienBullet
    {
        private Canvas PlayArea;

        public Rectangle b;
        public double top;
        public double left;

        public int HEIGHT = 25;

        private const int bdy = 4;

        public alienBullet(Canvas PlayArea, double top, double left)
        {
            this.b = new Rectangle()
            {
                Width = 3,
                Height = HEIGHT,
                Fill = Brushes.DarkGreen,
                Stroke = Brushes.LightGreen,
                StrokeThickness = 1,
            };

            this.top = top;
            this.left = left;

            this.PlayArea = PlayArea;

            PlayArea.Children.Add(b);
            Canvas.SetTop(b, top);
            Canvas.SetLeft(b, left);
        }//end EVC alienBullet

        public void moveABullet()
        {
            top += bdy;
            Canvas.SetTop(b, top);
        }//end moveABullet

        public bool reachedBottom()
        {
            return top + this.b.Height >= PlayArea.Height;
        }//end reachedBottom

        public void removeFromCanvas()
        {
            PlayArea.Children.Remove(b);
        }//end removeFromCanvas

    }//end alienBullet Class
}//end namespace

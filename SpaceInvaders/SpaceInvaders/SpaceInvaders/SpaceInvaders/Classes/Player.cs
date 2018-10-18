using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace SpaceInvaders
{
    class Player
    {
        private const string playerURI = @".\Resources\Player.png";
        private const int WIDTH = 75;

        private const int START_TOP = 610;
        private const int START_LEFT = 484;

        public Image image;

        private double pdx = 5;

        public double top;
        public double left;

        private Canvas PlayArea;

        private bool moveLeft;
        private bool moveRight;

        #region Setters
        public void setMoveLeft(bool moveLeft)
        {
            this.moveLeft = moveLeft;
        }
        public void setMoveRight(bool moveRight)
        {
            this.moveRight = moveRight;
        }

        #endregion

        public Player(Canvas PlayArea)
        {
            image = generateImage(playerURI);

            this.PlayArea = PlayArea;
            PlayArea.Children.Add(image);

            moveLeft = false;
            moveRight = false;

            setPlayerMiddle();
        }

        public void setPlayerMiddle()
        {
            this.top = START_TOP;
            this.left = START_LEFT;
            
            Canvas.SetTop(image, top);
            Canvas.SetLeft(image, left);
        }

        public void movePlayer()
        {
            if (moveLeft)
                if (left - pdx >= 0)
                    left -= pdx;

            if (moveRight)
                if (left + WIDTH + pdx <= PlayArea.Width)
                    left += pdx;

            Canvas.SetTop(image, top);
            Canvas.SetLeft(image, left);
        }

        public Bullet shootBullet()
        {
            double middle = left + (WIDTH / 2);
            return new Bullet(PlayArea, top, middle);
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
    }
}

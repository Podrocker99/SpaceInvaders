using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

using System.Windows.Controls;

namespace SpaceInvaders
{ 
    class Shield
    {
        private Canvas PlayArea;
        private ArrayList segments;
        public ArrayList getSegments()
        {
            return this.segments;
        }

        private const int SEGMENT_WIDTH = 15;
        private const int SEGMENT_HEIGHT = 15;

        private const int NUM_COLS = 6;
        private const int NUM_ROWS = 4;

        public Shield(Canvas PlayArea)
        {
            this.PlayArea = PlayArea;
            this.segments = new ArrayList();
        }

        /*
          this method knows how to build a shield out of segments 
          given the top left coordinate
             
          s s s s s s
          s s s s s s
          s s     s s
          s s     s s
        */
        public void buildShield(double top, double left, int livesPerSegment)
        {
            for (int row = 0; row < NUM_ROWS; row++)
                for (int col = 0; col < NUM_COLS; col++)
                    if (!(                            //not
                        (row >= 2) &&                 //row 3 or 4
                        (col == 2 || col == 3)        //and col 3 or 4
                        ))
                    {
                        double t = top + row * SEGMENT_HEIGHT;
                        double l = left + col * SEGMENT_WIDTH;

                        fillShields(new Segment(t, l, livesPerSegment));
                    }
        }

        private void fillShields(Segment segment)
        {
            this.segments.Add(segment);
            PlayArea.Children.Add(segment.seg);
            Canvas.SetTop(segment.seg, segment.top);
            Canvas.SetLeft(segment.seg, segment.left);
        }//end fill shield array
    }

    class Segment
    {
        public Rectangle seg;
        public double top;
        public double left;
        public double life;

        public const int WIDTH  = 15;
        private const int HEIGHT = 15;

        public Segment(double top, double left, double life)
        {
            this.seg = new Rectangle()
            {
                Width = WIDTH,
                Height = HEIGHT,
                Fill = Brushes.GreenYellow,
                Opacity = 1,
            };
            this.top = top;
            this.left = left;
            this.life = life;
        }//end Segment EVC

    }//end Segment Class

}//end namespace

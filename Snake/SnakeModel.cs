using System;
using System.Collections.Generic;
using System.Text;

namespace Snake
{
    public class SnakeModel
    {
        //public  DisplayPosition[] body  { get; set; }
        public List<DisplayPosition> body { get; set; }

        public int lenght { get; set; }


        public SnakeModel()
        {
            // DisplayPosition[] body = new DisplayPosition[100] ;

            body = new List<DisplayPosition>();
        }

        //public void AddBody(int i, int x, int y)
        //{
        //    this.body[i].x = x;
        //    this.body[i].y = y;

        //}

        public void AddBody(int x, int y)
        {
            DisplayPosition displayPosition = new DisplayPosition();
            displayPosition.x = x;
            displayPosition.y = y;
            this.body.Add(displayPosition);
        }
    }
}

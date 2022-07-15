using System;
using System.Collections.Generic;
using System.Text;

namespace Snake
{
    public class SnakeModel
    {
        public  DisplayPosition[] body  { get; set; }
        public int lenght { get; set; }


        public SnakeModel()
        {
            DisplayPosition[] body = new DisplayPosition[100] ;
        }

        public void AddBody(int i, int x, int y)
        {
            this.body[i].x = x;
            this.body[i].y = y;
            
        }
    }
}

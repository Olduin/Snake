using System;
using System.Collections.Generic;
using System.Text;

namespace Snake
{
    public class FoodModel
    {
        public virtual DisplayPosition position { get; set; }
        
        public FoodModel()
        {
            this.position = new DisplayPosition();
        }
    }

    


}

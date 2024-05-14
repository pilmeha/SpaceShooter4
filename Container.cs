using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter3
{
    internal class Container
    {
        public Line Width { get; set; }
        public Line Height { get; set; }
        public Container(Line width, Line height) 
        {
            Width = width;
            Height = height;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Aircraft.Enums;

namespace Aircraft
{
    public abstract class BaseObj
    {
        public BaseObj(int width, int height, Size bounds) {
            this.Bounds = new Rectangle(new Point(0, 0), bounds);
            Size = new Size(width, height);
        }

        public virtual UInt16 Unit { get { return 2; } }
        private Point location;
        public Point Location
        {
            get
            {
                return Rec.Location;
            }
            set
            {
                location = value;
            }
        }
        private Size size;
        public Size Size
        {
            get
            {
                return this.Rec.Size;
            }
            set {
                size = value;
            }
        }
        public Rectangle Rec
        {
            get { return new Rectangle(location, size); }
        }

        public Rectangle Bounds { get; private set; }
        
        public abstract void Move(Direction direction);

        public abstract void Draw(Graphics g);

        public virtual bool CheckInBounds() {
            return Rec.IntersectsWith(this.Bounds);
        }
    }
}

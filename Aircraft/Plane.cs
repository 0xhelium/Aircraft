using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircraft
{
    public class Plane : BaseObj
    {
        public Plane(int width, int height, Size bounds) : base(width, height, bounds)
        {
        }

        public bool IsMoveing { get; set; }

        public Enums.Direction Diretion { get; set; }
        
        private static Image img = new Bitmap("resource/S-37-Berkut.png");
        
        public override ushort Unit
        {
            get
            {
                return 5;
            }
        }

        public bool IsDead { get; set; }

        public override void Draw(Graphics g)
        {
            g.DrawImage(img, new Point(
            this.Location.X - (img.Size.Width - this.Rec.Size.Width) / 2,
            this.Location.Y - (img.Size.Height - this.Rec.Size.Height) / 2));
        }

        public override void Move(Enums.Direction direction)
        {
            Point p = new Point();
            #region -SWITCH-
            switch (direction)
            {
                case Enums.Direction.UP:
                    p = new Point(this.Location.X, this.Location.Y - Unit);
                    break;
                case Enums.Direction.DOWN:
                    p = new Point(this.Location.X, this.Location.Y + Unit);
                    break;
                case Enums.Direction.LEFT:
                    p = new Point(this.Location.X - Unit, this.Location.Y);
                    break;
                case Enums.Direction.RIGHT:
                    p = new Point(this.Location.X + Unit, this.Location.Y);
                    break;
                case Enums.Direction.UP | Enums.Direction.LEFT:
                    p = new Point(this.Location.X - Unit, this.Location.Y - Unit);
                    break;
                case Enums.Direction.UP | Enums.Direction.RIGHT:
                    p = new Point(this.Location.X + Unit, this.Location.Y - Unit);
                    break;
                case Enums.Direction.DOWN | Enums.Direction.LEFT:
                    p = new Point(this.Location.X - Unit, this.Location.Y + Unit);
                    break;
                case Enums.Direction.DOWN | Enums.Direction.RIGHT:
                    p = new Point(this.Location.X + Unit, this.Location.Y + Unit);
                    break;
            }
            #endregion
            this.Location = p;
        }

        public override bool CheckInBounds()
        {
            return Rec.Top >= this.Bounds.Top &&
               Rec.Left >= this.Bounds.Left &&
               Rec.Right <= this.Bounds.Right &&
               Rec.Bottom <= this.Bounds.Bottom;
        }

        public virtual void Shoot(Bullet bullet)
        {
            bullet.Direction = Enums.Direction.UP;
            bullet.Location = new Point(
                this.Location.X + (int)(this.Rec.Width / 2d - bullet.Size.Width / 2d),
                this.Location.Y);
        }
    }
}

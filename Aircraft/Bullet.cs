using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircraft
{
    public class Bullet : BaseObj
    {
        public Bullet(int width, int height, Size bounds) : base(width, height, bounds)
        {
        }

        private static Image img = new Bitmap("resource/bullets/0.png");
        private static Image img2 = new Bitmap("resource/bullets/5.png");

        public Enums.Direction Direction { get; set; }

        public bool IsMine { get; set; }

        public override ushort Unit
        {
            get
            {
                return 6;
            }
        }

        public override void Draw(Graphics g)
        {
            //g.DrawRectangle(new Pen(Color.Red), this.Rec);
            g.DrawImage(IsMine ? img2 : img, new Point(
                this.Location.X - (img.Size.Width - this.Rec.Size.Width) / 2,
                this.Location.Y - (img.Size.Height - this.Rec.Size.Height) / 2));
        }

        public override void Move(Enums.Direction direction)
        {
            direction = Direction;
            if (direction == Enums.Direction.DOWN)
            {
                this.Location = new Point(this.Location.X, this.Location.Y + Unit);
            }
            if (direction == Enums.Direction.UP)
            {
                this.Location = new Point(this.Location.X, this.Location.Y - Unit);
            }
        }
    }
}

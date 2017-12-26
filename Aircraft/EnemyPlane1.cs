using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircraft
{
    public class EnemyPlane1 : Plane
    {
        public EnemyPlane1(int width, int height, Size bounds) : base(width, height, bounds)
        {
        }
        static Image img = new Bitmap("resource/MiG-MFI.png");
        static Image imgDie = new Bitmap("resource/die.png");
        public override ushort Unit
        {
            get
            {
                return 2;
            }
        }

        public override void Draw(Graphics g)
        {
            if (IsDead)
            {
                g.DrawImage(imgDie,
                    new Point(
                        this.Location.X - (imgDie.Size.Width - this.Rec.Size.Width) / 2 - 15,
                        this.Location.Y - (imgDie.Size.Height - this.Rec.Size.Height) / 2 - 15));
            }
            g.DrawImage(img, new Point(
                this.Location.X - (img.Size.Width - this.Rec.Size.Width) / 2,
                this.Location.Y - (img.Size.Height - this.Rec.Size.Height) / 2));
        }

        public override void Move(Enums.Direction direction)
        {
            base.Move(direction);
        }

        public override void Shoot(Bullet bullet)
        {
            bullet.Location = new Point(
                this.Location.X + (int)(this.Rec.Width / 2d - bullet.Size.Width / 2d),
                this.Location.Y + this.Rec.Height);
            bullet.Direction = Enums.Direction.DOWN;
        }
    }
}

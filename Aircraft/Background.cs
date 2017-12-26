using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircraft
{
    public class Background : BaseObj
    {
        public Background(int width, int height, Size bounds) : base(width, height, bounds)
        {
        }

        static Image img = new Bitmap("resource/bg.png");

        public override ushort Unit
        {
            get { return 1; }
        }

        public override void Draw(Graphics g)
        {
            g.DrawImage(img, this.Location);
        }

        public override void Move(Enums.Direction direction)
        {
            var p = new Point(this.Location.X, this.Location.Y + Unit);
            this.Location = p;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircraft
{
    public class Bg : BaseObj
    {
        public Bg(int width, int height, Size bounds) : base(width, height, bounds)
        {
        }

        static Image Bg1 = new Bitmap("resource/bg/22.png");
        static Image Bg2 = new Bitmap("resource/bg/23.png");
        static Image Bg3 = new Bitmap("resource/bg/24.png");

        public override ushort Unit
        {
            get
            {
                return 2;
            }
        }

        public int BgType { get; set; }

        public override void Draw(Graphics g)
        {
            g.DrawImage(BgType == 0 ? Bg1 : (BgType == 1 ? Bg2 : Bg3), this.Location);
        }

        public override void Move(Enums.Direction direction)
        {
            var p = new Point(this.Location.X, this.Location.Y + Unit);
            this.Location = p;
        }
    }
}

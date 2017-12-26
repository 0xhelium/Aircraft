using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aircraft
{
    public class GameController
    {
        Panel _container;
        static object _root = new object();
        IList<BaseObj> Enemies = new List<BaseObj>();
        IList<BaseObj> Bullets = new List<BaseObj>();
        IList<BaseObj> Bgs = new List<BaseObj>();
        IList<BaseObj> Backounds = new List<BaseObj>();
        BaseObj Self;
        List<Keys> DownKeys = new List<Keys>();
        bool IsMoving
        {
            get { return DownKeys.Any(); }
        }
        Enums.Direction Direction
        {
            get
            {
                Enums.Direction dir = 0;
                if (DownKeys.Any())
                {
                    if (DownKeys.Contains(Keys.Up))
                    {
                        dir = dir | Enums.Direction.UP;
                    }
                    if (DownKeys.Contains(Keys.Down))
                    {
                        dir = dir | Enums.Direction.DOWN;
                    }
                    if (DownKeys.Contains(Keys.Left))
                    {
                        dir = dir | Enums.Direction.LEFT;
                    }
                    if (DownKeys.Contains(Keys.Right))
                    {
                        dir = dir | Enums.Direction.RIGHT;
                    }
                }
                return dir;
            }
        }



        public GameController(Panel container)
        {
            _container = container;
        }

        public void GenerateEnemy()
        {
            try
            {
                EnemyPlane1 enemy = new EnemyPlane1(50, 60, this._container.Size);
                var d = new Random().Next(100);
                if (d % 3 == 0)
                    enemy.Location = new Point(enemy.Bounds.Width - enemy.Rec.Width - (new Random().Next((enemy.Bounds.Width - enemy.Rec.Width) / 2)), 0);
                if (d % 3 == 1)
                    enemy.Location = new Point((enemy.Bounds.Width - enemy.Rec.Width) / 2 - (new Random().Next((enemy.Bounds.Width - enemy.Rec.Width) / 2)), 0);
                if (d % 3 == 2)
                    enemy.Location = new Point((new Random().Next((enemy.Bounds.Width - enemy.Rec.Width) / 2)), 0);
                Enemies.Add(enemy);
            }
            catch { }
        }

        public void GenerateBg()
        {
            Bg bg = new Bg(130, 130, this._container.Size);
            bg.BgType = new Random().Next(1, 4);
            var d = new Random().Next(100);
            if (d % 3 == 0)
                bg.Location = new Point(bg.Bounds.Width - bg.Rec.Width - (new Random().Next((bg.Bounds.Width - bg.Rec.Width) / 2)), 0);
            if (d % 3 == 1)
                bg.Location = new Point((bg.Bounds.Width - bg.Rec.Width) / 2 - (new Random().Next((bg.Bounds.Width - bg.Rec.Width) / 2)), 0);
            if (d % 3 == 2)
                bg.Location = new Point((new Random().Next((bg.Bounds.Width - bg.Rec.Width) / 2)), 0);
            Bgs.Add(bg);
        }

        public void GenerateSelf()
        {
            Plane self = new Plane(60, 80, this._container.Size);
            self.Location = new Point(this._container.Size.Width / 2 - self.Rec.Width / 2, this._container.Size.Height - self.Rec.Height);
            Self = self;
        }

        public Bullet GenerateBullet()
        {
            Bullet bullet = new Bullet(5, 13, this._container.Size);
            Bullets.Add(bullet);
            return bullet;
        }

        public Background GenerateBackground()
        {
            Background b = new Background(this._container.Size.Width, this._container.Size.Height, this._container.Size);
            Backounds.Add(b);
            return b;
        }

        internal void Listening(Keys keyCode, string type)
        {
            if (Self != null && new[] { Keys.Up, Keys.Down, Keys.Left, Keys.Right }.Contains(keyCode))
            {
                if (type == "UP")
                {
                    DownKeys.Remove(keyCode);
                }
                if (type == "DOWN")
                {
                    if ((keyCode == Keys.Down && DownKeys.Contains(Keys.Up)) ||
                        (keyCode == Keys.Up && DownKeys.Contains(Keys.Down)) ||
                        (keyCode == Keys.Left && DownKeys.Contains(Keys.Right)) ||
                        (keyCode == Keys.Right && DownKeys.Contains(Keys.Left)) ||
                        DownKeys.Count >= 2
                        )
                    {
                        return;
                    }
                    else
                    {
                        if (!DownKeys.Contains(keyCode))
                            DownKeys.Add(keyCode);
                    }
                }
            }
        }

        public void Draw(Graphics g)
        {            
            foreach (var item in Backounds.ToList())
            {
                if (item != null)
                    item.Draw(g);
            }
            foreach (var item in Bgs.ToList())
            {
                if (item != null)
                    item.Draw(g);
            }
            foreach (EnemyPlane1 item in Enemies.ToList())
            {
                if (item != null)
                {
                    item.Draw(g);
                }
            }
            foreach (var item in Bullets.ToList())
            {
                if (item != null)
                    item.Draw(g);
            }
            Self.Draw(g);
        }

        public async void Begin()
        {
            GenerateBackground();

            var t = Task.Run(() =>
            {
                while (true)
                {
                    foreach (EnemyPlane1 item in Enemies.ToList())
                    {
                        if (item.IsDead)
                        {
                            Enemies.Remove(item);
                        }
                    }

                    Thread.Sleep(1000);
                }
            });
            
                        
            await Task.Run(() =>
            {
                var dt = DateTime.Now;
                var dtForBg = DateTime.Now;
                var moveBg = true;
                while (true)
                {
                    var toBeRemovedEnmies = new List<BaseObj>();
                    foreach (var item in Enemies)
                    {
                        if (!item.CheckInBounds())
                        {
                            toBeRemovedEnmies.Add(item);
                        }
                    }
                    toBeRemovedEnmies.ForEach(x => Enemies.Remove(x));

                    foreach (var item in Bullets)
                    {
                        if (!item.CheckInBounds())
                        {
                            toBeRemovedEnmies.Add(item);
                        }
                    }
                    toBeRemovedEnmies.ForEach(x => Bullets.Remove(x));

                    foreach (var item in Bgs)
                    {
                        if (!item.CheckInBounds())
                        {
                            toBeRemovedEnmies.Add(item);
                        }
                    }
                    toBeRemovedEnmies.ForEach(x => Bgs.Remove(x));

                    foreach (var item in Backounds)
                    {
                        if (!item.CheckInBounds())
                        {
                            toBeRemovedEnmies.Add(item);
                        }
                    }
                    toBeRemovedEnmies.ForEach(x => Backounds.Remove(x));

                    if ((DateTime.Now - dt).TotalSeconds > new Random().Next(20, 40) / 10d)
                    {
                        dt = DateTime.Now;
                        GenerateEnemy();
                    }

                    if ((DateTime.Now - dtForBg).TotalSeconds > 4)
                    {
                        dtForBg = DateTime.Now;
                        GenerateBg();
                    }

                    if (Backounds.Count < 2)
                    {
                        var bg = GenerateBackground();
                        bg.Location = new Point(0, Backounds.Any() ? Backounds.First().Location.Y - this._container.Height : 0);
                    }

                    foreach (EnemyPlane1 item in Enemies)
                    {
                        item.Move(Enums.Direction.DOWN);
                        if (DateTime.Now.Millisecond > 985 && !item.IsDead)
                        {
                            var bullet = GenerateBullet();
                            item.Shoot(bullet);
                        }
                    }

                    foreach (var item in Bullets)
                    {
                        item.Move(Enums.Direction.DOWN);
                    }

                    foreach (var item in Bgs)
                    {
                        item.Move(Enums.Direction.DOWN);
                    }

                    foreach (var item in Backounds)
                    {
                        item.Move(Enums.Direction.DOWN);
                    }

                    if (IsMoving)
                    {
                        var p = new Point(Self.Location.X, Self.Location.Y);
                        if (Self.CheckInBounds())
                        {
                            Self.Move(Direction);
                            if (!Self.CheckInBounds())
                            {
                                Self.Location = p;
                            }
                        }
                    }
                    if (DateTime.Now.Millisecond % 100 > 90)
                    {
                        var b = GenerateBullet();
                        b.IsMine = true;
                        ((Plane)Self).Shoot(b);
                    }

                    //死亡判断
                    foreach (Bullet item in Bullets.Where(x => !((Bullet)x).IsMine))
                    {
                        if (item.Rec.IntersectsWith(Self.Rec))
                        {
                        }
                    }
                    foreach (Bullet item in Bullets.Where(x => ((Bullet)x).IsMine))
                    {
                        foreach (EnemyPlane1 ep in Enemies.ToList())
                        {
                            if (item.Rec.IntersectsWith(ep.Rec))
                            {
                                ep.IsDead = true;
                                //Enemies.Remove(ep);
                            }
                        }
                    }

                    Thread.Sleep(15);
                }
            });
        }
    }
}

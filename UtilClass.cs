using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonMapper
{
    static internal class UtilClass
    {
        static public void DoGrid(Bitmap? bitmap)
        {
            if (bitmap != null)
            {
                Graphics g = Graphics.FromImage(bitmap);
                Pen pen = new(Storage.grid);
                for (int y = 0; y < bitmap.Height; y += 25)
                    g.DrawLine(pen, 0, y, bitmap.Width - 1, y);
                for (int x = 0; x < bitmap.Width; x += 25)
                    g.DrawLine(pen, x, 0, x, bitmap.Height - 1);
                g.Dispose();
            }
        }

        public static void RotatePic(Button button, int rotate)
        {
            Image image = button.Image;
            Bitmap bmp = new(image.Width, image.Height);
            Graphics g = Graphics.FromImage(bmp);
            Rectangle src = new(0, 0, image.Width, image.Height);
            g.DrawImage(image, src, src, GraphicsUnit.Pixel);
            g.Dispose();
            RotatePic(bmp, rotate);
            //g = Graphics.FromImage(image);
            //g.DrawImage(bmp, src, src, GraphicsUnit.Pixel);
            button.Image = bmp;
        }

        public static void RotatePic(Bitmap bmp, int rotate)
        {
            int r = rotate;
            while (r < 0)
            {
                bmp.RotateFlip(RotateFlipType.Rotate90FlipNone);
                r++;
            }
            while (r > 0)
            {
                bmp.RotateFlip(RotateFlipType.Rotate270FlipNone);
                r--;
            }
        }
        public static Bitmap RemapColors(Bitmap bmp)
        {
            ImageAttributes imageAttributes = new();
            int width = bmp.Width;
            int height = bmp.Height;
            ColorMap colorMap0 = new() { OldColor = Color.FromArgb(255, 0, 0, 0), NewColor = Storage.back };
            ColorMap colorMap1 = new() { OldColor = Color.FromArgb(255, 255, 255, 255), NewColor = Storage.fore };
            ColorMap[] remapTable = { colorMap0, colorMap1 };
            imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);
            Bitmap b = new(width, height);
            Graphics g = Graphics.FromImage(b);
            g.DrawImage(bmp, new Rectangle(0, 0, 25, 25), 0, 0, 25, 25, GraphicsUnit.Pixel, imageAttributes);
            g.Dispose();
            return b;
        }

        static public void DoGrid(Bitmap? bitmap, Color gridcolor)
        {
            if (bitmap != null)
            {
                Graphics g = Graphics.FromImage(bitmap);
                Pen pen = new(gridcolor);
                for (int y = 0; y < bitmap.Height; y += 25)
                    g.DrawLine(pen, 0, y, bitmap.Width - 1, y);
                for (int x = 0; x < bitmap.Width; x += 25)
                    g.DrawLine(pen, x, 0, x, bitmap.Height - 1);
                g.Dispose();
            }
        }
    }

    internal class Storage
    {
        static public Color back, fore, grid, butt;
        static public bool gridview, gridsave;
        static public int buttonset, sizex, sizey;
        static public Image[] buttons0 = new Image[21];
        static public Image[] buttons1 = new Image[21];
    }

    internal class BufferClass
    {
        public int x, y, sel, rot, set;
        private void InitBufferClass(int x, int y, int sel, int rot, int set)
        {
            this.x = x;
            this.y = y;
            this.sel = sel;
            this.rot = rot;
            this.set = set;
        }
        public BufferClass(int x, int y, int sel, int rot) => InitBufferClass(x, y, sel, rot, 0);
        public BufferClass(int x, int y, int sel, int rot, int set) => InitBufferClass(x, y, sel, rot, set);
        public BufferClass() => InitBufferClass(-1, -1, -1, 0, 0);
        public int GetX() => x;
        public int GetY() => y;
        public int GetSel() => sel;
        public int GetRot() => rot;
        public int GetSet() => set;
        public void SetX(int x) => this.x = x;
        public void SetY(int y) => this.y = y;
        public void SetSel(int sel) => this.sel = sel;
        public void SetRot(int rot) => this.rot = rot;
        public void SetSet(int set) => this.set = set;
        public Point GetXY() => new(x, y);
        public void SetXY(Point xy) => InitBufferClass(xy.X, xy.Y, sel, rot, set);
        public void SetValues(int x, int y, int sel, int rot, int set) => InitBufferClass(x, y, sel, rot, set);
        public void SetValues(int x, int y, int sel, int rot) => InitBufferClass(x, y, sel, rot, set);
        public void SetValues(Point xy, int sel, int rot, int set) => InitBufferClass(xy.X, xy.Y, sel, rot, set);
        public void SetValues(Point xy, int sel, int rot) => InitBufferClass(xy.X, xy.Y, sel, rot, set);

        public void WriteTo(Bitmap bmp)
        {
            Bitmap b = new(bmp.Width, bmp.Height);
            Graphics g = Graphics.FromImage(b);
            g.DrawImage(bmp, 0, 0);
            Rectangle src = new(0, 0, 25, 25);
            Rectangle dst = new(x, y, 25, 25);
            Bitmap b2 = new(25, 25);
            Graphics g2 = Graphics.FromImage(b2);
            if (set == 0)
                g2.DrawImage(Storage.buttons0[sel], src, src, GraphicsUnit.Pixel);
            else
                g2.DrawImage(Storage.buttons1[sel], src, src, GraphicsUnit.Pixel);
            g2.Dispose();
            Bitmap b3 = UtilClass.RemapColors(b2);
            b2.Dispose();
            UtilClass.RotatePic(b3, rot);
            g.DrawImage(b3, dst, src, GraphicsUnit.Pixel);
            g.Dispose();
            g = Graphics.FromImage(bmp);
            g.DrawImage(b, 0, 0);
            g.Dispose();
        }
    }
}

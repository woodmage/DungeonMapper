using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DungeonMapper
{
    public partial class Settings : Form
    {
        readonly Random random = new();
        readonly private Color oldback, oldfore, oldgrid, oldbutt;
        readonly private bool oldgridview, oldgridsave;
        readonly private int oldbuttonset, oldsizex, oldsizey;
        bool domapsize = false;

        public Settings()
        {
            oldback = Storage.back;
            oldfore = Storage.fore;
            oldgrid = Storage.grid;
            oldbutt = Storage.butt;
            oldgridview = Storage.gridview;
            oldgridsave = Storage.gridsave;
            oldbuttonset = Storage.buttonset;
            oldsizex = Storage.sizex;
            oldsizey = Storage.sizey;
            InitializeComponent();
        }

        private void ButtonColor(object sender, EventArgs e)
        {
            if (colorsel.ShowDialog() == DialogResult.OK)
                Storage.butt = colorsel.Color;
            Draw();
        }

        private void GridColor(object sender, EventArgs e)
        {
            if (colorsel.ShowDialog() == DialogResult.OK)
                Storage.grid = colorsel.Color;
            Draw();
        }

        private void ForegroundColor(object sender, EventArgs e)
        {
            if (colorsel.ShowDialog() == DialogResult.OK)
                Storage.fore = colorsel.Color;
            Draw();
        }

        private void BackgroundColor(object sender, EventArgs e)
        {
            if (colorsel.ShowDialog() == DialogResult.OK)
                Storage.back = colorsel.Color;
            Draw();
        }

        private void Accept(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Cancel(object sender, EventArgs e)
        {
            Storage.back = oldback;
            Storage.fore = oldfore;
            Storage.grid = oldgrid;
            Storage.butt = oldbutt;
            Storage.gridview = oldgridview;
            Storage.gridsave = oldgridsave;
            Storage.buttonset = oldbuttonset;
            Storage.sizex = oldsizex;
            Storage.sizey = oldsizey;
            this.Close();
        }

        private void Draw()
        {
            but0.BackColor = Storage.butt;
            but1.BackColor = Storage.butt;
            if (Storage.buttonset == 0)
            {
                but0.Image = Storage.buttons0[3];
                but1.Image = Storage.buttons0[4];
            }
            else
            {
                but0.Image = Storage.buttons1[3];
                but1.Image = Storage.buttons1[4];
            }
            but0.Invalidate();
            but1.Invalidate();
            Bitmap bmp = new(picture.Width, picture.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Storage.back);
            Pen pen = new(Storage.grid);
            if (Storage.gridview)
            {
                for (int x = 0; x < picture.Width; x += 25)
                    g.DrawLine(pen, x, 0, x, picture.Height - 1);
                for (int y = 0; y < picture.Height; y += 25)
                    g.DrawLine(pen, 0, y, picture.Width - 1, y);

            }
            g.Dispose();
            for (int i = 0; i < 10; i++)
            {
                int sel = random.Next() % 21;
                int x = (random.Next() % picture.Width) / 25 * 25;
                int y = (random.Next() % picture.Height) / 25 * 25;
                int rot = ((random.Next() % 9) - 4);
                int set = random.Next() % 2;
                BufferClass b = new(x, y, sel, rot, set);
                b.WriteTo(bmp);
            }
            picture.Image = bmp;
            picture.Invalidate();
        }

        private void ButtonSet(object sender, EventArgs e)
        {
            Storage.buttonset = (int)updnbuttonset.Value;
            Draw();
        }

        private void OnLoad(object sender, EventArgs e)
        {
            domapsize = false;
            cbgridview.Checked = Storage.gridview;
            cbgridsave.Checked = Storage.gridsave;
            mapsizex.Value = Storage.sizex;
            mapsizey.Value = Storage.sizey;
            domapsize = true;
            Draw();
        }

        private void GridView(object sender, EventArgs e)
        {
            Storage.gridview = cbgridview.Checked;
        }

        private void GridSave(object sender, EventArgs e)
        {
            Storage.gridsave = cbgridsave.Checked;
        }

        private void MapSizeChanged(object sender, EventArgs e)
        {
            if (domapsize)
            {
                Storage.sizex = (int)mapsizex.Value / 25 * 25;
                Storage.sizey = (int)mapsizey.Value / 25 * 25;
                mapsizex.Value = Storage.sizex;
                mapsizey.Value = Storage.sizey;
            }
        }
    }
}

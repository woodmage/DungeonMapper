using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;

namespace DungeonMapper
{
    public partial class Form1 : Form
    {
        static readonly string[] filebut = { "blank", "wall", "corner", "tee", "intersection", "door", "secretdoor", "arrowslit",
            "portcullis", "portculliscorner", "portcullistee", "portcullisintersection", "portculliswalltee",
            "portculliswallintersection", "stairsdown", "stairsup", "throne", "chest", "circle", "pentagram", "trapdoor" };
        static readonly Image[] bmpbut = new Image[21];
        static readonly Image[] bmp2but = new Image[21];
        static readonly string[] filecom = { "back", "erase", "clear", "right", "left", "savepic", "save", "load", "settings", "exit" };
        static readonly Image[] bmpcom = new Image[10];
        static readonly Button[] butbut = new Button[21];
        static readonly Button[] combut = new Button[10];
        static readonly EventHandler[] funbut = { Select00, Select01, Select02, Select03, Select04, Select05, Select06, Select07,
            Select08, Select09, Select10, Select11, Select12, Select13, Select14, Select15, Select16, Select17, Select18, Select19,
            Select20, Select21 };
        static readonly EventHandler[] funcom = { Command0, Command1, Command2, Command3, Command4, Command5, Command6, Command7,
            Command8, Command9 };
        static Size clsize;
        static PictureBox? picture;
        static Bitmap? picbmp;
        static Graphics? gpicbmp;
        static Button? selbut;
        static int selection = 0, rotation = 0;
        static readonly List<BufferClass> buffer = new();
        static readonly ImageFormat[] formats = { ImageFormat.Png, ImageFormat.Jpeg, ImageFormat.Bmp, ImageFormat.Gif };
        static bool dosizeflag = false;
        public Form1()
        {
            for (int i = 0; i < 21; i++)
            {
                Storage.buttons0[i] = bmpbut[i] = GetImageByName("0" + filebut[i]);
                Storage.buttons1[i] = bmp2but[i] = GetImageByName("1" + filebut[i]);
            }
            for (int i = 0; i < 10; i++)
            {
                bmpcom[i] = GetImageByName("c_" + filecom[i]);
            }
            Storage.back = Color.Black;
            Storage.fore = Color.White;
            Storage.grid = Color.Gray;
            Storage.butt = Color.Black;
            Storage.gridview = true;
            Storage.gridsave = false;
            Storage.buttonset = 0;
            InitializeComponent();
        }

        public static Image GetImageByName(string image)
        {
            string respath = "DungeonMapper.Resources." + image + ".png";
            Assembly asm = Assembly.GetExecutingAssembly();
            Stream? fs = asm.GetManifestResourceStream(respath);
            if (fs == null)
                return new Bitmap(25, 25);
            return Bitmap.FromStream(fs);
        }

        public static void UpdateButtons()
        {
            for (int i = 0; i < 21; i++)
            {
                Bitmap bmp = new(25, 25);
                Graphics g = Graphics.FromImage(bmp);
                Rectangle src = new(0, 0, 25, 25);
                if (Storage.buttonset == 0)
                    g.DrawImage(bmpbut[i], src, src, GraphicsUnit.Pixel);
                else
                    g.DrawImage(bmp2but[i], src, src, GraphicsUnit.Pixel);
                g.Dispose();
                int r = rotation;
                while (r > 0)
                {
                    bmp.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    r--;
                }
                while (r < 0)
                {
                    bmp.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    r++;
                }
                butbut[i].Image = bmp;
                butbut[i].Invalidate();
            }
            if (selection == -1) return;
            Bitmap bmp2 = new(25, 25);
            Graphics g2 = Graphics.FromImage(bmp2);
            Rectangle src2 = new(0, 0, 25, 25);
            if (Storage.buttonset == 0)
                g2.DrawImage(bmpbut[selection], src2, src2, GraphicsUnit.Pixel);
            else
                g2.DrawImage(bmp2but[selection], src2, src2, GraphicsUnit.Pixel);
            g2.Dispose();
            int r2 = rotation;
            while (r2 > 0)
            {
                bmp2.RotateFlip(RotateFlipType.Rotate270FlipNone);
                r2--;
            }
            while (r2 < 0)
            {
                bmp2.RotateFlip(RotateFlipType.Rotate90FlipNone);
                r2++;
            }
            if (selbut == null) return;
            selbut.Image = bmp2;
            selbut.Invalidate();
        }

        private void HandleKey(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Back:
                    Command0(sender, e);
                    break;
                case Keys.Delete:
                    Command1(sender, e);
                    break;
                case Keys.C:
                    Command2(sender, e);
                    break;
                case Keys.L:
                    Command3(sender, e);
                    break;
                case Keys.R:
                    Command4(sender, e);
                    break;
                case Keys.Enter:
                    Command5(sender, e);
                    break;
                case Keys.Down:
                    Command6(sender, e);
                    break;
                case Keys.Up:
                    Command7(sender, e);
                    break;
                case Keys.S:
                    Command8(sender, e);
                    break;
                case Keys.Escape:
                    Command9(sender, e);
                    break;
            }
        }

        private void OnLoad(object sender, EventArgs e)
        {
            buffer.Clear();
            dosizeflag = false;
            Size size = Screen.FromPoint(Cursor.Position).WorkingArea.Size; //get desktop size
            this.Size = size; //set form size to desktop size
            this.StartPosition = FormStartPosition.Manual; //set form position to upper left corner
            this.Location = new Point(0, 0);
            clsize = this.ClientSize; //get clientsize
            Panel panel = new() { AutoScroll = true, Size = new Size(clsize.Width, clsize.Height - 45), Location = new Point(0, 45) };
            picture = new PictureBox { Size = new Size(clsize.Width, clsize.Height - 45), Location = new Point(0, 0), 
                Dock = DockStyle.None, SizeMode = PictureBoxSizeMode.AutoSize }; //make picturebox
            picture.MouseClick += PictureMouseClick;
            Storage.sizex = clsize.Width;
            Storage.sizey = clsize.Height - 45;
            picbmp = new(Storage.sizex, Storage.sizey); //make bmp for picture
            gpicbmp = Graphics.FromImage(picbmp);
            gpicbmp.Clear(Storage.back);
            if (Storage.gridview) UtilClass.DoGrid(picbmp, Storage.grid);
            gpicbmp.Dispose();
            picture.Image = picbmp;
            panel.Controls.Add(picture);
            this.Controls.Add(panel);
            for (int i = 0; i < 21; i++)
            {
                butbut[i] = new Button { Location = new Point(i * 35 + 5, 5), Size = new Size(35, 35), BackColor = Storage.butt };
                if (Storage.buttonset == 0)
                    butbut[i].Image = bmpbut[i];
                else
                    butbut[i].Image = bmp2but[i];
                butbut[i].Click += funbut[i];
                this.Controls.Add(butbut[i]);
            }
            for (int i = 0; i < 10; i++)
            {
                combut[i] = new Button { Location = new Point(775 + i * 35, 5), Size = new Size(35, 35), BackColor = Storage.butt, Image = bmpcom[i] };
                combut[i].Click += funcom[i];
                this.Controls.Add(combut[i]);
            }
            selbut = new Button { Location = new Point(1160, 5), Size = new Size(35, 35), BackColor = Storage.butt };
            if (Storage.buttonset == 0)
                selbut.Image = bmpbut[selection];
            else
                selbut.Image = bmp2but[selection];
            this.Controls.Add(selbut);
            dosizeflag = true;
        }

        private static void FixButtons()
        {
            for (int i = 0; i < 21; i++)
            {
                butbut[i].BackColor = Storage.butt;
                if (Storage.buttonset == 0)
                    butbut[i].Image = bmpbut[i];
                else
                    butbut[i].Image = bmp2but[i];
                UtilClass.RotatePic(butbut[i], rotation);
                butbut[i].Invalidate();
            }
        }

        private static void FixSelection(int sel)
        {
            selection = sel;
            if (selbut == null) return;
            if (selection != -1)
            {
                selbut.BackColor = Storage.butt;
                if (Storage.buttonset == 0)
                    selbut.Image = bmpbut[sel];
                else
                    selbut.Image = bmp2but[sel];
                UtilClass.RotatePic(selbut, rotation);
            }
            else
                selbut.Image = bmpcom[1];
            selbut.Invalidate();
        }

        private static BufferClass? FindIt(int x, int y)
        {
            BufferClass? bufret = null;
            foreach (BufferClass buf in buffer)
            {
                if ((buf.GetX() == x) && (buf.GetY() == y))
                    return buf;
            }
            return bufret;
        }

        static private void ClearPicture()
        {
            if (picbmp == null) return;
            Graphics g = Graphics.FromImage(picbmp);
            g.Clear(Storage.back);
            g.Dispose();
            if (Storage.gridview) UtilClass.DoGrid(picbmp);
        }
        static private void RedrawIt()
        {
            ClearPicture();
            foreach (BufferClass buf in buffer)
            {
                if (picbmp != null)
                    buf.WriteTo(picbmp);
            }
            if (picture == null) return;
            picture.Image = picbmp;
            picture.Invalidate();
        }

        private void PictureMouseClick(object? sender, MouseEventArgs e)
        {
            int x = e.Location.X / 25 * 25, y = e.Location.Y / 25 * 25;
            if (selection > 0)
            {
                BufferClass buf = new(x, y, selection, rotation, Storage.buttonset);
                buffer.Add(buf);
                if (picbmp != null)
                    buf.WriteTo(picbmp);
            }
            else if (selection == 0)
            {
                BufferClass? buf = FindIt(x, y);
                while (buf != null)
                {
                    buffer.Remove(buf);
                    buf = FindIt(x, y);
                }
                RedrawIt();
            }
            else
            {
                BufferClass? buf = FindIt(x, y);
                if (buf != null)
                    buffer.Remove(buf);
                RedrawIt();
            }
            if (picture == null) return;
            picture.Image = picbmp;
            picture.Invalidate();
        }
        static private bool GetYesNo(string prompt) => MessageBox.Show(prompt, "Verify", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        static private void Command0(object? o, EventArgs e) //back
        {
            if (buffer.Count > 0)
            {
                buffer.RemoveAt(buffer.Count - 1);
                RedrawIt();
            }
        }
        static private void Command1(object? o, EventArgs e) //erase
        {
            FixSelection(-1);
        }
        static private void Command2(object? o, EventArgs e) //clear
        {
            if (GetYesNo("Clear the screen?  Are you sure?"))
            {
                buffer.Clear();
                RedrawIt();
            }
        }
        static private void Command3(object? o, EventArgs e) //right
        {
            rotation--;
            if (rotation < -3)
                rotation += 4;
            UpdateButtons();
        }
        static private void Command4(object? o, EventArgs e) //left
        {
            rotation++;
            if (rotation > 3)
                rotation -= 4;
            UpdateButtons();
        }
        static private void Command5(object? o, EventArgs e) //savepic
        {
            if (picbmp != null)
            {
                SaveFileDialog saveFileDialog1 = new() { Filter = "Png Image|*.png|JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif", Title = "Save an Image File" };
                saveFileDialog1.ShowDialog();
                if (saveFileDialog1.FileName != "")
                {
                    System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog1.OpenFile();
                    picbmp.Save(fs, formats[saveFileDialog1.FilterIndex - 1]);
                    fs.Close();
                }
            }

            ;
        }
        static private void Command6(object? o, EventArgs e) //save
        {
            if (buffer.Count > 0)
            {
                SaveFileDialog save = new() { Filter = "Dungeon Mapper file (*.map)|*.map|Any file (*.*)|*.*", Title = "Save Dungeon Mapper file" };
                save.ShowDialog();
                if (save.FileName != "")
                {
                    using Stream fs = save.OpenFile();
                    {
                        using BinaryWriter bw = new(fs);
                        {
                            bw.Write(buffer.Count);
                            foreach (BufferClass buf in buffer)
                            {
                                bw.Write(buf.GetX());
                                bw.Write(buf.GetY());
                                bw.Write(buf.GetSel());
                                bw.Write(buf.GetRot());
                                bw.Write(buf.GetSet());
                            }
                        }
                    }
                }
            }
        }
        static private void Command7(object? o, EventArgs e) //load
        {
            using OpenFileDialog openFileDialog = new();
            openFileDialog.Filter = "Mapper files (*.map)|*.map|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string? filePath = openFileDialog.FileName;
                using Stream fs = openFileDialog.OpenFile();
                {
                    using BinaryReader br = new(fs);
                    {
                        int nbuf = br.ReadInt32();
                        buffer.Clear();
                        for (int i = 0; i < nbuf; i++)
                        {
                            int x = br.ReadInt32();
                            int y = br.ReadInt32();
                            int s = br.ReadInt32();
                            int r = br.ReadInt32();
                            int b = br.ReadInt32();
                            buffer.Add(new BufferClass(x, y, s, r, b));
                        }
                    }
                }
                RedrawIt();
            }
        }
        static private void Command8(object? o, EventArgs e) //settings
        {
            Form f = new Settings();
            f.ShowDialog();
            FixButtons();
            picbmp?.Dispose();
            picbmp = new(Storage.sizex, Storage.sizey); //make bmp for picture
            gpicbmp = Graphics.FromImage(picbmp);
            gpicbmp.Clear(Storage.back);
            if (Storage.gridview) UtilClass.DoGrid(picbmp, Storage.grid);
            gpicbmp.Dispose();
            RedrawIt();
        }
        static private void Command9(object? o, EventArgs e) //exit
        {
            if (GetYesNo("Exit?  Are you sure?"))
                Application.Exit();
        }
        static private void Select00(object? o, EventArgs e) => FixSelection(0);
        static private void Select01(object? o, EventArgs e) => FixSelection(1);
        static private void Select02(object? o, EventArgs e) => FixSelection(2);
        static private void Select03(object? o, EventArgs e) => FixSelection(3);
        static private void Select04(object? o, EventArgs e) => FixSelection(4);
        static private void Select05(object? o, EventArgs e) => FixSelection(5);
        static private void Select06(object? o, EventArgs e) => FixSelection(6);
        static private void Select07(object? o, EventArgs e) => FixSelection(7);
        static private void Select08(object? o, EventArgs e) => FixSelection(8);
        static private void Select09(object? o, EventArgs e) => FixSelection(9);
        static private void Select10(object? o, EventArgs e) => FixSelection(10);
        static private void Select11(object? o, EventArgs e) => FixSelection(11);
        static private void Select12(object? o, EventArgs e) => FixSelection(12);
        static private void Select13(object? o, EventArgs e) => FixSelection(13);
        static private void Select14(object? o, EventArgs e) => FixSelection(14);
        static private void Select15(object? o, EventArgs e) => FixSelection(15);
        static private void Select16(object? o, EventArgs e) => FixSelection(16);
        static private void Select17(object? o, EventArgs e) => FixSelection(17);
        static private void Select18(object? o, EventArgs e) => FixSelection(18);
        static private void Select19(object? o, EventArgs e) => FixSelection(19);
        static private void Select20(object? o, EventArgs e) => FixSelection(20);
        static private void Select21(object? o, EventArgs e) => FixSelection(21);

        private void OnResize(object sender, EventArgs e)
        {
            if (dosizeflag)
            {
                clsize = this.ClientSize; //get clientsize
                if (picture == null) return;
                picture.Size = new Size(clsize.Width, clsize.Height - 45);
                picture.Location = new Point(0, 45);
                picbmp?.Dispose();
                picbmp = new(Storage.sizex, Storage.sizey); //make bmp for picture
                gpicbmp = Graphics.FromImage(picbmp);
                gpicbmp.Clear(Storage.back);
                if (Storage.gridview) UtilClass.DoGrid(picbmp, Storage.grid);
                gpicbmp.Dispose();
                picture.Image = picbmp;
                for (int i = 0; i < 21; i++)
                {
                    if (Storage.buttonset == 0)
                        butbut[i].Image = bmpbut[i];
                    else
                        butbut[i].Image = bmp2but[i];
                }
                for (int i = 0; i < 10; i++)
                {
                }
                if (selbut != null)
                {
                    if (Storage.buttonset == 0)
                        selbut.Image = bmpbut[selection];
                    else
                        selbut.Image = bmp2but[selection];
                }
                RedrawIt();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Threading;

namespace mine_clearance
{
    public partial class MyButton : UserControl
    {
        public MyButton()
        {
            InitializeComponent();
            this.BtnText.Text = " ";
        }
        public String t { get; set; }
        public static Label flagLabel { get; set; }
        public static int[,] plate { get; set; }
        public static MyButton[,] btnArray { get; set; }
        public static int[,] flagplate { get; set; }//标记棋盘
        public int x { get; set; }
        public int y { get; set; }
        public void setText()
        {
            this.BtnText.Text = t;
        }

        private Boolean rightTag = false;
        private Boolean leftTag = false;

        private void BtnText_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.BtnText.Text == " ")
                this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            if (e.Button == MouseButtons.Left)
                leftTag = true;
            else if (e.Button == MouseButtons.Right)
                rightTag = true;
            int a = 0;
            if (rightTag && leftTag && Int32.TryParse(this.BtnText.Text, out a))
            {
                List<Point> plist = getPointSurronded(x, y);
                int width = plate.GetLength(1);
                int height = plate.GetLength(0);
                foreach (var i in plist)
                {
                    if (OutSideJudgement(i))
                        continue;
                    if (btnArray[i.Y - 1, i.X - 1].BtnText.Text == " " && btnArray[i.Y - 1, i.X - 1].t != " ")
                    {
                        showbtnlist.Add(btnArray[i.Y - 1, i.X - 1]);
                    }
                }
                foreach (var i in showbtnlist)
                {
                    i.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
                }
            }

        }
        private List<MyButton> showbtnlist = new List<MyButton>();
        private void BtnText_MouseUp(object sender, MouseEventArgs e)
        {
            if (rightTag && leftTag)
            {
                foreach (var i in showbtnlist)
                {
                    i.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                }
                showbtnlist.Clear();

                if (this.BtnText.Text == " " || this.BtnText.Text == "?" || this.BtnText.Text == "M")
                {
                    this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    rightTag = false;
                    leftTag = false;
                    return;
                }

                int Minessurronded = 0;
                if (Int32.TryParse(this.BtnText.Text, out Minessurronded))
                {
                    List<Point> plist = getPointSurronded(x, y);

                    int tag = 0;
                    foreach (var i in plist)//统计已标记雷数和实际雷数比较
                    {
                        if (flagplate[i.Y, i.X] == -1)
                            tag++;
                    }
                    if (tag < Minessurronded)//标记雷数不足
                    {
                        this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                        rightTag = false;
                        leftTag = false;
                        return;
                    }
                    if (tag >= Minessurronded)
                    {
                        if (tag > Minessurronded)
                        {
                            show();
                            return;
                        }
                        Boolean match = true;
                        List<MyButton> showbtns = new List<MyButton>();
                        foreach (var i in plist)//统计已标记雷数和实际雷数比较
                        {
                            if (flagplate[i.Y, i.X] == -1 && plate[i.Y, i.X] != -1)
                            {
                                match = false;
                                break;
                            }
                            if (plate[i.Y, i.X] >= 0 && !OutSideJudgement(i))
                            {
                                showbtns.Add(btnArray[i.Y - 1, i.X - 1]);
                            }
                        }
                        if (!match)
                        {
                            show();
                            return;
                        }
                        if (showbtns.Count > 0 && match)
                        {
                            foreach (var i in showbtns)
                            {
                                if (plate[i.y, i.x] == 0 && flagplate[i.y, i.x] == -99)
                                {
                                    List<MyButton> btnlist = i.getBtnlist();
                                    foreach (var a in btnlist)
                                    {
                                        a.btnMouseUp();
                                    }
                                }
                                i.btnMouseUp();
                            }
                        }

                    }
                }


            }
            else if (e.Button.Equals(MouseButtons.Right))
            {
                if (this.BtnText.Text == " ")
                {
                    this.BtnText.Text = "M";
                    setflag();
                }
                else if (this.BtnText.Text == "M")
                {
                    cancelflag();
                    this.BtnText.Text = "?";
                }
                else if (this.BtnText.Text == "?")
                    this.BtnText.Text = " ";
                this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            }
            else if (e.Button.Equals(MouseButtons.Left))
            {
                if (BtnText.Text == "M")
                {
                    this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    this.BtnText.Text = "?";
                    cancelflag();
                    return;
                }
                else if (BtnText.Text == "?")
                {
                    this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    this.BtnText.Text = " ";
                    return;
                }
                else
                {
                    btnMouseUp();
                    List<MyButton> btnlist = getBtnlist();
                    foreach (var i in btnlist)
                    {
                        i.btnMouseUp();
                    }
                }
            }
            if (MainForm.flag >= MainForm.Mines - 1)
            {
                if (MainForm.Check(plate, flagplate))
                {
                    show();
                    MessageBox.Show("宝宝好厉害！");
                }
            }
        }

        private List<MyButton> getBtnlist()
        {
            List<MyButton> btnlist = new List<MyButton>();
            List<Point> PointList = new List<Point>();
            int[,] plate = MyButton.plate;
            int width = plate.GetLength(1);
            int height = plate.GetLength(0);

            PointList.Add(new Point(x, y));

            for (int i = 0; i < PointList.Count; i++)
            {
                int count = PointList.Count;
                List<Point> l = checkone(PointList[i].X, PointList[i].Y, plate);
                if (l != null)
                {
                    PointList.AddRange(l);
                    PointList = PointList.Distinct().ToList<Point>();
                    if (count == PointList.Count && i == PointList.Count - 1)
                        break;
                }
            }

            List<MyButton> btns = new List<MyButton>();
            foreach (var i in PointList)
            {
                if (OutSideJudgement(i))
                    continue;
                btns.Add(btnArray[i.Y - 1, i.X - 1]);
            }
            return btns;

        }

        private List<Point> checkone(int x, int y, int[,] plate)
        {
            if (OutSideJudgement(new Point(x, y)))
                return null;
            List<Point> plist = getPointSurronded(x, y);

            if (plate[y, x] == 0)
                return plist;
            if (plate[y, x] > 0)
            {
                List<Point> returnlist = new List<Point>();

                foreach (var i in plist)
                {
                    if (plate[i.Y, i.X] == -1 || OutSideJudgement(i))
                        continue;
                    if (plate[i.Y, i.X] == 0)
                    {
                        returnlist.Add(i);
                        if (i.X == x || i.Y == y)
                        {
                            int xd = i.X - x;
                            int yd = i.Y - y;
                            if (xd == 0)
                            {
                                returnlist.Add(new Point(x - 1, y));
                                returnlist.Add(new Point(x + 1, y));
                                returnlist.Add(new Point(x - 1, y + yd));
                                returnlist.Add(new Point(x + 1, y + yd));
                            }
                            else
                            {
                                returnlist.Add(new Point(x, y + 1));
                                returnlist.Add(new Point(x, y - 1));
                                returnlist.Add(new Point(x + xd, y + 1));
                                returnlist.Add(new Point(x + xd, y - 1));
                            }
                        }
                        if (i.X != x && i.Y != y)
                        {
                            int xd = i.X - x;
                            int yd = i.Y - y;
                            returnlist.Add(new Point(x, y + yd));
                            returnlist.Add(new Point(x + xd, y));

                        }
                    }

                }
                returnlist = returnlist.Distinct().ToList<Point>();


                return returnlist;
            }
            return null;
        }

        private void setflag()
        {
            flagplate[y, x] = -1;
            MainForm.flag++;
            MyButton.flagLabel.Text = "flags:" + MainForm.flag;
        }
        private void cancelflag()
        {
            flagplate[y, x] = -99;
            MainForm.flag--;
            MyButton.flagLabel.Text = "flags:" + MainForm.flag;
        }

        private void btnMouseUp()
        {
            if (t == "*")
            {
                //MessageBox.Show("game over!");
                this.BtnText.Text = "*";
                this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                show();
                flagplate[y, x] = -1;
            }
            if (this != null && this.t != "*")
            {
                if (t == " ")
                {
                    this.Paint += new System.Windows.Forms.PaintEventHandler(this.btnReDraw);
                    this.InvokePaint(this, new PaintEventArgs(this.CreateGraphics(), new Rectangle()));
                    this.BtnText.MouseUp -= new System.Windows.Forms.MouseEventHandler(this.BtnText_MouseUp);
                    this.BtnText.MouseDown -= new System.Windows.Forms.MouseEventHandler(this.BtnText_MouseDown);
                    flagplate[y, x] = 0;
                }//
                else
                {
                    this.BtnText.Text = t;
                    this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    flagplate[y, x] = Int32.Parse(t);

                }
            }
        }
        private void btnReDraw(object sender, PaintEventArgs e)
        {
            this.BorderStyle = System.Windows.Forms.BorderStyle.None;
            MyButton pnl = (MyButton)sender;
            Pen pen = new Pen(Color.Black);
            pen.DashStyle = DashStyle.Dot;
            e.Graphics.DrawLine(pen, 0, 0, 0, pnl.Height - 0);
            e.Graphics.DrawLine(pen, 0, 0, pnl.Width - 0, 0);
            e.Graphics.DrawLine(pen, pnl.Width - 1, pnl.Height - 1, 0, pnl.Height - 1);
            e.Graphics.DrawLine(pen, pnl.Width - 1, pnl.Height - 1, pnl.Width - 1, 0);
        }

        private void show()
        {
            Thread t = new Thread(() =>
            {
                this.BeginInvoke(new Action(() =>
                {
                    foreach (var i in btnArray)
                    {

                        if (i.t == " ")
                        {
                            i.Paint += new System.Windows.Forms.PaintEventHandler(i.btnReDraw);
                            i.InvokePaint(i, new PaintEventArgs(i.CreateGraphics(), new Rectangle()));
                        }
                        else
                        {
                            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                        }
                        i.BtnText.Text = i.t;
                        i.BtnText.MouseUp -= new System.Windows.Forms.MouseEventHandler(i.BtnText_MouseUp);
                        i.BtnText.MouseDown -= new System.Windows.Forms.MouseEventHandler(i.BtnText_MouseDown);
                    }
                }));
            });
            t.Start();
        }

        private Boolean OutSideJudgement(Point i)
        {
            int width = plate.GetLength(1);
            int height = plate.GetLength(0);
            if (i.X == 0 || i.Y == 0 || i.X >= width - 1 || i.Y >= height - 1)
                return true;
            else
                return false;
        }

        private List<Point> getPointSurronded(int x, int y)
        {
            List<Point> plist = new List<Point>();
            plist.Add(new Point(x + 1, y));
            plist.Add(new Point(x - 1, y));
            plist.Add(new Point(x + 1, y + 1));
            plist.Add(new Point(x - 1, y + 1));
            plist.Add(new Point(x + 1, y - 1));
            plist.Add(new Point(x - 1, y - 1));
            plist.Add(new Point(x, y + 1));
            plist.Add(new Point(x, y - 1));

            for (int i = 0; i < plist.Count; i++)
            {
                if (OutSideJudgement(plist[i]))
                {
                    plist.Remove(plist[i]);
                    if (i > 0)
                        i--;
                }
            }

            return plist;
        }
    }
}

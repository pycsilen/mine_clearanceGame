using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoCaculate;

namespace mine_clearance
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            Point p = new Point(1, 1);
            List<Point> pl = new List<Point>();

            Plate pp = new Plate()
            {
                plate = new int[101, 101]
            };
            Console.WriteLine();
            Start();

        }

        private void Start()
        {
            this.flaglabel.Text = "flags:0";
            flag = 0;
            gamepanel.Controls.Clear();
            try
            {
                height = Int32.Parse(txt_Height.Text);
                width = Int32.Parse(txt_Width.Text);
                Mines = Int32.Parse(txt_Mines.Text);
            }
            catch (Exception)
            {
                height = 16;
                width = 30;
                Mines = 99;
            }
            plate = initial(height, width, Mines);

            this.gamepanel.Width = width * 20 + 10;
            this.gamepanel.Height = height * 20 + 10;
            this.Width = Math.Max(560, width * 20 + 40);
            this.Height = height * 20 + 80;

            MyButton[,] btnArray = new MyButton[height, width];

            expandplate = expand(plate);
            for (int i = 0; i < plate.GetLength(0); i++)
            {
                for (int j = 0; j < plate.GetLength(1); j++)
                {
                    btnArray[i, j] = new MyButton()
                    {
                        t = plate[i, j] >= 0 ? plate[i, j] == 0 ? " " : plate[i, j].ToString() : "*",
                        x = j + 1,
                        y = i + 1
                    };
                    //btnArray[i, j].setText();
                    btnArray[i, j].Location = new Point(1 + j * 20, 1 + i * 20);
                    gamepanel.Controls.Add(btnArray[i, j]);

                }
            }
            MyButton.flagLabel = this.flaglabel;
            MyButton.plate = expandplate;
            MyButton.btnArray = btnArray;
            MyButton.flagplate = new int[expandplate.GetLength(0), expandplate.GetLength(1)];
            
            for (int i = 0; i < MyButton.flagplate.GetLength(0); i++)
            {
                for (int j = 0; j < MyButton.flagplate.GetLength(1); j++)
                {
                    MyButton.flagplate[i, j] = -99;
                }
            }
        }

        public static int width = 30;//宽
        public static int height = 16;//高
        public static int Mines = 99;//雷
        public static int flag = 0;//旗
        private int[,] plate;//棋盘 -1:Mine 0:None 1-8:Num of Mine surrounded
        private int[,] expandplate;//扩展棋盘
        private int[,] flagplate;//扩展棋盘
        private int[,] initial(int height, int width, int Mines)
        {
            int count = height * width;

            List<int> id = new List<int>();
            for (int i = 0; i < count; i++)
            {
                id.Add(i + 1);
            }

            int[] selected = new int[Mines];
            do
            {
                int selectedid = new Random(DateTime.Now.Millisecond * Mines * id.Count).Next(id.Count);
                selected[Mines - 1] = id[selectedid];
                id.RemoveAt(selectedid);
                Mines--;
            } while (Mines > 0);

            int[,] plate = new int[height, width];
            foreach (var i in selected)
            {
                int x = (i - 1) - (i - 1) / width * width;
                int y = (i - 1) / width;
                plate[y, x] = -1;

                if (y - 1 >= 0)
                {
                    if (x - 1 >= 0 && plate[y - 1, x - 1] != -1)
                        plate[y - 1, x - 1]++;
                    if (x + 1 < width && plate[y - 1, x + 1] != -1)
                        plate[y - 1, x + 1]++;
                    if (plate[y - 1, x] != -1)
                        plate[y - 1, x]++;
                }
                if (y + 1 < height)
                {
                    if (x - 1 >= 0 && plate[y + 1, x - 1] != -1)
                        plate[y + 1, x - 1]++;
                    if (x + 1 < width && plate[y + 1, x + 1] != -1)
                        plate[y + 1, x + 1]++;
                    if (plate[y + 1, x] != -1)
                        plate[y + 1, x]++;
                }
                {
                    if (x - 1 >= 0 && plate[y, x - 1] != -1)
                        plate[y, x - 1]++;
                    if (x + 1 < width && plate[y, x + 1] != -1)
                        plate[y, x + 1]++;
                    if (plate[y, x] != -1)
                        plate[y, x]++;
                }
            }
            return plate;
        }

        private int[,] expand(int[,] plate)//扩展
        {
            int[,] expandplate = new int[plate.GetLength(0) + 2, plate.GetLength(1) + 2];

            for (int i = 0; i < plate.GetLength(0); i++)
            {
                for (int j = 0; j < plate.GetLength(1); j++)
                {
                    expandplate[i + 1, j + 1] = plate[i, j];
                }
            }
            return expandplate;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Start();
        }

        public static Boolean Check(int[,] plate, int[,] flagplate)//比较两个数组是否相等
        {
            if (plate.Length != flagplate.Length)
                return false;
            if (plate.GetLength(0) != flagplate.GetLength(0) || plate.GetLength(1) != flagplate.GetLength(1))
                return false;

            Boolean MinesCheck = true;
            Boolean SafeAreaCheck = true;
            for (int i = 0; i < plate.GetLength(0); i++)
            {
                for (int j = 0; j < flagplate.GetLength(1); j++)
                {
                    if (plate[i, j] != flagplate[i, j])
                    {
                        if (plate[i, j] == -1 && flagplate[i, j] == -99)
                            MinesCheck = false;
                        if (flagplate[i, j] == -99 && plate[i, j] >0)
                            SafeAreaCheck = false;
                    }
                }
            }
            return MinesCheck || SafeAreaCheck;
        }

        private void Auto_Click(object sender, EventArgs e)
        {

        }

    }
}

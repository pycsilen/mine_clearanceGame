using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AutoCaculate
{
    public class Plate
    {
        public int[,] plate { get; set; }//[y,x]
        public int[,] flagplate { get; set; }//[y,x]
        public int width { get; set; }//1
        public int height { get; set; }//0
        private List<Point> UnKnownList = new List<Point>();

        private Boolean Initial()
        {
            if (plate.Length != flagplate.Length)
                return false;
            if (plate.GetLength(0) != flagplate.GetLength(0))
                return false;
            if (width != plate.GetLength(1))
                return false;
            if (height != plate.GetLength(0))
                return false;
            for (int i = 1; i < plate.GetLength(0) - 1; i++)
            {
                for (int j = 1; j < plate.GetLength(1) - 1; j++)
                {
                    UnKnownList.Add(new Point(j, i));
                }
            }

            return true;
        }

        enum State
        {
            UnKnown = -99,
            Mines = -1,
            Null = 0
        }
        enum Click
        {
            Left = 1,
            Right = -1
        }
        private void Caculate()
        {
            if (!Initial())
                return;
            int Count = UnKnownList.Count;
            RandomLeftClick();

        }
        private Boolean RandomLeftClick()
        {
            int x = new Random(DateTime.Now.Second * DateTime.Now.Millisecond).Next(width - 2) + 1;
            int y = new Random(DateTime.Now.Second * DateTime.Now.Millisecond * DateTime.Now.Second).Next(height - 2) + 1;
            return LeftClick(x, y);
        }
        private Boolean LeftClick(int x, int y)
        {
            if (plate[y, x] == -1)
                return false;
            List<Point> list = getlist(x, y);
            list.Add(new Point(x, y));
            foreach (var i in list)
            {
                if (!setClick(i.X, i.Y, Click.Left))
                    return false;
            }
            return true;
        }
        private Boolean RightClick(int x, int y)
        {
            if (plate[y, x] != -1)
                return false;
            return setClick(x, y, Click.Right);
        }
        private List<Point> getSurroundedList(int x, int y)
        {
            if (OutLineCheck(x, y))
                return null;
            List<Point> pl = new List<Point>();
            pl.Add(new Point(x + 1, y));
            pl.Add(new Point(x - 1, y));
            pl.Add(new Point(x + 1, y + 1));
            pl.Add(new Point(x - 1, y + 1));
            pl.Add(new Point(x, y + 1));
            pl.Add(new Point(x + 1, y - 1));
            pl.Add(new Point(x - 1, y - 1));
            pl.Add(new Point(x, y - 1));
            for (int i = 0; i < pl.Count; i++)
            {
                if (OutLineCheck(pl[i]))
                {
                    pl.Remove(pl[i]);
                    if (i > 0)
                        i--;
                }
            }
            return pl;
        }

        private Boolean setClick(int x, int y, Click c)
        {
            if ((plate[y, x] != -1 && c == Click.Left) || (plate[y, x] == -1 && c == Click.Right))
            {
                flagplate[y, x] = plate[y, x];
                UnKnownList.Remove(new Point(x, y));
                return true;
            }
            return false;
        }



        private Boolean OutLineCheck(int x, int y)
        {
            if (x <= 0 || y <= 0 || y >= plate.GetLength(0) - 1 || x >= plate.GetLength(1) - 1)
                return true;
            else
                return false;
        }
        private Boolean OutLineCheck(Point p)
        {
            if (p.X <= 0 || p.Y <= 0 || p.Y >= plate.GetLength(0) - 1 || p.X >= plate.GetLength(1) - 1)
                return true;
            else
                return false;
        }


        private List<Point> getlist(int x, int y)
        {
            List<Point> PointList = new List<Point>();
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
            return PointList;

        }

        private List<Point> checkone(int x, int y, int[,] plate)
        {
            if (OutLineCheck(new Point(x, y)))
                return null;
            List<Point> plist = getSurroundedList(x, y);

            if (plate[y, x] == 0)
                return plist;
            if (plate[y, x] > 0)
            {
                List<Point> returnlist = new List<Point>();

                foreach (var i in plist)
                {
                    if (plate[i.Y, i.X] == -1 || OutLineCheck(i))
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
    }


}

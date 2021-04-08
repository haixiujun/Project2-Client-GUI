using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project2_Client_GUI
{
    public partial class Form3 : Form
    {
        private DataSet dataSet;
        public Form3(DataSet data)
        {
            InitializeComponent();
            dataSet = data;
        }

        //直接在显示时绘图
        private void Form2_Shown(object sender, EventArgs e)
        {
            //获取当前图形
            Graphics g = this.CreateGraphics();

            //创建新原点
            PointF origin = new PointF(40, this.Height - 80);
            //绘制坐标系的初始线
            g.DrawLine(new Pen(Brushes.Black, 2), origin, new PointF(this.Width, this.Height - 80));
            g.DrawLine(new Pen(Brushes.Black, 2), origin, new PointF(40, 0));
            PointF p1;
            PointF p2;
            //绘制刻度
            for (int i = 1; i * 20 + 40 < this.Width; i++)
            {
                p1 = new PointF(40 + i * 20, this.Height - 80);
                p2 = new PointF(40 + i * 20, this.Height - 85);
                g.DrawLine(new Pen(Brushes.Black, 1), p1, p2);
                if (i % 5 == 0)
                {
                    p2 = new PointF(30 + i * 20, this.Height - 75);
                    g.DrawString((i * 20).ToString(), new Font("黑体", 8, FontStyle.Bold), new SolidBrush(Color.Black), p2);
                }
            }
            //绘制刻度下方的字符串
            for (int i = 1; i * 20 + 40 < this.Height; i++)
            {
                g.DrawLine(new Pen(Brushes.Black, 1), new PointF(40, this.Height - 80 - 20 * i), new PointF(45, this.Height - 80 - i * 20));
                if (i % 5 == 0)
                {
                    p2 = new PointF(15, this.Height - 85 - 20 * i);
                    g.DrawString((i * 20).ToString(), new Font("黑体", 8, FontStyle.Bold), new SolidBrush(Color.Black), p2);
                }
            }
            //绘制字符串x和y和原点0
            p2 = new PointF(30, this.Height - 75);
            g.DrawString("0", new Font("黑体", 8, FontStyle.Bold), new SolidBrush(Color.Black), p2);
            p2 = new PointF(this.Width - 30, this.Height - 77);
            g.DrawString("x", new Font("黑体", 8, FontStyle.Bold), new SolidBrush(Color.Black), p2);
            p2 = new PointF(30, 5);
            g.DrawString("y", new Font("黑体", 8, FontStyle.Bold), new SolidBrush(Color.Black), p2);

            //获取数据组个数
            int item_Count = dataSet.get_Item_Sets_Count();
            List<ItemSet> itemSets = dataSet.get_Item_Sets();
            for (int i = 0; i < item_Count; i++)
            {
                ItemSet item = itemSets[i];
                //绘制当前数据组
                for (int j = 0; j < 3; j++)
                {
                    draw_Point(item.get_Profit(j), item.get_Weight(j));
                }
            }
        }
        //在以orign为原点的向上为y轴向右为x轴的坐标系中画点（x，y）
        private void draw_Point(int x, int y)
        {
            Graphics g = this.CreateGraphics();
            int new_X = 40 + x;
            int new_Y = this.Height - 80 - y;
            g.FillEllipse(new SolidBrush(Color.YellowGreen), new_X, new_Y, 4, 4);
        }
    }
}

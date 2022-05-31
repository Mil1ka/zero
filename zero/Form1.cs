using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace zero
{
    public partial class Form1 : Form
    {
        bool PC = false;
        bool Human = false;
        int lastX = 0;
        int lastY = 0;
        Button[,] button;
        string Hod = "X";
        int height = 3;
        int width = 3;
        Random rnd = new Random();
        bool running = true;
        public Form1()
        {
            InitializeComponent();
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            height = Convert.ToInt32(textBox1.Text);
            width = Convert.ToInt32(textBox1.Text);
            button = new Button[width, height];
            int cnt = 0;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    button[i, j] = new Button();
                    button[i, j].Parent = panel1;
                    button[i, j].Width = panel1.Width / width;
                    button[i, j].Height = panel1.Height / height;
                    button[i, j].Top = j * panel1.Height / height;
                    button[i, j].Left = i * panel1.Width / width;
                    button[i, j].Click += new EventHandler(onclick);
                    button[i, j].Tag = cnt;
                    cnt++;
                }
            }
            PC = radioButton1.Checked ? true : false;
            if (PC)
            {
                button[width/2,width/2].Text = Hod;
            }
            button1.Visible = false;
            textBox1.Visible = false;
            label1.Visible = false;
            radioButton1.Visible = false;
            radioButton2.Visible = false;
        }
        public void check(string x)
        {
            bool win = true;
            for (int i = 0; i < width; i++)
            {
                win = true;
                for (int j = 0; j < height; j++)
                {
                    if (button[i, j].Text != x)
                    {
                        win = false;
                    }
                }
                if (win == true)
                {
                    winer(x);
                    break;
                }
            }
            for (int i = 0; i < height; i++)
            {
                win = true;
                for (int j = 0; j < width; j++)
                {
                    if (button[j, i].Text != x)
                    {
                        win = false;
                    }
                }
                if (win == true)
                {
                    winer(x);
                    break;
                }
            }
                win = true;
                for (int j = 0; j < width; j++)
                {
                    if (button[j, j].Text != x)
                    {
                        win = false;
                    }
                }
                if (win == true)
                {
                    winer(x);
                }
                win = true;
                int d = width - 1;
                for (int j = 0; j < width; j++)
                {
                    
                    if (button[j, d].Text != x)
                    {
                        win = false;
                    }
                    d--;
                }
                if (win == true)
                {
                    winer(x);
                }
        }
        public void winer(string x)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    button[i, j].Enabled = false;
       
                }
            }
            MessageBox.Show(x + " ви виграли!");
            running = false;
        }
        public void Draw()
        {
            bool draw = true;
            for (int i = 0; i < width; i++) //gorizontal;
            {
                for (int j = 0; j < height; j++)
                {
                    if (button[i, j].Text.Equals(""))
                    {
                        draw = false;
                    }
                }
            }
            if (draw)
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        button[i, j].Enabled = false;

                    }
                }
                MessageBox.Show(" нічия!");
                running = false;
            }

        }

        private void onclick (object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (b.Text == "")
            {
                
                if (PC == false)
                {
                    b.Text = Hod;
                    check(Hod);
                    Draw();
                    if (Hod == "X")
                        Hod = "0";
                    else
                        Hod = "X";
                }
                else
                {
                    b.Text = "0";
                    check("0");
                    Draw();
                    if (running)
                    {
                        PC_HOD();
                        check("X");
                        Draw();
                    }
                }
            }   
        }
        public void PC_HOD()
        {
            //textBox2.Text = "PC";
            int[] arr = FindLineBigX();
            button[arr[0], arr[1]].Text = "X";
            //textBox2.Text = arr[0].ToString()+"  "+arr[1].ToString();
        }


        public int[] FindLineBigX()
        {

            bool isGorizontalLine = false;
            int gorizont = 0;
            int lineX = 0;
            for (int i = 0; i < width; i++) //gorizontal;
            {
                bool isG = true;
                int dx = 0;
                for (int j = 0; j < height; j++)
                {
                    if (button[i, j].Text.Equals("X"))
                    {
                        dx++;
                    }
                    if (button[i, j].Text.Equals("0"))
                    {
                        isG = false;
                    }
                }
                if (isG == true)
                {
                    isGorizontalLine = true;
                    if (dx >= gorizont)
                    {
                        lineX = i;
                        gorizont = dx;
                    }
                }
            }


            bool isVerticalLine = false;
            int vertical = 0;
            int lineY = 0;
            /*
            for (int i = 0; i < width; i++)  //vertical
            {
                bool isV = true;
                int dy = 0;
                for (int j = 0; j < height; j++)
                {
                    if (button[j, i].Text.Equals("X"))
                    {
                        dy++;
                    }
                    if (button[j, i].Text.Equals("0"))
                    {
                        isV = true;
                    }
                }
                if (isV == true)
                {
                    if (dy >= vertical)
                    {
                        lineY = i;
                        vertical = dy;
                        isVerticalLine = true;
                    }
                }
            }
            */
            int[] arr;
            if (isGorizontalLine == false && isVerticalLine == false)
            {
                arr = FindRandom();
                //textBox3.Text = "FindRandom";
            }
            else
            {
                if (gorizont >= vertical && gorizont != 0)
                {
                    arr = FindCell(lineX, "Gorizontal");
                    //textBox3.Text = "Gorizontal";
                }
                else if (vertical != 0)
                {
                    arr = FindCell(lineY, "Vertical");
                    //textBox3.Text = "Vertical";
                }
                else
                {
                    arr = FindRandom();
                    //textBox3.Text = "FindRandom";
                }
            }
            
            
            return arr;
         
        }
        public int[] FindCell(int line, string orientation)
        {
            int d=45;
            int[] arr = new int[2];
            if (orientation.Equals("Gorizontal"))
            {
                for (int j = 0; j < width; j++)
                {
                    if (button[line, j].Text!="X"&& button[line, j].Text!="0")
                    {
                        d = j;
                    }
                }
                arr[0] = line;
                arr[1] = d;
            }
            else
            {
                for (int j = 0; j < height; j++)
                {
                    if (button[line, j].Text != "X" && button[line, j].Text != "0")
                    {
                        d = j;
                    }
                }
                arr[0] = d;
                arr[1] = line;
            } 
            return arr;
        }

        public int[] FindRandom()
        {
            int x = rnd.Next(0, width);
            int y = rnd.Next(0, height);
            if(!button[x, y].Text.Equals(""))
            {
                return FindRandom();
            }
            else
            {
                int[] arr = new int[2];
                arr[0] = x;
                arr[1] = y;
                return arr;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    button[i, j].Dispose();

                }
            }
            button1.Visible = true;
            textBox1.Visible = true;
            label1.Visible = true;
            radioButton1.Visible = true;
            radioButton2.Visible = true;
            running = true;
            Hod = "X";

        }
    }
}

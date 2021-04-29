using System;
using System.Collections.Generic;

using System.Drawing;

using System.Windows.Forms;

namespace Example2
{
    enum Tool
    {
        Line,
        Rectangle,
        Pen,
        Circle,
        Erase,
        Fill,
        Text,
    }
    public partial class Form1 : Form
    {
        Color currentColor = Color.Black;
        Font drawFont = new Font("Arial", 10);
        SolidBrush drawBrush = new SolidBrush(Color.Black);
        
        Bitmap bitmap = default(Bitmap);
        Graphics graphics = default(Graphics);
        Pen pen = new Pen(Color.Black);
        Pen pen2 = new Pen(Color.White);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        String drawString = "";
        Point prevPoint = default(Point);
        Point currentPoint = default(Point);
        bool isMousePressed = false;
        Tool currentTool = Tool.Pen;
        


        public Form1()
        {
            InitializeComponent();
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(bitmap);
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            
            
            pictureBox1.Image = bitmap;
            graphics.Clear(Color.White);
            openToolStripMenuItem.Click += OpenToolStripMenuItem_Click;
            saveToolStripMenuItem.Click += SaveToolStripMenuItem_Click;

            button3.Select();
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            // Displays a SaveFileDialog so the user can save the Image
            // assigned to Button2.
            saveFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
            saveFileDialog1.Title = "Save an Image File";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Saves the Image via a FileStream created by the OpenFile method.
                System.IO.FileStream fs =
                    (System.IO.FileStream)saveFileDialog1.OpenFile();
                // Saves the Image in the appropriate ImageFormat based upon the
                // File type selected in the dialog box.
                // NOTE that the FilterIndex property is one-based.
                switch (saveFileDialog1.FilterIndex)
                {
                    case 1:
                        bitmap.Save(fs,
                          System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;

                    case 2:
                        bitmap.Save(fs,
                          System.Drawing.Imaging.ImageFormat.Bmp);
                        break;

                    case 3:
                        bitmap.Save(fs,
                          System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                }

                fs.Close();
            }
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
           if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                bitmap = Bitmap.FromFile(openFileDialog1.FileName) as Bitmap;
                pictureBox1.Image = bitmap;
                graphics = Graphics.FromImage(bitmap);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            currentTool = Tool.Line;
        }


        Rectangle GetMRectangle(Point pPoint, Point cPoint)
        {
            return new Rectangle
            {
                X = Math.Min(pPoint.X, cPoint.X),
                Y = Math.Min(pPoint.Y, cPoint.Y),
                Width = Math.Abs(pPoint.X - cPoint.X),
                Height = Math.Abs(pPoint.Y - cPoint.Y)
            };
        }
        Rectangle GetERectangle(Point pPoint, Point cPoint)
        {
            return new Rectangle
            {
                X = Math.Min(pPoint.X, cPoint.X),
                Y = Math.Min(pPoint.Y, cPoint.Y),
                Width = Math.Abs(pPoint.X - cPoint.X)+25,
                Height = Math.Abs(pPoint.Y - cPoint.Y)+25
            };
        }

        private void Fill(Bitmap bmp, Point pt, Color targetColor, Color replacementColor)
        {
            targetColor = bmp.GetPixel(pt.X, pt.Y);
            if (targetColor.ToArgb().Equals(replacementColor.ToArgb()))
            {
                return;
            }

            Stack<Point> pixels = new Stack<Point>();

            pixels.Push(pt);
            while (pixels.Count != 0)
            {
                Point temp = pixels.Pop();
                int y1 = temp.Y;
                while (y1 >= 0 && bmp.GetPixel(temp.X, y1) == targetColor)
                {
                    y1--;
                }
                y1++;
                bool spanLeft = false;
                bool spanRight = false;
                while (y1 < bmp.Height && bmp.GetPixel(temp.X, y1) == targetColor)
                {
                    bmp.SetPixel(temp.X, y1, replacementColor);

                    if (!spanLeft && temp.X > 0 && bmp.GetPixel(temp.X - 1, y1) == targetColor)
                    {
                        pixels.Push(new Point(temp.X - 1, y1));
                        spanLeft = true;
                    }
                    else if (spanLeft && temp.X - 1 == 0 && bmp.GetPixel(temp.X - 1, y1) != targetColor)
                    {
                        spanLeft = false;
                    }
                    if (!spanRight && temp.X < bmp.Width - 1 && bmp.GetPixel(temp.X + 1, y1) == targetColor)
                    {
                        pixels.Push(new Point(temp.X + 1, y1));
                        spanRight = true;
                    }
                    else if (spanRight && temp.X < bmp.Width - 1 && bmp.GetPixel(temp.X + 1, y1) != targetColor)
                    {
                        spanRight = false;
                    }
                    y1++;
                }

            }
            pictureBox1.Refresh();

        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            toolStripStatusLabel1.Text = e.Location.ToString();
            if (isMousePressed)
            {
                
                switch (currentTool)
                {
                    case Tool.Line:
                    case Tool.Rectangle:
                        currentPoint = e.Location;
                        break;
                    case Tool.Pen:
                        prevPoint = currentPoint;
                        currentPoint = e.Location;
                        graphics.DrawLine(pen, prevPoint, currentPoint);
                        break;
                    case Tool.Circle:
                        currentPoint = e.Location;
                        break;
                    case Tool.Erase:
                        prevPoint = currentPoint;
                        currentPoint = e.Location;
                        Rectangle rect = GetERectangle(prevPoint, currentPoint);
                        graphics.FillRectangle(whiteBrush, rect);
                        
                        break;

                    default:
                        break;
                }
                              
                pictureBox1.Refresh();
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            pen.Color = currentColor;
            prevPoint = e.Location;
            currentPoint = e.Location;
            isMousePressed = true;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isMousePressed = false;
            
            switch (currentTool)
            {
                case Tool.Line:
                    graphics.DrawLine(pen, prevPoint, currentPoint);
                    break;
                case Tool.Rectangle:
                    graphics.DrawRectangle(pen, GetMRectangle(prevPoint, currentPoint));
                    break;
                case Tool.Pen:
                    break;
                case Tool.Circle:
                    graphics.DrawEllipse(pen, GetMRectangle(prevPoint, currentPoint));
                    break;
                case Tool.Fill:
                    Fill(bitmap, currentPoint, Color.White, currentColor);
                    break;
                case Tool.Text:
                    drawBrush.Color = currentColor;
                    StringFormat drawFormat = new StringFormat();
                    drawFormat.Alignment = StringAlignment.Center;
                    graphics.DrawString(drawString, drawFont, drawBrush,currentPoint.X,currentPoint.Y, drawFormat);
                    pictureBox1.Refresh();
                    break;
                    
                default:
                    break;
            }
            prevPoint = e.Location;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            switch (currentTool)
            {
                case Tool.Line:
                    e.Graphics.DrawLine(pen, prevPoint, currentPoint);
                    break;
                case Tool.Rectangle:
                    e.Graphics.DrawRectangle(pen, GetMRectangle(prevPoint, currentPoint));
                    break;
                case Tool.Pen:
                    break;
                case Tool.Circle:
                    e.Graphics.DrawEllipse(pen, GetMRectangle(prevPoint, currentPoint));
                    break;
                default:
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            currentTool = Tool.Rectangle;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            currentTool = Tool.Pen;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            currentTool = Tool.Circle;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            currentTool = Tool.Erase;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            currentTool = Tool.Fill;
        }


        private void takeColor_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            currentColor = btn.BackColor;
            pictureBox2.BackColor = currentColor;
        }

        private void button21_BackColorChanged(object sender, EventArgs e)
        {
            PictureBox pic = sender as PictureBox;
            pic.BackColor = currentColor;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            textBox1.Text = Convert.ToString(trackBar1.Value);
            textBox2.Text = Convert.ToString(trackBar2.Value);
            textBox3.Text = Convert.ToString(trackBar3.Value);
            int r, g, b;
            r = Convert.ToInt32(textBox1.Text);
            g = Convert.ToInt32(textBox2.Text);
            b = Convert.ToInt32(textBox3.Text);
            button16.BackColor = Color.FromArgb(r, g, b);
        }

        private void button20_Click(object sender, EventArgs e)
        {
            drawString = textBox4.Text;
            float s = trackBar4.Value;
            drawFont = new Font("Arial", s);
             
            currentTool=Tool.Text;
        }
    }
}

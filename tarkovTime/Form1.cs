namespace tarkovTime
{
    public partial class Form1 : Form
    {
        Point mOff = Point.Empty;
        bool isMouseDown = false;
        bool isDraggable = false;


        public Form1()
        {
            InitializeComponent();

            textBox1.Text = timeUntilRelative(false);
            textBox2.Text = timeUntilRelative(true);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mOff = new Point(-e.X - SystemInformation.FrameBorderSize.Width, -e.Y - SystemInformation.FrameBorderSize.Height);

                isMouseDown = true;
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown && isDraggable)
            {
                Point local_mousePos = Control.MousePosition;
                local_mousePos.Offset(mOff.X, mOff.Y);
                Location = local_mousePos;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            isDraggable = !isDraggable;

            button1.BackColor = !isDraggable ? SystemColors.Control : SystemColors.ActiveCaption;

        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) isMouseDown = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            textBox1.Text = timeUntilRelative(false);
            textBox2.Text = timeUntilRelative(true);
        }
        private long hrs(int num)
        {
            return 1000 * 60 * 60 * num;
        }
        private double realTimeToTarkovTime(bool left)
        {
            var oneDay = hrs(24);
            var russia = hrs(3);

            var offset = russia + (left ? 0 : hrs(12));

            var tarkovTime = ((offset + (DateTime.UtcNow.TimeOfDay.TotalMilliseconds * 7.0)) % oneDay);

            return tarkovTime;
        }
        private string timeUntilRelative(bool left)
        {
            var ts = TimeSpan.FromMilliseconds(realTimeToTarkovTime(left));

            return $"{fixTime(ts.Hours)}:{fixTime(ts.Minutes)}";
        }
        private string fixTime(int h)
        {
            if (h.ToString().Length < 2) return $"0{h}";
            else return h.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TopMost = !TopMost;

            button3.BackColor = !TopMost ? SystemColors.Control : SystemColors.ActiveCaption;
        }
    }
}
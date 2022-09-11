using BRY;

namespace AEUtilSkelton
{
	public partial class Form1 : Form
	{
		// ********************************************************************************
		private Point mousePoint = new Point(0, 0);

		protected override void OnMouseDown(MouseEventArgs e)
		{
			if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
			{
				mousePoint = new Point(e.X, e.Y);
			}
		}
		protected override void OnMouseMove(MouseEventArgs e)
		{
			if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
			{
				int xx = e.X - mousePoint.X;
				int yy = e.Y - mousePoint.Y;
				this.Location = new Point(this.Location.X + xx, this.Location.Y + yy);
			}
		}

		NavBar m_navBar = new NavBar();
		// ********************************************************************************
		public Form1()
		{
			InitializeComponent();
			NavBarSetup();
		}
		// ********************************************************************
		private void NavBarSetup()
		{
			m_navBar.Form = this;
			m_navBar.SizeSet();
			m_navBar.LocSet();
			m_navBar.Show();

		}
		// ********************************************************************
		private void Form1_Load(object sender, EventArgs e)
		{
			PrefFile pf = new PrefFile();
			this.Text = pf.AppName;
			if (pf.Load() == true)
			{
				bool ok = false;
				Rectangle r = pf.GetRect("Bound", out ok);
				if (ok)
				{
					if (PrefFile.ScreenIn(r) == true)
					{
						this.Bounds = r;
					}
				}
			}
			Command(Environment.GetCommandLineArgs().Skip(1).ToArray(),true);
		}
		// ********************************************************************
		private void Form1_FormClosed(object sender, FormClosedEventArgs e)
		{
			PrefFile pf = new PrefFile();
			pf.SetRect("Bound", this.Bounds);
			pf.Save();
		}
		// ********************************************************************
		private void quitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}
		// ********************************************************************
		public void Command(string[] args,bool IsFirst=true)
		{
			string r = "";
			if (args == null || args.Length == 0)
			{
				r = "引数なし";
			}
			else
			{
				foreach (string ehArg in args)
					r += ehArg + "\r\n"; //引数を表示
			}
			textBox1.Text = r;
			textBox1.Select(0, 0);

		}
	}
}
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace BZEditor
{
	public class WindowManagerItemControl : UserControl
	{
		private System.Windows.Forms.Panel panel;
		private System.Windows.Forms.Label lIcon;
		private bool selected = false;
        private System.Windows.Forms.Label lName;
		private System.Windows.Forms.MenuItem miCloseWindow;
		private System.Windows.Forms.ContextMenu contextMenu;
		private Container components = null;
		private System.Windows.Forms.Label lCloseWindow;
		private MouseButtons LastButton;

		public delegate void MouseClickEvent(WindowManagerItemControl sender);
        public event MouseClickEvent MouseClickedEvent;  
	
		public delegate void CloseMenuItemClick(WindowManagerItemControl sender);
		public event CloseMenuItemClick CloseMenuItemClicked;  
	
		public WindowManagerItemControl()
		{
			InitializeComponent();
		}

		public WindowManagerItemControl(ImageList iList)
		{
			InitializeComponent();
			this.lCloseWindow.ImageList = iList;
			this.lCloseWindow.ImageIndex = 0;
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		public string CaptionText
		{
			get {return lName.Text;}
			set {lName.Text = value;}
		}

		public bool Selected
		{
			get
			{
				return this.selected;
			}
			set
			{
				this.selected = value;
				if (this.selected)
				{
					this.BackColor = Color.DarkRed;
					panel.BackColor = SystemColors.Control;
				}
				else
					this.BackColor = SystemColors.Control;
			}
		}

		public Image Icon
		{
			set
			{
				lIcon.Image = value;
			}
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.panel = new System.Windows.Forms.Panel();
            this.lCloseWindow = new System.Windows.Forms.Label();
            this.lName = new System.Windows.Forms.Label();
            this.contextMenu = new System.Windows.Forms.ContextMenu();
            this.miCloseWindow = new System.Windows.Forms.MenuItem();
            this.lIcon = new System.Windows.Forms.Label();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel.Controls.Add(this.lCloseWindow);
            this.panel.Controls.Add(this.lName);
            this.panel.Controls.Add(this.lIcon);
            this.panel.Location = new System.Drawing.Point(3, 2);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(306, 26);
            this.panel.TabIndex = 0;
            this.panel.MouseLeave += new System.EventHandler(this.onMouseLeave);
            this.panel.Click += new System.EventHandler(this.onClick);
            this.panel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.onMouseDown);
            this.panel.MouseEnter += new System.EventHandler(this.onMouseEnter);
            this.panel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.onMouseUp);
            // 
            // lCloseWindow
            // 
            this.lCloseWindow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lCloseWindow.Location = new System.Drawing.Point(286, 5);
            this.lCloseWindow.Name = "lCloseWindow";
            this.lCloseWindow.Size = new System.Drawing.Size(13, 13);
            this.lCloseWindow.TabIndex = 2;
            this.lCloseWindow.MouseLeave += new System.EventHandler(this.lCloseWindow_MouseLeave);
            this.lCloseWindow.Click += new System.EventHandler(this.miCloseWindow_Click);
            this.lCloseWindow.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lCloseWindow_MouseDown);
            this.lCloseWindow.MouseEnter += new System.EventHandler(this.lCloseWindow_MouseEnter);
            this.lCloseWindow.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lCloseWindow_MouseUp);
            // 
            // lName
            // 
            this.lName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lName.Location = new System.Drawing.Point(22, 5);
            this.lName.Name = "lName";
            this.lName.Size = new System.Drawing.Size(258, 15);
            this.lName.TabIndex = 1;
            this.lName.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lName.MouseLeave += new System.EventHandler(this.onMouseLeave);
            this.lName.Click += new System.EventHandler(this.onClick);
            this.lName.MouseDown += new System.Windows.Forms.MouseEventHandler(this.onMouseDown);
            this.lName.MouseEnter += new System.EventHandler(this.onMouseEnter);
            this.lName.MouseUp += new System.Windows.Forms.MouseEventHandler(this.onMouseUp);
            // 
            // contextMenu
            // 
            this.contextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miCloseWindow});
            // 
            // miCloseWindow
            // 
            this.miCloseWindow.Index = 0;
            this.miCloseWindow.Text = "Закрыть окно";
            this.miCloseWindow.Click += new System.EventHandler(this.miCloseWindow_Click);
            // 
            // lIcon
            // 
            this.lIcon.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lIcon.Image = global::BZEditor.Properties.Resources.MapMisc;
            this.lIcon.Location = new System.Drawing.Point(0, 0);
            this.lIcon.Name = "lIcon";
            this.lIcon.Size = new System.Drawing.Size(24, 24);
            this.lIcon.TabIndex = 0;
            this.lIcon.MouseLeave += new System.EventHandler(this.onMouseLeave);
            this.lIcon.Click += new System.EventHandler(this.onClick);
            this.lIcon.MouseDown += new System.Windows.Forms.MouseEventHandler(this.onMouseDown);
            this.lIcon.MouseEnter += new System.EventHandler(this.onMouseEnter);
            this.lIcon.MouseUp += new System.Windows.Forms.MouseEventHandler(this.onMouseUp);
            // 
            // WindowManagerItemControl
            // 
            this.Controls.Add(this.panel);
            this.Name = "WindowManagerItemControl";
            this.Size = new System.Drawing.Size(312, 30);
            this.panel.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private void onMouseEnter(object sender, System.EventArgs e)
		{
			panel.BackColor = SystemColors.ActiveCaption;
			lName.BackColor = SystemColors.ActiveCaption;
		}

		private void onMouseLeave(object sender, System.EventArgs e)
		{
			panel.BackColor = SystemColors.Control;
			lName.BackColor = SystemColors.Control;
		}

		private void onMouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			panel.BackColor = SystemColors.Highlight;	
			lName.BackColor = SystemColors.Highlight;
		}

		private void onMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			LastButton = e.Button;
			panel.BackColor = SystemColors.ActiveCaption;
			lName.BackColor = SystemColors.ActiveCaption;
			if (e.Button == MouseButtons.Right)
				this.contextMenu.Show(this, new Point(e.X, e.Y));
		}

		private void onClick(object sender, System.EventArgs e)
		{
			if (LastButton != MouseButtons.Right)
                MouseClickedEvent(this);
		}

		private void miCloseWindow_Click(object sender, System.EventArgs e)
		{
			if (LastButton == MouseButtons.Left)
				CloseMenuItemClicked(this);
		}

		private void lCloseWindow_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			LastButton = e.Button;
			lCloseWindow.ImageIndex = 2;
		}

		private void lCloseWindow_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			lCloseWindow.ImageIndex = 1;
		}

		private void lCloseWindow_MouseEnter(object sender, System.EventArgs e)
		{
			onMouseEnter(sender, e);
			lCloseWindow.ImageIndex = 1;
		}

		private void lCloseWindow_MouseLeave(object sender, System.EventArgs e)
		{
			onMouseLeave(sender, e);
			lCloseWindow.ImageIndex = 0;
		}
	}
}

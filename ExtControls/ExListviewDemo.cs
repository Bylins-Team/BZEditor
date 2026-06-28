using System;
using System.Windows.Forms;
using System.Collections;
using EXControls;
using System.Drawing;

class MyForm : Form 
{
    
    private EXListView lstv;
    private Button btn;
    private Button btn1;
	private StatusStrip statusstrip1;
	private ToolStripStatusLabel toolstripstatuslabel1;
            
    public MyForm() {
        statusstrip1 = new StatusStrip();
        toolstripstatuslabel1 = new ToolStripStatusLabel();
        btn = new Button();
        btn1 = new Button();
        InitializeComponent();
    }
    
    private void InitializeComponent() {
        //imglst_genre
        ImageList imglst_genre = new ImageList();
        imglst_genre.ColorDepth = ColorDepth.Depth32Bit;
        imglst_genre.Images.Add(Image.FromFile("music.png"));
        imglst_genre.Images.Add(Image.FromFile("love.png"));
        imglst_genre.Images.Add(Image.FromFile("comedy.png"));
        imglst_genre.Images.Add(Image.FromFile("drama.png"));
	    imglst_genre.Images.Add(Image.FromFile("horror.ico"));
	    imglst_genre.Images.Add(Image.FromFile("family.ico"));
        //excmbx_genre
        EXComboBox excmbx_genre = new EXComboBox();
        excmbx_genre.DropDownStyle = ComboBoxStyle.DropDownList;
        excmbx_genre.MyHighlightBrush = Brushes.Goldenrod;
	    excmbx_genre.ItemHeight = 20;
        excmbx_genre.Items.Add(new EXComboBox.EXImageItem(imglst_genre.Images[0], "Music"));
	    excmbx_genre.Items.Add(new EXComboBox.EXImageItem(imglst_genre.Images[1], "Romantic"));
	    excmbx_genre.Items.Add(new EXComboBox.EXImageItem(imglst_genre.Images[2], "Comedy"));
	    excmbx_genre.Items.Add(new EXComboBox.EXImageItem(imglst_genre.Images[3], "Drama"));
	    excmbx_genre.Items.Add(new EXComboBox.EXImageItem(imglst_genre.Images[4], "Horror"));
	    excmbx_genre.Items.Add(new EXComboBox.EXImageItem(imglst_genre.Images[5], "Family"));
        excmbx_genre.Items.Add(new EXComboBox.EXMultipleImagesItem(new ArrayList(new object[] {Image.FromFile("love.png"), Image.FromFile("comedy.png")}), "Romantic comedy"));
        //excmbx_rate
        EXComboBox excmbx_rate = new EXComboBox();
        excmbx_rate.MyHighlightBrush = Brushes.Goldenrod;
	    excmbx_rate.DropDownStyle = ComboBoxStyle.DropDownList;
	    ImageList imglst_rate = new ImageList();
	    imglst_rate.ColorDepth = ColorDepth.Depth32Bit;
	    imglst_rate.Images.Add(Image.FromFile("rate.png"));
	    for (int i = 1; i < 6; i++) {
		    ArrayList _arlst1 = new ArrayList();
		    for (int j = 0; j < i; j++) {
		        _arlst1.Add(imglst_rate.Images[0]);
		    }
		    excmbx_rate.Items.Add(new EXComboBox.EXMultipleImagesItem("", _arlst1, i.ToString()));
	    }
        
        //lstv
        lstv = new EXListView();
        lstv.MySortBrush = SystemBrushes.ControlLight;
        lstv.MyHighlightBrush = Brushes.Goldenrod;
        lstv.GridLines = true;
	    lstv.Location = new Point(10, 40);
	    lstv.Size = new Size(480, 400);
	    lstv.MouseMove += new MouseEventHandler(lstv_MouseMove);
	    lstv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
        //add SmallImageList to ListView - images will be shown in ColumnHeaders
        ImageList colimglst = new ImageList();
        colimglst.Images.Add("down", Image.FromFile("down.png"));
        colimglst.Images.Add("up", Image.FromFile("up.png"));
        colimglst.ColorDepth = ColorDepth.Depth32Bit;
        colimglst.ImageSize = new Size(20, 20); // this will affect the row height
        lstv.SmallImageList = colimglst;
        //add columns and items
        lstv.Columns.Add(new EXEditableColumnHeader("Movie", 120));
        lstv.Columns.Add(new EXColumnHeader("Date", 100));
        lstv.Columns.Add(new EXEditableColumnHeader("Genre", excmbx_genre, 60));
        lstv.Columns.Add(new EXEditableColumnHeader("Rate", excmbx_rate, 100));
        EXBoolColumnHeader boolcol = new EXBoolColumnHeader("Conclusion", 80);
        boolcol.Editable = true;
        boolcol.TrueImage = Image.FromFile("true.png");
        boolcol.FalseImage = Image.FromFile("false.png");
        lstv.Columns.Add(boolcol);
	    string[] movies = new string[] {"Deep blue", "From dusk till dawn", "There's something about Mary", "Dumb and dumber", "The silence of the lambs", "Leaving Las Vegas", "Amadeus", "Platoon", "Four weddings and a funeral", "Kramer vs. Kramer"};
        for (int i = 0; i < movies.Length; i++) {
		    //movie
		    EXListViewItem item = new EXListViewItem(movies[i]);
		    //date
            item.SubItems.Add(new EXListViewSubItem(DateTime.Now.AddDays(i).ToShortDateString()));
		    //genre
            item.SubItems.Add(new EXMultipleImagesListViewSubItem(new ArrayList(new object[] {imglst_genre.Images[1], imglst_genre.Images[2]}), "Romantic comedy"));
            //rate
            item.SubItems.Add(new EXMultipleImagesListViewSubItem(new ArrayList(new object[] {imglst_rate.Images[0]}), "1"));
		    //conclusion
            item.SubItems.Add(new EXBoolListViewSubItem(true));
            lstv.Items.Add(item);
        }
        
        //statusstrip1
        statusstrip1.Items.AddRange(new ToolStripItem[] {toolstripstatuslabel1});
        //btn
        btn.Location = new Point(10, 450);
        btn.Text = "Add image";
        btn.Click += new EventHandler(btn_Click);
	    btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        //btn1
        btn1.AutoSize = true;
        btn1.Location = new Point(100, 450);
	    btn1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        btn1.Text = "Remove image";
        btn1.Click += new EventHandler(btn1_Click);
        //this
        this.ClientSize = new Size(500, 500);
        this.Controls.Add(statusstrip1);
        Label lbl = new Label();
        lbl.Text = "Doubleclick on the subitems to edit...";
        lbl.Bounds = new Rectangle(10, 10, 480, 20);
        this.Controls.Add(lbl);
        this.Controls.Add(lstv);
        this.Controls.Add(btn);
        this.Controls.Add(btn1);
    }
	
	private void lstv_MouseMove(object sender, MouseEventArgs e) {
	    ListViewHitTestInfo lstvinfo = lstv.HitTest(e.X, e.Y);
        ListViewItem.ListViewSubItem subitem = lstvinfo.SubItem;
        if (subitem == null) return;
        if (subitem is EXListViewSubItem) {
		    toolstripstatuslabel1.Text = ((EXListViewSubItem) subitem).MyValue;
	    }
	}
        
    private void btn_Click(object sender, EventArgs e) {
        EXMultipleImagesListViewSubItem subitem = lstv.Items[1].SubItems[2] as EXMultipleImagesListViewSubItem;
        subitem.MyImages.Add(Image.FromFile("love.png"));
        lstv.Invalidate(subitem.Bounds);
    }
        
    private void btn1_Click(object sender, EventArgs e) {
        EXMultipleImagesListViewSubItem subitem = lstv.Items[1].SubItems[2] as EXMultipleImagesListViewSubItem;
        if (subitem == null) return;
        subitem.MyImages.Clear();
        lstv.Invalidate(subitem.Bounds);
    }
    
    public static void Main() {
        Application.EnableVisualStyles();
        Application.Run(new MyForm());
    }
    
}
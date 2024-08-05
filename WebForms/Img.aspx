<%@ Page Language="C#" %>
<%@ Import Namespace="System" %>
<%@ Import Namespace="System.Drawing" %>
<%@ Import Namespace="System.Drawing.Imaging" %>
<%@ Import Namespace="System.Text" %>
<%@ Import Namespace="System.IO" %>

<script runat="server">
    public Random rand = new Random();
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Response.Cache.SetCacheability(HttpCacheability.NoCache);
       

        if (!this.IsPostBack)
        {
            if (this.Session["VerificationString"] == null)
            {
                this.Session["VerificationString"] = GetRandomText();

                //GenerateCaptcha();
                CaptchaImage(null,true);
            }
            else
            {
              
                    this.Session["VerificationString"] = GetRandomText();

                   // GenerateCaptcha();
                    CaptchaImage(null, true);
               
            }
        }
    }
    private void GenerateCaptcha()
    {
        string code = this.Session["VerificationString"].ToString();
         string c = string.Empty;
        for (int i = 0; i < code.Length; i++)
        {
           c += code[i].ToString()+ "";
        }
        //code = code.Insert(1, " ");
        Bitmap bitmap = new Bitmap(145, 45, PixelFormat.Format32bppArgb);
        Graphics g = Graphics.FromImage(bitmap);
        Pen pen = new Pen(Color.Transparent);
        Rectangle rect = new Rectangle(0, 0, 145, 45);
        SolidBrush b = new SolidBrush(Color.Transparent);
        SolidBrush blue = new SolidBrush(Color.Black);
        int counter = 0;

        g.DrawRectangle(pen, rect);
        g.FillRectangle(b, rect);

        for (int i = 0; i < c.Length; i++)
        {
           
            g.DrawString(c[i].ToString(), new Font("smalle", 12 ), blue, new PointF(6 + counter, 6));
            counter += 20;
        }

        //DrawRandomLines(g);
        MemoryStream MemStream = new MemoryStream();
        this.Response.ContentType = "image/png";
        bitmap.Save(MemStream, ImageFormat.Png);
        MemStream.WriteTo(Response.OutputStream);

        blue.Dispose();
        blue = null;

        b.Dispose();
        b = null;

        pen.Dispose();
        pen = null;

        g.Dispose();
        g = null;

        bitmap.Dispose();
        bitmap = null;

        MemStream.Dispose();
        MemStream = null;

    }

    private string GetRandomText()
    {
        String code = null;

        StringBuilder randomText = new StringBuilder();

        if (code == null)
        {
            string alphabets = "ABCDEFGHJKLMNPQRSTUVWXYZ123456789abcdefghijkmnopqrstuvwxyz";

            Random r = new Random();

            for (int i = 1; i <= 6; i++)
            {
                randomText.Append(alphabets[r.Next(alphabets.Length)]);
            }

            code = randomText.ToString();
        }

        randomText = null;

        return code;
    }

    private void DrawRandomLines(Graphics g)
    {
        SolidBrush green = new SolidBrush(Color.DarkGoldenrod);

        for (int i = 0; i < 3; i++)
        {
            g.DrawLines(new Pen(green, 2), GetRandomPoints());
        }

        green.Dispose();
        green = null;
    }

    private Point[] GetRandomPoints()
    {
        Point[] points = { new Point(rand.Next(-99, 999), rand.Next(-99, 999)), new Point(rand.Next(-9, 99), rand.Next(-9, 99)) };

        return points;
    }



    private void CaptchaImage(string prefix, bool noisy = true)
    {
        var rand = new Random((int)DateTime.Now.Ticks);
        //generate new question
        int a = rand.Next(10, 99);
        int b = rand.Next(0, 9);
        var captcha = string.Format("{0} + {1} ", a, b);

        //store answer
        Session["VerificationString" + prefix] = a + b;


        //image stream
        //FileContentResult img = null;

        using (var mem = new MemoryStream())
        using (var bmp = new Bitmap(100, 35))
        using (var gfx = Graphics.FromImage((System.Drawing.Image)bmp))
        {
            gfx.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            gfx.FillRectangle(Brushes.White, new Rectangle(0, 0, bmp.Width, bmp.Height));

            //add noise
            if (noisy)
            {
                int i, r, x, y;
                var pen = new Pen(Color.Yellow);
                for (i = 1; i < 5; i++)
                {
                    pen.Color = Color.FromArgb(
                    (rand.Next(0, 255)),
                    (rand.Next(0, 255)),
                    (rand.Next(0, 255)));

                    r = rand.Next(0, (130 / 3));
                    x = rand.Next(0, 130);
                    y = rand.Next(0, 30);

                    gfx.DrawEllipse(pen, x - r, y - r, r, r);
                }
            }

            //add question
            gfx.DrawString(captcha, new Font("Tahoma", 15), Brushes.Black, 2, 3);

            //render as Jpeg
            bmp.Save(mem, System.Drawing.Imaging.ImageFormat.Jpeg);
            //img = this.File(mem.GetBuffer(), "image/Jpeg");

            this.Response.ContentType = "image/Jpeg";
            mem.WriteTo(Response.OutputStream);
        }

        //return img;
    }
    
    
</script>



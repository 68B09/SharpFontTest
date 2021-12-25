using SharpFont;
using System.Drawing;
using System.Windows.Forms;

namespace SharpFontTest
{
	public partial class Form1 : Form
	{
		private Library sfLib;
		private Face sfFace;
		private Pen penBlue = new Pen(Color.Blue);
		private Pen penRed = new Pen(Color.Red);
		private Pen penGray = new Pen(Color.Gray);

		public Form1()
		{
			InitializeComponent();

			this.sfLib = new Library();

			// 3つ目の引数はファイル内インデックス。
			// MSGOTHIC.TTCには3つのフォントが入っていて、0番目は「ＭＳ ゴシック」。
			// .ttfや.otfの場合はフォントが一つしか入っていないので0を指定する。
			// 使用済みになったFaceはDispose()すること。
			this.sfFace = new Face(this.sfLib
								 , @"C:\WINDOWS\Fonts\MSGOTHIC.TTC"
								 , 0);

			this.sfFace.SelectCharmap(SharpFont.Encoding.Unicode);   // UNICODEマップを使う
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			if (this.DesignMode) {
				return;
			}

			string text = @"abcdefghijklmnopqrstuvwxyz,._あ。、";
			int fontSize = 48;

			// SetPixelSizes()を呼び出してからGetCharIndex()などを呼ばないとエラーになる
			this.sfFace.SetPixelSizes(0, (uint)fontSize);

			this.Draw1(e.Graphics, this.lblBad.Location.X, this.lblBad.Location.Y + this.lblBad.Height, text, fontSize);
			this.Draw2(e.Graphics, this.lblBaseLine.Location.X, this.lblBaseLine.Location.Y + this.lblBaseLine.Height, text, fontSize);
		}

		// ベースラインを考えない
		private void Draw1(Graphics g, int x, int y, string text, int fontSize)
		{
			foreach (char code in text) {
				uint charIndex = this.sfFace.GetCharIndex(code);
				this.sfFace.LoadGlyph(charIndex, LoadFlags.Color | LoadFlags.Render, LoadTarget.Normal);

				this.sfFace.Glyph.RenderGlyph(RenderMode.Mono);

				if (this.sfFace.Glyph.Bitmap.Width > 0) {
					using (Bitmap bitmap = this.sfFace.Glyph.Bitmap.ToGdipBitmap(Color.Black)) {
						g.DrawImage(bitmap, x, y);

						x += bitmap.Width;
					}
				}
			}

			g.DrawLine(this.penBlue, 0, y, 1000, y);
			g.DrawLine(this.penRed, 0, y + fontSize, 1000, y + fontSize);
		}

		// ベースラインを計算する
		private void Draw2(Graphics g, int x, int y, string text, int fontSize)
		{
			// 文字高とディセンダ高からディセンダ率を算出。
			// 次にフォントサイズ×ディセンダ率でディセンダのピクセルサイズを算出。
			// 率を求める理由はFace.DescenderとFace.Heightが(おそらく)仮想サイズであるため。
			// 例えばFace.HeightはSetPixelSizes()で設定した値ではなく"256"だったりする。
			// https://freetype.org/freetype2/docs/tutorial/step2.html
			int descender = (int)(fontSize * ((double)-this.sfFace.Descender / (double)this.sfFace.Height));

			foreach (char code in text) {
				// コードポイントからインデックスを取得
				uint charIndex = this.sfFace.GetCharIndex(code);
				// インデックスからグリフ情報を読み込む
				this.sfFace.LoadGlyph(charIndex, LoadFlags.Color | LoadFlags.Render, LoadTarget.Normal);
				// グリフをレンダリング
				this.sfFace.Glyph.RenderGlyph(RenderMode.Mono);

				if (this.sfFace.Glyph.Bitmap.Width > 0) {
					// Bitmapに変換したレンダリング結果を取得
					using (Bitmap bitmap = this.sfFace.Glyph.Bitmap.ToGdipBitmap(Color.Black)) {
						// y + fontSize = 底辺座標。
						// - descender = 底辺からディセンダを引いてベースライン座標を求める。
						// - HorizontalBearingY = ベースラインから上部の高さを引いて描画座標を求める。
						// この計算で良いかどうかは謎。
						int yy = (int)(y + fontSize - descender - this.sfFace.Glyph.Metrics.HorizontalBearingY);
						g.DrawImage(bitmap, x, yy);

						x += bitmap.Width;
					}
				}
			}

			g.DrawLine(this.penBlue, 0, y, 1000, y);
			g.DrawLine(this.penRed, 0, y + fontSize, 1000, y + fontSize);
			g.DrawLine(this.penGray, 0, y + fontSize - descender, 1000, y + fontSize - descender);
		}
	}
}

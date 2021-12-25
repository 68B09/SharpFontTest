
namespace SharpFontTest
{
	partial class Form1
	{
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.lblBad = new System.Windows.Forms.Label();
			this.lblBaseLine = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lblBad
			// 
			this.lblBad.AutoSize = true;
			this.lblBad.Location = new System.Drawing.Point(12, 9);
			this.lblBad.Name = "lblBad";
			this.lblBad.Size = new System.Drawing.Size(345, 12);
			this.lblBad.TabIndex = 0;
			this.lblBad.Text = "ベースラインを計算しないとカンマやアンダーラインなどが悲惨なことになる。";
			// 
			// lblBaseLine
			// 
			this.lblBaseLine.AutoSize = true;
			this.lblBaseLine.Location = new System.Drawing.Point(12, 121);
			this.lblBaseLine.Name = "lblBaseLine";
			this.lblBaseLine.Size = new System.Drawing.Size(93, 12);
			this.lblBaseLine.TabIndex = 1;
			this.lblBaseLine.Text = "ベースラインを計算";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.lblBaseLine);
			this.Controls.Add(this.lblBad);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblBad;
		private System.Windows.Forms.Label lblBaseLine;
	}
}


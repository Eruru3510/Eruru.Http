
namespace Pixivic {
	partial class Form_Home {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose (bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose ();
			}
			base.Dispose (disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent () {
			this.Panel = new System.Windows.Forms.Panel ();
			this.SuspendLayout ();
			// 
			// Panel
			// 
			this.Panel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
			| System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.Panel.AutoScroll = true;
			this.Panel.Location = new System.Drawing.Point (5, 5);
			this.Panel.Name = "Panel";
			this.Panel.Size = new System.Drawing.Size (790, 440);
			this.Panel.TabIndex = 0;
			this.Panel.Scroll += new System.Windows.Forms.ScrollEventHandler (this.Panel_Scroll);
			this.Panel.MouseWheel += new System.Windows.Forms.MouseEventHandler (this.Panel_MouseWheel);
			// 
			// Form_Home
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF (6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size (800, 450);
			this.Controls.Add (this.Panel);
			this.Name = "Form_Home";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Form_Home";
			this.Activated += new System.EventHandler (this.Form_Home_Activated);
			this.Load += new System.EventHandler (this.Form_Home_Load);
			this.ResumeLayout (false);

		}

		#endregion

		private System.Windows.Forms.Panel Panel;
	}
}
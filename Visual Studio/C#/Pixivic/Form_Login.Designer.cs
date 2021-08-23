
namespace Pixivic {
	partial class Form_Login {
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose (bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose ();
			}
			base.Dispose (disposing);
		}

		#region Windows 窗体设计器生成的代码

		/// <summary>
		/// 设计器支持所需的方法 - 不要修改
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent () {
			this.PictureBox_Captcha = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.TextBox_Account = new System.Windows.Forms.TextBox();
			this.TextBox_Password = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.TextBox_Captcha = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.Button_Login = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.PictureBox_Captcha)).BeginInit();
			this.SuspendLayout();
			// 
			// PictureBox_Captcha
			// 
			this.PictureBox_Captcha.Location = new System.Drawing.Point(5, 55);
			this.PictureBox_Captcha.Name = "PictureBox_Captcha";
			this.PictureBox_Captcha.Size = new System.Drawing.Size(200, 60);
			this.PictureBox_Captcha.TabIndex = 0;
			this.PictureBox_Captcha.TabStop = false;
			this.PictureBox_Captcha.Click += new System.EventHandler(this.PictureBox_Captcha_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(5, 5);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 20);
			this.label1.TabIndex = 1;
			this.label1.Text = "账  号：";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TextBox_Account
			// 
			this.TextBox_Account.Location = new System.Drawing.Point(55, 5);
			this.TextBox_Account.Name = "TextBox_Account";
			this.TextBox_Account.Size = new System.Drawing.Size(150, 21);
			this.TextBox_Account.TabIndex = 2;
			// 
			// TextBox_Password
			// 
			this.TextBox_Password.Location = new System.Drawing.Point(55, 30);
			this.TextBox_Password.Name = "TextBox_Password";
			this.TextBox_Password.PasswordChar = '*';
			this.TextBox_Password.Size = new System.Drawing.Size(150, 21);
			this.TextBox_Password.TabIndex = 4;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(5, 30);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 20);
			this.label2.TabIndex = 3;
			this.label2.Text = "密  码：";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TextBox_Captcha
			// 
			this.TextBox_Captcha.Location = new System.Drawing.Point(55, 120);
			this.TextBox_Captcha.Name = "TextBox_Captcha";
			this.TextBox_Captcha.Size = new System.Drawing.Size(150, 21);
			this.TextBox_Captcha.TabIndex = 6;
			this.TextBox_Captcha.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_Captcha_KeyDown);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(5, 120);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100, 20);
			this.label3.TabIndex = 5;
			this.label3.Text = "验证码：";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// Button_Login
			// 
			this.Button_Login.Location = new System.Drawing.Point(130, 145);
			this.Button_Login.Name = "Button_Login";
			this.Button_Login.Size = new System.Drawing.Size(75, 23);
			this.Button_Login.TabIndex = 7;
			this.Button_Login.Text = "登录";
			this.Button_Login.UseVisualStyleBackColor = true;
			this.Button_Login.Click += new System.EventHandler(this.Button_Login_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(209, 171);
			this.Controls.Add(this.Button_Login);
			this.Controls.Add(this.TextBox_Captcha);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.TextBox_Password);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.TextBox_Account);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.PictureBox_Captcha);
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.PictureBox_Captcha)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox PictureBox_Captcha;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox TextBox_Account;
		private System.Windows.Forms.TextBox TextBox_Password;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox TextBox_Captcha;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button Button_Login;
	}
}


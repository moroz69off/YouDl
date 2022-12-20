﻿
namespace YouDl
{
	partial class MainForm
	{
		/// <summary>
		/// Обязательная переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Код, автоматически созданный конструктором форм Windows

		/// <summary>
		/// Требуемый метод для поддержки конструктора — не изменяйте 
		/// содержимое этого метода с помощью редактора кода.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.input_textBox = new System.Windows.Forms.TextBox();
			this.result_textBox = new System.Windows.Forms.TextBox();
			this.button = new System.Windows.Forms.Button();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.buttonSafe = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// input_textBox
			// 
			this.input_textBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.input_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
			this.input_textBox.Location = new System.Drawing.Point(12, 12);
			this.input_textBox.Name = "input_textBox";
			this.input_textBox.Size = new System.Drawing.Size(494, 24);
			this.input_textBox.TabIndex = 0;
			this.input_textBox.TextChanged += new System.EventHandler(this.input_textBox_TextChanged);
			// 
			// result_textBox
			// 
			this.result_textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.result_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
			this.result_textBox.Location = new System.Drawing.Point(12, 54);
			this.result_textBox.Multiline = true;
			this.result_textBox.Name = "result_textBox";
			this.result_textBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.result_textBox.Size = new System.Drawing.Size(640, 451);
			this.result_textBox.TabIndex = 2;
			// 
			// button
			// 
			this.button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
			this.button.Location = new System.Drawing.Point(512, 12);
			this.button.Name = "button";
			this.button.Size = new System.Drawing.Size(140, 24);
			this.button.TabIndex = 1;
			this.button.Text = "Go!";
			this.button.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.button.UseVisualStyleBackColor = true;
			this.button.Click += new System.EventHandler(this.ButtonGo_Click);
			// 
			// saveFileDialog
			// 
			this.saveFileDialog.Filter = "Video|*.mp4|Audio|*.mp3";
			this.saveFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.SaveFileDialog_FileOk);
			// 
			// buttonSafe
			// 
			this.buttonSafe.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
			this.buttonSafe.Location = new System.Drawing.Point(477, 511);
			this.buttonSafe.Name = "buttonSafe";
			this.buttonSafe.Size = new System.Drawing.Size(175, 43);
			this.buttonSafe.TabIndex = 3;
			this.buttonSafe.Text = "Save as...";
			this.buttonSafe.UseVisualStyleBackColor = true;
			this.buttonSafe.Click += new System.EventHandler(this.ButtonSave_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(664, 575);
			this.Controls.Add(this.buttonSafe);
			this.Controls.Add(this.result_textBox);
			this.Controls.Add(this.button);
			this.Controls.Add(this.input_textBox);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(680, 614);
			this.Name = "MainForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.Text = "YouDl";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox input_textBox;
		private System.Windows.Forms.Button button;
		private System.Windows.Forms.TextBox result_textBox;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.Button buttonSafe;
	}
}


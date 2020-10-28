namespace ld39
{
    partial class Feiv
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Feiv));
            this.grid = new System.Windows.Forms.PictureBox();
            this.grid2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid2)).BeginInit();
            this.SuspendLayout();
            // 
            // grid
            // 
            this.grid.Location = new System.Drawing.Point(96, 149);
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(8, 8);
            this.grid.TabIndex = 0;
            this.grid.TabStop = false;
            this.grid.Click += new System.EventHandler(this.grid_Click);
            this.grid.MouseHover += new System.EventHandler(this.grid_MouseHover);
            // 
            // grid2
            // 
            this.grid2.Location = new System.Drawing.Point(103, 112);
            this.grid2.Name = "grid2";
            this.grid2.Size = new System.Drawing.Size(100, 50);
            this.grid2.TabIndex = 1;
            this.grid2.TabStop = false;
            this.grid2.Click += new System.EventHandler(this.grid_Click);
            this.grid2.MouseHover += new System.EventHandler(this.grid_MouseHover);
            // 
            // Feiv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(124)))), ((int)(((byte)(63)))));
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.grid2);
            this.Controls.Add(this.grid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Feiv";
            this.Text = "Survival on Feiv";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox grid;
        private System.Windows.Forms.PictureBox grid2;
    }
}


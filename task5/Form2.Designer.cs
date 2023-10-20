namespace lab5
{
    partial class Form2
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
            this.pbCanvas = new System.Windows.Forms.PictureBox();
            this.trackBarRoughness = new System.Windows.Forms.TrackBar();
            this.trackBarDisplacement = new System.Windows.Forms.TrackBar();
            this.lblRoughness = new System.Windows.Forms.Label();
            this.lblDisplacement = new System.Windows.Forms.Label();
            this.btnGenerate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbCanvas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRoughness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarDisplacement)).BeginInit();
            this.SuspendLayout();
            // 
            // pbCanvas
            // 
            this.pbCanvas.Location = new System.Drawing.Point(54, 33);
            this.pbCanvas.Name = "pbCanvas";
            this.pbCanvas.Size = new System.Drawing.Size(945, 760);
            this.pbCanvas.TabIndex = 0;
            this.pbCanvas.TabStop = false;
            this.pbCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.pbCanvas_Paint);
            // 
            // trackBarRoughness
            // 
            this.trackBarRoughness.Location = new System.Drawing.Point(1005, 101);
            this.trackBarRoughness.Name = "trackBarRoughness";
            this.trackBarRoughness.Size = new System.Drawing.Size(612, 90);
            this.trackBarRoughness.TabIndex = 1;
            this.trackBarRoughness.Value = 5;
            this.trackBarRoughness.Scroll += new System.EventHandler(this.trackBarRoughness_Scroll);
            // 
            // trackBarDisplacement
            // 
            this.trackBarDisplacement.Location = new System.Drawing.Point(1005, 261);
            this.trackBarDisplacement.Name = "trackBarDisplacement";
            this.trackBarDisplacement.Size = new System.Drawing.Size(612, 90);
            this.trackBarDisplacement.TabIndex = 2;
            this.trackBarDisplacement.Value = 5;
            this.trackBarDisplacement.Scroll += new System.EventHandler(this.trackBarDisplacement_Scroll);
            // 
            // lblRoughness
            // 
            this.lblRoughness.AutoSize = true;
            this.lblRoughness.Location = new System.Drawing.Point(1220, 33);
            this.lblRoughness.Name = "lblRoughness";
            this.lblRoughness.Size = new System.Drawing.Size(121, 25);
            this.lblRoughness.TabIndex = 3;
            this.lblRoughness.Text = "Roughness";
            // 
            // lblDisplacement
            // 
            this.lblDisplacement.AutoSize = true;
            this.lblDisplacement.Location = new System.Drawing.Point(1225, 198);
            this.lblDisplacement.Name = "lblDisplacement";
            this.lblDisplacement.Size = new System.Drawing.Size(142, 25);
            this.lblDisplacement.TabIndex = 4;
            this.lblDisplacement.Text = "Displacement";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(1162, 418);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(286, 58);
            this.btnGenerate.TabIndex = 5;
            this.btnGenerate.Text = "Нарисовать массив";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1814, 805);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.lblDisplacement);
            this.Controls.Add(this.lblRoughness);
            this.Controls.Add(this.trackBarDisplacement);
            this.Controls.Add(this.trackBarRoughness);
            this.Controls.Add(this.pbCanvas);
            this.Name = "Form2";
            this.Text = "midpoint displacement";
            ((System.ComponentModel.ISupportInitialize)(this.pbCanvas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRoughness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarDisplacement)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbCanvas;
        private System.Windows.Forms.TrackBar trackBarRoughness;
        private System.Windows.Forms.TrackBar trackBarDisplacement;
        private System.Windows.Forms.Label lblRoughness;
        private System.Windows.Forms.Label lblDisplacement;
        private System.Windows.Forms.Button btnGenerate;
    }
}
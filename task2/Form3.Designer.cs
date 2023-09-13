namespace lab2
{
    partial class Form3
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
            pbOriginal = new PictureBox();
            numHue = new TrackBar();
            numSaturation = new TrackBar();
            numValue = new TrackBar();
            btnProcess = new Button();
            btnSave = new Button();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)pbOriginal).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numHue).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numSaturation).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numValue).BeginInit();
            SuspendLayout();
            // 
            // pbOriginal
            // 
            pbOriginal.Location = new Point(69, 59);
            pbOriginal.Name = "pbOriginal";
            pbOriginal.Size = new Size(706, 329);
            pbOriginal.TabIndex = 0;
            pbOriginal.TabStop = false;
            // 
            // numHue
            // 
            numHue.Location = new Point(886, 59);
            numHue.Maximum = 180;
            numHue.Minimum = -180;
            numHue.Name = "numHue";
            numHue.Size = new Size(437, 90);
            numHue.TabIndex = 2;
            // 
            // numSaturation
            // 
            numSaturation.Location = new Point(895, 209);
            numSaturation.Maximum = 100;
            numSaturation.Minimum = -100;
            numSaturation.Name = "numSaturation";
            numSaturation.Size = new Size(437, 90);
            numSaturation.TabIndex = 3;
            // 
            // numValue
            // 
            numValue.Location = new Point(886, 325);
            numValue.Maximum = 100;
            numValue.Minimum = -100;
            numValue.Name = "numValue";
            numValue.Size = new Size(437, 90);
            numValue.TabIndex = 4;
            // 
            // btnProcess
            // 
            btnProcess.Location = new Point(895, 435);
            btnProcess.Name = "btnProcess";
            btnProcess.Size = new Size(410, 46);
            btnProcess.TabIndex = 5;
            btnProcess.Text = "Обработать";
            btnProcess.UseVisualStyleBackColor = true;
            btnProcess.Click += btnProcess_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(895, 547);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(410, 46);
            btnSave.TabIndex = 6;
            btnSave.Text = "Сохранить";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // button1
            // 
            button1.Location = new Point(218, 435);
            button1.Name = "button1";
            button1.Size = new Size(410, 46);
            button1.TabIndex = 7;
            button1.Text = "Открыть изображение";
            button1.UseVisualStyleBackColor = true;
            button1.Click += btnOpen_Click;
            // 
            // Form3
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1458, 664);
            Controls.Add(button1);
            Controls.Add(btnSave);
            Controls.Add(btnProcess);
            Controls.Add(numValue);
            Controls.Add(numSaturation);
            Controls.Add(numHue);
            Controls.Add(pbOriginal);
            Name = "Form3";
            Text = "Form3";
            ((System.ComponentModel.ISupportInitialize)pbOriginal).EndInit();
            ((System.ComponentModel.ISupportInitialize)numHue).EndInit();
            ((System.ComponentModel.ISupportInitialize)numSaturation).EndInit();
            ((System.ComponentModel.ISupportInitialize)numValue).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pbOriginal;
        private TrackBar numHue;
        private TrackBar numSaturation;
        private TrackBar numValue;
        private Button btnProcess;
        private Button btnSave;
        private Button button1;
    }
}
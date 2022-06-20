namespace Задание_5._4._2__исполнение_команд_
{
    partial class Form1
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
            this.tbFirstNum = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbSecondNum = new System.Windows.Forms.TextBox();
            this.tbResultMull = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.rtbArray = new System.Windows.Forms.RichTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbNumWords = new System.Windows.Forms.TextBox();
            this.rtbWords = new System.Windows.Forms.RichTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tbNMatrix = new System.Windows.Forms.TextBox();
            this.rtbMatrix = new System.Windows.Forms.RichTextBox();
            this.tbMaxMatrix = new System.Windows.Forms.TextBox();
            this.tbMinMatrix = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.tbNSieveEratosthenes = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.rtbSieveEratosthenes = new System.Windows.Forms.RichTextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbFirstNum
            // 
            this.tbFirstNum.Enabled = false;
            this.tbFirstNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbFirstNum.Location = new System.Drawing.Point(132, 45);
            this.tbFirstNum.Name = "tbFirstNum";
            this.tbFirstNum.Size = new System.Drawing.Size(186, 23);
            this.tbFirstNum.TabIndex = 0;
            this.tbFirstNum.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.tbFirstNum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(24, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Число 1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(24, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Число 2";
            // 
            // tbSecondNum
            // 
            this.tbSecondNum.Enabled = false;
            this.tbSecondNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbSecondNum.Location = new System.Drawing.Point(132, 84);
            this.tbSecondNum.Name = "tbSecondNum";
            this.tbSecondNum.Size = new System.Drawing.Size(186, 23);
            this.tbSecondNum.TabIndex = 4;
            this.tbSecondNum.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            this.tbSecondNum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox2_KeyPress);
            // 
            // tbResultMull
            // 
            this.tbResultMull.Enabled = false;
            this.tbResultMull.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbResultMull.Location = new System.Drawing.Point(132, 122);
            this.tbResultMull.Name = "tbResultMull";
            this.tbResultMull.ReadOnly = true;
            this.tbResultMull.Size = new System.Drawing.Size(186, 23);
            this.tbResultMull.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(24, 122);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Результат";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(8, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 18);
            this.label4.TabIndex = 7;
            this.label4.Text = "Задание 1";
            // 
            // rtbArray
            // 
            this.rtbArray.Enabled = false;
            this.rtbArray.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rtbArray.Location = new System.Drawing.Point(21, 232);
            this.rtbArray.Name = "rtbArray";
            this.rtbArray.ReadOnly = true;
            this.rtbArray.Size = new System.Drawing.Size(297, 72);
            this.rtbArray.TabIndex = 8;
            this.rtbArray.Text = "";
            this.rtbArray.WordWrap = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(12, 163);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 18);
            this.label5.TabIndex = 9;
            this.label5.Text = "Задание 2";
            // 
            // tbNumWords
            // 
            this.tbNumWords.Enabled = false;
            this.tbNumWords.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbNumWords.Location = new System.Drawing.Point(237, 359);
            this.tbNumWords.Name = "tbNumWords";
            this.tbNumWords.ReadOnly = true;
            this.tbNumWords.Size = new System.Drawing.Size(81, 23);
            this.tbNumWords.TabIndex = 10;
            // 
            // rtbWords
            // 
            this.rtbWords.Enabled = false;
            this.rtbWords.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rtbWords.Location = new System.Drawing.Point(21, 393);
            this.rtbWords.Name = "rtbWords";
            this.rtbWords.ReadOnly = true;
            this.rtbWords.Size = new System.Drawing.Size(297, 55);
            this.rtbWords.TabIndex = 11;
            this.rtbWords.Text = "";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(12, 321);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 18);
            this.label6.TabIndex = 12;
            this.label6.Text = "Задание 3";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(18, 359);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(183, 17);
            this.label7.TabIndex = 13;
            this.label7.Text = "Число повторений слов(а)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(406, 15);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 18);
            this.label8.TabIndex = 14;
            this.label8.Text = "Задание 4";
            // 
            // tbNMatrix
            // 
            this.tbNMatrix.Enabled = false;
            this.tbNMatrix.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbNMatrix.Location = new System.Drawing.Point(459, 53);
            this.tbNMatrix.Name = "tbNMatrix";
            this.tbNMatrix.Size = new System.Drawing.Size(91, 23);
            this.tbNMatrix.TabIndex = 15;
            this.tbNMatrix.TextChanged += new System.EventHandler(this.tbNMatrix_TextChanged);
            this.tbNMatrix.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbNMatrix_KeyPress);
            // 
            // rtbMatrix
            // 
            this.rtbMatrix.Enabled = false;
            this.rtbMatrix.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rtbMatrix.Location = new System.Drawing.Point(409, 119);
            this.rtbMatrix.Name = "rtbMatrix";
            this.rtbMatrix.ReadOnly = true;
            this.rtbMatrix.Size = new System.Drawing.Size(319, 185);
            this.rtbMatrix.TabIndex = 16;
            this.rtbMatrix.Text = "";
            this.rtbMatrix.WordWrap = false;
            // 
            // tbMaxMatrix
            // 
            this.tbMaxMatrix.Enabled = false;
            this.tbMaxMatrix.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbMaxMatrix.Location = new System.Drawing.Point(616, 26);
            this.tbMaxMatrix.Name = "tbMaxMatrix";
            this.tbMaxMatrix.ReadOnly = true;
            this.tbMaxMatrix.Size = new System.Drawing.Size(112, 23);
            this.tbMaxMatrix.TabIndex = 17;
            // 
            // tbMinMatrix
            // 
            this.tbMinMatrix.Enabled = false;
            this.tbMinMatrix.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbMinMatrix.Location = new System.Drawing.Point(616, 71);
            this.tbMinMatrix.Name = "tbMinMatrix";
            this.tbMinMatrix.ReadOnly = true;
            this.tbMinMatrix.Size = new System.Drawing.Size(112, 23);
            this.tbMinMatrix.TabIndex = 18;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.Location = new System.Drawing.Point(430, 56);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(16, 17);
            this.label9.TabIndex = 19;
            this.label9.Text = "n";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label10.Location = new System.Drawing.Point(577, 32);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(33, 17);
            this.label10.TabIndex = 20;
            this.label10.Text = "Max";
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label11.Location = new System.Drawing.Point(577, 74);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(30, 17);
            this.label11.TabIndex = 21;
            this.label11.Text = "Min";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label12.Location = new System.Drawing.Point(414, 94);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(66, 17);
            this.label12.TabIndex = 22;
            this.label12.Text = "Матрица";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label13.Location = new System.Drawing.Point(406, 321);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(88, 18);
            this.label13.TabIndex = 23;
            this.label13.Text = "Задание 5";
            // 
            // tbNSieveEratosthenes
            // 
            this.tbNSieveEratosthenes.Enabled = false;
            this.tbNSieveEratosthenes.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbNSieveEratosthenes.Location = new System.Drawing.Point(459, 353);
            this.tbNSieveEratosthenes.Name = "tbNSieveEratosthenes";
            this.tbNSieveEratosthenes.Size = new System.Drawing.Size(91, 23);
            this.tbNSieveEratosthenes.TabIndex = 24;
            this.tbNSieveEratosthenes.TextChanged += new System.EventHandler(this.tbNSieveEratosthenes_TextChanged);
            this.tbNSieveEratosthenes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbNSieveEratosthenes_KeyPress);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label14.Location = new System.Drawing.Point(430, 356);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(16, 17);
            this.label14.TabIndex = 25;
            this.label14.Text = "n";
            // 
            // rtbSieveEratosthenes
            // 
            this.rtbSieveEratosthenes.Enabled = false;
            this.rtbSieveEratosthenes.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rtbSieveEratosthenes.Location = new System.Drawing.Point(409, 393);
            this.rtbSieveEratosthenes.Name = "rtbSieveEratosthenes";
            this.rtbSieveEratosthenes.ReadOnly = true;
            this.rtbSieveEratosthenes.Size = new System.Drawing.Size(319, 55);
            this.rtbSieveEratosthenes.TabIndex = 26;
            this.rtbSieveEratosthenes.Text = "";
            this.rtbSieveEratosthenes.WordWrap = false;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label15.Location = new System.Drawing.Point(18, 202);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(165, 17);
            this.label15.TabIndex = 27;
            this.label15.Text = "Массив без повторений";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 493);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.rtbSieveEratosthenes);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.tbNSieveEratosthenes);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.tbMinMatrix);
            this.Controls.Add(this.tbMaxMatrix);
            this.Controls.Add(this.rtbMatrix);
            this.Controls.Add(this.tbNMatrix);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.rtbWords);
            this.Controls.Add(this.tbNumWords);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.rtbArray);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbResultMull);
            this.Controls.Add(this.tbSecondNum);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbFirstNum);
            this.Name = "Form1";
            this.Text = "Вычисление по запросу";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbFirstNum;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbSecondNum;
        private System.Windows.Forms.TextBox tbResultMull;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox rtbArray;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbNumWords;
        private System.Windows.Forms.RichTextBox rtbWords;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbNMatrix;
        private System.Windows.Forms.RichTextBox rtbMatrix;
        private System.Windows.Forms.TextBox tbMaxMatrix;
        private System.Windows.Forms.TextBox tbMinMatrix;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tbNSieveEratosthenes;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.RichTextBox rtbSieveEratosthenes;
        private System.Windows.Forms.Label label15;
    }
}


namespace ArendaAvto
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
            this.LoginText = new MaterialSkin.Controls.MaterialTextBox2();
            this.PasswordText = new MaterialSkin.Controls.MaterialTextBox2();
            this.EnterButton = new MaterialSkin.Controls.MaterialButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.LoginErrorLabel = new MaterialSkin.Controls.MaterialLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // LoginText
            // 
            this.LoginText.AnimateReadOnly = false;
            this.LoginText.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.LoginText.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.LoginText.Depth = 0;
            this.LoginText.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.LoginText.HideSelection = true;
            this.LoginText.LeadingIcon = null;
            this.LoginText.Location = new System.Drawing.Point(82, 107);
            this.LoginText.MaxLength = 32767;
            this.LoginText.MouseState = MaterialSkin.MouseState.OUT;
            this.LoginText.Name = "LoginText";
            this.LoginText.PasswordChar = '\0';
            this.LoginText.PrefixSuffixText = null;
            this.LoginText.ReadOnly = false;
            this.LoginText.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.LoginText.SelectedText = "";
            this.LoginText.SelectionLength = 0;
            this.LoginText.SelectionStart = 0;
            this.LoginText.ShortcutsEnabled = true;
            this.LoginText.Size = new System.Drawing.Size(250, 48);
            this.LoginText.TabIndex = 2;
            this.LoginText.TabStop = false;
            this.LoginText.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.LoginText.TrailingIcon = null;
            this.LoginText.UseSystemPasswordChar = false;
            // 
            // PasswordText
            // 
            this.PasswordText.AnimateReadOnly = false;
            this.PasswordText.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.PasswordText.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.PasswordText.Depth = 0;
            this.PasswordText.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.PasswordText.HideSelection = true;
            this.PasswordText.LeadingIcon = null;
            this.PasswordText.Location = new System.Drawing.Point(81, 161);
            this.PasswordText.MaxLength = 32767;
            this.PasswordText.MouseState = MaterialSkin.MouseState.OUT;
            this.PasswordText.Name = "PasswordText";
            this.PasswordText.PasswordChar = '\0';
            this.PasswordText.PrefixSuffixText = null;
            this.PasswordText.ReadOnly = false;
            this.PasswordText.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.PasswordText.SelectedText = "";
            this.PasswordText.SelectionLength = 0;
            this.PasswordText.SelectionStart = 0;
            this.PasswordText.ShortcutsEnabled = true;
            this.PasswordText.Size = new System.Drawing.Size(250, 48);
            this.PasswordText.TabIndex = 3;
            this.PasswordText.TabStop = false;
            this.PasswordText.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.PasswordText.TrailingIcon = null;
            this.PasswordText.UseSystemPasswordChar = false;
            // 
            // EnterButton
            // 
            this.EnterButton.AutoSize = false;
            this.EnterButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.EnterButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.EnterButton.Depth = 0;
            this.EnterButton.HighEmphasis = true;
            this.EnterButton.Icon = null;
            this.EnterButton.Location = new System.Drawing.Point(82, 218);
            this.EnterButton.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.EnterButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.EnterButton.Name = "EnterButton";
            this.EnterButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.EnterButton.Size = new System.Drawing.Size(249, 36);
            this.EnterButton.TabIndex = 4;
            this.EnterButton.Text = "Войти";
            this.EnterButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.EnterButton.UseAccentColor = false;
            this.EnterButton.UseVisualStyleBackColor = true;
            this.EnterButton.Click += new System.EventHandler(this.EnterButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.ImageLocation = "";
            this.pictureBox1.Location = new System.Drawing.Point(41, 107);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(35, 34);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.ImageLocation = "";
            this.pictureBox2.Location = new System.Drawing.Point(38, 158);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(35, 34);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 6;
            this.pictureBox2.TabStop = false;
            // 
            // LoginErrorLabel
            // 
            this.LoginErrorLabel.Depth = 0;
            this.LoginErrorLabel.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.LoginErrorLabel.Location = new System.Drawing.Point(82, 71);
            this.LoginErrorLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.LoginErrorLabel.Name = "LoginErrorLabel";
            this.LoginErrorLabel.Size = new System.Drawing.Size(249, 23);
            this.LoginErrorLabel.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 274);
            this.Controls.Add(this.LoginErrorLabel);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.EnterButton);
            this.Controls.Add(this.PasswordText);
            this.Controls.Add(this.LoginText);
            this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "Form1";
            this.Sizable = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "                                 Вход";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private MaterialSkin.Controls.MaterialTextBox2 LoginText;
        private MaterialSkin.Controls.MaterialTextBox2 PasswordText;
        private MaterialSkin.Controls.MaterialButton EnterButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private MaterialSkin.Controls.MaterialLabel LoginErrorLabel;
    }
}


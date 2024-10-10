namespace IsoContainerPlayback.TestClient
{
    partial class PickerForm
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
            listBox = new ListBox();
            buttonOpen = new Button();
            buttonCancel = new Button();
            SuspendLayout();
            // 
            // listBox
            // 
            listBox.BorderStyle = BorderStyle.None;
            listBox.FormattingEnabled = true;
            listBox.ItemHeight = 15;
            listBox.Location = new Point(12, 12);
            listBox.Name = "listBox";
            listBox.Size = new Size(207, 210);
            listBox.TabIndex = 0;
            listBox.SelectedValueChanged += listBox_SelectedValueChanged;
            listBox.DoubleClick += listBox_DoubleClick;
            // 
            // buttonOpen
            // 
            buttonOpen.Location = new Point(144, 233);
            buttonOpen.Name = "buttonOpen";
            buttonOpen.Size = new Size(75, 23);
            buttonOpen.TabIndex = 1;
            buttonOpen.Text = "Open";
            buttonOpen.UseVisualStyleBackColor = true;
            buttonOpen.Click += buttonOpen_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.Location = new Point(63, 233);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(75, 23);
            buttonCancel.TabIndex = 2;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // PickerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(231, 268);
            Controls.Add(buttonCancel);
            Controls.Add(buttonOpen);
            Controls.Add(listBox);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "PickerForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "PickerForm";
            ResumeLayout(false);
        }

        #endregion

        private ListBox listBox;
        private Button buttonOpen;
        private Button buttonCancel;
    }
}
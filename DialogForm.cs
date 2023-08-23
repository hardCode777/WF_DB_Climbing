using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WF_DB_Climbing
{
    public abstract partial class DialogForm : Form
    {
        protected const int lenght = 200;
        protected const int shift = 20;

        protected Button ok = null;
        protected Button cancel = null;
        protected Panel panel = null;
        
        public DialogForm()
        {
            InitializeComponent();
            //
            panel = CreateButtons();
            this.Controls.Add(panel);
            //
            SetElements(); //+ sizes of form
            SetForm();
        }
        abstract public void SetElements();
        private void SetForm()
        {   
            //this.ClientSize = new Size(textBox.Right + shift, ok.Bottom + shift);
            this.AcceptButton = ok;         //biding with Enter!!!
            this.CancelButton = cancel;     //biding with escape!!!
            this.StartPosition = FormStartPosition.CenterParent;
        }
        private Panel CreateButtons()
        {
            panel = new()
            {
                Dock = DockStyle.Top,
                BorderStyle = BorderStyle.Fixed3D
            };

            ok = new() 
            {
            Text = "OK",
            Width = 60,
            Top = shift,
            Left = shift
            };
            panel.Controls.Add(ok);
            ok.Click += Ok_Click;

            cancel = new()
            {
                Text = "Cancel",
                Width = 60,
                Top = ok.Top,
                Left = ok.Right+(shift*4),
                DialogResult = DialogResult.Cancel
            };
            panel.Controls.Add(cancel);
            panel.Height = ok.Height + (shift * 2);
            return panel;
            //cancel.Click += Cancel_Click;
            //SetControls();
        }
        public void Ok_Click(object sender, EventArgs e)
        {
            if (CheckElements())
            {
                this.DialogResult = DialogResult.OK;
            }

            //if (String.IsNullOrWhiteSpace(textBox.Text))
            //{
            //    MessageBox.Show("Enter value into texbox");
            //}
            //else
            //{
            //    this.DialogResult = DialogResult.OK;
            //}
        }
        abstract public bool CheckElements ();
       
    }
}

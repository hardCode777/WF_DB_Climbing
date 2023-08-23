using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Data;

namespace WF_DB_Climbing
{
    class ContriesDialog : DialogForm
    {
        private TextBox textBox;
        public string CountryName {
            get
            { return textBox.Text.Trim(); }
            set
            {
                textBox.Text = value;
            }
        }

        public ContriesDialog() : base ()
        {

        }
     
        public override void SetElements()
        {
            textBox = new()
            {
                Width = lenght,
                Top = panel.Bottom + shift,
                Left = shift
            };
            this.Controls.Add(textBox);
            this.ClientSize = new Size(textBox.Right + shift, textBox.Bottom + shift);
        }
        public override bool CheckElements()
        {
            if (String.IsNullOrWhiteSpace(textBox.Text))
            {
                MessageBox.Show("Enter value into texbox");
                return false;
            }
            return true;
        }
    }
    class DistrictDialog : DialogForm
    {   
        private TextBox textBox;
        private ListBox listBox;
        DataTable table;
        public int CountryId
        {
            get
            {   
                return int.Parse(listBox.SelectedValue.ToString());
            }
            set
            {   
                listBox.SelectedValue = value;
            }
        }
        public string DistrictName
        {
            get
            { return textBox.Text.Trim(); }
            set
            {
                textBox.Text = value;
            }
        }
        public DistrictDialog(DataTable table) : base()
        {
            this.table = table;
            listBox.DataSource = table;
            listBox.DisplayMember = "Name";
            listBox.ValueMember = "Id";
        }
        public override void SetElements()
        {
            textBox = new()
            {
                Width = lenght,
                Top = panel.Bottom + shift,
                Left = shift
            };
            listBox = new()
            {
                Dock = DockStyle.Right,
                Width = textBox.Width,
                ////DataSource = table,
                //DisplayMember = "Name",
                //ValueMember = "Id"
            };
            this.Controls.Add(listBox);
            this.Controls.Add(textBox);
            this.ClientSize = new Size(textBox.Width*2+shift*2, textBox.Bottom + shift);
        }
        public override bool CheckElements()
        {
            if (String.IsNullOrWhiteSpace(textBox.Text))
            {
                MessageBox.Show("Enter value into texbox");
                return false;
            }
            if (listBox.SelectedIndex == -1)
            {
                MessageBox.Show("Select value from listbox");
                return false;
            }
            return true;
        }
        //public override void SetControls()
        //{
        //    textBox = new()
        //    {
        //        Width = lenght,
        //        Top = ok.Bottom + shift,
        //        Left = shift
        //    };
        //    this.Controls.Add(textBox);
        //    listBox = new()
        //    {
        //        Dock = (DockStyle)Right,
        //        Width = textBox.Width / 2
        //    };
        //    this.Controls.Add(listBox);
        //}
        //public override void Ok_Click(object sender, EventArgs e)
        //{
        //    if (String.IsNullOrWhiteSpace(textBox.Text))
        //    {
        //        MessageBox.Show("Enter value into texbox");
        //    }
        //    else
        //    {
        //        this.DialogResult = DialogResult.OK;
        //    }
        //}
    }
}



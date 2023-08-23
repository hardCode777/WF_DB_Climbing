using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using System.Configuration;

namespace WF_DB_Climbing
{
    public enum TablesList
    {
        Country,
        District
    }
    public partial class Form1 : Form
    {  
        //
        SqlConnection sqlConnection = null;
        //
        DataTable tableCounties = null;
        public static DataSet tableSet = new DataSet();
        int tablesCount = 6;
        SqlDataAdapter[] adapters;
        //
        TabControl tabControl = null;
        SqlDataAdapter adapterCounties = null;
        //SqlCommandBuilder commandBuilder = null;
        DataGridView gridCounties = null;
        public Form1()
        {
            InitializeComponent();
            
            tabControl = new TabControl();
            tabControl.Dock = DockStyle.Fill;
            this.Controls.Add(tabControl);
            SetMenu();

            this.Load += Form1_Load;
            this.FormClosed += Form1_FormClosed;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {  
                if (adapters!=null)
                {
                    for (int i = 0; i < adapters.Length; i++)
                    {
                        new SqlCommandBuilder(adapters[i]);
                        adapters[i].Update(tableSet.Tables[i]);
                    }
                    //adapterCounties.Update(tableCounties);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["myDb"].ConnectionString);
            adapters = new SqlDataAdapter[]
            { 
            new SqlDataAdapter("select * from Country", sqlConnection),
            new SqlDataAdapter("select * from District", sqlConnection),
             };
            for (int i = 0; i < adapters.Length; i++)
            {
                adapters[i].Fill(tableSet, i.ToString());
            }
            //sqlConnection.Open();
            //if (sqlConnection.State!=null)
            //{
            //    MessageBox.Show("Ok");
            //}
        }

        private void SetMenu()
        {
            // main Menustrip
            MenuStrip menu = new MenuStrip();
            menu.Dock = DockStyle.Top;
            this.Controls.Add(menu);
            // View Menu
            ToolStripMenuItem viewMenu = new ToolStripMenuItem("View");
            menu.Items.Add(viewMenu);
            // ! create and adding temporary TestView in menu View !
            ToolStripMenuItem testView = new ToolStripMenuItem("TestView");
            testView.Click += TestView_Handler;
            viewMenu.DropDownItems.Add(testView);
            //
            ToolStripMenuItem counties = new ToolStripMenuItem("Counties");
            counties.Click += (s, e) => CreateTabPage("Country", tableSet.Tables[(int)TablesList.Country]);
            ToolStripMenuItem mountains = new ToolStripMenuItem("Mountains");
            //mountains.Click += (s, e) => CreateTabPage("Mountain");
            ToolStripMenuItem districts = new ToolStripMenuItem("Districts");
            districts.Click += (s, e) => CreateTabPage("District", tableSet.Tables[(int)TablesList.District]);
            ToolStripMenuItem climbers = new ToolStripMenuItem("Climbers");
            //climbers.Click += (s, e) => CreateTabPage("Climber");
            ToolStripMenuItem climbings = new ToolStripMenuItem("Climbings");
            //climbings.Click += (s, e) => CreateTabPage("Climbing");
            ToolStripMenuItem climbingAndclimber = new ToolStripMenuItem("Climbing_And_Climber");
            //climbingAndclimber.Click += (s, e) => CreateTabPage("ClimbingClimber");
            ToolStripMenuItem tabClose = new ToolStripMenuItem("Tab_Close");
            tabClose.Click += TabClose_Click;
            // add command to menu View
            viewMenu.DropDownItems.AddRange(new ToolStripItem[]
            {   new ToolStripSeparator(),
                counties,
                districts,
                mountains, 
                new ToolStripSeparator(), 
                climbers,
                climbings,
                climbingAndclimber, 
                new ToolStripSeparator (),
                tabClose}); 
            //Edit Menu with commands
            ToolStripMenuItem editMenu = new ToolStripMenuItem("Edit");
            menu.Items.Add(editMenu);
            ToolStripMenuItem insert = new ToolStripMenuItem("Insert");
            editMenu.DropDownItems.Add(insert);
            insert.Click += Insert_Click;
            ToolStripMenuItem delete = new ToolStripMenuItem("Delete");
            editMenu.DropDownItems.Add(delete);
            delete.Click += Delete_Click;
            ToolStripMenuItem update = new ToolStripMenuItem("Update");
            editMenu.DropDownItems.Add(update);
            update.Click += Update_Click;
        }

        private void TabClose_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab is not null)
            {
                tabControl.TabPages.Remove(tabControl.SelectedTab);
            }
        }


        private void Update_Click(object sender, EventArgs e)
        {
            if (tabControl.TabPages.Count > 0)
            {
                (tabControl.SelectedTab.Controls[0] as TabContent).DataUpdate();
            }
            //if (gridCounties.SelectedRows.Count > 0)
            //{
            //    try
            //    {
            //        int selectedIndex = gridCounties.SelectedRows[0].Index;
            //        DataRow? selectedRow = (gridCounties?.Rows[selectedIndex]?.DataBoundItem as DataRowView)?.Row;
            //        if (selectedRow != null)
            //        {
            //            DialogForm updateDialog = new DialogForm();
            //            updateDialog.textBox.Text = selectedRow["Name"].ToString();
            //            if (updateDialog.ShowDialog()==DialogResult.OK)
            //            {
            //                selectedRow["Name"] = updateDialog.textBox.Text;
            //                gridCounties.Refresh();
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    }
            // }
        }
        private void Delete_Click(object sender, EventArgs e)
        {
            if (tabControl.TabPages.Count > 0)
            {
                //MessageBox.Show(tabControl.SelectedTab.Text);
                (tabControl.SelectedTab.Controls[0] as TabContent).Delete();
            }
            //if(gridCounties.SelectedRows.Count > 0)
            //{
            //    try
            //    {
            //        int selectedIndex = gridCounties.SelectedRows[0].Index;
            //        DataRow? selectedRow = (gridCounties?.Rows[selectedIndex]?.DataBoundItem as DataRowView)?.Row;
            //        if (selectedRow != null)
            //        {
            //            if (selectedRow.RowState == DataRowState.Added)
            //            {
            //                tableCounties.Rows.Remove(selectedRow);
            //            }
            //            else
            //            {
            //                selectedRow.Delete();
            //            }
            //            gridCounties.Refresh();
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message, "Delete");
            //    }
            //}
        }
        //private DataRow GetDataRow(int gridIndex)
        //{
        //    DataRowView selectedRowView = gridCounties.Rows[gridIndex].DataBoundItem as DataRowView; // ?DataBoundItem
        //                                                                                             // ?DataRowView
        //    if (selectedRowView != null)
        //    {
        //        return selectedRowView.Row;
        //    }
        //    else return null;
        //}

        private void Insert_Click(object sender, EventArgs e)
        {
            if (tabControl.TabPages.Count>0)
            {
                //MessageBox.Show(tabControl.SelectedTab.Text);
                (tabControl.SelectedTab.Controls[0] as TabContent).Insert();
            }
            //DialogForm insertDialog = new DialogForm();
            //if (tableCounties != null && insertDialog.ShowDialog() == DialogResult.OK)
            //{   
            //    try
            //    {   
            //        DataRow row = tableCounties.NewRow();
            //        row["Name"] = insertDialog.textBox.Text.Trim();
            //        tableCounties.Rows.Add(row);
            //        gridCounties.Refresh();
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    }
            //}
        }
        private void CreateTabPage(string tabKey, DataTable dataTable)
        {
            if (!tabControl.TabPages.ContainsKey(tabKey))
            {
                tabControl.TabPages.Add(tabKey, tabKey);
                TabContent content = new TabContent(tabKey, dataTable);
                tabControl.TabPages[tabKey].Controls.Add(content);
            }
                tabControl.SelectTab(tabControl.TabPages[tabKey]);
        }

        // TestView Handler
        private void TestView_Handler(object sender, EventArgs e)
        {
            TabPage tabCounties = new TabPage("Counties");
            tabControl.Controls.Add(tabCounties);
            gridCounties = new DataGridView();
            gridCounties.AllowUserToAddRows = false;
            gridCounties.AllowUserToDeleteRows = false;
            gridCounties.ReadOnly = true;
            gridCounties.Dock = DockStyle.Fill;
            gridCounties.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            tabCounties.Controls.Add(gridCounties);

            adapterCounties = new SqlDataAdapter("Select * from Country", sqlConnection);
            new SqlCommandBuilder(adapterCounties);
            tableCounties = new DataTable();
            adapterCounties.Fill(tableCounties);
            gridCounties.DataSource = tableCounties;
        }
    }
}

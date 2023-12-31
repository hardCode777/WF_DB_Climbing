﻿using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WF_DB_Climbing
{
    abstract class TabContent : Panel
    {
       protected DialogForm dialogForm = null; 
       DataTable table;
       DataGridView grid;
       protected DataRow dataRow; 
       string tableName;

        public TabContent( string tableName, DataTable table)
        {
            this.Dock = DockStyle.Fill;
            grid = new DataGridView();
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.ReadOnly = true;
            grid.Dock = DockStyle.Fill;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.Controls.Add(grid);
            //adapter = new SqlDataAdapter($"Select * from {tableName}", connection);
            //new SqlCommandBuilder(adapter);
            this.table = table;
            //adapter.Fill(table);
            grid.DataSource = table;
            this.tableName = tableName;
        }
        public void Insert()
        {
            //dialogForm = null;
            //switch (tableName)
            //{
            //    case "Country":
            //        dialogForm = new ContriesDialog();
            //        break;
            //    case "District":
            //        dialogForm = new DistrictDialog(Form1.tableSet.Tables[(int)TablesList.Country]);
            //        break;
            //}
            ClearDialogData();
            if (table != null && dialogForm.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                    dataRow = table.NewRow();
                    GetDialogData();
                    //switch (tableName)
                    //{
                    //    case "Country":
                    //        row["Name"] = ((ContriesDialog)dialogForm).CountryName;
                    //        break;
                    //    case "District":
                    //        row["Name"] = ((DistrictDialog)dialogForm).DistrictName;
                    //        row["CountryId"] = ((DistrictDialog)dialogForm).CountryId;
                    //        break;
                    //}
                    table.Rows.Add(dataRow);
                        grid.Refresh();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

            
        }
        public void Delete()
        {
            if (grid.SelectedRows.Count > 0)
            {
                try
                {
                    int selectedIndex = grid.SelectedRows[0].Index;
                    DataRow? selectedRow = (grid?.Rows[selectedIndex]?.DataBoundItem as DataRowView)?.Row;
                    if (selectedRow != null)
                    {
                        if (selectedRow.RowState == DataRowState.Added)
                        {
                            table.Rows.Remove(selectedRow); //!!!whole deleting
                        }
                        else
                        {
                            selectedRow.Delete();
                        }
                        grid.Refresh();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Something went wrong !");
                }
            }
        }
        public  void DataUpdate()
        {
            if (grid.SelectedRows.Count > 0)
            {
                try
                {
                    int selectedIndex = grid.SelectedRows[0].Index;
                    dataRow = (grid?.Rows[selectedIndex]?.DataBoundItem as DataRowView)?.Row;
                    if (dataRow != null)
                    {
                        SetDialogData();
                        //DialogForm dialogForm = null;
                        //switch (tableName)
                        //{
                        //    case "Country":
                        //        dialogForm = new ContriesDialog();
                        //        ((ContriesDialog)dialogForm).CountryName = ["Name"].ToString();
                        //        break;
                        //    case "District":
                        //        dialogForm = new DistrictDialog(Form1.tableSet.Tables[(int)TablesList.Country]);
                        //        ((DistrictDialog)dialogForm).DistrictName = selectedRow["Name"].ToString();
                        //        ((DistrictDialog)dialogForm).CountryId = int.Parse(selectedRow["CountryId"].ToString());
                        //        break;
                        //}
                        if (table != null && dialogForm.ShowDialog() == DialogResult.OK)
                        {
                            try
                            {
                                GetDialogData();
                                //switch (tableName)
                                //{
                                //    case "Country":
                                //        selectedRow["Name"] = ((ContriesDialog)dialogForm).CountryName;
                                //        break;
                                //    case "District":
                                //        selectedRow["Name"] = ((DistrictDialog)dialogForm).DistrictName;
                                //        selectedRow["CountryId"] = ((DistrictDialog)dialogForm).CountryId;
                                //        break;
                                //}
                                
                                grid.Refresh();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }

                        //DialogForm updateDialog = new DialogForm();
                        //updateDialog.textBox.Text = selectedRow["Name"].ToString();
                        //if (updateDialog.ShowDialog() == DialogResult.OK)
                        //{
                        //    selectedRow["Name"] = updateDialog.textBox.Text;
                        //    gridCounties.Refresh();
                        //}
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        // abstract classes
        protected abstract void GetDialogData(); 
        protected abstract void SetDialogData(); 
        protected abstract void ClearDialogData(); 
    }
}

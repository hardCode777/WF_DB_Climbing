using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace WF_DB_Climbing
{

    class CountryContent: TabContent
    {
        public CountryContent(string table, DataTable data) : base(table, data)
        {
            dialogForm = new ContriesDialog();
        }
        protected override void GetDialogData()
        {
            dataRow["Name"] = ((ContriesDialog)dialogForm).CountryName;
        }
        protected override void SetDialogData()
        {
            ((ContriesDialog)dialogForm).CountryName = dataRow["Name"].ToString();
        }
        protected override void ClearDialogData()
        {
            ((ContriesDialog)dialogForm).CountryName = String.Empty;
        }
    }
    class DistrictContent: TabContent
    {
        public DistrictContent(string table, DataTable data, DataTable countiesTable) : base(table, data)
        {
            dialogForm = new DistrictDialog(countiesTable);
        }
        protected override void GetDialogData()
        {
            dataRow["Name"] = ((DistrictDialog)dialogForm).DistrictName;
            dataRow["CountryId"] = ((DistrictDialog)dialogForm).CountryId;
        }
        protected override void SetDialogData()
        {
            ((DistrictDialog)dialogForm).DistrictName = dataRow["Name"].ToString();
            ((DistrictDialog)dialogForm).CountryId = int.Parse(dataRow["CountryId"].ToString());
        }
        protected override void ClearDialogData()
        {
            ((DistrictDialog)dialogForm).DistrictName = String.Empty;
        }
    }
}

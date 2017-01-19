using Core.Common.Enums;
using Inventory.Contracts.Inventory;
using Inventory.Data.Inventory;
using Inventory.Repositories.Inventory;
using Inventory.ViewModels.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Inventory.PeopleViewer.Inventory
{
    public partial class Transfer : System.Web.UI.Page
    {
        IAdministrationRepository _AdministrationRepository = new AdministrationRepository();
        ITransferRepository _TransferRepository = new TransferRepository(new InventoryContext());

        List<WareHouseVM> _Warehouses = null;
        List<RackVM> _Racks = null;
        List<ShelfVM> _Shelves = null;
        List<TransferVM> _Transfers = new List<TransferVM>();

        public string[] SerialNumbers { get; set; }
        public string SerialNumber { get; set; } = string.Empty;
        public int RowIndex { get; set; } = 1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindRacks();
                BindWarehouses();
                BindShelves();
            }
        }

        private void BindRacks()
        {
            _Racks = _AdministrationRepository.GetAllRacks();
            ddlToRacks.DataSource = ddlFromRacks.DataSource = _Racks;
            ddlToRacks.DataBind();
            ddlFromRacks.DataBind();
            ddlFromRacks.Items.Insert(0, new ListItem("-- Select Rack --", "0"));
            ddlToRacks.Items.Insert(0, new ListItem("-- Select Rack --", "0"));
        }

        private void BindShelves()
        {
            _Shelves = _AdministrationRepository.GetAllShelves();
            ddlFromshelves.DataSource = ddlToshelves.DataSource = _Shelves;
            ddlToshelves.DataBind();
            ddlFromshelves.DataBind();
            ddlFromshelves.Items.Insert(0, new ListItem("-- Select Shelf --", "0"));
            ddlToshelves.Items.Insert(0, new ListItem("-- Select Shelf --", "0"));
        }

        private void BindWarehouses()
        {
            _Warehouses = _AdministrationRepository.GetAllWarehouses();
            ddlFromWarehouses.DataSource = ddlToWarehouses.DataSource = _Warehouses;
            ddlToWarehouses.DataBind();
            ddlFromWarehouses.DataBind();
            ddlFromWarehouses.Items.Insert(0, new ListItem("-- Select Warehouse --", "0"));
            ddlToWarehouses.Items.Insert(0, new ListItem("-- Select Warehouse --", "0"));
        }

        protected void linkButtonSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtItemDescription.Text) || string.IsNullOrWhiteSpace(hiddenFieldItemID.Value))
                throw new ApplicationException("Item description  field is required.");
            _Transfers = _TransferRepository.SearchTransfers(int.Parse(hiddenFieldItemID.Value));
            GridTransferHistory.Visible = true;
            GridTransferHistory.DataSource = _Transfers;
            GridTransferHistory.DataBind();
        }

        protected void linkButtonReset_Click(object sender, EventArgs e)
        {
            ClearFormData();
        }

        private void ClearFormData()
        {
            try
            {
                txtTransferredDate.Text = hiddenFieldItemID.Value = txtSerialNo.Text = txtItemDescription.Text = string.Empty;
                ddlFromRacks.ClearSelection();
                ddlToRacks.ClearSelection();
                ddlFromshelves.ClearSelection();
                ddlToshelves.ClearSelection();
                ddlFromWarehouses.ClearSelection();
                ddlToWarehouses.ClearSelection();
                _Transfers.Clear();
                ViewState.Clear();
            }
            catch (Exception)
            {
                ucInformation.ShowErrorMessage();
            }
        }

        protected void linkButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateTransfer();
                SerialNumbers = txtSerialNo.Text.Trim().Split(Environment.NewLine.ToCharArray());
                PrepareTransferData();
                _TransferRepository.SaveTransfers(_Transfers);
                ucInformation.ShowSaveInfomationMessage();
                ClearFormData();
            }
            catch (ApplicationException Ae)
            {
                ucInformation.ShowErrorMessage(Ae.Message);
            }
            catch (Exception)
            {
                ucInformation.ShowErrorMessage();
            }
        }

        private void PrepareTransferData()
        {
            var transfers = SerialNumbers.Select(sr => new TransferVM()
            {
                EntityState = ObjectState.Added,
                FromRackID = int.Parse(ddlFromRacks.SelectedValue),
                FromShelfID = int.Parse(ddlFromshelves.SelectedValue),
                FromWarehouseID = int.Parse(ddlFromWarehouses.SelectedValue),
                ToRackID = int.Parse(ddlToRacks.SelectedValue),
                ToShelfID = int.Parse(ddlToshelves.SelectedValue),
                ToWarehouseID = int.Parse(ddlToWarehouses.SelectedValue),
                TransferDate = Convert.ToDateTime(txtTransferredDate.Text),
                ItemID = int.Parse(hiddenFieldItemID.Value),
                SerialNo = sr
            });
            _Transfers.AddRange(transfers);
        }

        private void ValidateTransfer()
        {
            if (string.IsNullOrWhiteSpace(txtItemDescription.Text) || string.IsNullOrWhiteSpace(hiddenFieldItemID.Value))
                throw new ApplicationException("Item description  field is required.");
            if (string.IsNullOrWhiteSpace(txtSerialNo.Text))
                throw new ApplicationException("Serial no's  field is required.");
            if (string.IsNullOrWhiteSpace(txtTransferredDate.Text))
                throw new ApplicationException("Transfer date field is required.");
            if (ddlFromRacks.SelectedIndex == 0)
                throw new ApplicationException("From Rack field is required");
            if (ddlToRacks.SelectedIndex == 0)
                throw new ApplicationException("To Rack field is required");
            if (ddlFromshelves.SelectedIndex == 0)
                throw new ApplicationException("From shelf field is required");
            if (ddlToshelves.SelectedIndex == 0)
                throw new ApplicationException("To shelf field is required");
            if (ddlFromWarehouses.SelectedIndex == 0)
                throw new ApplicationException("From warehouse field is required");
            if (ddlToWarehouses.SelectedIndex == 0)
                throw new ApplicationException("To warehouse field is required");
        }

        protected void GridTransferHistory_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void GridTransferHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void GridTransferHistory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                SerialNumber = DataBinder.Eval(e.Row.DataItem, "SerialNo").ToString();
            }
        }

        protected void GridTransferHistory_RowCreated(object sender, GridViewRowEventArgs e)
        {
            bool IsHistoryRowNeedToAdd = false;

            if ((SerialNumber != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "SerialNo") != null))
                if (SerialNumber != DataBinder.Eval(e.Row.DataItem, "SerialNo").ToString())
                    IsHistoryRowNeedToAdd = true;

            if ((SerialNumber == string.Empty) && (DataBinder.Eval(e.Row.DataItem, "SerialNo") != null))
                AddGrouping(e);

            if (IsHistoryRowNeedToAdd)
                if (DataBinder.Eval(e.Row.DataItem, "SerialNo") != null)
                    AddGrouping(e);
        }

        private void AddGrouping(GridViewRowEventArgs e)
        {
            GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
            TableCell cell = new TableCell();
            cell.Text = $"<strong>Serial No : { DataBinder.Eval(e.Row.DataItem, "SerialNo").ToString()}</strong>";
            cell.ColumnSpan = 9;
            cell.CssClass = "bg-primary text-center";
            row.Cells.Add(cell);
            GridTransferHistory.Controls[0].Controls.AddAt(e.Row.RowIndex + RowIndex, row);
            RowIndex++;
        }
    }
}
